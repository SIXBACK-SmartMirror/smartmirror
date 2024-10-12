package com.sixback.backend.domain.service;

import java.util.List;
import java.util.stream.Collectors;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;

import com.sixback.backend.common.exception.GoodsNotFoundException;
import com.sixback.backend.common.exception.NullNLPException;
import com.sixback.backend.common.exception.OptionNotFoundException;
import com.sixback.backend.common.service.FileService;
import com.sixback.backend.common.service.NLPClientService;
import com.sixback.backend.common.service.STTClientService;
import com.sixback.backend.domain.dto.GoodsDetailDto;
import com.sixback.backend.domain.dto.GoodsDto;
import com.sixback.backend.domain.dto.OptionDto;
import com.sixback.backend.domain.dto.OptionInfoDto;
import com.sixback.backend.domain.dto.SearchReqDto;
import com.sixback.backend.domain.dto.SearchResultDto;
import com.sixback.backend.domain.repository.GoodsOptionRepository;

import lombok.RequiredArgsConstructor;
import reactor.core.publisher.Mono;

/**
 * 상품 검색 관련 서비스.
 */
@Service
@RequiredArgsConstructor
public class GoodsService {
	// 매장 관련 서비스
	private final MarketService marketService;
	// 파일 관련 서비스
	private final FileService fileService;
	// STT 서버 요청 & 처리 관련 서비스
	private final STTClientService sttClientService;
	// NLP 서버 요청 & 처리 관련 서비스
	private final NLPClientService nlpClientService;
	// 상품 조회 리포지토리
	private final GoodsOptionRepository goodsOptionRepository;

	/**
	 * 주어진 매장 ID와 검색 요청 DTO를 사용하여 상품 검색 메서드.
	 *
	 * @param marketId 매장 ID.
	 * @param searchReqDto 검색 요청 DTO(page, size, 사용자 입력 검색어 or 사용자 질문 음성 녹음 파일).
	 * @return SearchResultDto 검색 결과 DTO(검색 키워드, 결과 상품 목록).
	 */
	public SearchResultDto findAllGoods(Long marketId, SearchReqDto searchReqDto) {
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// 오디오/사용자 입력에서 키워드 추출
		String keyword = removeSpecialCharacters(findKeyword(searchReqDto).block());
		// 해당 keyword로 DB에서 조회
		return findAllGoodsByKeyword(keyword, searchReqDto.getPage(), searchReqDto.getSize());
	}

	public SearchResultDto testFindAllGoods(Long marketId, SearchReqDto searchReqDto) {
		// 오디오에서 키워드 추출
		// 해당 keyword로 DB에서 (like "%keyword%") 조회
		return findAllGoodsByKeyword("블러셔", searchReqDto.getPage(), searchReqDto.getSize());
	}

	public Mono<String> findKeyword(SearchReqDto searchReqDto) {
		// 이미 사용자가 키워드를 입력했을 경우 해당 키워드로 검색
		if (searchReqDto.getKeyword() != null && !searchReqDto.getKeyword().isBlank()) {
			return Mono.just(searchReqDto.getKeyword());
		}
		fileService.checkValidAudioFile(searchReqDto.getAudioFile());
		// STT 통신 요청 - 이때 webclient 비동기 openAI한테 STT 요청 보냄
		return sttClientService.sendRequest(searchReqDto.getAudioFile())
			// STT 결과에서 openAI chat을 통해 상품명만 추출 후 요청 결과를 반환
			.flatMap(nlpClientService::sendRequest);
	}

	/**
	 * 주어진 키워드로 상품 조회 메서드.
	 *
	 * @param keyword 검색 키워드.
	 * @param page 페이지 번호.
	 * @param size 페이지 크기.
	 * @return SearchResultDto 검색 결과 DTO(검색 키워드, 결과 상품 목록).
	 */
	public SearchResultDto findAllGoodsByKeyword(String keyword, int page, int size) {
		// 출시 날짜 기준으로 정렬
		Pageable pageable = PageRequest.of(page, size, Sort.by("latest_release_at").descending());
		Page<GoodsDto> goodsDtoPage = goodsOptionRepository.findAllGoodsByKeyword(keyword, pageable);
		return new SearchResultDto(keyword, goodsDtoPage);
	}

	/**
	 * 주어진 매장 ID와 상품 ID로 상품의 상세 정보(옵션) 조회 메서드.
	 *
	 * @param marketId 매장 ID.
	 * @param goodsId 상품 ID.
	 * @return 상품 상세 DTO(상세 옵션 정보 목록, 해당 옵션들 위치 목록).
	 */
	public GoodsDetailDto findByGoodsId(Long marketId, Long goodsId) {
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// goods_id 검사
		goodsOptionRepository.findByValidGoodsId(goodsId).orElseThrow(GoodsNotFoundException::new);
		// 해당 매장의 특정 상품 옵션 정보 조회
		List<OptionInfoDto> locationList = goodsOptionRepository.findAllOptionByGoodsId(marketId, goodsId);
		if (locationList.isEmpty()) {
			throw new OptionNotFoundException();
		}
		// 모든 정보에서 특정 정보만 추출
		List<OptionDto> optionDtoList = locationList.stream()
			.map(OptionInfoDto -> OptionDto.builder()
				.optionId(OptionInfoDto.getOptionId())
				.optionName(OptionInfoDto.getOptionName())
				.optionImage(OptionInfoDto.getOptionImage())
				.optionPrice(OptionInfoDto.getOptionPrice())
				.optionDiscountPrice(OptionInfoDto.getOptionDiscountPrice())
				.stock(OptionInfoDto.getStock())
				.isInMarket(OptionInfoDto.getIsInMarket())
				.build())
			.collect(Collectors.toList());
		return new GoodsDetailDto(optionDtoList, locationList);
	}

	/**
	 * 정규 표현식을 사용하여 입력 문자열에서 특수 문자를 제거 메서드.
	 *
	 * @param input 입력 문자열.
	 * @return 특수 문자가 제거된 문자열.
	 * @throws NullNLPException 특수 문자가 제거된 결과가 비어 있는 경우.
	 */
	private String removeSpecialCharacters(String input) {
		String result = input.replaceAll("[^a-zA-Z0-9가-힣\\s]", "").trim();
		if (result.isBlank()) {
			throw new NullNLPException();
		}
		return result;
	}
}

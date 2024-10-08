package com.sixback.backend.domain.service;

import java.util.List;
import java.util.stream.Collectors;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import com.sixback.backend.common.exception.EmptyFileException;
import com.sixback.backend.common.exception.GoodsNotFoundException;
import com.sixback.backend.common.exception.NoSearchKeywordException;
import com.sixback.backend.common.exception.NullNLPException;
import com.sixback.backend.common.exception.OptionNotFoundException;
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

@Service
@RequiredArgsConstructor
public class GoodsService {

	private final MarketService marketService;
	private final STTClientService sttClientService;
	private final NLPClientService nlpClientService;
	private final GoodsOptionRepository goodsOptionRepository;

	public SearchResultDto findAllGoods(Long marketId, SearchReqDto searchReqDto) {
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// 오디오에서 키워드 추출
		String keyword = removeSpecialCharacters(findKeyword(searchReqDto).block());
		// 해당 keyword로 DB에서 (like "%keyword%") 조회
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
		checkVailAudioFile(searchReqDto.getAudioFile());
		// STT 통신 요청 - 이때 webclient 비동기 openAI한테 STT 요청 보냄
		return sttClientService.sendRequest(searchReqDto.getAudioFile())
			// STT 결과에서 openAI chat을 통해 상품명만 추출 후 요청 결과를 반환
			.flatMap(nlpClientService::sendRequest);
	}

	public void checkVailAudioFile(MultipartFile audioFile) {
		if (audioFile == null) {
			throw new NoSearchKeywordException();
			// 검색 입력 없음
		} else if (audioFile.isEmpty()) {
			// 빈파일 보냄
			throw new EmptyFileException();
		}
	}

	public SearchResultDto findAllGoodsByKeyword(String keyword, int page, int size) {
		// 화장 스타일 식별번호 순으로 정렬
		Pageable pageable = PageRequest.of(page, size, Sort.by("latest_release_at").descending());
		Page<GoodsDto> goodsDtoPage = goodsOptionRepository.findAllGoodsByKeyword(keyword, pageable);
		return new SearchResultDto(keyword, goodsDtoPage);
	}

	public GoodsDetailDto findByGoodsId(Long marketId, Long goodsId) {
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// goods_id 검사
		goodsOptionRepository.findByValidGoodsId(goodsId).orElseThrow(GoodsNotFoundException::new);
		List<OptionInfoDto> locationList = goodsOptionRepository.findAllOptionByGoodsId(marketId, goodsId);
		if (locationList.isEmpty()) {
			throw new OptionNotFoundException();
		}
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

	// 정규 표현식을 사용하여 특수문자 제거
	private String removeSpecialCharacters(String input) {
		String result = input.replaceAll("[^a-zA-Z0-9가-힣\\s]", "").trim();
		if (result.isBlank()) {
			throw new NullNLPException();
		}
		return result;
	}
}

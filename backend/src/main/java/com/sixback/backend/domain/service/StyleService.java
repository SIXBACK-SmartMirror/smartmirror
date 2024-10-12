package com.sixback.backend.domain.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;

import com.sixback.backend.common.dto.gan.GanRequestDto;
import com.sixback.backend.common.exception.OptionNotFoundException;
import com.sixback.backend.common.exception.StyleNotFoundException;
import com.sixback.backend.common.exception.StyleUseOptionNotFoundException;
import com.sixback.backend.common.service.FileService;
import com.sixback.backend.common.service.GanClientService;
import com.sixback.backend.common.service.RedisService;
import com.sixback.backend.domain.dto.OptionInfoDto;
import com.sixback.backend.domain.dto.StyleInfoDto;
import com.sixback.backend.domain.dto.StyleInfoListDto;
import com.sixback.backend.domain.dto.StyleResultDto;
import com.sixback.backend.domain.dto.UseOptionDetailDto;
import com.sixback.backend.domain.dto.UseOptionLocationListDto;
import com.sixback.backend.domain.dto.StyleMakeupReqDto;
import com.sixback.backend.domain.entity.Style;
import com.sixback.backend.domain.repository.GoodsOptionRepository;
import com.sixback.backend.domain.repository.StyleRepository;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;
import reactor.core.scheduler.Schedulers;

/**
 * 스타일 화장과 관련된 서비스.
 */
@Service
@RequiredArgsConstructor
@Slf4j
public class StyleService {
	// 매장 관련 서비스
	private final MarketService marketService;
	// 파일 관련 서비스
	private final FileService fileService;
	// 로그 관련 서비스
	private final LogService logService;
	// Gan 서버 요청 & 처리 관련 서비스
	private final GanClientService ganClientService;
	// Redis 관련 서비스
	private final RedisService redisService;
	// 스타일 조회 레포지토리
	private final StyleRepository styleRepository;
	// 상품 조회 레포지토리
	private final GoodsOptionRepository goodsOptionRepository;
	// Redis 스타일 캐싱 TTL
	@Value("${spring.data.style.redis.ttl.seconds}")
	private long redisStyleCacheSeconds;
	// 스타일 캐싱 키 접두사
	private final String STYLE_PREFIX = "STYLE";

	/**
	 * 모든 스타일 정보를 조회하는 메서드.
	 *
	 * @param marketId 매장 ID
	 * @param page 페이지 번호
	 * @param size 페이지 크기
	 * @return StyleInfoListDto 스타일 정보 리스트 DTO
	 */
	public StyleInfoListDto findAllStyle(Long marketId, int page, int size) {
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// 화장 스타일 식별번호 순으로 정렬
		Pageable pageable = PageRequest.of(page, size, Sort.by("styleId").ascending());
		Page<StyleInfoDto> styleInfoDtoPage = styleRepository.findAllDto(pageable);
		return new StyleInfoListDto(styleInfoDtoPage);
	}

	/**
	 * 스타일 화장 합성을 생성하는 메서드.
	 *
	 * @param marketId 매장 ID.
	 * @param styleMakeupReqDto 화장 스타일 생성 요청 DTO(적용 스타일 ID, 사용자 이미지).
	 * @return Mono(StyleResultDto) 화장 스타일 결과 DTO.
	 */
	public Mono<StyleResultDto> createStyleMakeup(Long marketId, StyleMakeupReqDto styleMakeupReqDto) {
		// 파일 크기 검증
		fileService.validateFileSize(styleMakeupReqDto.getInputImage());
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// 스타일 식별 번호 검증
		Style style = styleRepository.findById(styleMakeupReqDto.getStyleId())
			.orElseThrow(StyleNotFoundException::new);
		// 화장 스타일 로그 저장
		logService.saveMakeupStyleLog("style_makeup", styleMakeupReqDto.getStyleId(), marketId);

		// 사용된 상품 정보 조회 (비동기 처리)
		Mono<List<OptionInfoDto>> useOptionInfoListMono = Mono.fromCallable(() ->
			styleRepository.findAllUseOptionInfoList(marketId, style.getStyleId())
		).subscribeOn(Schedulers.boundedElastic());  // 블로킹 작업을 별도 스레드에서 실행;

		String cacheKey = generateCacheKey(marketId, style.getStyleId(), styleMakeupReqDto);
		// RedisService를 사용하여 캐시된 이미지 존재 확인 후, 없으면 GAN 서비스 호출
		Mono<String> makeupImageMono = Mono.fromCallable(() ->
			redisService.getData(cacheKey, String.class)
		).switchIfEmpty(
			ganClientService.sendRequest(
				GanRequestDto.builder()
					.inputImage(styleMakeupReqDto.getInputImage())
					.styleImage(style.getStyleImage())
					.build()
			).doOnNext(result ->
				// 결과를 캐시
				redisService.setDataExpire(cacheKey, result, redisStyleCacheSeconds)
			)
		);

		// 두 작업 병렬 처리 후 결과를 조합
		return Mono.zip(useOptionInfoListMono, makeupImageMono)
			.flatMap(tuple -> {
				List<OptionInfoDto> useOptionInfoList = tuple.getT1(); // DB 조회 결과
				String makeupImage = tuple.getT2(); // 캐시 또는 GAN AI 서버 응답
				return Mono.just(StyleResultDto.builder()
					.styleId(style.getStyleId())
					.goodsOptionList(useOptionInfoList)
					.makeupImage(makeupImage)
					.build());
			});
	}

	/**
	 *  특정 스타일에 사용된 상품 옵션 정보를 조회하는 메서드.
	 *
	 * @param marketId 매장 ID.
	 * @param styleId 스타일 ID.
	 * @param optionId 옵션 ID.
	 * @return 사용된 상품 옵션 정보 또는 위치 리스트.
	 */
	public Object findUseGoodsOptionInfo(Long marketId, Long styleId, Long optionId) {
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		if (optionId == null) {
			// 모든 옵션 위치 리스트 반환
			return findAllUseOptonLocationList(marketId, styleId);
		} else {
			// 특정 옵션 ID에 대한 정보 반환
			return findByOptionId(marketId, styleId, optionId);
		}
	}

	/**
	 * 특정 스타일 ID에 대한 사용된 모든 상품 위치 정보를 조회하는 메서드.
	 *
	 * @param marketId 매장 ID.
	 * @param styleId 스타일 ID.
	 * @return UseOptionLocationListDto 사용된 상품 위치 리스트 DTO.
	 */
	public UseOptionLocationListDto findAllUseOptonLocationList(Long marketId, Long styleId) {
		// 스타일 식별 번호 검증 & 사용된 상품 위치 정보 가져오기
		List<OptionInfoDto> results = styleRepository.findAllUseOptionInfoList(marketId, styleId);
		return new UseOptionLocationListDto(results);
	}

	/**
	 * 특정 옵션 ID에 대한 정보를 조회하는 메서드.
	 *
	 * @param marketId 매장 ID.
	 * @param styleId 스타일 ID.
	 * @param optionId 옵션 ID.
	 * @return UseOptionDetailDto 사용된 상품 상세 정보 DTO.
	 */
	public UseOptionDetailDto findByOptionId(Long marketId, Long styleId, Long optionId) {
		// 스타일 식별 번호 & 옵션 ID 검증
		styleRepository.findByStyleIdAndOptionId(styleId, optionId).orElseThrow(StyleUseOptionNotFoundException::new);
		// 사용된 상품 정보 가져오기
		return goodsOptionRepository.findTopByMarketIdAndOptionId(marketId, optionId)
			.orElseThrow(OptionNotFoundException::new);
	}

	/**
	 * 다른 스타일에 대한 미리 요청하는 메서드.
	 *
	 * @param marketId 매장 ID.
	 * @param styleMakeupReqDto 화장 스타일 요청 DTO.
	 */
	public void prefetchOtherStyles(Long marketId, StyleMakeupReqDto styleMakeupReqDto) {
		// 현재 스타일을 제외한 다른 스타일 조회
		List<Style> otherStyles = styleRepository.findNotStyleId(styleMakeupReqDto.getStyleId());
		for (Style otherStyle : otherStyles) {
			// 비동기로 각 스타일에 대한 합성 요청 처리
			ganClientService.sendRequest(GanRequestDto.builder()
					.inputImage(styleMakeupReqDto.getInputImage())
					.styleImage(otherStyle.getStyleImage())
					.build())
				.flatMap(makeupImage -> {
					// 결과를 캐시
					String cacheKey = generateCacheKey(marketId, otherStyle.getStyleId(), styleMakeupReqDto);
					redisService.setDataExpire(cacheKey, makeupImage, redisStyleCacheSeconds);
					return Mono.empty();
				}).subscribe(); // 비동기적으로 실행
		}
	}

	/**
	 * 캐시 키를 생성하는 메서드.
	 *
	 * @param marketId 매장 ID.
	 * @param styleId 스타일 ID.
	 * @param styleMakeupReqDto 화장 스타일 요청 DTO(적용 스타일 ID, 사용자 이미지).
	 * @return 생성된 캐시 키.
	 */
	private String generateCacheKey(Long marketId, Long styleId, StyleMakeupReqDto styleMakeupReqDto) {
		// 기존 포맷에 맞춰 baseString 생성
		if (styleMakeupReqDto.getInputImageBase64() == null) {
			// Base64 변환
			String base64 = fileService.convertFileToBase64(styleMakeupReqDto.getInputImage());
			styleMakeupReqDto.setInputImageBase64(base64);
		}
		String baseString = "%d%d%s".formatted(marketId, styleId, styleMakeupReqDto.getInputImageBase64());
		// generateKey 메서드 활용
		return redisService.generateKey(STYLE_PREFIX, baseString);
	}
}

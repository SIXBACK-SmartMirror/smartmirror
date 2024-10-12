package com.sixback.backend.domain.service;

import java.io.IOException;
import java.util.Base64;
import java.util.List;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import com.sixback.backend.common.dto.gan.GanRequestDto;
import com.sixback.backend.common.exception.EmptyFileException;
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
import com.sixback.backend.domain.dto.VirtualMakeupReqDto;
import com.sixback.backend.domain.entity.Style;
import com.sixback.backend.domain.repository.GoodsOptionRepository;
import com.sixback.backend.domain.repository.StyleRepository;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;
import reactor.core.scheduler.Schedulers;

@Service
@RequiredArgsConstructor
@Slf4j
public class StyleService {

	private final MarketService marketService;
	// 파일 관련 서비스
	private final FileService fileService;
	private final GanClientService ganClientService;
	private final RedisService redisService;
	private final LogService logService;
	private final StyleRepository styleRepository;
	private final GoodsOptionRepository goodsOptionRepository;

	@Value("${spring.data.style.redis.ttl.seconds}")
	private long redisStyleCacheSeconds;

	private final String STYLE_PREFIX = "STYLE";

	public StyleInfoListDto findAllStyle(Long marketId, int page, int size) {
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// 화장 스타일 식별번호 순으로 정렬
		Pageable pageable = PageRequest.of(page, size, Sort.by("styleId").ascending());
		Page<StyleInfoDto> styleInfoDtoPage = styleRepository.findAllDto(pageable);
		return new StyleInfoListDto(styleInfoDtoPage);
	}

	public Mono<StyleResultDto> createVirtualMakeup(Long marketId, VirtualMakeupReqDto virtualMakeupReqDto) {
		validateFileSize(virtualMakeupReqDto.getInputImage());
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// 스타일 식별 번호 검증
		Style style = styleRepository.findById(virtualMakeupReqDto.getStyleId())
			.orElseThrow(StyleNotFoundException::new);
		// 화장 스타일 로그 저장
		logService.saveMakeupStyleLog("style_makeup", virtualMakeupReqDto.getStyleId(), marketId);

		// 사용된 상품 정보 조회 (비동기 처리)
		Mono<List<OptionInfoDto>> useOptionInfoListMono = Mono.fromCallable(() ->
			styleRepository.findAllUseOptionInfoList(marketId, style.getStyleId())
		).subscribeOn(Schedulers.boundedElastic());  // 블로킹 작업을 별도 스레드에서 실행;

		String cacheKey = generateCacheKey(marketId, style.getStyleId(), virtualMakeupReqDto);
		// RedisService를 사용하여 캐시된 이미지 확인 후, 없으면 GAN 서비스 호출
		Mono<String> makeupImageMono = Mono.fromCallable(() ->
			redisService.getData(cacheKey, String.class)
		).switchIfEmpty(
			ganClientService.sendRequest(
				GanRequestDto.builder()
					.inputImage(virtualMakeupReqDto.getInputImage())
					.styleImage(style.getStyleImage())
					.build()
			).doOnNext(result ->
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

	public void validateFileSize(MultipartFile file) {
		if (file.getSize() < 0) {
			throw new EmptyFileException();
		}
	}

	public Object findUseGoodsOptionInfo(Long marketId, Long styleId, Long optionId) {
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		if (optionId == null) {
			return findAllUseOptonLocationList(marketId, styleId);
		} else {
			return findByOptionId(marketId, styleId, optionId);
		}
	}

	public UseOptionLocationListDto findAllUseOptonLocationList(Long marketId, Long styleId) {
		// 스타일 식별 번호 검증 & 사용된 상품 위치 정보 가져오기
		List<OptionInfoDto> results = styleRepository.findAllUseOptionInfoList(marketId, styleId);
		return new UseOptionLocationListDto(results);
	}

	public UseOptionDetailDto findByOptionId(Long marketId, Long styleId, Long optionId) {
		// 스타일 식별 번호 & 옵션 ID 검증
		styleRepository.findByStyleIdAndOptionId(styleId, optionId).orElseThrow(StyleUseOptionNotFoundException::new);
		// 사용된 상품 정보 가져 오기
		return goodsOptionRepository.findTopByMarketIdAndOptionId(marketId, optionId)
			.orElseThrow(OptionNotFoundException::new);
	}

	public void prefetchOtherStyles(Long marketId, VirtualMakeupReqDto virtualMakeupReqDto) {
		List<Style> otherStyles = styleRepository.findNotStyleId(virtualMakeupReqDto.getStyleId());

		for (Style otherStyle : otherStyles) {
			// 비동기로 각 스타일에 대한 합성 요청 처리
			ganClientService.sendRequest(GanRequestDto.builder()
					.inputImage(virtualMakeupReqDto.getInputImage())
					.styleImage(otherStyle.getStyleImage())
					.build())
				.flatMap(makeupImage -> {
					// 결과를 캐시
					String cacheKey = generateCacheKey(marketId, otherStyle.getStyleId(), virtualMakeupReqDto);
					redisService.setDataExpire(cacheKey, makeupImage, redisStyleCacheSeconds);
					return Mono.empty();
				}).subscribe(); // 비동기적으로 실행
		}
	}

	private String generateCacheKey(Long marketId, Long styleId, VirtualMakeupReqDto virtualMakeupReqDto) {
		try {
			// 기존 포맷에 맞춰 baseString 생성
			if(virtualMakeupReqDto.getInputImageBase64() == null) {
				byte[] bytes = virtualMakeupReqDto.getInputImage().getBytes();
				String base64 = Base64.getEncoder().encodeToString(bytes);
				virtualMakeupReqDto.setInputImageBase64(base64);
			}
			String baseString = "%d%d%s".formatted(marketId, styleId, virtualMakeupReqDto.getInputImageBase64());
			// 기존 generateKey 메서드 활용
			return redisService.generateKey(STYLE_PREFIX, baseString);
		} catch (IOException e) {
			log.error("Failed to read input image file {}", e.getMessage());
			throw new RuntimeException(e);
		}
			// Base64 변환
			String base64 = fileService.convertFileToBase64(styleMakeupReqDto.getInputImage());
	}
}

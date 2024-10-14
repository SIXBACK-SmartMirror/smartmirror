package com.sixback.backend.domain.service;

import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

import org.springframework.stereotype.Service;

import com.sixback.backend.common.service.FacerClientService;
import com.sixback.backend.common.service.FileService;
import com.sixback.backend.domain.dto.CustomMakeupReqDto;
import com.sixback.backend.domain.dto.CustomResultDto;
import com.sixback.backend.domain.dto.OptionInfoDto;
import com.sixback.backend.domain.repository.GoodsOptionRepository;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

/**
 * 커스텀 화장 관련 서비스.
 */
@Service
@RequiredArgsConstructor
@Slf4j
public class CustomService {
	// 매장 관련 서비스
	private final MarketService marketService;
	// 파일 관련 서비스
	private final FileService fileService;
	// 로그 관련 서비스
	private final LogService logService;
	// Facer 서버 요청 & 처리 관련 서비스
	private final FacerClientService facerClientService;
	// 상품 조회 레포지토리
	private final GoodsOptionRepository goodsOptionRepository;

	/**
	 * 가능한 커스텀 옵션을 조회하고, 페이지네이션을 적용 메서드.
	 *
	 * @param marketId 매장 ID.
	 * @param page 페이지 번호.
	 * @param size 페이지 크기.
	 * @return 커스텀 옵션의 유형에 따라 그룹화된 옵션 정보 Map.
	 */
	public Map<String, List<OptionInfoDto>> findAllCustomOption(Long marketId, int page, int size) {
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		int offset = page * size; // 시작 위치 계산
		List<OptionInfoDto> customOptionPage = goodsOptionRepository.findAllCustomOption(size, offset);
		// 타입별로 그룹화 후 커스텀 옵션 반환
		return customOptionPage.stream()
			.collect(Collectors.groupingBy(OptionInfoDto::getCustomOptionTypeName));
	}

	/**
	 * 커스텀 화장을 생성하고 결과 이미지를 반환 메서드.
	 *
	 * @param marketId 매장 ID.
	 * @param customMakeupReqDto 커스텀 화장 요청 DTO(사용자 사진 및 선택 정보).
	 * @return Mono(CustomResultDto) 커스텀 화장 결과 DTO(결과 이미지).
	 */
	public Mono<CustomResultDto> creatCustomMakeup(Long marketId, CustomMakeupReqDto customMakeupReqDto) {
		// 파일 유효성(크기) 검사
		fileService.validateFileSize(customMakeupReqDto.getInputImage());
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// 커스텀 합성 로그 저장
		logService.saveMakeupCustomLog("custom_makeup", marketId,
			customMakeupReqDto.getEyebrowColor(),
			customMakeupReqDto.getSkinColor(),
			customMakeupReqDto.getLipColor(),
			customMakeupReqDto.getLipMode());
		// Facer 서버로 커스텀 화장 요청
		return facerClientService.sendRequest(customMakeupReqDto)
			.map(result -> CustomResultDto.builder()
				.makeupImage(result)  // sendRequest에서 반환된 String 값을 result에 넣음
				.build());
	}
}

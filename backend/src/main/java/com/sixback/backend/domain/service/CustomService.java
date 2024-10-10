package com.sixback.backend.domain.service;

import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import com.sixback.backend.common.exception.EmptyFileException;
import com.sixback.backend.common.service.FacerClientService;
import com.sixback.backend.domain.dto.CustomMakeupReqDto;
import com.sixback.backend.domain.dto.CustomResultDto;
import com.sixback.backend.domain.dto.OptionInfoDto;
import com.sixback.backend.domain.repository.GoodsOptionRepository;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

@Service
@RequiredArgsConstructor
@Slf4j
public class CustomService {
	private final MarketService marketService;
	private final GoodsOptionRepository goodsOptionRepository;
	private final FacerClientService facerClientService;
	private final LogService logService;

	public Map<String, List<OptionInfoDto>> findAllCustomOption(Long marketId, int page, int size) {
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// 화장 스타일 식별번호 순으로 정렬
		int offset = page * size; // 시작 위치 계산
		List<OptionInfoDto> customOptionPage = goodsOptionRepository.findAllCustomOption(size, offset);
		return customOptionPage.stream()
			.collect(Collectors.groupingBy(OptionInfoDto::getCustomOptionTypeName));
	}

	public Mono<CustomResultDto> creatCustomMakeup(Long marketId, CustomMakeupReqDto customMakeupReqDto) {
		validateFileSize(customMakeupReqDto.getInputImage());
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// 커스텀 합성 로그 저장
		logService.saveMakeupCustomLog("custom_makeup", marketId,
			customMakeupReqDto.getEyebrowColor(),
			customMakeupReqDto.getSkinColor(),
			customMakeupReqDto.getLipColor(),
			customMakeupReqDto.getLipMode());
		return facerClientService.sendRequest(customMakeupReqDto)
			.map(result -> CustomResultDto.builder()
				.makeupImage(result)  // sendRequest에서 반환된 String 값을 result에 넣음
				.build());
	}

	public void validateFileSize(MultipartFile file) {
		if (file.getSize() < 0) {
			throw new EmptyFileException();
		}
	}
}

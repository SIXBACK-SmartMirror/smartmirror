package com.sixback.backend.domain.service;

import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import com.sixback.backend.common.exception.EmptyFileException;
import com.sixback.backend.common.service.FacerClientService;
import com.sixback.backend.domain.dto.CustomMakeupReqDto;
import com.sixback.backend.domain.dto.CustomResultDto;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

@Service
@RequiredArgsConstructor
@Slf4j
public class CustomService {
	private final MarketService marketService;
	private final FacerClientService facerClientService;

	public Mono<CustomResultDto> creatCustomMakeup(Long marketId, CustomMakeupReqDto customMakeupReqDto) {
		validateFileSize(customMakeupReqDto.getInputImage());
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
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

package com.sixback.backend.domain.controller;

import java.util.List;
import java.util.Map;

import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.sixback.backend.common.dto.ResponseDto;
import com.sixback.backend.domain.dto.CustomMakeupReqDto;
import com.sixback.backend.domain.dto.OptionInfoDto;
import com.sixback.backend.domain.service.CustomService;

import jakarta.validation.Valid;
import jakarta.validation.constraints.Min;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

/**
 * 커스텀 화장 관련 요청을 처리하는 컨트롤러.
 * 특정 마켓에 대한 커스텀 옵션 조회 및 커스텀 화장 합성.
 */
@Slf4j
@RestController
@RequiredArgsConstructor
@RequestMapping("/market/{marketId}/custom")
public class CustomController {
	private final CustomService customService;

	/**
	 * 특정 마켓의 모든 커스텀 옵션을 조회하는 메서드.
	 *
	 * @param marketId 요청한 마켓의 ID.
	 * @param page 페이지 번호 (기본값: 0).
	 * @param size 페이지 당 항목 수 (기본값: 10).
	 * @return 커스텀 옵션 목록(List(OptionInfoDto))과 상태 코드.
	 */
	@GetMapping
	public ResponseEntity<?> findAllCustomOption(@PathVariable("marketId") Long marketId,
		@Min(0) @RequestParam(defaultValue = "0", value = "page") int page,
		@Min(1) @RequestParam(defaultValue = "10", value = "size") int size) {
		// 커스텀 옵션 목록을 조회
		Map<String, List<OptionInfoDto>> CustomOptionList = customService.findAllCustomOption(marketId, page, size);
		return new ResponseEntity<>(new ResponseDto<>("A00", CustomOptionList), HttpStatus.OK);
	}

	/**
	 * 커스텀 화장 합성 메서드.
	 *
	 * @param marketId 요청한 마켓의 ID.
	 * @param customMakeupReqDto 사용자 이미지와 커스텀 화장 정보 DTO.
	 * @return 생성된 화장 이미지 base64(CustomResultDto)와 상태 코드.
	 */
	@PostMapping(consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public Mono<ResponseEntity<?>> creatCustomMakeup(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute CustomMakeupReqDto customMakeupReqDto) {
		log.debug("creatCustomMakeup body = {}", customMakeupReqDto);
		// 커스텀 화장 요청 처리
		return customService.creatCustomMakeup(marketId, customMakeupReqDto)
			.map(makeupImage -> new ResponseEntity<>(new ResponseDto<>("A00", makeupImage), HttpStatus.OK));
	}
}

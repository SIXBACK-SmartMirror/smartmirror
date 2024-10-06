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
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

@RestController
@RequiredArgsConstructor
@RequestMapping("/market/{marketId}/custom")
@Slf4j
public class CustomController {
	private final CustomService customService;

	@GetMapping
	public ResponseEntity<?> findAllCustomOption(@PathVariable("marketId") Long marketId,
		@RequestParam(defaultValue = "0", value = "page") int page,
		@RequestParam(defaultValue = "10", value = "size") int size) {
		Map<String, List<OptionInfoDto>> CustomOptionList = customService.findAllCustomOption(marketId, page, size);
		return new ResponseEntity<>(new ResponseDto<>("A00", CustomOptionList), HttpStatus.OK);
	}

	// 커스텀 화장 하기
	@PostMapping(consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public Mono<ResponseEntity<?>> creatCustomMakeup(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute CustomMakeupReqDto customMakeupReqDto) {
		log.debug("creatCustomMakeup body = {}", customMakeupReqDto);
		return customService.creatCustomMakeup(marketId, customMakeupReqDto)
			.map(makeupImage -> new ResponseEntity<>(new ResponseDto<>("A00", makeupImage), HttpStatus.OK));
	}

}
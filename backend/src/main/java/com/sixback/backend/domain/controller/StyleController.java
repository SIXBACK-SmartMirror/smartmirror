package com.sixback.backend.domain.controller;

import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.sixback.backend.common.dto.ResponseDto;
import com.sixback.backend.domain.dto.StyleResultDto;
import com.sixback.backend.domain.dto.VirtualMakeupReqDto;
import com.sixback.backend.domain.service.StyleService;

import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

@RestController
@RequiredArgsConstructor
@RequestMapping("/market/{marketId}/styles")
@Slf4j
public class StyleController {

	private final StyleService styleService;

	// 가상 화장 하기
	@PostMapping(consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public Mono<ResponseEntity<?>> creatVirtualMakeup(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute VirtualMakeupReqDto virtualMakeupReqDto) {
		return styleService.createVirtualMakeup(marketId, virtualMakeupReqDto)
			.map(styleResultDto -> new ResponseEntity<>(new ResponseDto<>("A00", styleResultDto), HttpStatus.OK));
	}

	@PostMapping(value = "/test", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public ResponseEntity<?> testCreatVirtualMakeup(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute VirtualMakeupReqDto virtualMakeupReqDto) {
		log.debug(String.format("request : marketId = %d\nbody = %s", marketId, virtualMakeupReqDto));
		StyleResultDto styleResultDto = styleService.testCreatVirtualMakeup(marketId, virtualMakeupReqDto);
		return new ResponseEntity<>(new ResponseDto<>("A00", styleResultDto), HttpStatus.OK);
	}

	@PostMapping(value = "/testAi", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public Mono<ResponseEntity<?>> testAICreatVirtualMakeup(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute VirtualMakeupReqDto virtualMakeupReqDto) {
		return styleService.testAIcreateVirtualMakeup(marketId, virtualMakeupReqDto)
			.map(styleResultDto -> new ResponseEntity<>(new ResponseDto<>("A00", styleResultDto), HttpStatus.OK));
	}

}

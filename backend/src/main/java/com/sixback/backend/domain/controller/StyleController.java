package com.sixback.backend.domain.controller;

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
import com.sixback.backend.domain.dto.StyleInfoListDto;
import com.sixback.backend.domain.dto.StyleResultDto;
import com.sixback.backend.domain.dto.VirtualMakeupReqDto;
import com.sixback.backend.domain.service.StyleService;

import jakarta.validation.Valid;
import jakarta.validation.constraints.Min;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

@RestController
@RequiredArgsConstructor
@RequestMapping("/market/{marketId}/styles")
@Slf4j
public class StyleController {

	private final StyleService styleService;

	// 화장 스타일 목록 조회
	@GetMapping
	public ResponseEntity<?> findAllStyle(@PathVariable("marketId") Long marketId,
		@Min(0) @RequestParam("page") int page, @Min(1) @RequestParam("size") int size) {
		StyleInfoListDto styleInfoListDto = styleService.findAllStyle(marketId, page, size);
		return new ResponseEntity<>(new ResponseDto<>("A00", styleInfoListDto), HttpStatus.OK);
	}

	// 가상 화장 하기
	@PostMapping(consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public Mono<ResponseEntity<ResponseDto<StyleResultDto>>> createVirtualMakeup(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute VirtualMakeupReqDto virtualMakeupReqDto) {
		return styleService.createVirtualMakeup(marketId, virtualMakeupReqDto)
			.map(styleResultDto -> new ResponseEntity<>(new ResponseDto<>("A00", styleResultDto), HttpStatus.OK))
			.doOnSuccess(response -> {
				styleService.prefetchOtherStyles(marketId, virtualMakeupReqDto);
			});
	}

	// @PostMapping(value = "/test", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	// public ResponseEntity<?> testCreatVirtualMakeup(@PathVariable("marketId") Long marketId,
	// 	@Valid @ModelAttribute VirtualMakeupReqDto virtualMakeupReqDto) {
	// 	log.debug(String.format("request : marketId = %d\nbody = %s", marketId, virtualMakeupReqDto));
	// 	StyleResultDto styleResultDto = styleService.testCreatVirtualMakeup(marketId, virtualMakeupReqDto);
	// 	return new ResponseEntity<>(new ResponseDto<>("A00", styleResultDto), HttpStatus.OK);
	// }
	//
	// @PostMapping(value = "/testAi", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	// public Mono<ResponseEntity<?>> testAICreatVirtualMakeup(@PathVariable("marketId") Long marketId,
	// 	@Valid @ModelAttribute VirtualMakeupReqDto virtualMakeupReqDto) {
	// 	return styleService.testAIcreateVirtualMakeup(marketId, virtualMakeupReqDto)
	// 		.map(styleResultDto -> new ResponseEntity<>(new ResponseDto<>("A00", styleResultDto), HttpStatus.OK));
	// }

	// 현재 스타일에 사용된 모든 상품 위치 또는 특정 상품 상세 정보 확인
	@GetMapping("/{styleId}")
	public ResponseEntity<?> findUseOptionInfo(@PathVariable("marketId") Long marketId,
		@PathVariable("styleId") Long styleId, @RequestParam(value = "optionId", required = false) Long optionId) {
		Object useOptionInfo = styleService.findUseGoodsOptionInfo(marketId, styleId, optionId);
		return new ResponseEntity<>(new ResponseDto<>("A00", useOptionInfo), HttpStatus.OK);
	}
}

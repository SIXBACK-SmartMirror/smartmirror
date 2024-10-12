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
import com.sixback.backend.domain.dto.StyleMakeupReqDto;
import com.sixback.backend.domain.dto.StyleResultDto;
import com.sixback.backend.domain.service.StyleService;

import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

/**
 * 스타일 화장 관련 요청을 처리하는 컨트롤러.
 */
@Slf4j
@RestController
@RequiredArgsConstructor
@RequestMapping("/market/{marketId}/styles")
public class StyleController {
	private final StyleService styleService;

	/**
	 * 화장 스타일 목록을 조회하는 메서드.
	 *
	 * @param marketId 요청한 마켓의 ID.
	 * @param page 페이지 번호 (기본값: 0).
	 * @param size 페이지 당 항목 수 (기본값: 10).
	 * @return 스타일 목록(StyleInfoListDto)과 상태 코드.
	 */
	@GetMapping
	public ResponseEntity<?> findAllStyle(@PathVariable("marketId") Long marketId,
		@RequestParam(defaultValue = "0", value = "page") int page,
		@RequestParam(defaultValue = "10", value = "size") int size) {
		// 스타일 목록 조회
		StyleInfoListDto styleInfoListDto = styleService.findAllStyle(marketId, page, size);
		return new ResponseEntity<>(new ResponseDto<>("A00", styleInfoListDto), HttpStatus.OK);
	}

	/**
	 * 가상 화장을 생성하는 메서드.
	 *
	 * @param marketId 요청한 마켓의 ID.
	 * @param styleMakeupReqDto 가상 화장(적용 스타일 번호, 사용자 이미지) 요청 DTO.
	 * @return 생성된 가상 화장 결과(StyleResultDto)와 상태 코드.
	 */
	@PostMapping(consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public Mono<ResponseEntity<ResponseDto<StyleResultDto>>> createStyleMakeup(
		@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute StyleMakeupReqDto styleMakeupReqDto) {
		// 스타일 가상 화장 요청 처리
		return styleService.createStyleMakeup(marketId, styleMakeupReqDto)
			.map(styleResultDto -> new ResponseEntity<>(new ResponseDto<>("A00", styleResultDto), HttpStatus.OK))
			// 가상 화장이 생성된 후 다른 스타일을 미리 가져옴 (pre-fetching)
			.doOnSuccess(response -> styleService.prefetchOtherStyles(marketId, styleMakeupReqDto));
	}

	/**
	 * 현재 스타일에 사용된 모든 상품 위치 또는 특정 상품의 상세 정보를 조회하는 메서드.
	 *
	 * @param marketId 요청한 마켓의 ID.
	 * @param styleId 조회할 스타일의 ID.
	 * @param optionId 특정 상품의 ID (선택 사항).
	 * @return 사용된 상품 위치 또는 상세 정보를 담은 ResponseDto와 상태 코드.
	 */
	@GetMapping("/{styleId}")
	public ResponseEntity<?> findUseOptionInfo(@PathVariable("marketId") Long marketId,
		@PathVariable("styleId") Long styleId, @RequestParam(value = "optionId", required = false) Long optionId) {
		// 사용된 상품 위치 또는 상세 정보 조회
		Object useOptionInfo = styleService.findUseGoodsOptionInfo(marketId, styleId, optionId);
		return new ResponseEntity<>(new ResponseDto<>("A00", useOptionInfo), HttpStatus.OK);
	}
}

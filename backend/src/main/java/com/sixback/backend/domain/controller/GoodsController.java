package com.sixback.backend.domain.controller;

import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.sixback.backend.common.dto.ResponseDto;
import com.sixback.backend.domain.dto.GoodsDetailDto;
import com.sixback.backend.domain.dto.SearchReqDto;
import com.sixback.backend.domain.dto.SearchResultDto;
import com.sixback.backend.domain.service.GoodsService;

import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;

/**
 * 상품 관련 요청을 처리하는 컨트롤러.
 * 특정 마켓에 대한 상품 검색 및 상세 정보 조회 기능을 담당.
 */
@RestController
@RequiredArgsConstructor
@RequestMapping("/market/{marketId}/goods")
public class GoodsController {
	private final GoodsService goodsService;

	/**
	 * 음성 검색을 통한 상품 검색 메서드.
	 *
	 * @param marketId 요청한 마켓의 ID.
	 * @param searchReqDto 음성 검색 (타이핑 검색어, 녹음파일, page, size) DTO.
	 * @return 검색 결과(SearchResultDto)와 상태 코드.
	 */
	@PostMapping(consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public ResponseEntity<?> findAllGoodsBySTT(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute SearchReqDto searchReqDto) {
		// 음성 검색 요청 처리
		SearchResultDto searchResultDto = goodsService.findAllGoods(marketId, searchReqDto);
		return new ResponseEntity<>(new ResponseDto<>("A00", searchResultDto), HttpStatus.OK);
	}

	/**
	 * 터치 검색을 통한 상품 검색 메서드.
	 *
	 * @param marketId 요청한 마켓의 ID.
	 * @param searchReqDto 음성 검색 (타이핑 검색어, 녹음파일, page, size) DTO.
	 * @return 검색 결과(SearchResultDto)와 상태 코드.
	 */
	@GetMapping
	public ResponseEntity<?> findAllGoodsByKeyword(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute SearchReqDto searchReqDto) {
		// 터치 검색 요청 처리
		SearchResultDto searchResultDto = goodsService.findAllGoods(marketId, searchReqDto);
		return new ResponseEntity<>(new ResponseDto<>("A00", searchResultDto), HttpStatus.OK);
	}

	/**
	 * 특정 상품의 상세 정보를 조회하는 메서드.
	 *
	 * @param marketId 요청한 마켓의 ID.
	 * @param goodsId 조회할 상품의 ID.
	 * @return 상품 상세 정보(GoodsDetailDto)와 상태 코드.
	 */
	@GetMapping("/{goodsId}")
	public ResponseEntity<?> findByGoodsId(@PathVariable("marketId") Long marketId,
		@PathVariable("goodsId") Long goodsId) {
		// 상품 상세 정보 요청 처리
		GoodsDetailDto goodsDetailDto = goodsService.findByGoodsId(marketId, goodsId);
		return new ResponseEntity<>(new ResponseDto<>("A00", goodsDetailDto), HttpStatus.OK);
	}

}

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

@RestController
@RequiredArgsConstructor
@RequestMapping("/market/{marketId}/goods")
public class GoodsController {

	private final GoodsService goodsService;

	//음성 검색
	@PostMapping(consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public ResponseEntity<?> findAllGoodsBySTT(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute SearchReqDto searchReqDto) {
		SearchResultDto searchResultDto = goodsService.findAllGoods(marketId, searchReqDto);
		return new ResponseEntity<>(new ResponseDto<>("A00", searchResultDto), HttpStatus.OK);
	}

	// 타이핑 검색
	@GetMapping
	public ResponseEntity<?> findAllGoodsByKeyword(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute SearchReqDto searchReqDto) {
		SearchResultDto searchResultDto = goodsService.findAllGoods(marketId, searchReqDto);
		return new ResponseEntity<>(new ResponseDto<>("A00", searchResultDto), HttpStatus.OK);
	}

	// 상세 상품 정보 확인
	@GetMapping("/{goodsId}")
	public ResponseEntity<?> findByGoodsId(@PathVariable("marketId") Long marketId,
		@PathVariable("goodsId") Long goodsId) {
		GoodsDetailDto goodsDetailDto = goodsService.findByGoodsId(marketId, goodsId);
		return new ResponseEntity<>(new ResponseDto<>("A00", goodsDetailDto), HttpStatus.OK);
	}

}

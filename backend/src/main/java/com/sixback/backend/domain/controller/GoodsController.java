package com.sixback.backend.domain.controller;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.sixback.backend.common.dto.ResponseDto;
import com.sixback.backend.domain.dto.GoodsDetailDto;
import com.sixback.backend.domain.dto.SearchResultDto;
import com.sixback.backend.domain.service.GoodsService;

import lombok.RequiredArgsConstructor;

@RestController
@RequiredArgsConstructor
@RequestMapping("/market/{marketId}/goods")
public class GoodsController {

	private final GoodsService goodsService;

	// 타이핑 검색
	@GetMapping
	public ResponseEntity<?> findAllGoodsByKeyword(@PathVariable("marketId") Long marketId,
		@RequestParam("keyword") String keyword,
		@RequestParam("page") int page,
		@RequestParam("size") int size) {
		SearchResultDto searchResultDto = goodsService.findAllGoodsByKeyword(marketId, keyword, page, size);
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

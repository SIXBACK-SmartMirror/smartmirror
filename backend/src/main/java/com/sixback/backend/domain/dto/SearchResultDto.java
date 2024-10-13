package com.sixback.backend.domain.dto;

import org.springframework.data.domain.Page;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 검색 결과를 담는 DTO.
 */
@Getter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class SearchResultDto {
	// 검색에 사용된 키워드
	private String searchKeyword;
	// 검색 결과로 반환된 상품 목록 (페이지 정보 포함)
	private Page<GoodsDto> goodsList;
}

package com.sixback.backend.domain.dto;

import org.springframework.data.domain.Page;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class SearchResultDto {
	private String searchKeyword;
	private Page<GoodsDto> goodsList;
}

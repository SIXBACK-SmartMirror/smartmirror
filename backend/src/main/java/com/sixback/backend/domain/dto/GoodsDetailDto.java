package com.sixback.backend.domain.dto;

import java.util.List;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 상품(Goods)의 자세한 설명(포함 옵션, 옵션별 위치) 정보 DTO
 */
@Getter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class GoodsDetailDto {
	// 상품의 포함된 옵션 목록
	private List<OptionDto> optionDtoList;
	// 각 옵션별 위치 정보
	private List<OptionInfoDto> locationList;
}

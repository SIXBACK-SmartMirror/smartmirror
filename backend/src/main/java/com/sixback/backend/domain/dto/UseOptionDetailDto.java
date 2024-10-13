package com.sixback.backend.domain.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 사용된 옵션의 상세 정보를 담는 DTO.
 */
@Getter
@AllArgsConstructor
@NoArgsConstructor
public class UseOptionDetailDto {
	String brandNameKr;
	String goodsName;
	String optionName;
	String optionImage;
	long optionPrice;
	long optionDiscountPrice;
	boolean isInMarket;
	int stock;
	LocationDto location;
}

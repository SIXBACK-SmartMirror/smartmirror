package com.sixback.backend.domain.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@AllArgsConstructor
@NoArgsConstructor
public class UseOptionDetailDto {
	String brandNameKr;
	Long optionPrice;
	long optionDiscountPrice;
	boolean isInMarket;
	int stock;
	LocationDto location;
}
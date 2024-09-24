package com.sixback.backend.domain.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@AllArgsConstructor
@NoArgsConstructor
public class OptionDto {
	private Long optionId;
	private String optionName;
	private String optionImage;
	private Long optionPrice;
	private Long optionDiscountPrice;
	private int stock;
}

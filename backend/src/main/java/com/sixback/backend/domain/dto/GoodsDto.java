package com.sixback.backend.domain.dto;

import java.time.LocalDateTime;

import com.fasterxml.jackson.annotation.JsonIgnore;

public interface GoodsDto {
	Long getGoodsId();
	String getGoodsImage();
	String getGoodsName();
	Long getGoodsPrice();
	Long getGoodsDiscountPrice();
	String getBrandNameKr();
	@JsonIgnore
	LocalDateTime getLatestReleaseAt();
}


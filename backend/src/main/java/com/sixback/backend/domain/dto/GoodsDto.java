package com.sixback.backend.domain.dto;

import java.time.LocalDateTime;

import com.fasterxml.jackson.annotation.JsonIgnore;

/**
 * 상품 정보를 제공하는 DTO 인터페이스.
 * 이 인터페이스는 상품의 ID, 이미지, 이름, 가격, 브랜드 이름 등의 정보 제공.
 */
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


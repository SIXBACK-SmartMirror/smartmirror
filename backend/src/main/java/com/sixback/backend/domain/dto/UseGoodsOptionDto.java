package com.sixback.backend.domain.dto;

import com.fasterxml.jackson.annotation.JsonProperty;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 스타일에 사용된 상품 정보를 담기위한 DTO.
 */
@Getter
@AllArgsConstructor
@NoArgsConstructor
public class UseGoodsOptionDto {
	// 사용된 옵션 상품 ID, 상품명, 옵션명, 옵션 이미지 URL
	@JsonProperty("option_id")
	private Long optionId;
	@JsonProperty("goods_name")
	private String goodsName;
	@JsonProperty("option_name")
	private String optionName;
	@JsonProperty("option_image")
	private String optionImage;

	// Json 형태로 toString()
	@Override
	public String toString() {
		return String.format("""
			{
			"option_id": %d,
			"goods_name": "%s",
			"option_name": "%s",
			"option_image": "%s"
			}
			""", optionId, goodsName, optionName, optionImage);
	}
}

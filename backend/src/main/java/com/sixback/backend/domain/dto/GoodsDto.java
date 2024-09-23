package com.sixback.backend.domain.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class GoodsDto {
	private Long goodsId;
	private String goodsImage;
	private String goodsName;
	private Long goodsPrice;
	private Long goodsDiscountPrice;
	private String brandNameKr;
}

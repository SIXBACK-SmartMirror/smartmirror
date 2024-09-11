package com.sixback.backend.domain.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@AllArgsConstructor
@NoArgsConstructor
public class UseGoodsOptionDto {
    private Long optionId;
    private String goodsName;
    private String optionName;
    private String optionImage;

    @Override
    public String toString() {
        return String.format("""
                {
                "option_id":%d,
                "goods_name":"%s",
                "option_name":"%s",
                "option_image":"%s",
                }
                """, optionId, goodsName, optionName, optionImage);
    }
}

package com.sixback.backend.domain.dto;

import java.util.List;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 스타일 화장 결과를 담는 DTO.
 */
@Getter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class StyleResultDto {
	private Long styleId;
	// 스타일에 사용된 상품 옵션 목록
	private List<OptionInfoDto> goodsOptionList;
	private String makeupImage;
}

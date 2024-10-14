package com.sixback.backend.domain.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 화장 스타일 정보를 담는 DTO.
 */
@Getter
@AllArgsConstructor
@NoArgsConstructor
public class StyleInfoDto {
	private Long styleId;
	private String styleImage;
	private String styleName;
}

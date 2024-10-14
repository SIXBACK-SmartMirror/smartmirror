package com.sixback.backend.domain.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 커스텀 메이크업 결과 DTO.
 * 생성된 메이크업 이미지.
 */
@Getter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class CustomResultDto {
	// 생성된 메이크업 이미지
	private String makeupImage;
}

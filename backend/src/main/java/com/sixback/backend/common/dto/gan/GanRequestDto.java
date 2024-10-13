package com.sixback.backend.common.dto.gan;

import org.springframework.web.multipart.MultipartFile;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * Gan 모델(스타일+사용자 사진 합성) 요청 DTO.
 */
@Getter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class GanRequestDto {
	// 사용자 이미지
	private MultipartFile inputImage;
	// 합성할 스타일 이미지 URL
	private String styleImage;
}

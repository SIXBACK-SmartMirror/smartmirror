package com.sixback.backend.domain.dto;

import org.springframework.web.multipart.MultipartFile;

import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import lombok.ToString;

/**
 * 커스텀 메이크업 요청 DTO.
 * 사용자가 선택한 화장 정보.
 */
@ToString
@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
public class CustomMakeupReqDto {
	// 사용자 이미지
	@NotNull
	private MultipartFile inputImage;
	// 눈썹 색상
	private String eyebrowColor;
	// 피부 색상
	private String skinColor;
	// 입술 색상
	private String lipColor;
	// 입술 모드, 기본값은 'full'
	private String lipMode = "full";
}

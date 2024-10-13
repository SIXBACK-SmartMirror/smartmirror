package com.sixback.backend.domain.dto;

import org.springframework.web.multipart.MultipartFile;

import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import lombok.ToString;

/**
 * 가상 화장을 위한 요청 DTO 클래스.
 */
@ToString
@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
public class StyleMakeupReqDto {
	@NotNull
	private Long styleId;
	@NotNull
	private MultipartFile inputImage;
	private String inputImageBase64;
}

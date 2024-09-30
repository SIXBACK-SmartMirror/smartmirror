package com.sixback.backend.domain.dto;

import org.springframework.web.multipart.MultipartFile;

import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import lombok.ToString;

@ToString
@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
public class CustomMakeupReqDto {
	@NotNull
	private MultipartFile inputImage;
	private String eyebrowColor;
	private String skinColor;
	private String lipColor;
	private String lipMode;
}

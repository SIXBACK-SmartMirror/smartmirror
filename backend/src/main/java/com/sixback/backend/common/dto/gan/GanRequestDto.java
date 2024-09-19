package com.sixback.backend.common.dto.gan;

import org.springframework.web.multipart.MultipartFile;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class GanRequestDto {
	private MultipartFile inputImage;
	private String styleImage;
}

package com.sixback.backend.domain.dto;

import org.springframework.web.multipart.MultipartFile;

import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import lombok.ToString;

@ToString
@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
public class VirtualMakeupReqDto {
	@NotNull
	private Long styleId;
	@NotNull
	private MultipartFile inputImage;
}

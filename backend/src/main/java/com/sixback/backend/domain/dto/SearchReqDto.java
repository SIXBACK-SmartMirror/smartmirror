package com.sixback.backend.domain.dto;

import org.springframework.web.multipart.MultipartFile;

import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
public class SearchReqDto {
	@NotNull
	@Min(0)
	private int page;
	@NotNull
	@Min(1)
	private int size;
	private String keyword;
	private MultipartFile audioFile;
}

package com.sixback.backend.domain.dto;

import org.springframework.web.multipart.MultipartFile;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

/**
 * 검색 요청에 필요한 정보를 담는 DTO.
 */
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
	// 음성 파일 (.wav 형식)
	private MultipartFile audioFile;
}

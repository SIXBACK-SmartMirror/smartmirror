package com.sixback.backend.domain.dto;

import org.springframework.web.multipart.MultipartFile;

import jakarta.validation.constraints.Min;
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
	// 요청할 페이지 번호 (기본값: 0)
	@Min(0)
	private int page = 0;
	// 요청할 페이지의 크기 (기본값: 1)
	@Min(1)
	private int size = 1;
	private String keyword;
	// 음성 파일 (.wav 형식)
	private MultipartFile audioFile;
}

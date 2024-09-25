package com.sixback.backend.domain.dto;

import org.springframework.web.multipart.MultipartFile;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@AllArgsConstructor
@NoArgsConstructor
public class STTSearchReqDto {
	private MultipartFile audioFile;
	private int page;
	private int size;
}

package com.sixback.backend.domain.dto;

import org.springframework.data.domain.Page;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 화장 스타일 정보 목록을 담는 DTO.
 */
@Getter
@AllArgsConstructor
@NoArgsConstructor
public class StyleInfoListDto {
	Page<StyleInfoDto> styleInfoList;
}

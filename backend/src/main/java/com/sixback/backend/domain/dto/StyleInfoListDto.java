package com.sixback.backend.domain.dto;

import org.springframework.data.domain.Page;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@AllArgsConstructor
@NoArgsConstructor
public class StyleInfoListDto {
	Page<StyleInfoDto> styleInfoList;
}

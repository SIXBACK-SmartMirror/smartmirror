package com.sixback.backend.domain.dto;

import java.util.List;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@AllArgsConstructor
@NoArgsConstructor
public class UseOptionLocationListDto {
	private List<OptionInfoDto> locationList; // 옵션 위치 목록
}

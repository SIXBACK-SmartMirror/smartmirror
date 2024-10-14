package com.sixback.backend.domain.dto;

import java.util.List;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 스타일 화장에 사용된 옵션 위치 목록을 담는 DTO 클래스.
 */
@Getter
@AllArgsConstructor
@NoArgsConstructor
public class UseOptionLocationListDto {
	private List<OptionInfoDto> locationList; // 옵션 위치 목록
}

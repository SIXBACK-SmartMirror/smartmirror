package com.sixback.backend.domain.dto;

import java.util.List;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class GoodsDetailDto {
	private List<OptionDto> optionDtoList;
	private List<OptionInfoDto> locationList;
}

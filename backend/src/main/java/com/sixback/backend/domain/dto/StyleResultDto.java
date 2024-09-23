package com.sixback.backend.domain.dto;

import java.util.List;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class StyleResultDto {
	private Long styleId;
	private List<UseOptionDto> goodsOptionList;
	private String makeupImage;
	private String qrImage;
}

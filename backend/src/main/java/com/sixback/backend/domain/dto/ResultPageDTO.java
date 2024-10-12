package com.sixback.backend.domain.dto;

import java.util.List;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

/**
 * QR로 접속한 페이지에 표시될 정보를 담는 DTO.
 */
@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class ResultPageDTO {
	private String marketName;
	private List<UseOptionDetailDto> goodsList;
	private String makeupImage;
	private String blueprintImage;
}

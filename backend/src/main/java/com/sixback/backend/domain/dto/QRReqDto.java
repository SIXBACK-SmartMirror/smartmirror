package com.sixback.backend.domain.dto;

import java.util.List;

import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class QRReqDto {
	private Long marketId;
	@NotNull
	private List<Long> optionIdList;
	@NotNull
	private String makeupImage; // Base64
}

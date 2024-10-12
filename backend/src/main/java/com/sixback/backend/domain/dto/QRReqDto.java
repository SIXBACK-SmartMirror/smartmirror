package com.sixback.backend.domain.dto;

import java.util.List;
import java.util.stream.Collectors;

import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

/**
 * QR 코드 생성 정보를 담는 DTO.
 */
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
	private String makeupImage;  // 메이크업 이미지

	/**
	 * 옵션 ID 목록을 문자열로 변환하여 반환.
	 * @return 변환된 옵션 ID 문자열 (콤마로 구분됨)
	 */
	public String getOptionIdListString(){
		return optionIdList.stream()
			.sorted()
			.map(String::valueOf)
			.collect(Collectors.joining(","));
	}
}

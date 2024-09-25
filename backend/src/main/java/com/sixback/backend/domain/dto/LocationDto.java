package com.sixback.backend.domain.dto;

import com.fasterxml.jackson.annotation.JsonProperty;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 상품, 매장 매대 위치 정보를 담기위한 DTO.
 */
@Getter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class LocationDto {
	// 매대명, 위치 행, 위치 열
	@JsonProperty("name")
	private String name;
	@JsonProperty("row")
	private short row;
	@JsonProperty("col")
	private short col;

	// Json 형태로 toString()
	@Override
	public String toString() {
		return String.format("""
			{
			"name":"%s",
			"row":%d,
			"col":%d,
			}
			""", name, row, col);
	}
}

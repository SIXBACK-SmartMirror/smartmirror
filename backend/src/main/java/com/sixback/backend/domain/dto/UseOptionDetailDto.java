package com.sixback.backend.domain.dto;

import java.io.IOException;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.databind.ObjectMapper;

public interface UseOptionDetailDto {
	String getBrandNameKr();

	Long getOptionPrice();

	Long getOptionDiscountPrice();

	// raw 값을 직접 노출하지 않고 Boolean 반환
	default Boolean getIsInMarket() {
		return getIsInMarketRaw() != null && getIsInMarketRaw() == 1;
	}

	int getStock();

	// JSON 문자열을 LocationDto로 변환하여 반환
	default LocationDto getLocation() {
		String locationRaw = getLocationRaw();
		if (locationRaw == null || locationRaw.isEmpty()) {
			return null;
		}
		ObjectMapper objectMapper = new ObjectMapper();
		try {
			return objectMapper.readValue(locationRaw, LocationDto.class);
		} catch (IOException e) {
			e.printStackTrace();
			return null;
		}
	}

	@JsonIgnore
		// raw 값을 위한 내부 메서드
	String getLocationRaw(); // 내부적으로 사용

	@JsonIgnore
		// 원시 값을 위한 내부 메서드
	Integer getIsInMarketRaw(); // 내부적으로 사용
}

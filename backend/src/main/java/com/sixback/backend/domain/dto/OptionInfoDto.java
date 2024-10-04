package com.sixback.backend.domain.dto;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonView;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

public interface OptionInfoDto {
	interface useOptionView {
	}

	@JsonView(useOptionView.class)
	Long getOptionId();

	@JsonView(useOptionView.class)
	String getGoodsName();

	@JsonView(useOptionView.class)
	String getOptionImage();

	String getOptionColor();

	@JsonIgnore
	Long getOptionPrice();

	@JsonIgnore
	Long getOptionDiscountPrice();

	@JsonIgnore
	String getLocationRaw(); // 내부적으로 사용

	@JsonIgnore
	Integer getIsInMarketRaw(); // 내부적으로 사용

	@JsonIgnore
	String getOptionTypeNameRaw();

	@JsonIgnore
	default String getCustomOptionTypeName() {
		String optionTypeName = getOptionTypeNameRaw();
		return switch (optionTypeName) {
			case "아이메이크업" -> "eyebrowList";
			case "베이스메이크업" -> "skinList";
			default -> "lipList";
		};
	}

	String getOptionName();

	int getStock();

	// raw 값을 직접 노출하지 않고 Boolean 반환
	default boolean getIsInMarket() {
		return getIsInMarketRaw() != null && getIsInMarketRaw() == 1;
	}

	// JSON 문자열을 LocationDto로 변환하여 반환
	default LocationDto getLocation() {
		String locationRaw = getLocationRaw();
		if (locationRaw == null || locationRaw.isEmpty()) {
			return null;
		}
		ObjectMapper objectMapper = new ObjectMapper();
		try {
			return objectMapper.readValue(locationRaw, LocationDto.class);
		} catch (JsonProcessingException e) {
			throw new RuntimeException(e);
		}
	}
}

package com.sixback.backend.domain.dto;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

/**
 * 상품 옵션 정보를 제공하는 DTO 인터페이스.
 */
public interface OptionInfoDto {
	Long getOptionId();

	String getGoodsName();

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

	/**
	 * 커스텀 옵션 타입 이름에 따라 그룹화할 타입명 지정.
	 * @return 그룹화할 타입명.
	 */
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

	/**
	 * 원시 값을 직접 노출하지 않고 매장 존재 여부를 반환.
     * @return 매장 존재 여부.
     */
	default boolean getIsInMarket() {
		return getIsInMarketRaw() != null && getIsInMarketRaw() == 1;
	}

	/**
	 * JSON 문자열을 LocationDto로 변환하여 반환.
	 * @return 위치 정보 DTO.
	 */
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

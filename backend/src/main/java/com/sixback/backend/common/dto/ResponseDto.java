package com.sixback.backend.common.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;

/**
 * 응답 DTO.
 *
 * @param <T> 응답할 데이터 타입 List, String 등.
 */
@Getter
@AllArgsConstructor
public class ResponseDto<T> {
	String code;
	T data;
}

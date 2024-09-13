package com.sixback.backend.common.exception;

/**
 * 사용자 지정 예외.
 */
public class CustomException extends RuntimeException {
	public CustomException(String message) {
		super(message);
	}
}

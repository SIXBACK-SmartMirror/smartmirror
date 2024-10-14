package com.sixback.backend.common.exception;

/**
 * 서버 처리 중 발생한 예외.
 */
public class CustomServerException extends RuntimeException {
	public CustomServerException(String message) {
		super(message);
	}
}

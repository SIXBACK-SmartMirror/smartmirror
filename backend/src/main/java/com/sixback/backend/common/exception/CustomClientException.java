package com.sixback.backend.common.exception;

/**
 * 잘못된 client 요청 예외.
 */
public class CustomClientException extends RuntimeException {
	public CustomClientException(String message) {
		super(message);
	}
}

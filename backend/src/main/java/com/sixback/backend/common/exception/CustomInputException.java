package com.sixback.backend.common.exception;

/**
 * client 요청내 잘못된 파일 예외.
 */
public class CustomInputException extends RuntimeException {
	public CustomInputException(String message) {
		super(message);
	}
}

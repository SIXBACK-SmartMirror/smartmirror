package com.sixback.backend.common.exception;

/**
 * 사용자 요청내 잘못된 파일 예외.
 */
public class CustomFileException extends RuntimeException {
	public CustomFileException(String message) {
		super(message);
	}
}

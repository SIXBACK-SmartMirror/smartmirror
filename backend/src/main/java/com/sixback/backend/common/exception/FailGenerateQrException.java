package com.sixback.backend.common.exception;

/**
 * 빈 파일이 전달됐을 떄 예외 발생
 */
public class FailGenerateQrException extends CustomServerException {
	public FailGenerateQrException() {
		super("J00");
	}
}

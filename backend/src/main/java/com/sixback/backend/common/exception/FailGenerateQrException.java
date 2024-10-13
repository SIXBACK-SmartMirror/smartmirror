package com.sixback.backend.common.exception;

/**
 * QR 생성 실패 예외.
 */
public class FailGenerateQrException extends CustomServerException {
	public FailGenerateQrException() {
		super("J00");
	}
}

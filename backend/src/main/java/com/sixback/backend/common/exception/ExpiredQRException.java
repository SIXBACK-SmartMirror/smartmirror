package com.sixback.backend.common.exception;

/**
 * QR 코드가 만료되어 발생하는 예외.
 */
public class ExpiredQRException extends CustomClientException {
	public ExpiredQRException() {
		super("J01");
	}
}

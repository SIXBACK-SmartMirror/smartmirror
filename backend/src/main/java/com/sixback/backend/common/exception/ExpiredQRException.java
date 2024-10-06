package com.sixback.backend.common.exception;

/**
 * 해당 market을 찾을 수 없음
 */
public class ExpiredQRException extends CustomClientException {
	public ExpiredQRException() {
		super("J01");
	}
}

package com.sixback.backend.common.exception;

/**
 * style을 찾을 수 없을 경우
 */
public class StyleNotFoundException extends CustomClientException {
	public StyleNotFoundException() {
		super("C01");
	}
}

package com.sixback.backend.common.exception;

/**
 * client 요청 base64의 decoding 실패로 발생한 예외.
 */
public class FailDecodeBase64Exception extends CustomInputException {
	public FailDecodeBase64Exception() {
		super("F03");
	}
}

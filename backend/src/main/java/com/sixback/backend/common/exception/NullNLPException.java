package com.sixback.backend.common.exception;

/**
 * NLP 응답 결과가 없을 시 발생하는 예외.
 */
public class NullNLPException extends CustomServerException {
	public NullNLPException() {
		super("H03");
	}
}

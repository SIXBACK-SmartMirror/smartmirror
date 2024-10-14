package com.sixback.backend.common.exception;

/**
 * NLP AI 처리 실패 예외.
 */
public class FailNLPException extends CustomServerException {
	public FailNLPException() {
		super("H04");
	}
}

package com.sixback.backend.common.exception;

/**
 * AI 서버 처리 실패 예외.
 */
public class FailAIException extends CustomServerException {
	public FailAIException() {
		super("G00");
	}
}

package com.sixback.backend.common.exception;

/**
 * GAN AI 서버 응답 예외
 */
public class FailGanException extends CustomServerException {
	public FailGanException() {
		super("G00");
	}
}

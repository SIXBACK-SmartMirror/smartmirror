package com.sixback.backend.common.exception;

/**
 * STT 응답 결과가 없을 시 발생하는 예외.
 */
public class NullSTTException extends CustomServerException {
	public NullSTTException() {
		super("H01");
	}
}

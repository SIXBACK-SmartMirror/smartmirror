package com.sixback.backend.common.exception;

/**
 * 키워드 없이 검색할 경우 발생한 예외.
 */
public class NoSearchKeywordException extends CustomInputException {
	public NoSearchKeywordException() {
		super("I02");
	}
}

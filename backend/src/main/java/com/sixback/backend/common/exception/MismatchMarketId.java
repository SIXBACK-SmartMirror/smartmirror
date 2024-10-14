package com.sixback.backend.common.exception;

/**
 * 접근한 marketId와 요청한 marketId가 불일치 시 예외 발생.
 */
public class MismatchMarketId extends CustomInputException {
	public MismatchMarketId() {
		super("E03");
	}
}

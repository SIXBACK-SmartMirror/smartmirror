package com.sixback.backend.common.exception;

/**
 * 접근 불가능한 market 접근시 발생.
 */
public class InvalidMarketException extends CustomClientException {
	public InvalidMarketException() {
		super("E02");
	}
}

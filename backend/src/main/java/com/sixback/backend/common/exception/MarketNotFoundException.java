package com.sixback.backend.common.exception;

/**
 * 해당 market을 찾을 수 없음
 */
public class MarketNotFoundException extends CustomClientException {
	public MarketNotFoundException() {
		super("C04");
	}
}

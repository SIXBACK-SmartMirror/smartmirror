package com.sixback.backend.common.exception;

/**
 * 요청 option을 찾을 수 없음.
 */
public class OptionNotFoundException extends CustomClientException {
	public OptionNotFoundException() {
		super("C02");
	}
}

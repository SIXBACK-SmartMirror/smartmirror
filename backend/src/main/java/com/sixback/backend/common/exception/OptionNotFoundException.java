package com.sixback.backend.common.exception;

/**
 * 관련 option을 찾을 수 없음
 */
public class OptionNotFoundException extends CustomClientException {
	public OptionNotFoundException() {
		super("C02");
	}
}

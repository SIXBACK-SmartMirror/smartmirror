package com.sixback.backend.common.exception;

/**
 * style에 사용된 GoodsOption을 찾을 수 없을 경우
 */
public class StyleUseOptionNotFoundException extends CustomClientException {
	public StyleUseOptionNotFoundException() {
		super("C03");
	}
}

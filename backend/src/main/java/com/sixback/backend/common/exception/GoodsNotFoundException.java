package com.sixback.backend.common.exception;

/**
 * 관련 Goods를 찾을 수 없음.
 */
public class GoodsNotFoundException extends CustomClientException {
	public GoodsNotFoundException() {
		super("E00");
	}
}

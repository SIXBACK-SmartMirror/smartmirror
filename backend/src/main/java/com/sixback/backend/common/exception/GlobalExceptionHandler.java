package com.sixback.backend.common.exception;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestControllerAdvice;

import com.sixback.backend.common.dto.ResponseDto;

@RestControllerAdvice
public class GlobalExceptionHandler {
	/**
	 * 사용자 지정 예외 발생.
	 *
	 * @param e 사용자 지정 예외.
	 * @return
	 */
	@ExceptionHandler(CustomException.class)
	public ResponseEntity<?> handleCustomException(CustomException e) {
		return new ResponseEntity<>(new ResponseDto<>(e.getMessage(), null), HttpStatus.OK);
	}
}

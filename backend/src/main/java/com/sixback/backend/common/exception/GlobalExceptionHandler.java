package com.sixback.backend.common.exception;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestControllerAdvice;

import com.sixback.backend.common.dto.ResponseDto;

import lombok.extern.slf4j.Slf4j;

@RestControllerAdvice
@Slf4j
public class GlobalExceptionHandler {
	/**
	 * 요청 처리 중 서버 문제로 사용자 지정 예외 발생.
	 *
	 * @param e 사용자 지정 예외.
	 * @return
	 */
	@ExceptionHandler(CustomServerException.class)
	public ResponseEntity<?> handleCustomServerException(CustomServerException e) {
		return new ResponseEntity<>(new ResponseDto<>(e.getMessage(), null), HttpStatus.INTERNAL_SERVER_ERROR);
	}

	/**
	 * 잘못된 요청 등으로 사용자 지정 예외 발생.
	 *
	 * @param e 사용자 지정 예외.
	 * @return
	 */
	@ExceptionHandler(CustomClientException.class)
	public ResponseEntity<?> handleCustomClientException(CustomClientException e) {
		return new ResponseEntity<>(new ResponseDto<>(e.getMessage(), null), HttpStatus.NOT_FOUND);
	}

	/**
	 * 잘못된 요청(파일) 등으로 사용자 지정 예외 발생.
	 *
	 * @param e 사용자 지정 예외.
	 * @return
	 */
	@ExceptionHandler(CustomFileException.class)
	public ResponseEntity<?> handleCustomFileException(CustomFileException e) {
		return new ResponseEntity<>(new ResponseDto<>(e.getMessage(), null), HttpStatus.BAD_REQUEST);
	}

	/**
	 * 부적절항 양식 요청시 예외  발생.
	 * @param e
	 * @return
	 */
	@ExceptionHandler(MethodArgumentNotValidException.class)
	public ResponseEntity<?> handleValidException(MethodArgumentNotValidException e) {
		return new ResponseEntity<>(new ResponseDto<>("I00", null), HttpStatus.BAD_REQUEST);
	}

	/**
	 * 알 수 없는 예외 발생.
	 * @param e
	 * @return
	 */
	@ExceptionHandler(Exception.class)
	public ResponseEntity<?> handleUnknownException(Exception e) {
		log.error("Exception Error " + e.getMessage());
		return new ResponseEntity<>(new ResponseDto<>("B00", null), HttpStatus.INTERNAL_SERVER_ERROR);
	}
}

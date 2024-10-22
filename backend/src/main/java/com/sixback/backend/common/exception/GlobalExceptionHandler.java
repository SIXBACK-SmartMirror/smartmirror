package com.sixback.backend.common.exception;

import java.io.IOException;

import org.springframework.dao.DataAccessException;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.dao.EmptyResultDataAccessException;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestControllerAdvice;
import org.springframework.web.client.RestClientException;
import org.springframework.web.method.annotation.HandlerMethodValidationException;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.sixback.backend.common.dto.ResponseDto;

import lombok.extern.slf4j.Slf4j;

/**
 * GlobalExceptionHandler 예외 발생시 처리 handler.
 */
@RestControllerAdvice
@Slf4j
public class GlobalExceptionHandler {
	/**
	 * 요청 처리 중 서버 문제로 사용자 지정 예외 발생.
	 *
	 * @param e 사용자 지정 예외 CustomServerException.
	 * @return ResponseEntity 처리 실패 응답.
	 */
	@ExceptionHandler(CustomServerException.class)
	public ResponseEntity<?> handleCustomServerException(CustomServerException e) {
		log.error(e.getMessage());
		return new ResponseEntity<>(new ResponseDto<>(e.getMessage(), null), HttpStatus.INTERNAL_SERVER_ERROR);
	}

	/**
	 * 잘못된 client 요청 등으로 사용자 지정 예외 발생.
	 *
	 * @param e 사용자 지정 예외 CustomClientException.
	 * @return ResponseEntity 처리 실패 응답.
	 */
	@ExceptionHandler(CustomClientException.class)
	public ResponseEntity<?> handleCustomClientException(CustomClientException e) {
		log.error(e.getMessage());
		return new ResponseEntity<>(new ResponseDto<>(e.getMessage(), null), HttpStatus.NOT_FOUND);
	}

	/**
	 * 잘못된 요청(파일) 등으로 사용자 지정 예외 발생.
	 *
	 * @param e 사용자 지정 예외 CustomInputException.
	 * @return ResponseEntity 처리 실패 응답.
	 */
	@ExceptionHandler(CustomInputException.class)
	public ResponseEntity<?> handleCustomFileException(CustomInputException e) {
		log.error(e.getMessage());
		return new ResponseEntity<>(new ResponseDto<>(e.getMessage(), null), HttpStatus.BAD_REQUEST);
	}

	/**
	 * 알 수 없는 예외 발생.
	 * @param e Exception.
	 * @return ResponseEntity 처리 실패 응답.
	 */
	@ExceptionHandler(Exception.class)
	public ResponseEntity<?> handleUnknownException(Exception e) {
		log.error("Exception Error {}", e.getMessage());
		return new ResponseEntity<>(new ResponseDto<>("B00", null), HttpStatus.INTERNAL_SERVER_ERROR);
	}

	/**
	 * 파일 IO 관련 파일 처리 중 발생.
	 * @param e IOException.
	 * @return ResponseEntity 처리 실패 응답.
	 */
	@ExceptionHandler(IOException.class)
	public ResponseEntity<?> FileIOException(IOException e) {
		log.error(e.getMessage());
		return new ResponseEntity<>(new ResponseDto<>("F00", null), HttpStatus.INTERNAL_SERVER_ERROR);
	}

	/**
	 * 유효성 검사 오류 (Request Body에서의 유효성 검사).
	 * @param e MethodArgumentNotValidException.
	 * @return ResponseEntity 처리 실패 응답.
	 */
	@ExceptionHandler(MethodArgumentNotValidException.class)
	public ResponseEntity<?> handleValidationException(MethodArgumentNotValidException e) {
		log.error(e.getMessage());
		return new ResponseEntity<>(new ResponseDto<>("I00", null), HttpStatus.BAD_REQUEST);
	}

	/**
	 * 유효성 검사 오류 (Request Header에서의 유효성 검사).
	 * @param e HandlerMethodValidationException.
	 * @return ResponseEntity 처리 실패 응답.
	 */
	@ExceptionHandler(HandlerMethodValidationException.class)
	public ResponseEntity<?> handleValidationException(HandlerMethodValidationException e) {
		log.error(e.getMessage());
		return new ResponseEntity<>(new ResponseDto<>("I01", null), HttpStatus.BAD_REQUEST);
	}

	/**
	 * db에 정보가 없을 때.
	 * @param e EmptyResultDataAccessException.
	 * @return ResponseEntity 처리 실패 응답.
	 */
	@ExceptionHandler(EmptyResultDataAccessException.class)
	public ResponseEntity<?> noDataException(EmptyResultDataAccessException e) {
		log.error(e.getMessage());
		return new ResponseEntity<>(new ResponseDto<>("C00", null), HttpStatus.NOT_FOUND);
	}

	/**
	 * 무결성 제약 위반시 발생.
	 * @param e DataIntegrityViolationException.
	 * @return ResponseEntity 처리 실패 응답.
	 */
	@ExceptionHandler(DataIntegrityViolationException.class)
	public ResponseEntity<?> integrityViolationException(DataIntegrityViolationException e) {
		log.error(e.getMessage());
		return new ResponseEntity<>(new ResponseDto<>("D00", null), HttpStatus.INTERNAL_SERVER_ERROR);
	}

	/**
	 * 데이터 베이스 전반 오류.
	 * @param e DataAccessException.
	 * @return ResponseEntity 처리 실패 응답.
	 */
	@ExceptionHandler(DataAccessException.class)
	public ResponseEntity<?> dataException(DataAccessException e) {
		log.error(e.getMessage());
		return new ResponseEntity<>(new ResponseDto<>("D00", null), HttpStatus.INTERNAL_SERVER_ERROR);
	}

	/**
	 * 외부 API 통신 실패.
	 * @param e RestClientException.
	 * @return ResponseEntity 처리 실패 응답.
	 */
	@ExceptionHandler(RestClientException.class)
	public ResponseEntity<?> apiFailException(RestClientException e) {
		log.error(e.getMessage());
		return new ResponseEntity<>(new ResponseDto<>("H00", null), HttpStatus.INTERNAL_SERVER_ERROR);
	}

	/**
	 * 파싱 실패 예외.
	 * @param e JsonProcessingException.
	 * @return ResponseEntity 처리 실패 응답.
	 */
	@ExceptionHandler(JsonProcessingException.class)
	public ResponseEntity<?> JsonProcessingException(JsonProcessingException e) {
		log.error(e.getMessage());
		return new ResponseEntity<>(new ResponseDto<>("D01", null), HttpStatus.INTERNAL_SERVER_ERROR);
	}
}

package com.sixback.backend.common.service;

import org.springframework.core.io.ByteArrayResource;
import org.springframework.stereotype.Service;
import org.springframework.util.LinkedMultiValueMap;
import org.springframework.util.MultiValueMap;
import org.springframework.web.reactive.function.client.ClientResponse;
import org.springframework.web.reactive.function.client.WebClient;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.sixback.backend.common.exception.FailAIException;
import com.sixback.backend.domain.dto.CustomMakeupReqDto;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

/**
 * Facer 서버와의 통신하여 사용자가 원하는 분위 화장을 요청 서비스.
 * 요청을 보내고 서버로부터 받은 응답을 처리.
 */
@Slf4j
@Service
@RequiredArgsConstructor
public class FacerClientService {
	// Facer 서버와의 HTTP 통신을 위한 WebClient 인스턴스
	private final WebClient facerWebClient;
	// JSON 직렬화 및 역직렬화를 위한 ObjectMapper
	private final ObjectMapper objectMapper;
	// File 관련 처리 서비스
	private final FileService fileService;

	/**
	 * 에러 로그를 콘솔에 출력하는 메서드.
	 *
	 * @param errorBody 서버로부터 받은 에러 응답 본문.
	 */
	private static void errorLog(String errorBody) {
		// 에러 응답 본문을 콘솔에 출력
		log.error("Facer Server Error Response: {}", errorBody);
	}

	/**
	 * 커스텀 화장 요청을 Facer 서버에 보내는 메서드.
	 *
	 * @param customMakeupReqDto 사용자 이미지와 커스텀 정보가 포함된 요청 DTO.
	 * @return 서버로부터 받은 화장 결과 이미지 base64를 포함하는 Mono
	 */
	public Mono<String> sendRequest(CustomMakeupReqDto customMakeupReqDto) {
		// Multipart 요청을 위한 Body 생성
		MultiValueMap<String, Object> body = createBody(customMakeupReqDto);
		return facerWebClient.post()
			.uri("/ai/custom") // Facer 서버 요청 엔드포인트
			.bodyValue(body) // 요청 본문 설정
			.retrieve() // 요청 실행 및 응답 수신
			.onStatus(status -> !status.is2xxSuccessful(), // 상태 코드가 2xx가 아닐 경우
				this::handleErrorResponse) // 에러 처리 메서드 호출
			.bodyToMono(String.class) // 응답 본문을 문자열로 변환
			.flatMap(this::parseMakeupImage); // 화장 결과 이미지 파싱
	}

	/**
	 * 각 값이 null이 아닐 경우에만 추가하는 헬퍼 메서드.
	 *
	 * @param body 요청 본문.
	 * @param key 추가할 키.
	 * @param value 추가할 값.
	 */
	private void addIfNotNull(MultiValueMap<String, Object> body, String key, Object value) {
		if (value != null) {
			body.add(key, value); // 값이 null이 아닐 경우 본문에 추가
		}
	}

	/**
	 * 요청 본문을 생성하는 메서드.
	 *
	 * @param customMakeupReqDto 사용자 이미지와 커스텀 정보가 포함된 요청 DTO.
	 * @return 생성된 요청 본문.
	 */
	public MultiValueMap<String, Object> createBody(CustomMakeupReqDto customMakeupReqDto) {
		MultiValueMap<String, Object> body = new LinkedMultiValueMap<>();
		log.debug("customMakeupReqDto = {}", customMakeupReqDto);

		// 파일 데이터가 포함된 ByteArrayResource 생성
		ByteArrayResource resource = fileService.multipartFileToByteArray(customMakeupReqDto.getInputImage());

		// 요청 본문 구성
		body.add("inputImage", resource); // 요청 본문에 파일 추가

		// eyebrowColor, skinColor, lipColor는 null이 아닐 경우에만 추가
		addIfNotNull(body, "eyebrowColor", customMakeupReqDto.getEyebrowColor());
		addIfNotNull(body, "skinColor", customMakeupReqDto.getSkinColor());
		addIfNotNull(body, "lipColor", customMakeupReqDto.getLipColor());

		// lipMode (full 또는 gradient) 본문에 추가
		body.add("lipMode", customMakeupReqDto.getLipMode());
		log.debug("body = {}", body);
		return body; // 생성된 본문 반환
	}

	/**
	 * 서버 응답 중 에러가 발생한 경우 처리하는 메서드.
	 *
	 * @param clientResponse 클라이언트 응답.
	 * @return 에러를 나타내는 Mono.
	 * @throws FailAIException 2XX 외 Facer 서버 응답.
	 */
	private Mono<Throwable> handleErrorResponse(ClientResponse clientResponse) {
		return clientResponse.bodyToMono(String.class) // 응답 본문을 문자열로 변환
			.doOnNext(FacerClientService::errorLog) // 에러 로그 남기기
			.then(Mono.error(new FailAIException())); // 예외 발생
	}

	/**
	 * 서버로부터 받은 결과 이미지을 파싱하는 메서드.
	 *
	 * @param responseBody 응답 본문.
	 * @return 파싱된 결과 이미지 base64을 포함하는 Mono.
	 * @throws FailAIException Gan 응답 파싱 실패.
	 */
	private Mono<String> parseMakeupImage(String responseBody) {
		try {
			log.debug(responseBody); // 응답 본문 로그
			JsonNode root = objectMapper.readTree(responseBody); // JSON 파싱
			String makeupImage = root.path("data").path("makeupImage").asText(); // 메이크업 이미지 추출
			return Mono.just(makeupImage); // 메이크업 이미지 반환
		} catch (JsonProcessingException e) {
			errorLog(e.getMessage()); // JSON 파싱 오류 로그
			return Mono.error(new FailAIException()); // 예외 발생
		}
	}
}

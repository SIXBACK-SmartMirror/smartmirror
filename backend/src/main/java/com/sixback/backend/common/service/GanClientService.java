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
import com.sixback.backend.common.dto.gan.GanRequestDto;
import com.sixback.backend.common.exception.FailAIException;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

/**
 * GAN 서버와 통신하여 사용자의 사진과 스타일을 합성 요청 서비스.
 * 요청을 보내고 서버로부터 받은 응답을 처리.
 */
@Slf4j
@Service
@RequiredArgsConstructor
public class GanClientService {
	// Gan 서버와의 HTTP 통신을 위한 WebClient 인스턴스
	private final WebClient ganWebClient;
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
		log.error("GAN Server Error Response: {}", errorBody);
	}

	/**
	 * 스타일 화장 합성 요청을 GAN 서버에 보내는 메서드.
	 *
	 * @param ganRequestDto 사용자 이미지와 스타일 정보가 포함된 요청 DTO.
	 * @return 서버로부터 받은 화장 결과 이미지 base64를 포함하는 Mono.
	 */
	public Mono<String> sendRequest(GanRequestDto ganRequestDto) {
		// Multipart 요청을 위한 Body 생성
		MultiValueMap<String, Object> body = createBody(ganRequestDto);
		return ganWebClient.post()
			.uri("/ai/makeup") // GAN 서버 요청 엔드포인트
			.bodyValue(body) // 요청 본문 설정
			.retrieve() // 요청 실행 및 응답 수신
			.onStatus(status -> !status.is2xxSuccessful(), // 상태 코드가 2xx가 아닐 경우 에러 처리
				this::handleErrorResponse) // 에러 처리 메서드 호출
			.bodyToMono(String.class) // 응답 본문을 문자열로 변환
			.flatMap(this::parseMakeupImage); // 화장 결과 파싱
	}

	/**
	 * 요청 본문을 생성하는 메서드.
	 *
	 * @param ganRequestDto 사용자 이미지와 스타일 정보가 포함된 요청 DTO.
	 * @return 생성된 요청 본문.
	 */
	public MultiValueMap<String, Object> createBody(GanRequestDto ganRequestDto) {
		MultiValueMap<String, Object> body = new LinkedMultiValueMap<>();
		// 파일 데이터가 포함된 ByteArrayResource 생성
		ByteArrayResource resource = fileService.multipartFileToByteArray(ganRequestDto.getInputImage());
		// 요청 본문 구성
		body.add("inputImage", resource); // 요청 본문에 사용자의 이미지 파일 추가
		body.add("styleImage", ganRequestDto.getStyleImage()); // 스타일 이미지 URL 추가
		return body; // 생성된 요청 본문 반환
	}

	/**
	 * 서버 응답 중 에러가 발생한 경우 처리하는 메서드.
	 *
	 * @param clientResponse 클라이언트 응답.
	 * @return 에러를 나타내는 Mono.
	 * @throws FailAIException 2XX 외 Gan 서버 응답.
	 */
	private Mono<Throwable> handleErrorResponse(ClientResponse clientResponse) {
		return clientResponse.bodyToMono(String.class) // 응답 본문을 문자열로 변환
			.doOnNext(GanClientService::errorLog) // 에러 로그 출력
			.then(Mono.error(new FailAIException())); // 예외 발생
	}

	/**
	 * 서버로부터 받은 결과 이미지을 파싱하는 메서드.
	 *
	 * @param responseBody 서버 응답 본문.
	 * @return 파싱된 결과 이미지 base64을 포함하는 Mono.
	 * @throws FailAIException Gan 응답 파싱 실패.
	 */
	private Mono<String> parseMakeupImage(String responseBody) {
		try {
			// 응답 본문을 JSON으로 파싱
			JsonNode root = objectMapper.readTree(responseBody);
			String makeupImage = root.path("data").path("makeupImage").asText();
			return Mono.just(makeupImage);
		} catch (JsonProcessingException e) {
			// JSON 파싱 중 오류 발생 시 로그 출력 및 예외 처리
			errorLog(e.getMessage());
			return Mono.error(new FailAIException());
		}
	}
}

package com.sixback.backend.common.service;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.io.ByteArrayResource;
import org.springframework.stereotype.Service;
import org.springframework.util.LinkedMultiValueMap;
import org.springframework.util.MultiValueMap;
import org.springframework.web.client.RestClientException;
import org.springframework.web.multipart.MultipartFile;
import org.springframework.web.reactive.function.client.ClientResponse;
import org.springframework.web.reactive.function.client.WebClient;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.sixback.backend.common.exception.FailSTTException;
import com.sixback.backend.common.exception.NullSTTException;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

/**
 * STT 서버와의 통신하여 음성 파일을 텍스트로 변환 요청 서비스.
 * 요청을 보내고 서버로부터 받은 응답을 처리.
 */
@Slf4j
@Service
@RequiredArgsConstructor
public class STTClientService {
	// STT 서버와의 HTTP 통신을 위한 WebClient 인스턴스
	private final WebClient sttWebClient;
	// JSON 직렬화 및 역직렬화를 위한 ObjectMapper
	private final ObjectMapper objectMapper;
	// 파일 처리 서비스 인스턴스
	private final FileService fileService;
	// STT API의 URI 설정
	@Value("${spring.data.stt.uri}")
	private String STT_API_URI;
	// STT 모델 설정
	@Value("${spring.data.stt.model}")
	private String STT_MODEL;

	/**
	 * 에러 응답 본문을 콘솔에 출력하는 정적 메서드.
	 *
	 * @param errorBody 에러 응답 본문.
	 */
	private static void errorLog(String errorBody) {
		// 에러 응답 본문을 콘솔에 출력
		log.error("STT Server Error Response: {}", errorBody);
	}

	/**
	 * STT 서버에 음성 파일을 전송하여 텍스트 결과를 요청하는 메서드.
	 *
	 * @param audioFile 변환할 음성 파일.
	 * @return STT 서버 응답으로부터 얻은 텍스트 결과.
	 */
	public Mono<String> sendRequest(MultipartFile audioFile) {
		// Multipart 요청을 위한 Body 생성
		MultiValueMap<String, Object> body = creatBody(audioFile);
		return sttWebClient.post()
			.uri(STT_API_URI) // STT API의 URI
			.bodyValue(body) // 요청 본문 설정
			.retrieve() // 요청 실행 및 응답 수신
			.onStatus(status -> !status.is2xxSuccessful(),
				this::handleErrorResponse) // 에러 처리 메서드 호출
			.bodyToMono(String.class) // 응답 본문을 문자열로 변환
			.flatMap(this::parseSTTResult); // STT 결과 파싱
	}

	/**
	 * STT 요청 본문을 생성하는 메서드.
	 *
	 * @param audioFile 음성 파일.
	 * @return 생성된 STT 요청 본문.
	 */
	public MultiValueMap<String, Object> creatBody(MultipartFile audioFile) {
		MultiValueMap<String, Object> body = new LinkedMultiValueMap<>();
		// 파일 데이터가 포함된 ByteArrayResource 생성
		ByteArrayResource resource = fileService.multipartFileToByteArray(audioFile);
		// 요청 본문 구성
		body.add("file", resource); // 요청 본문에 파일 추가
		body.add("model", STT_MODEL); // 요청 본문에 모델 추가
		body.add("language", "ko"); // 요청 본문에 언어 설정 추가
		return body;
	}

	/**
	 * 서버 응답 중 에러가 발생한 경우 처리하는 메서드.
	 *
	 * @param clientResponse 클라이언트 응답.
	 * @return 에러를 나타내는 Mono.
	 * @throws RestClientException STT 서버와의 통신 오류.
	 */
	private Mono<Throwable> handleErrorResponse(ClientResponse clientResponse) {
		// 에러 응답 본문을 콘솔에 출력
		return clientResponse.bodyToMono(String.class)
			.doOnNext(STTClientService::errorLog) // 에러 로그 출력
			.then(Mono.error(new RestClientException("STT Server communication failure"))); // 예외 발생
	}

	/**
	 * STT 서버의 응답을 파싱하여 텍스트 결과를 추출하는 메서드.
	 *
	 * @param responseBody 응답 본문.
	 * @return 추출된 텍스트 결과.
	 * @throws FailSTTException STT 응답 파싱 실패.
	 */
	private Mono<String> parseSTTResult(String responseBody) {
		try {
			// JSON 파싱 후 text 키 추출
			JsonNode root = objectMapper.readTree(responseBody);
			JsonNode textNode = root.path("text");
			if (textNode.isMissingNode()) {
				// text 키가 존재하지 않을 때
				errorLog("text 키가 없습니다.");
				return Mono.error(new FailSTTException());
			} else if (textNode.asText().isBlank()) {
				// text 키가 존재하지만 값이 빈 문자열일 때
				errorLog("text 키는 존재하지만 값이 비어 있습니다.");
				return Mono.error(new NullSTTException());
			}
			// 텍스트 결과 추출
			String sttResult = textNode.asText();
			log.debug("STT Result: {}", sttResult);
			return Mono.just(sttResult);
		} catch (JsonProcessingException e) {
			errorLog(e.getMessage());
			return Mono.error(new FailSTTException());
		}
	}
}

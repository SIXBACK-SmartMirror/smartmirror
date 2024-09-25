package com.sixback.backend.common.service;

import java.io.IOException;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.io.ByteArrayResource;
import org.springframework.http.MediaType;
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

@Slf4j
@Service
@RequiredArgsConstructor
public class STTClientService {

	private final WebClient sttWebClient;
	private final ObjectMapper objectMapper;
	@Value("${spring.data.stt.url}")
	private String STT_API_URL;
	@Value("${spring.data.stt.model}")
	private String STT_MODEL;

	public Mono<String> sendRequest(MultipartFile autioFile) {
		// Multipart 요청을 위한 Body 생성
		MultiValueMap<String, Object> body = creatBody(autioFile);
		return sttWebClient.post()
			.uri(STT_API_URL)
			.contentType(MediaType.MULTIPART_FORM_DATA)
			.bodyValue(body)
			.retrieve()
			.onStatus(status -> !status.is2xxSuccessful(),
				this::handleErrorResponse)
			.bodyToMono(String.class)
			.flatMap(this::parseSTTResult);
	}

	public MultiValueMap<String, Object> creatBody(MultipartFile autioFile) {
		MultiValueMap<String, Object> body = new LinkedMultiValueMap<>();
		// 파일 데이터가 포함된 ByteArrayResource 생성
		ByteArrayResource resource = multipartFileToByteArray(autioFile);
		// 요청 본문 구성
		body.add("file", resource); // 요청 본문에 파일 추가
		body.add("model", STT_MODEL);
		body.add("language ", "ko");
		return body;
	}

	public ByteArrayResource multipartFileToByteArray(MultipartFile autioFile) {
		try {
			return new ByteArrayResource(autioFile.getBytes()) {
				@Override
				public String getFilename() {
					// 파일 이름 제공
					return autioFile.getOriginalFilename();
				}
			};
		} catch (IOException ex) {
			throw new RuntimeException(ex);
		}
	}

	private Mono<Throwable> handleErrorResponse(ClientResponse clientResponse) {
		return clientResponse.bodyToMono(String.class)
			.doOnNext(errorBody -> {
				// 에러 응답 본문을 콘솔에 출력
				log.error("STT Server Error Response: " + errorBody);
			})
			.then(Mono.error(new RestClientException("STT Server communication failure")));
	}

	private Mono<String> parseSTTResult(String responseBody) {
		try {
			JsonNode root = objectMapper.readTree(responseBody);
			JsonNode textNode = root.path("text");
			if (textNode.isMissingNode()) {
				// text 키가 존재하지 않을 때
				log.error("text 키가 없습니다.");
				return Mono.error(new FailSTTException());
			} else if (textNode.asText().isEmpty()) {
				// text 키가 존재하지만 값이 빈 문자열일 때
				log.error("text 키는 존재하지만 값이 비어 있습니다.");
				return Mono.error(new NullSTTException());
			}
			String sttResult = textNode.asText();
			return Mono.just(sttResult);
		} catch (JsonProcessingException e) {
			log.error("Failed to parse STT server response", e);
			return Mono.error(new FailSTTException());
		}
	}
}

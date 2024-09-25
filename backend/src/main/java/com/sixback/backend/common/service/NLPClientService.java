package com.sixback.backend.common.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Service;
import org.springframework.util.LinkedMultiValueMap;
import org.springframework.util.MultiValueMap;
import org.springframework.web.client.RestClientException;
import org.springframework.web.reactive.function.client.ClientResponse;
import org.springframework.web.reactive.function.client.WebClient;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.sixback.backend.common.dto.nlp.ChatMessageDto;
import com.sixback.backend.common.dto.nlp.NLPResponseDto;
import com.sixback.backend.common.exception.FailNLPException;
import com.sixback.backend.common.exception.NullNLPException;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

@Slf4j
@Service
@RequiredArgsConstructor
public class NLPClientService {

	private final WebClient nlpWebClient;
	private final ObjectMapper objectMapper;
	@Value("${spring.data.chat.uri}")
	private String CHAT_API_URI;
	@Value("${spring.data.chat.model}")
	private String CHAT_MODEL;
	@Value("${spring.data.chat.temperature}")
	private float TEMPERATURE;
	@Value("${spring.data.chat.max.token}")
	private short MAX_TOKENS;

	public Mono<String> sendRequest(String sttResult) {
		// Multipart 요청을 위한 Body 생성
		MultiValueMap<String, Object> body = creatBody(sttResult);
		System.out.println(body);
		return nlpWebClient.post()
			.uri(CHAT_API_URI)
			.contentType(MediaType.MULTIPART_FORM_DATA)
			.bodyValue(body)
			.retrieve()
			.onStatus(status -> !status.is2xxSuccessful(),
				this::handleErrorResponse)
			.bodyToMono(String.class)
			.flatMap(this::parseNLPResult);
	}

	public MultiValueMap<String, Object> creatBody(String sttResult) {
		MultiValueMap<String, Object> body = new LinkedMultiValueMap<>();
		String prompt = String.format("Extract only the product name from the following sentence, and output just the product name with nothing else: %s", sttResult);
		// 메시지 생성
		ChatMessageDto message = new ChatMessageDto("user", prompt);
		// MultiValueMap 구성
		body.add("model", CHAT_MODEL);
		body.add("messages", message);
		// body.add("temperature", TEMPERATURE);
		// body.add("max_tokens", MAX_TOKENS);
		return body;
	}

	private Mono<Throwable> handleErrorResponse(ClientResponse clientResponse) {
		return clientResponse.bodyToMono(String.class)
			.doOnNext(errorBody -> {
				// 에러 응답 본문을 콘솔에 출력
				log.error("NLP Server Error Response: " + errorBody);
			})
			.then(Mono.error(new RestClientException("NLP Server communication failure")));
	}

	private Mono<String> parseNLPResult(String responseBody) {
		try {
			NLPResponseDto responseDto = objectMapper.readValue(responseBody, NLPResponseDto.class);
			if (responseDto.getChoices() == null || responseDto.getChoices().isEmpty()) {
				log.error("응답 대답(content) 키가 없습니다.");
				return Mono.error(new FailNLPException());
			}
			String keyword = responseDto.getChoices().get(0).getMessage().getContent();
			if(keyword == null || keyword.isEmpty()){
				log.error("content 키는 존재하지만 값이 비어 있습니다.");
				return Mono.error(new NullNLPException());
			}
			log.debug("NLP Result: " + keyword);
			return Mono.just(keyword);
		} catch (JsonProcessingException e) {
			log.error("Failed to parse NLP server response", e);
			return Mono.error(new FailNLPException());
		}
	}
}

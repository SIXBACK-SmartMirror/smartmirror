package com.sixback.backend.common.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestClientException;
import org.springframework.web.reactive.function.client.ClientResponse;
import org.springframework.web.reactive.function.client.WebClient;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.sixback.backend.common.dto.nlp.ChatMessageDto;
import com.sixback.backend.common.dto.nlp.NLPProductExtractionDto;
import com.sixback.backend.common.dto.nlp.NLPResponseDto;
import com.sixback.backend.common.exception.FailNLPException;
import com.sixback.backend.common.exception.NullNLPException;

import jakarta.annotation.PostConstruct;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

/**
 * NLP 서버와의 통신하여 질문 분석 후 상품명만 추출 요청 서비스.
 * 요청을 보내고 서버로부터 받은 응답을 처리.
 */
@Slf4j
@Service
@RequiredArgsConstructor
public class NLPClientService {
	// NLP 서버와의 HTTP 통신을 위한 WebClient 인스턴스
	private final WebClient nlpWebClient;
	// JSON 직렬화 및 역직렬화를 위한 ObjectMapper
	private final ObjectMapper objectMapper;
	// Chat API의 URI 설정
	@Value("${spring.data.chat.uri}")
	private String CHAT_API_URI;
	// Chat 모델 설정
	@Value("${spring.data.chat.model}")
	private String CHAT_MODEL;
	// Chat 응답의 temperature 설정
	@Value("${spring.data.chat.temperature}")
	private float TEMPERATURE;
	// 최대 완성 토큰 수 설정
	@Value("${spring.data.chat.max.completion.token}")
	private short MAX_COMPLETION_TOKENS;
	// 시스템 메시지 프롬프트 설정
	@Value("${spring.data.chat.prompt}")
	private String PROMPT;
	// 시스템 메시지 객체
	private ChatMessageDto systemMessage;

	/**
	 * 서비스 초기화 메서드.
	 * 시스템 메시지를 초기화.
	 */
	@PostConstruct
	private void init() {
		this.systemMessage = new ChatMessageDto("system", PROMPT);
	}

	/**
	 * 에러 응답 본문을 콘솔에 출력하는 정적 메서드.
	 *
	 * @param errorBody 에러 응답 본문.
	 */
	private static void errorLog(String errorBody) {
		// 에러 응답 본문을 콘솔에 출력
		log.error("NLP Server Error Response: {}", errorBody);
	}

	/**
	 * NLP 서버에 요청을 보내는 메서드.
	 *
	 * @param sttResult 음성 인식 결과.
	 * @return NLP 서버 응답으로부터 얻은 문자열.
	 */
	public Mono<String> sendRequest(String sttResult) {
		// 요청 본문을 생성
		NLPProductExtractionDto body = creatBody(sttResult);
		log.debug("body = {}", body);
		return nlpWebClient.post()
			.uri(CHAT_API_URI) // NLP API의 URI
			.bodyValue(body) // 요청 본문 설정
			.retrieve() // 요청 실행 및 응답 수신
			.onStatus(status -> !status.is2xxSuccessful(), // 상태 코드가 2xx가 아닐 경우
				this::handleErrorResponse) // 에러 처리 메서드 호출
			.bodyToMono(String.class) // 응답 본문을 문자열로 변환
			.flatMap(this::parseNLPResult); // NLP 결과 파싱
	}

	/**
	 * NLP 요청 본문을 생성하는 메서드.
	 *
	 * @param sttResult 음성 인식 키워드.
	 * @return 생성된 NLP 요청 본문.
	 */
	public NLPProductExtractionDto creatBody(String sttResult) {
		// 사용자 메시지 생성
		ChatMessageDto userMessage = new ChatMessageDto("user", sttResult);
		return NLPProductExtractionDto.builder()
			.model(CHAT_MODEL) // 설정된 모델 추가
			.messages(List.of(systemMessage, userMessage)) // 시스템 메시지와 사용자 메시지 추가
			.temperature(TEMPERATURE) // temperature 설정
			.max_completion_tokens(MAX_COMPLETION_TOKENS) // 최대 완성 토큰 수 설정
			.build(); // 생성된 본문 반환
	}

	/**
	 * 서버 응답 중 에러가 발생한 경우 처리하는 메서드.
	 *
	 * @param clientResponse 클라이언트 응답.
	 * @return 에러를 나타내는 Mono.
	 * @throws RestClientException NLP 서버와의 통신 오류.
	 */
	private Mono<Throwable> handleErrorResponse(ClientResponse clientResponse) {
		// 에러 응답 본문을 콘솔에 출력
		return clientResponse.bodyToMono(String.class)
			.doOnNext(NLPClientService::errorLog) // 에러 로그 출력
			.then(Mono.error(new RestClientException("NLP Server communication failure"))); // 예외 발생
	}

	/**
	 * NLP 결과를 파싱하는 메서드.
	 *
	 * @param responseBody 응답 본문.
	 * @return NLP 결과 상품명,
	 * @throws FailNLPException NLP 응답 파싱 실패,
	 */
	private Mono<String> parseNLPResult(String responseBody) {
		try {
			NLPResponseDto responseDto = objectMapper.readValue(responseBody, NLPResponseDto.class); // JSON 파싱
			// 응답 대답(content) 키 체크
			if (responseDto.getChoices() == null || responseDto.getChoices().isEmpty()) {
				errorLog("응답 대답(content) 키가 없습니다.");
				return Mono.error(new FailNLPException());
			}
			// 키워드 추출
			String keyword = responseDto.getChoices().get(0).getMessage().getContent();
			log.debug("NLP Result: {}", keyword);
			// 키워드가 비어있을 경우 체크
			if (keyword == null || keyword.isBlank()) {
				errorLog("content 키는 존재하지만 값이 비어 있습니다.");
				return Mono.error(new NullNLPException());
			}
			return Mono.just(keyword);
		} catch (JsonProcessingException e) {
			errorLog(e.getMessage());
			return Mono.error(new FailNLPException());
		}
	}
}

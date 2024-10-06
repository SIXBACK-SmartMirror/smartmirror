package com.sixback.backend.common.service;

import java.io.IOException;

import org.springframework.core.io.ByteArrayResource;
import org.springframework.stereotype.Service;
import org.springframework.util.LinkedMultiValueMap;
import org.springframework.util.MultiValueMap;
import org.springframework.web.multipart.MultipartFile;
import org.springframework.web.reactive.function.client.ClientResponse;
import org.springframework.web.reactive.function.client.WebClient;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.sixback.backend.common.exception.FailGanException;
import com.sixback.backend.domain.dto.CustomMakeupReqDto;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

@Slf4j
@Service
@RequiredArgsConstructor
public class FacerClientService {

	private final WebClient facerWebClient;
	private final ObjectMapper objectMapper;

	private static void errorLog(String errorBody) {
		// 에러 응답 본문을 콘솔에 출력
		log.error("Facer Server Error Response: {}", errorBody);
	}

	public Mono<String> sendRequest(CustomMakeupReqDto customMakeupReqDto) {
		// Multipart 요청을 위한 Body 생성
		MultiValueMap<String, Object> body = createBody(customMakeupReqDto);
		return facerWebClient.post()
			.uri("/ai/custom")
			.bodyValue(body)
			.retrieve()
			.onStatus(status -> !status.is2xxSuccessful(),
				this::handleErrorResponse)
			.bodyToMono(String.class)
			.flatMap(this::parseMakeupImage);
	}

	public MultiValueMap<String, Object> createBody(CustomMakeupReqDto customMakeupReqDto) {
		MultiValueMap<String, Object> body = new LinkedMultiValueMap<>();
		// 파일 데이터가 포함된 ByteArrayResource 생성
		ByteArrayResource resource = multipartFileToByteArray(customMakeupReqDto.getInputImage());
		// 요청 본문 구성
		body.add("inputImage", resource); // 요청 본문에 파일 추가
		body.add("eyebrowColor", customMakeupReqDto.getEyebrowColor());
		body.add("skinColor", customMakeupReqDto.getSkinColor());
		body.add("lipColor", customMakeupReqDto.getLipColor());
		body.add("lipMode", customMakeupReqDto.getLipMode());
		log.debug("body = {}", body);
		return body;
	}

	public ByteArrayResource multipartFileToByteArray(MultipartFile image) {
		try {
			return new ByteArrayResource(image.getBytes()) {
				@Override
				public String getFilename() {
					// 파일 이름 제공
					return image.getOriginalFilename();
				}
			};
		} catch (IOException ex) {
			throw new RuntimeException(ex);
		}
	}

	private Mono<Throwable> handleErrorResponse(ClientResponse clientResponse) {
		return clientResponse.bodyToMono(String.class)
			.doOnNext(FacerClientService::errorLog)
			.then(Mono.error(new FailGanException()));
	}

	private Mono<String> parseMakeupImage(String responseBody) {
		try {
			log.debug(responseBody);
			JsonNode root = objectMapper.readTree(responseBody);
			String makeupImage = root.path("data").path("makeupImage").asText();
			return Mono.just(makeupImage);
		} catch (JsonProcessingException e) {
			errorLog(e.getMessage());
			return Mono.error(new FailGanException());
		}
	}
}
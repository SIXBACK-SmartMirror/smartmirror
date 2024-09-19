package com.sixback.backend.common.service;

import org.springframework.stereotype.Service;
import org.springframework.web.reactive.function.client.WebClient;

import com.sixback.backend.common.dto.gan.GanRequestDto;
import com.sixback.backend.common.exception.FailGanException;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

@Slf4j
@Service
@RequiredArgsConstructor
public class GanClientService {

	private final WebClient ganWebClient;

	public Mono<byte[]> sendRequest(GanRequestDto ganRequestDto) {
		return ganWebClient.post()
			.uri("/ai/makeup") // GAN 서버의 엔드포인트로 수정
			.bodyValue(ganRequestDto)
			.retrieve()
			.onStatus(status -> !status.is2xxSuccessful(), clientResponse ->
				clientResponse.bodyToMono(String.class).doOnNext(errorBody -> {
					// 에러 응답 본문을 콘솔에 출력
					log.error("GAN Server Error Response: " + errorBody);
				}).then(Mono.error(new FailGanException()))
			)
			.bodyToMono(byte[].class); // 실제 응답 타입에 맞게 수정
	}
}

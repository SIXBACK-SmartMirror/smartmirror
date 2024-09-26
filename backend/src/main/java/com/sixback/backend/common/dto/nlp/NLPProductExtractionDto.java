package com.sixback.backend.common.dto.nlp;

import java.util.List;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class NLPProductExtractionDto {
	// 사용할 모델의 이름
	private String model;
	// 전달할 메시지 목록(ChatMessageDto 객체로 표현)
	private List<ChatMessageDto> messages;
	// 생성되는 응답의 다양성을 조절하는 온도 값(기본값은 0.8)
	private float temperature = 0.8f;
	// 생성할 수 있는 최대 토큰 수(기본값은 35)
	private short maxCompletionTokens = 120;

	/**
	 * 객체를 JSON 문자열로 변환하여 반환.
	 *
	 * @return 변환된 JSON 형태의 문자열
	 */
	@Override
	public String toString() {
		return "{\"model\":\"%s\", \"messages\":%s, \"temperature\":%f, \"max_completion_tokens\":%d}"
			.formatted(model, messages, temperature, maxCompletionTokens);
	}
}

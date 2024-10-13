package com.sixback.backend.common.dto.nlp;

import java.util.List;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * NLP 모델 요청 후 응답 DTO.
 */
@Getter
@NoArgsConstructor
@AllArgsConstructor
public class NLPResponseDto {
	// 고유 식별자
	private String id;
	// 객체의 종류를 나타내는 문자열
	private String object;
	// 생성 시간 (타임스탬프)
	private long created;
	// 사용된 모델의 이름
	private String model;
	// 시스템의 고유 지문
	private String systemFingerprint;
	// 설정한 요청 응답 목록
	private List<Choice> choices;
	// 사용 통계 정보
	private Usage usage;

	/**
	 * 	요청 응답 정보를 담는 내부 클래스.
	 */
	@Getter
	@NoArgsConstructor
	@AllArgsConstructor
	public static class Choice {
		// 선택의 인덱스
		private int index;
		// 채팅 메시지 DTO
		private ChatMessageDto message;
		// 로그 확률 (옵션)
		private Object logprobs;
		// 응답 종료 사유
		private String finishReason;
	}

	/**
	 * 사용 통계를 담는 내부 클래스.
	 */
	@Getter
	@NoArgsConstructor
	@AllArgsConstructor
	public static class Usage {
		// 프롬프트 토큰 수
		private int promptTokens;
		// 응답 생성에 사용된 토큰 수
		private int completionTokens;
		// 총 사용된 토큰 수
		private int totalTokens;
		// 응답 생성 토큰의 세부 정보
		private CompletionTokensDetails completionTokensDetails;

		/**
		 * 응답 생성 토큰 세부 정보를 담는 내부 클래스.
		 */
		@Getter
		@NoArgsConstructor
		@AllArgsConstructor
		public static class CompletionTokensDetails {
			// 추론에 사용된 토큰 수
			private int reasoningTokens;
		}
	}
}


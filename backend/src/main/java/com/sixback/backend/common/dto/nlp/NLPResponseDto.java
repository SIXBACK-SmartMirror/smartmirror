package com.sixback.backend.common.dto.nlp;

import java.util.List;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@NoArgsConstructor
@AllArgsConstructor
public class NLPResponseDto {
	private String id;
	private String object;
	private long created;
	private String model;
	private String systemFingerprint;

	private List<Choice> choices;
	private Usage usage;

	@Getter
	@NoArgsConstructor
	@AllArgsConstructor
	public static class Choice {
		private int index;
		private ChatMessageDto message;
		private Object logprobs;
		private String finishReason;
	}

	@Getter
	@NoArgsConstructor
	@AllArgsConstructor
	public static class Usage {
		private int promptTokens;
		private int completionTokens;
		private int totalTokens;
		private CompletionTokensDetails completionTokensDetails;

		@Getter
		@NoArgsConstructor
		@AllArgsConstructor
		public static class CompletionTokensDetails {
			private int reasoningTokens;
		}
	}
}

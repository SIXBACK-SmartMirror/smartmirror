package com.sixback.backend.common.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.reactive.function.client.ExchangeStrategies;
import org.springframework.web.reactive.function.client.WebClient;

@Configuration
public class WebClientConfig {

	// Base configuration for WebClient
	@Bean
	public WebClient.Builder webClientBuilder() {
		return WebClient.builder()
			.exchangeStrategies(ExchangeStrategies.builder()
				.codecs(configurer -> configurer.defaultCodecs()
					.maxInMemorySize(-1)) //unlimited
				.build());
	}

	// WebClient for GAN server
	@Bean
	public WebClient ganWebClient(WebClient.Builder webClientBuilder) {
		return webClientBuilder
			.baseUrl("https://localhost:8000")
			.defaultHeader("Content-Type", "multipart/form-data")  // For sending image files
			.build();
	}

	// WebClient for STT server
	@Bean
	public WebClient sttWebClient(WebClient.Builder webClientBuilder) {
		return webClientBuilder
			.baseUrl("https://stt-server-url.com")
			.defaultHeader("Content-Type", "multipart/form-data")  // For sending audio files
			.build();
	}

	// WebClient for NLP server
	@Bean
	public WebClient nlpWebClient(WebClient.Builder webClientBuilder) {
		return webClientBuilder
			.baseUrl("https://nlp-server-url.com")
			.defaultHeader("Content-Type", "application/json")  // Assuming NLP server expects JSON data
			.build();
	}
}

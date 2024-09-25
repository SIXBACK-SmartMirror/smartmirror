package com.sixback.backend.common.config;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.web.reactive.function.client.ExchangeStrategies;
import org.springframework.web.reactive.function.client.WebClient;

@Configuration
public class WebClientConfig {

	@Value("${spring.data.api.key}")
	private String API_KEY;
	@Value("${spring.data.api.url}")
	private String API_URL;
	@Value("${spring.data.gan.url}")
	private String GAN_API_URL;

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
			.baseUrl(GAN_API_URL)
			.build();
	}

	// WebClient for STT server
	@Bean
	public WebClient sttWebClient(WebClient.Builder webClientBuilder) {
		return webClientBuilder
			.baseUrl(API_URL)
			.defaultHeader(HttpHeaders.AUTHORIZATION, "Bearer " + API_KEY)
			.defaultHeader(HttpHeaders.CONTENT_TYPE, MediaType.MULTIPART_FORM_DATA_VALUE)
			.build();
	}

	// WebClient for NLP server
	@Bean
	public WebClient nlpWebClient(WebClient.Builder webClientBuilder) {
		return webClientBuilder
			.baseUrl(API_URL)
			.defaultHeader(HttpHeaders.AUTHORIZATION, "Bearer " + API_KEY)
			.defaultHeader(HttpHeaders.CONTENT_TYPE, MediaType.MULTIPART_FORM_DATA_VALUE)
			.build();
	}
}

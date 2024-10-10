package com.sixback.backend.common.config;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.web.reactive.function.client.ExchangeStrategies;
import org.springframework.web.reactive.function.client.WebClient;

/**
 * WebClient 초기 설정 Config
 */
@Configuration
public class WebClientConfig {

	// 외부 API Key & URL
	@Value("${spring.data.api.key}")
	private String API_KEY;
	@Value("${spring.data.api.url}")
	private String API_URL;
	// Gan 서버 URL
	@Value("${spring.data.gan.url}")
	private String GAN_API_URL;
	// Facer 서버 URL
	@Value("${spring.data.facer.url}")
	private String FACER_API_URL;

	/**
	 * webClientBuilder 공통 설정.
	 *
	 * @return 설정된 WebClient 인스턴스.
	 */
	@Bean
	public WebClient.Builder webClientBuilder() {
		return WebClient.builder()
			.exchangeStrategies(ExchangeStrategies.builder()
				.codecs(configurer -> configurer.defaultCodecs()
					// 메모리 크기 제한을 없앰
					.maxInMemorySize(-1)) //unlimited
				.build());
	}

	/**
	 * Gan 서버 요청 WebClient.
	 *
	 * @param webClientBuilder  WebClient.Builder 인스턴스.
	 * @return 설정된 WebClient 인스턴스.
	 */
	@Bean
	public WebClient ganWebClient(WebClient.Builder webClientBuilder) {
		return webClientBuilder
			.baseUrl(GAN_API_URL)
			.defaultHeader(HttpHeaders.CONTENT_TYPE, MediaType.MULTIPART_FORM_DATA_VALUE)
			.build();
	}

	/**
	 * Facer 서버 요청 WebClient.
	 *
	 * @param webClientBuilder  WebClient.Builder 인스턴스.
	 * @return 설정된 WebClient 인스턴스.
	 */
	@Bean
	public WebClient facerWebClient(WebClient.Builder webClientBuilder) {
		return webClientBuilder
			.baseUrl(FACER_API_URL)
			.defaultHeader(HttpHeaders.CONTENT_TYPE, MediaType.MULTIPART_FORM_DATA_VALUE)
			.build();
	}

	/**
	 * STT 서버 요청 WebClient.
	 *
	 * @param webClientBuilder  WebClient.Builder 인스턴스.
	 * @return 설정된 WebClient 인스턴스.
	 */
	@Bean
	public WebClient sttWebClient(WebClient.Builder webClientBuilder) {
		return webClientBuilder
			.baseUrl(API_URL)
			.defaultHeader(HttpHeaders.AUTHORIZATION, "Bearer " + API_KEY)
			.defaultHeader(HttpHeaders.CONTENT_TYPE, MediaType.MULTIPART_FORM_DATA_VALUE)
			.build();
	}

	/**
	 * NLP 서버 요청 WebClient.
	 *
	 * @param webClientBuilder  WebClient.Builder 인스턴스.
	 * @return 설정된 WebClient 인스턴스.
	 */
	@Bean
	public WebClient nlpWebClient(WebClient.Builder webClientBuilder) {
		return webClientBuilder
			.baseUrl(API_URL)
			.defaultHeader(HttpHeaders.AUTHORIZATION, "Bearer " + API_KEY)
			.defaultHeader(HttpHeaders.CONTENT_TYPE, MediaType.APPLICATION_JSON_VALUE)
			.build();
	}
}

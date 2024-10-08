package com.sixback.backend.common.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.data.web.config.EnableSpringDataWebSupport;
import org.springframework.http.converter.json.Jackson2ObjectMapperBuilder;

import com.fasterxml.jackson.databind.ObjectMapper;

/**
 * ObjectMapper 초기 설정 config
 */
@Configuration
@EnableSpringDataWebSupport(pageSerializationMode = EnableSpringDataWebSupport.PageSerializationMode.VIA_DTO)
public class ObjectMapperConfig {
	/**
	 *
	 * ObjectMapper를생성하는 빈 메서드.
	 *
	 * @param builder Jackson 라이브러리에서 제공하는 빌더 클래스.
	 * @return ObjectMapper XML 매퍼를 생성하지 않도록 설정하여 JSON 전용 ObjectMapper 반환.
	 */
	@Bean
	public ObjectMapper objectMapper(Jackson2ObjectMapperBuilder builder) {
		return builder.createXmlMapper(false).build();
	}
}

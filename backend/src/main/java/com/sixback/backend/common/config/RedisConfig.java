package com.sixback.backend.common.config;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.cache.annotation.EnableCaching;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.data.redis.connection.RedisConnectionFactory;
import org.springframework.data.redis.connection.RedisStandaloneConfiguration;
import org.springframework.data.redis.connection.lettuce.LettuceConnectionFactory;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.data.redis.serializer.Jackson2JsonRedisSerializer;
import org.springframework.data.redis.serializer.StringRedisSerializer;

@Configuration
@EnableCaching
public class RedisConfig {
	@Value("${spring.data.redis.host}")
	private String host;

	@Value("${spring.data.redis.port}")
	private int port;

	@Value("${spring.data.redis.password}")
	private String password;

	/**
	 * Redis 와의 연결을 위한 Connection을 생성
	 *
	 * @return new LettuceConnectionFactory(redisConfig)
	 */
	@Bean
	public RedisConnectionFactory redisConnectionFactory() {
		RedisStandaloneConfiguration redisConfig = new RedisStandaloneConfiguration(host, port);
		redisConfig.setPassword(password);
		return new LettuceConnectionFactory(redisConfig);
	}

	/**
	 * Redis 데이터 처리를 위한 템플릿을 구성
	 * 해당 구성된 RedisTemplate을 통해서 데이터 통신으로 처리되는 대한 직렬화를 수행
	 *
	 * @return redisTemplate
	 */
	@Bean
	public RedisTemplate<String, Object> redisTemplate() {
		RedisTemplate<String, Object> redisTemplate = new RedisTemplate<>();

		// Redis 연결
		redisTemplate.setConnectionFactory(redisConnectionFactory());

		// Key는 String으로 직렬화
		redisTemplate.setKeySerializer(new StringRedisSerializer());
		// Value는 JSON으로 직렬화 (객체 저장)
		redisTemplate.setValueSerializer(new Jackson2JsonRedisSerializer<>(Object.class));

		// Hash Key-Value 형태로 직렬화를 수행
		redisTemplate.setHashKeySerializer(new StringRedisSerializer());
		redisTemplate.setHashValueSerializer(new Jackson2JsonRedisSerializer<>(Object.class));

		// 기본적으로 직렬화를 수행
		redisTemplate.setDefaultSerializer(new StringRedisSerializer());

		return redisTemplate;
	}
}

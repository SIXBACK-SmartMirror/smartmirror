package com.sixback.backend.common.service;

import java.time.Duration;
import java.util.concurrent.TimeUnit;

import org.apache.commons.codec.digest.DigestUtils;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.data.redis.core.ValueOperations;
import org.springframework.stereotype.Service;

import com.fasterxml.jackson.databind.ObjectMapper;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@Service
@RequiredArgsConstructor
@Slf4j
public class RedisService {

	private final RedisTemplate<String, Object> redisTemplate;
	private final ObjectMapper objectMapper;

	@Value("${spring.data.hash.salt}")
	private String SALT;
	private static final int KEY_LENGTH = 32;

	public String generateKey(String prefix, String baseString) {
		String dataToHash = "%s%s".formatted(baseString, SALT);
		String key = "%s:%s".formatted(prefix, DigestUtils.sha256Hex(dataToHash).substring(0, KEY_LENGTH));
		log.debug("Generated key: {}", key);
		return key;
	}

	public <T> T getData(String key, Class<T> clazz) {
		Object value = redisTemplate.opsForValue().get(key);
		// value가 null인 경우를 처리
		if (value == null) {
			log.warn("No value found for key: {}", key);
			return null;
		}
		log.debug("get {} value ok", key);
		// LinkedHashMap을 T 타입으로 변환
		return objectMapper.convertValue(value, clazz);
	}

	public boolean existData(String key) {
		return Boolean.TRUE.equals(redisTemplate.hasKey(key));
	}

	public void setExpire(String key, long duration) {
		redisTemplate.expire(key, duration, TimeUnit.MILLISECONDS);
	}

	public void setDataExpire(String key, Object value, long duration) {
		log.debug("setDataExpire: key = {}", key);
		ValueOperations<String, Object> ops = redisTemplate.opsForValue();
		Duration expireDuration = Duration.ofMillis(duration);
		// 객체를 JSON 문자열로 변환하여 저장
		ops.set(key, value, expireDuration);
	}
}

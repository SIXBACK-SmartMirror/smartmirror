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

/**
 * Redis 데이터 저장소와의 상호작용을 담당하는 서비스 클래스.
 * 키-값 저장소에 데이터를 저장하고 가져오는 기능을 제공.
 */
@Slf4j
@Service
@RequiredArgsConstructor
public class RedisService {
	// RedisTemplate을 사용하여 Redis와의 데이터 작업 수행
	private final RedisTemplate<String, Object> redisTemplate;
	// JSON 직렬화 및 역직렬화를 위한 ObjectMapper
	private final ObjectMapper objectMapper;
	// 해시 키 생성 시 사용할 salt 값
	@Value("${spring.data.hash.salt}")
	private String SALT;
	// 생성할 해시 키의 길이 상수
	private static final int KEY_LENGTH = 32;

	/**
	 * 주어진 prefix와 baseString을 기반으로 Redis 키를 생성하는 메서드.
	 *
	 * @param prefix 키의 접두사.
	 * @param baseString 키 생성에 사용할 문자열.
	 * @return 생성된 Redis 키.
	 */
	public String generateKey(String prefix, String baseString) {
		// 해시할 데이터를 생성
		String dataToHash = "%s%s".formatted(baseString, SALT);
		// 해시를 사용하여 키 생성
		String key = "%s:%s".formatted(prefix, DigestUtils.sha256Hex(dataToHash).substring(0, KEY_LENGTH));
		log.debug("Generated key: {}", key);
		return key;
	}

	/**
	 * 주어진 키에 해당하는 데이터를 Redis에서 가져오는 메서드.
	 *
	 * @param key Redis에서 검색할 키.
	 * @param clazz 반환할 데이터 타입.
	 * @param <T> 반환할 데이터의 타입.
	 * @return 해당 키에 대한 데이터, 없으면 null.
	 */
	public <T> T getData(String key, Class<T> clazz) {
		Object value = redisTemplate.opsForValue().get(key);
		// value가 null인 경우를 처리
		if (value == null) {
			log.debug("No value found for key: {}", key);
			return null; // null 반환
		}
		// 값을 정상적으로 가져왔음을 로그 출력
		log.debug("get {} value ok", key);
		// LinkedHashMap을 T 타입으로 변환하여 반환
		return objectMapper.convertValue(value, clazz);
	}

	/**
	 * 주어진 키가 Redis에 존재하는지 확인하는 메서드.
	 *
	 * @param key 확인할 키.
	 * @return 키의 존재 여부.
	 */
	public boolean existData(String key) {
		return Boolean.TRUE.equals(redisTemplate.hasKey(key));
	}

	/**
	 * 주어진 키의 만료 시간을 설정하는 메서드.
	 *
	 * @param key 만료 시간을 설정할 키.
	 * @param duration 만료 시간 (밀리초 단위).
	 */
	public void setExpire(String key, long duration) {
		redisTemplate.expire(key, duration, TimeUnit.MILLISECONDS);
	}

	/**
	 * 주어진 키에 데이터를 저장하고 만료 시간을 설정하는 메서드.
	 *
	 * @param key 저장할 데이터의 키.
	 * @param value 저장할 데이터.
	 * @param duration 만료 시간 (밀리초 단위).
	 */
	public void setDataExpire(String key, Object value, long duration) {
		// 설정할 키 로그 출력
		log.debug("setDataExpire: key = {}", key);
		ValueOperations<String, Object> ops = redisTemplate.opsForValue();
		Duration expireDuration = Duration.ofMillis(duration);
		// 객체를 JSON 변환하여 저장
		ops.set(key, value, expireDuration); // Redis에 데이터 저장 및 만료 시간 설정
	}
}


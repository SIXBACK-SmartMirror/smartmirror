package com.sixback.backend.common.service;

import java.time.Duration;
import java.util.concurrent.TimeUnit;

import org.apache.commons.codec.digest.DigestUtils;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.data.redis.core.ValueOperations;
import org.springframework.stereotype.Service;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.sixback.backend.domain.dto.QRReqDto;

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

	public String storeQrData(QRReqDto qrReqDto, long redisQrTtlSeconds) {
		String key = generateQrKey(qrReqDto);
		// 키가 존재하면 만료 시간만 갱신
		if (existData(key)) {
			setExpire(key, redisQrTtlSeconds);
		} else {
			setDataExpire(key, qrReqDto, redisQrTtlSeconds);
		}
		log.debug("Redis Set(Update) OR Key");
		return key;
	}

	private String generateQrKey(QRReqDto qrReqDto) {
		log.debug("gernerate Start");
		String baseString = "%d%s%s".formatted(qrReqDto.getMarketId(),
			qrReqDto.getOptionIdListString(),
			qrReqDto.getMakeupImage());
		log.debug("generateBase: {}", baseString);
		String dataToHash = "%s%s".formatted(baseString, urlSalt);
		return DigestUtils.sha256Hex(dataToHash).substring(0, KEY_LENGTH);
	}

	public <T> T getData(String key, Class<T> clazz) {
		Object value = redisTemplate.opsForValue().get(key);
		log.debug("get {} - value : {}", key, value);
		// value가 null인 경우를 처리
		if (value == null) {
			log.warn("No value found for key: {}", key);
			return null;
		}
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
		log.debug("setDataExpire: data = {}", value);
		ValueOperations<String, Object> ops = redisTemplate.opsForValue();
		Duration expireDuration = Duration.ofMillis(duration);
		// 객체를 JSON 문자열로 변환하여 저장
		ops.set(key, value, expireDuration);
	}
}

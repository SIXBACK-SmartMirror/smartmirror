package com.sixback.backend.domain.service;

import com.sixback.backend.domain.entity.User;
import com.sixback.backend.domain.repository.UserRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.data.redis.core.StringRedisTemplate;
import org.springframework.stereotype.Service;

import java.time.Duration;

@Service
@RequiredArgsConstructor
public class RedisService {

    private final StringRedisTemplate stringRedisTemplate;
    private final UserRepository userRepository;

    /**
     * Redis에서 값을 조회합니다.
     *
     * @param key 조회할 key
     * @return 조회된 값
     */
    public String getValue(String key) {
        return stringRedisTemplate.opsForValue().get(key);
    }

    /**
     * Redis에 값을 추가하거나 수정합니다.
     *
     * @param key   저장할 key
     * @param value 저장할 값
     * @return 저장 결과 (0: 실패, 1: 성공)
     */
    public int setValues(String key, String value) {
        return setValues(key, value, null);
    }

    /**
     * Redis에 값을 추가하거나 수정합니다. 만료 시간 설정 가능.
     *
     * @param key      저장할 key
     * @param value    저장할 값
     * @param duration 만료 시간 (null일 경우 무제한)
     * @return 저장 결과 (0: 실패, 1: 성공)
     */
    public int setValues(String key, String value, Duration duration) {
        try {
            if (duration == null) {
                stringRedisTemplate.opsForValue().set(key, value);
            } else {
                stringRedisTemplate.opsForValue().set(key, value, duration);
            }
            User user = new User();
            user.setName(key);
            userRepository.save(user);
            return 1; // 성공
        } catch (Exception e) {
            // 예외 처리
            System.out.println(e.getMessage());
            return 0; // 실패
        }
    }

    /**
     * Redis에서 값을 삭제합니다.
     *
     * @param key 삭제할 key
     * @return 삭제 결과 (0: 실패, 1: 성공)
     */
    public int deleteValue(String key) {
        try {
            Boolean deleted = stringRedisTemplate.delete(key);
            return deleted != null && deleted ? 1 : 0; // 삭제 성공 여부
        } catch (Exception e) {
            // 예외 처리
            return 0; // 실패
        }
    }
}
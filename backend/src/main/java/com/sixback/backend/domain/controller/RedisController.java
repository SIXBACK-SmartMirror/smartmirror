package com.sixback.backend.domain.controller;

import com.sixback.backend.domain.dto.RedisDto;
import com.sixback.backend.domain.service.RedisService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/")
@RequiredArgsConstructor
public class RedisController {

    private final RedisService redisService;

    /**
     * Redis의 값을 조회합니다.
     *
     * @param redisDto
     * @return
     */@GetMapping("/getValue")
    public ResponseEntity<?> getValue(@RequestBody RedisDto redisDto) {
        String result = redisService.getValue(redisDto.getKey());
        return new ResponseEntity<>(result, HttpStatus.OK);
    }

    /**
     * Redis의 값을 추가/수정합니다.
     *
     * @param redisDto
     * @return
     */@PostMapping("/setValue")
    public ResponseEntity<?> setValue(@RequestBody RedisDto redisDto) {
        System.out.println(redisDto.getKey());
        int result = 0;
        if (redisDto.getDuration() == null) {
            result = redisService.setValues(redisDto.getKey(), redisDto.getValue());
        } else {
            result = redisService.setValues(redisDto.getKey(), redisDto.getValue(), redisDto.getDuration());
        }

        return new ResponseEntity<>(result, HttpStatus.OK);
    }

    /**
     * Redis의 key 값을 기반으로 row를 제거합니다.
     *
     * @param redisDto
     * @return
     */@PostMapping("/deleteValue")
    public ResponseEntity<?> deleteRow(@RequestBody RedisDto redisDto) {
        int result = redisService.deleteValue(redisDto.getKey());
        return new ResponseEntity<>(result, HttpStatus.OK);
    }

    /**
     * Redis의 key 값에 만료 시간을 설정합니다.
     *
     * @param redisDto RedisDto 객체 (key와 만료 시간을 포함)
     * @return API 응답
     */
    @PostMapping("/setExpiration")
    public ResponseEntity<?> setExpiration(@RequestBody RedisDto redisDto) {
        // 만료 시간을 설정하려면 값도 필요하므로, 해당 값을 포함해 저장합니다.
        int result = redisService.setValues(redisDto.getKey(), redisDto.getValue(), redisDto.getDuration());
        return new ResponseEntity<>(result, HttpStatus.OK);
    }
}
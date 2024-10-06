package com.sixback.backend.domain.entity;

import lombok.Builder;
import lombok.Data;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

import java.time.LocalDateTime;
import java.util.Map;

@Data
@Builder
@Document(collection = "makeup_custom_logs")  // MongoDB의 makeup_custom_logs 컬렉션에 저장
public class MakeupCustomLog {

    @Id
    private String logId;  // MongoDB에서 자동 생성될 로그 ID
    private String event;  // "custom_synthesis" 또는 "qr_generation"
    private Long marketId; // 매장 ID
    private String eyebrowColor;  // 눈썹 색상
    private String skinColor;  // 피부 색상
    private String lipColor;  // 입술 색상
    private String lipMode;  // "full" 또는 "gradient"
    private LocalDateTime timestamp;  // 이벤트 발생 시간 (자동 생성)
    private Map<String, Object> additionalInfo;  // 추가 정보 (필요시 저장)
}

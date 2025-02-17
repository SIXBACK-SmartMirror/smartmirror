package com.sixback.backend.domain.entity;

import java.time.LocalDateTime;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

import lombok.Builder;
import lombok.Data;

@Data
@Builder
@Document(collection = "makeup_style_logs")  // MongoDB의 makeup_style_logs 컬렉션에 저장
public class MakeupStyleLog {

	@Id
	private String logId;  // MongoDB에서 자동 생성될 로그 ID
	private String event;  // "style_makeup" 또는 "qr_generation"
	private Long styleId;  // 화장 스타일 식별자
	private Long marketId; // 매장 ID
	private LocalDateTime timestamp;  // 이벤트 발생 시간 (자동 생성)
}

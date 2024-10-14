package com.sixback.backend.domain.entity;

import java.time.LocalDateTime;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

import lombok.Builder;
import lombok.Data;

@Data
@Builder
@Document(collection = "product_logs")  // MongoDB의 product_logs 컬렉션에 저장
public class ProductLog {

	@Id
	private String logId;  // MongoDB에서 자동 생성될 로그 ID
	private String event;  // "stock_in" 또는 "stock_out"
	private Long optionId;  // option 식별자 (goods_option과 연결)
	private Integer quantity;  // 입출고된 수량
	private Long marketId;  // 매장 ID
	private LocalDateTime timestamp;  // 이벤트 발생 시간 (자동 생성)
	private String description;  // 입출고에 대한 설명 (예: "신규 입고", "오프라인 판매 출고")
}


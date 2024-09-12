package com.sixback.backend.domain.entity;

import java.time.LocalDateTime;

import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.CreationTimestamp;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.FetchType;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.Table;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 상품별 옵션 Entity
 */
@Entity
@Table(name = "goods_option")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
public class GoodsOption {
	// 옵션 식별 번호
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(columnDefinition = "int unsigned", nullable = false)
	private Long optionId;

	// 상품 식별번호
	//외래키 조건 제거를 고려했으나, 데이터 정합성을 고려하여 사용하기로 함
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = "goods_id", nullable = false)
	private Goods goods;

	// 상품 옵션명
	@Column(columnDefinition = "varchar(100)", nullable = false)
	private String optionName;

	// 옵션 이미지 URL
	@Column(columnDefinition = "varchar(255)")
	private String optionImage;

	// 옵션 가격(정가)
	@Column(columnDefinition = "int unsigned", nullable = false)
	private Long optionPrice;

	// 할인율
	@Column(columnDefinition = "float", nullable = false)
	@ColumnDefault("0.0")
	private Float optionDiscount;

	// 출시 일시
	@Column(columnDefinition = "datetime", nullable = false)
	@CreationTimestamp
	private LocalDateTime releaseAt;
}

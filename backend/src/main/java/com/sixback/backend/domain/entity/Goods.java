package com.sixback.backend.domain.entity;

import java.time.LocalDateTime;

import org.hibernate.annotations.CreationTimestamp;
import org.hibernate.annotations.DynamicInsert;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.FetchType;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.Table;
import jakarta.validation.constraints.Size;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 상품 Entity
 */
@Entity
@Table(name = "goods")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
@Builder
@DynamicInsert
public class Goods {
	// 상품 식별번호
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(columnDefinition = "int unsigned", nullable = false)
	private Long goodsId;

	// 상품 타입 식별번호
	//외래키 조건 제거를 고려했으나, 데이터 정합성을 고려하여 사용하기로 함
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = "type_id", nullable = false)
	private GoodsType type;

	// 브랜드 식별번호
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = "brand_id", nullable = false)
	private Brand brand;

	// 상품 이미지 url
	@Column(columnDefinition = "varchar(255)")
	@Size(max = 255)
	private String goodsImage;

	// 상품 이름
	@Column(columnDefinition = "varchar(100)", nullable = false)
	@Size(min = 1, max = 100)
	private String goodsName;

	// 상품 가격
	@Column(columnDefinition = "int unsigned", nullable = false)
	private Long goodsPrice;

	// 거래 여부
	@Column(columnDefinition = "tinyint(1) default 1", nullable = false)
	@Builder.Default
	private boolean isPossible = true;

	// 출시 일시
	@Column(columnDefinition = "datetime default current_timestamp", nullable = false)
	@CreationTimestamp
	private LocalDateTime releaseAt;

	// 최대 할인률
	@Column(columnDefinition = "float default 0", nullable = false)
	@Builder.Default
	private float maxDiscount = 0;
}

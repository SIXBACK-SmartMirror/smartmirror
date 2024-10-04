package com.sixback.backend.domain.entity;

import java.time.LocalDateTime;

import org.hibernate.annotations.JdbcTypeCode;
import org.hibernate.type.SqlTypes;

import com.sixback.backend.domain.dto.LocationDto;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.FetchType;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.PrePersist;
import jakarta.persistence.PreUpdate;
import jakarta.persistence.Table;
import jakarta.validation.constraints.Min;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 매장 상품 재고 Entity
 */
@Entity
@Table(name = "stock")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
@Builder
public class Stock {
	// 옵션 상품 식별번호
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(columnDefinition = "int unsigned", nullable = false)
	private Long stockId;

	// 매장 식별번호
	// 외래키 조건 제거를 고려했으나, 데이터 정합성을 고려하여 사용하기로 함
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = "market_id", nullable = false)
	private Market market;

	// 옵션 식별번호
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = "option_id", nullable = false)
	private GoodsOption option;

	@Builder.Default
	@Column(columnDefinition = "int unsigned", nullable = false)
	@Min(0L)
	private Long count = 0L;

	// 판매여부
	@Builder.Default
	@Column(columnDefinition = "tinyint(1) default 1", nullable = false)
	private boolean isPossible = true;

	// 옵션 상품 위치
	@Column(columnDefinition = "json", nullable = false)
	@JdbcTypeCode(SqlTypes.JSON)
	private LocationDto location;

	@Column(columnDefinition = "datetime default current_timestamp", nullable = false)
	private LocalDateTime lastChangeRelease_at;

	// location 초기화(Default Value) 메서드
	@PrePersist
	public void onCreate() {
		if (this.location == null) {
			this.location = new LocationDto("A", (short)0, (short)0);
		}
		lastChangeRelease_at = LocalDateTime.now();
	}

	@PreUpdate
	protected void onUpdate() {
		lastChangeRelease_at = LocalDateTime.now(); // 엔티티가 수정될 때 현재 시간으로 설정
	}
}

package com.sixback.backend.domain.entity;

import org.hibernate.annotations.DynamicInsert;
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
import jakarta.persistence.Table;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 매장별 매대 위치 정보 Entity.
 */
@Entity
@Table(name = "area")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
@DynamicInsert
public class Area {
	// 구역 식별 번호
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(columnDefinition = "int unsigned", nullable = false)
	private Long areaId;

	// 매장 식별번호
	// 외래키 조건 제거를 고려했으나, 데이터 정합성을 고려하여 사용하기로 함
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = "market_id", nullable = false)
	private Market market;

	// 구역 위치 json {"name": , "row" : , " col ": }
	@Column(columnDefinition = "json")
	@JdbcTypeCode(SqlTypes.JSON)
	private LocationDto location;
}

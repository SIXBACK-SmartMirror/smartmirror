package com.sixback.backend.domain.entity;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 매장 Entity
 */
@Entity
@Table(name = "market")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
public class Market {
	// 매장 식별번호
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(columnDefinition = "int unsigned", nullable = false)
	private Long marketId;

	// 매장명
	@Column(columnDefinition = "varchar(30)", nullable = false)
	private String marketName;

	// 매장 주소
	@Column(columnDefinition = "varchar(100)", nullable = false)
	private String address;

	// 매장 도면 URL
	@Column(columnDefinition = "varchar(255)", nullable = false)
	private String blueprintImage;
}

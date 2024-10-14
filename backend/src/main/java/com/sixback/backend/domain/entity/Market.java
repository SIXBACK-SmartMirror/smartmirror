package com.sixback.backend.domain.entity;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import jakarta.validation.constraints.Size;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import lombok.Builder;
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
@Builder
public class Market {
	// 매장 식별번호
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(columnDefinition = "int unsigned", nullable = false)
	private Long marketId;

	// 매장명
	@Column(columnDefinition = "varchar(30)", nullable = false)
	@Size(min = 1, max = 30)
	private String marketName;

	// 매장 주소
	@Column(columnDefinition = "varchar(100)", nullable = false)
	@Size(min = 1, max = 100)
	private String address;

	// 매장 도면 URL
	@Column(columnDefinition = "varchar(255)", nullable = false)
	@Size(min = 1, max = 255)
	private String blueprintImage;

	// 거래 여부
	@Column(columnDefinition = "tinyint(1) default 0", nullable = false)
	@Builder.Default
	private boolean isClosed = false;

}

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
import lombok.Getter;
import lombok.NoArgsConstructor;

/**
 * 상품 브랜드 Entity
 */
@Entity
@Table(name = "brand")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
public class Brand {
	// 브랜드 식별번호
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(columnDefinition = "int unsigned", nullable = false)
	private Long brandId;

	// 한글 브랜드명
	@Column(columnDefinition = "varchar(60)", nullable = false)
	@Size(min = 1, max = 60)
	private String brandNameKr;

	// 영어 브랜드명
	@Column(columnDefinition = "varchar(60)", nullable = false)
	@Size(min = 1, max = 60)
	private String brandNameEng;
}

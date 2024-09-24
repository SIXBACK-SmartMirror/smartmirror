package com.sixback.backend.domain.entity;

import java.util.List;

import org.hibernate.annotations.JdbcTypeCode;
import org.hibernate.type.SqlTypes;

import com.sixback.backend.domain.dto.UseGoodsOptionDto;

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
 * 화장 스타일 정보 Entity
 */
@Entity
@Table(name = "style")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
public class Style {
	// 화장 스타일 식별번호
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(columnDefinition = "int unsigned", nullable = false)
	private Long styleId;

	// 화장 스타일 이미지 URL
	@Column(columnDefinition = "varchar(255)", nullable = false)
	@Size(min = 1, max = 255)
	private String styleImage;

	// 화장 스타일 이름
	@Column(columnDefinition = "varchar(30)", nullable = false)
	@Size(min = 1, max = 30)
	private String styleName;

	// 사용된 옵션 상품들
	@Column(columnDefinition = "json", nullable = false)
	@JdbcTypeCode(SqlTypes.JSON)
	private List<UseGoodsOptionDto> goodsOptionList;
}

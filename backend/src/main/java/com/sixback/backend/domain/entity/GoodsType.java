package com.sixback.backend.domain.entity;

import java.util.ArrayList;
import java.util.List;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.FetchType;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.OneToMany;
import jakarta.persistence.Table;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.ToString;

/**
 * 상품 타입 Entity
 */
@Entity
@Table(name = "goods_type")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
@ToString(exclude = "children")  //children 필드때문에 순환참조 문제 생김
public class GoodsType {
	// 상품 타입 식별번호
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(columnDefinition = "tinyint", nullable = false)
	private Byte typeId;

	// 상품 타입 이름
	@Column(columnDefinition = "varchar(30)", nullable = false)
	private String typeName;

	// 상위 상품 타입 식별번호 (자기참조)
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = "parent_id")
	private GoodsType parent;

	@OneToMany(mappedBy = "parent")
	private List<GoodsType> children = new ArrayList<>();
}

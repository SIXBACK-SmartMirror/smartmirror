package com.sixback.backend.domain.entity;

import java.io.Serializable;
import java.util.Objects;

/**
 * 기획 상품 옵션 테이블 복합키
 */
public class GroupOptionId implements Serializable {
	// 상품(기획) 식별번호
	private Goods goods;
	// 기획에 포함된 상품 식별번호
	private GoodsOption option;

	@Override
	public boolean equals(Object o) {
		if (this == o)
			return true;
		if (o == null || getClass() != o.getClass())
			return false;
		GroupOptionId that = (GroupOptionId)o;
		return Objects.equals(goods, that.goods) && Objects.equals(option, that.option);
	}

	@Override
	public int hashCode() {
		return Objects.hash(goods, option);
	}

}

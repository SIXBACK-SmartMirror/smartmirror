package com.sixback.backend.domain.entity;

import java.io.Serializable;
import java.util.Objects;

public class GroupOptionId implements Serializable {
    private Goods goods;
    private GoodsOption option;

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        GroupOptionId that = (GroupOptionId) o;
        return Objects.equals(goods, that.goods) && Objects.equals(option, that.option);
    }

    @Override
    public int hashCode() {
        return Objects.hash(goods, option);
    }

}

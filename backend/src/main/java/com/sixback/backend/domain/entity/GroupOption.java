package com.sixback.backend.domain.entity;

import jakarta.persistence.*;
import lombok.*;

@Entity
@Table(name="group_option")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
@IdClass(GroupOptionId.class)
public class GroupOption {
    @Id
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="goods_id", nullable = false)
    private Goods goods;
    @Id
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="option_id", nullable = false)
    private GoodsOption option;
}

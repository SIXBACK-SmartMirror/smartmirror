package com.sixback.backend.domain.entity;

import jakarta.persistence.*;
import lombok.*;
import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.CreationTimestamp;
import org.hibernate.annotations.DynamicInsert;

import java.time.LocalDateTime;

@Entity
@Table(name="goods")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
@DynamicInsert
public class Goods {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(columnDefinition = "int unsigned", nullable = false)
    private Long goodsId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="type_id", nullable = false)
    private GoodsType type;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="brand_id", nullable = false)
    private Brand brand;

    @Column(columnDefinition = "varchar(255)")
    private String goodsImage;

    @Column(columnDefinition = "varchar(100)", nullable = false)
    private String goodsName;

    @Column(columnDefinition = "int unsigned", nullable = false)
    private Long goodsPrice;

    @Column(columnDefinition = "tinyint(1) default 1", nullable = false)
    @ColumnDefault("true")
    private boolean isPassible;

    @Column(columnDefinition = "datetime", nullable = false)
    @CreationTimestamp
    private LocalDateTime releaseAt;

    @Column(columnDefinition = "float", nullable = false)
    private Float maxDiscount;
}

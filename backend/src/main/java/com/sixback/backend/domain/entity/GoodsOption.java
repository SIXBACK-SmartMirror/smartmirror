package com.sixback.backend.domain.entity;

import jakarta.persistence.*;
import lombok.*;
import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.CreationTimestamp;

import java.time.LocalDateTime;

//외래키 조건 제거를 고려했으나, 데이터 정합성을 고려하여 사용하기로 함
@Entity
@Table(name="goods_option")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
public class GoodsOption {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(columnDefinition = "int unsigned", nullable = false)
    private Long optionId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="goods_id", nullable = false)
    private Goods goods;

    @Column(columnDefinition = "varchar(100)", nullable = false)
    private String optionName;

    @Column(columnDefinition = "varchar(255)")
    private String optionImage;

    @Column(columnDefinition = "int unsigned", nullable = false)
    private Long optionPrice;

    @Column(columnDefinition = "float", nullable = false)
    @ColumnDefault("0.0")
    private Float optionDiscount;

    @Column(columnDefinition = "datetime", nullable = false)
    @CreationTimestamp
    private LocalDateTime releaseAt;
}

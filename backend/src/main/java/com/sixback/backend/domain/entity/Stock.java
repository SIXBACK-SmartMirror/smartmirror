package com.sixback.backend.domain.entity;

import com.sixback.backend.domain.dto.LocationDto;
import jakarta.persistence.*;
import lombok.*;
import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.JdbcTypeCode;
import org.hibernate.type.SqlTypes;

//외래키 조건 제거를 고려했으나, 데이터 정합성을 고려하여 사용하기로 함
@Entity
@Table(name="stock")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
public class Stock {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(columnDefinition = "int unsigned", nullable = false)
    private Long stockId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="market_id", nullable = false)
    private Market market;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="option_id", nullable = false)
    private GoodsOption option;

    @Column(columnDefinition = "tinyint(1) default 0", nullable = false)
    @ColumnDefault("false")
    private boolean isSelling;

    @Column(columnDefinition = "json", nullable = false)
    @JdbcTypeCode(SqlTypes.JSON)
    private LocationDto location;

    @PrePersist
    public void onCreate() {
        if (this.location == null) {
            this.location = new LocationDto("A", (short) 0, (short) 0);
        }
    }
}

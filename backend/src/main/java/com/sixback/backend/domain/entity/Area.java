package com.sixback.backend.domain.entity;

import com.sixback.backend.domain.dto.LocationDto;
import jakarta.persistence.*;
import lombok.*;
import org.hibernate.annotations.DynamicInsert;
import org.hibernate.annotations.JdbcTypeCode;
import org.hibernate.type.SqlTypes;

//외래키 조건 제거를 고려했으나, 데이터 정합성을 고려하여 사용하기로 함
@Entity
@Table(name="area")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
@DynamicInsert
public class Area {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(columnDefinition = "int unsigned", nullable = false)
    private Long areaId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="market_id", nullable = false)
    private Market market;

    @Column(columnDefinition = "json")
    @JdbcTypeCode(SqlTypes.JSON)
    private LocationDto location;
}

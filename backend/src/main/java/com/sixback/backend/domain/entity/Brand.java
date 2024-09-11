package com.sixback.backend.domain.entity;

import jakarta.persistence.*;
import lombok.*;

@Entity
@Table(name="brand")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
public class Brand {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(columnDefinition = "int unsigned", nullable = false)
    private Long brandId;

    @Column(columnDefinition = "varchar(60)", nullable = false)
    private String brandNameKr;

    @Column(columnDefinition = "varchar(60)", nullable = false)
    private String brandNameEng;
}

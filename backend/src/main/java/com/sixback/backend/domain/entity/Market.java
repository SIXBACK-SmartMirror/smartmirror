package com.sixback.backend.domain.entity;

import jakarta.persistence.*;
import lombok.*;

@Entity
@Table(name="market")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
public class Market {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(columnDefinition = "int unsigned", nullable = false)
    private Long marketId;
    @Column(columnDefinition = "varchar(30)", nullable = false)
    private String marketName;
    @Column(columnDefinition = "varchar(100)", nullable = false)
    private String address;
    @Column(columnDefinition = "varchar(255)", nullable = false)
    private String blueprintImage;
}

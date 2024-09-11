package com.sixback.backend.domain.entity;

import jakarta.persistence.*;
import lombok.*;

import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name="goods_type")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
@ToString(exclude = "children")  //children 필드때문에 순환참조 문제 생김
public class GoodsType {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(columnDefinition = "tinyint", nullable = false)
    private Byte typeId;

    @Column(columnDefinition = "varchar(30)", nullable = false)
    private String typeName;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="parent_id")
    private GoodsType parent;

    @OneToMany(mappedBy = "parent")
    private List<GoodsType> children = new ArrayList<>();
}

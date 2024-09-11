package com.sixback.backend.domain.entity;

import com.sixback.backend.domain.dto.UseGoodsOptionDto;
import jakarta.persistence.*;
import lombok.*;
import org.hibernate.annotations.JdbcTypeCode;
import org.hibernate.type.SqlTypes;

import java.util.List;

@Entity
@Table(name="style")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@AllArgsConstructor
@Getter
public class Style {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(columnDefinition = "int unsigned", nullable = false)
    private Long styleId;
    @Column(columnDefinition = "varchar(255)", nullable = false)
    private String styleImage;
    @Column(columnDefinition = "varchar(30)", nullable = false)
    private String styleName;
    @Column(columnDefinition = "json", nullable = false)
    @JdbcTypeCode(SqlTypes.JSON)
    private List<UseGoodsOptionDto> goodsOptionList;
}

package com.sixback.backend.domain.dto;

import lombok.*;

@Getter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class LocationDto {
    private String name;
    private short row;
    private short col;

    @Override
    public String toString() {
        return String.format("""
                {
                "name":"%s",
                "row":%d,
                "col":%d,
                }
                """, name, row, col);
    }
}

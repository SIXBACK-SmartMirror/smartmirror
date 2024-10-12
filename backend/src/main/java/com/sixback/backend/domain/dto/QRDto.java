package com.sixback.backend.domain.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

/**
 * QR 코드 정보를 담는 DTO.
 */
@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class QRDto {
	// QR 코드 이미지
	String qrImage;
}

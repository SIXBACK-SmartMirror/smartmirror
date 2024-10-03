package com.sixback.backend.common.service;

import java.nio.charset.StandardCharsets;
import java.util.Date;
import java.util.List;

import javax.crypto.SecretKey;
import javax.crypto.spec.SecretKeySpec;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import com.sixback.backend.domain.dto.QRReqDto;

import io.jsonwebtoken.Claims;
import io.jsonwebtoken.Jwts;

@Service
public class JwtService {
	private final SecretKey secretKey;

	public JwtService(@Value("${spring.jwt.secret}")String secret) {
		secretKey = new SecretKeySpec(secret.getBytes(StandardCharsets.UTF_8), Jwts.SIG.HS256.key().build().getAlgorithm());
	}

	public String generateToken(QRReqDto request) {
		long expiryTimeMillis = 3600000L; // 1시간

		return Jwts.builder()
			.claim("marketId", request.getMarketId())
			.claim("productIds", request.getProductIds())
			.claim("image", request.getResultImage())
			.issuedAt(new Date(System.currentTimeMillis()))
			.expiration(new Date(System.currentTimeMillis() + expiryTimeMillis))
			.signWith(secretKey)
			.compact();
	}

	public QRReqDto validateAndDecodeToken(String token)  {
			Claims claims = Jwts.parser()
				.verifyWith(secretKey).build()
				.parseSignedClaims(token)
				.getPayload();

			return QRReqDto.builder()
				.marketId(claims.get("marketId", Integer.class))
				.productIds(claims.get("productIds", List.class))
				.resultImage(claims.get("image", String.class))
				.build();
	}
}

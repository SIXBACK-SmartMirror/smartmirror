package com.sixback.backend.common.service;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.util.Arrays;
import java.util.Base64;
import java.util.Date;
import java.util.List;
import java.util.stream.Collectors;
import java.util.zip.GZIPInputStream;
import java.util.zip.GZIPOutputStream;

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

	public JwtService(@Value("${spring.jwt.secret}") String secret) {
		secretKey = new SecretKeySpec(secret.getBytes(StandardCharsets.UTF_8),
			Jwts.SIG.HS256.key().build().getAlgorithm());
	}

	public String generateQrToken(QRReqDto request) {
		long expiryTimeMillis = 3600000L; // 1시간
		// 1. ID 리스트를 간단한 문자열로 변환
		String optionIdListStr = convertIdsToString(request.getOptionIdList());

		// 2. 이미지 데이터와 함께 압축
		String compressedData = compressData(optionIdListStr, request.getMakeupImage());

		return Jwts.builder()
			.claim("m", request.getMarketId())
			.claim("d", compressedData)
			.issuedAt(new Date(System.currentTimeMillis()))
			.expiration(new Date(System.currentTimeMillis() + expiryTimeMillis))
			.signWith(secretKey)
			.compact();
	}

	public QRReqDto validateAndDecodeQrToken(String token) {
		Claims claims = Jwts.parser()
			.verifyWith(secretKey).build()
			.parseSignedClaims(token)
			.getPayload();

		Long marketId = claims.get("m", Long.class);
		String compressedData = claims.get("d", String.class);
		// 압축 해제 및 데이터 파싱
		String[] decompressedData = decompressData(compressedData);
		List<Long> optionIdList = convertStringToIds(decompressedData[0]);
		String makeupImage = decompressedData[1];

		return QRReqDto.builder()
			.marketId(marketId)
			.optionIdList(optionIdList)
			.makeupImage(makeupImage)
			.build();
	}

	private String convertIdsToString(List<Long> optionIdList) {
		return optionIdList.stream()
			.map(String::valueOf)
			.collect(Collectors.joining(","));
	}

	private List<Long> convertStringToIds(String idsStr) {
		return Arrays.stream(idsStr.split(","))
			.map(Long::parseLong)
			.collect(Collectors.toList());
	}

	private String compressData(String optionIdListStr, String imageData) {
		// optionIdList와 이미지 데이터를 구분자로 합침
		String combined = optionIdListStr + "|" + imageData;

		// GZIP 압축
		ByteArrayOutputStream bos = new ByteArrayOutputStream();
		try (GZIPOutputStream gzipOS = new GZIPOutputStream(bos)) {
			gzipOS.write(combined.getBytes(StandardCharsets.UTF_8));
		} catch (IOException e) {
			throw new RuntimeException(e);
		}

		// Base64로 인코딩하여 URL 안전하게 만듦
		return Base64.getUrlEncoder().encodeToString(bos.toByteArray());
	}

	private String[] decompressData(String compressedData) {
		// Base64 디코딩
		byte[] compressed = Base64.getUrlDecoder().decode(compressedData);
		// GZIP 압축 해제
		ByteArrayOutputStream bos = new ByteArrayOutputStream();
		try (ByteArrayInputStream bis = new ByteArrayInputStream(compressed);
			 GZIPInputStream gzipIS = new GZIPInputStream(bis)) {
			byte[] buffer = new byte[1024];
			int len;
			while ((len = gzipIS.read(buffer)) > 0) {
				bos.write(buffer, 0, len);
			}
			// 구분자로 나누어 반환
			String decompressed = bos.toString(StandardCharsets.UTF_8);
			return decompressed.split("\\|");
		} catch (IOException e) {
			throw new RuntimeException(e);
		}
	}
}

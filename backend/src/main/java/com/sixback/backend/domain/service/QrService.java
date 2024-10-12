package com.sixback.backend.domain.service;

import java.awt.image.BufferedImage;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.util.Base64;
import java.util.List;

import javax.imageio.ImageIO;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.google.zxing.BarcodeFormat;
import com.google.zxing.WriterException;
import com.google.zxing.client.j2se.MatrixToImageWriter;
import com.google.zxing.common.BitMatrix;
import com.google.zxing.qrcode.QRCodeWriter;
import com.sixback.backend.common.exception.ExpiredQRException;
import com.sixback.backend.common.exception.FailDecodeBase64Exception;
import com.sixback.backend.common.exception.FailGenerateQrException;
import com.sixback.backend.common.exception.MismatchMarketId;
import com.sixback.backend.common.service.FileService;
import com.sixback.backend.common.service.RedisService;
import com.sixback.backend.domain.dto.QrDto;
import com.sixback.backend.domain.dto.QrReqDto;
import com.sixback.backend.domain.dto.ResultPageDto;
import com.sixback.backend.domain.dto.UseOptionDetailDto;
import com.sixback.backend.domain.entity.Market;
import com.sixback.backend.domain.repository.GoodsOptionRepository;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

/**
 * QR 코드 생성 및 관리 서비스.
 */
@Service
@RequiredArgsConstructor
@Slf4j
@Transactional
public class QrService {
	// 매장 관련 서비스
	private final MarketService marketService;
	// 파일 관련 서비스
	private final FileService fileService;
	// Redis 관련 서비스
	private final RedisService redisService;
	// 상품 조회 리포지토리
	private final GoodsOptionRepository goodsOptionRepository;
	// Redis QR 코드 TTL
	@Value("${spring.data.qr.redis.ttl.seconds}")
	private long redisQrTtlSeconds;
	// QR 코드 기본 URL
	@Value("${spring.data.qr.base.url}")
	private String qrBaseUrl;
	// QR 코드 키 접두사
	private final String QR_PREFIX = "QR";

	/**
	 * QR 코드 생성을 위한 메서드.
	 *
	 * @param marketId 마켓 ID
	 * @param qrReqDto QR 요청 데이터
	 * @return QrDto 생성된 QR 코드 이미지 DTO
	 * @throws FailDecodeBase64Exception Base64 이미지 검증 실패 시
	 */
	public QrDto generateQRCode(Long marketId, QrReqDto qrReqDto) {
		// base64 검증
		if (!fileService.isValidBase64Image(qrReqDto.getMakeupImage())) {
			throw new FailDecodeBase64Exception();
		}
		// 마켓 ID 검증
		marketService.validateMarket(marketId);
		qrReqDto.setMarketId(marketId);
		// 요청 정보를 담긴 URL 생성
		String qrUrl = generateQrUrl(qrReqDto);
		log.debug("qrUrl: {}", qrUrl);
		// QR 코드 생성 및 Base64 인코딩 반환
		return QrDto.builder()
			.qrImage(generateQRImageBytes(qrUrl))
			.build();
	}

	/**
	 * QR 코드 URL 생성을 위한 메서드.
	 *
	 * @param qrReqDto QR 요청 데이터
	 * @return 생성된 QR 코드 URL 문자열
	 */
	private String generateQrUrl(QrReqDto qrReqDto) {
		String token = storeQrData(qrReqDto, redisQrTtlSeconds);
		return "%s/%d/result?user=%s".formatted(qrBaseUrl, qrReqDto.getMarketId(), token);
	}

	/**
	 * QR 코드 이미지를 생성하여 Base64로 변환하는 메서드.
	 *
	 * @param content QR 코드에 담을 내용
	 * @return Base64로 인코딩된 QR 코드 이미지
	 * @throws FailGenerateQrException QR 코드 생성 실패 시
	 */
	private String generateQRImageBytes(String content) {
		// QR 코드 크기 설정
		int size = 300;
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		try {
			QRCodeWriter qrCodeWriter = new QRCodeWriter();
			// BitMatrix 생성 (버전을 자동으로 결정)
			BitMatrix bitMatrix = qrCodeWriter.encode(content, BarcodeFormat.QR_CODE, size, size);
			BufferedImage qrImage = MatrixToImageWriter.toBufferedImage(bitMatrix);
			// BufferedImage를 PNG 형식으로 바이트 배열 출력 스트림에 작성
			ImageIO.write(qrImage, "PNG", baos);
			return Base64.getEncoder().encodeToString(baos.toByteArray());
		} catch (WriterException e) {
			log.error("generateQR error WriterException : {}", e.getMessage());
			throw new FailGenerateQrException();
		} catch (IOException e) {
			log.error("generateQR error IOException : {}", e.getMessage());
			throw new FailGenerateQrException();
		}
	}

	/**
	 * QR 코드에서 옵션 정보를 가져오는 메서드.
	 *
	 * @param marketId 마켓 ID
	 * @param token QR 코드 토큰
	 * @return QR 접속 페이지에 전할 DTO
	 * @throws ExpiredQRException QR 코드가 만료된 경우
	 * @throws MismatchMarketId 매장 ID 불일치 시
	 */
	public ResultPageDto getOptionInfoList(Long marketId, String token) {
		// 마켓 ID 검증
		Market market = marketService.validateMarket(marketId);
		// 토큰 검증 및 디코딩
		QrReqDto qrReqDto = getTokenInfo(marketId, token);
		// QR에 담긴 정보기반 사용 상품 목록 조회
		List<UseOptionDetailDto> goodsList = goodsOptionRepository.findAllByMarketIdAndUseOptionIdIn(marketId,
			qrReqDto.getOptionIdList());
		return ResultPageDto.builder()
			.marketName(market.getMarketName())
			.goodsList(goodsList)
			.makeupImage(qrReqDto.getMakeupImage())
			.blueprintImage(market.getBlueprintImage())
			.build();
	}

	/**
	 * 토큰 정보를 가져오는 메서드.
	 *
	 * @param marketId 마켓 ID
	 * @param token QR 코드 토큰
	 * @return QR 요청 DTO
	 * @throws ExpiredQRException QR 코드가 만료된 경우
	 * @throws MismatchMarketId 매장 ID 불일치 시
	 */
	private QrReqDto getTokenInfo(Long marketId, String token) {
		QrReqDto qrReqDto = redisService.getData(token, QrReqDto.class);
		// 해당 키-값 없을 경우
		if (qrReqDto == null) {
			throw new ExpiredQRException();
		}
		// 요청 매장 id와 QR에 담긴 매장 ID가 다를 경우
		if (qrReqDto.getMarketId() == null || !marketId.equals(qrReqDto.getMarketId())) {
			throw new MismatchMarketId();
		}
		return qrReqDto;
	}

	/**
	 * QR 요청 데이터를 Redis에 저장하는 메서드.
	 *
	 * @param qrReqDto QR 요청 데이터
	 * @param redisQrTtlSeconds Redis 저장 TTL
	 * @return 생성된 키
	 */
	public String storeQrData(QrReqDto qrReqDto, long redisQrTtlSeconds) {
		String baseString = "%d%s%s".formatted(qrReqDto.getMarketId(),
			qrReqDto.getOptionIdListString(),
			qrReqDto.getMakeupImage());
		String key = redisService.generateKey(QR_PREFIX, baseString);

		// 키가 존재하면 만료 시간만 갱신
		if (redisService.existData(key)) {
			redisService.setExpire(key, redisQrTtlSeconds);
		} else {
			redisService.setDataExpire(key, qrReqDto, redisQrTtlSeconds);
		}
		log.debug("Redis Set(Update) OR Key");
		return key;
	}
}

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
import com.sixback.backend.common.service.RedisService;
import com.sixback.backend.domain.dto.QRDto;
import com.sixback.backend.domain.dto.QRReqDto;
import com.sixback.backend.domain.dto.ResultPageDTO;
import com.sixback.backend.domain.dto.UseOptionDetailDto;
import com.sixback.backend.domain.entity.Market;
import com.sixback.backend.domain.repository.GoodsOptionRepository;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@Service
@RequiredArgsConstructor
@Slf4j
@Transactional
public class QrService {

	private final MarketService marketService;
	private final RedisService redisService;
	private final GoodsOptionRepository goodsOptionRepository;

	@Value("${spring.data.qr.redis.ttl.seconds}")
	private long redisQrTtlSeconds;
	@Value("${spring.data.qr.base.url}")
	private String qrBaseUrl;

	private final String QR_PREFIX = "QR";

	public QRDto generateQRCode(Long marketId, QRReqDto qrReqDto) {
		// base64 검증
		if (!isValidBase64Image(qrReqDto.getMakeupImage())) {
			throw new FailDecodeBase64Exception();
		}
		// 마켓 id 검증
		marketService.validateMarket(marketId);
		qrReqDto.setMarketId(marketId);

		// 요청 정보를 담긴 URL 생성
		String qrUrl = generateQrUrl(qrReqDto);
		log.info("qrUrl: {}", qrUrl);

		// QR 코드 생성 로직은 동일하나, Base64 인코딩 반환
		return QRDto.builder()
			.qrImage(generateQRImageBytes(qrUrl))
			.build();
	}

	private String generateQrUrl(QRReqDto qrReqDto) {
		String token = storeQrData(qrReqDto, redisQrTtlSeconds);
		return "%s/%d/result?user=%s".formatted(qrBaseUrl, qrReqDto.getMarketId(), token);
	}

	private String generateQRImageBytes(String content) {
		int size = 300;
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		try {
			QRCodeWriter qrCodeWriter = new QRCodeWriter();
			// BitMatrix 생성 (버전을 자동으로 결정)
			BitMatrix bitMatrix = qrCodeWriter.encode(content, BarcodeFormat.QR_CODE, size, size);
			BufferedImage qrImage = MatrixToImageWriter.toBufferedImage(bitMatrix);
			ImageIO.write(qrImage, "PNG", baos);
		} catch (WriterException e) {
			log.error("generateQR error WriterException : {}", e.getMessage());
			throw new FailGenerateQrException();
		} catch (IOException e) {
			log.error("generateQR error IOException : {}", e.getMessage());
			throw new FailGenerateQrException();
		}
		return Base64.getEncoder().encodeToString(baos.toByteArray());
	}

	public ResultPageDTO getOptionInfoList(Long marketId, String token) {
		// 마켓 id 검증
		Market market = marketService.validateMarket(marketId);
		// 토큰 검증 및 디코딩
		QRReqDto qrReqDto = getTokenInfo(marketId, token);

		List<UseOptionDetailDto> goodsList = goodsOptionRepository.findAllUseOptionId(marketId,
			qrReqDto.getOptionIdList());

		return ResultPageDTO.builder()
			.marketName(market.getMarketName())
			.goodsList(goodsList)
			.makeupImage(qrReqDto.getMakeupImage())
			.blueprintImage(market.getBlueprintImage())
			.build();
	}

	private QRReqDto getTokenInfo(Long marketId, String token) {
		QRReqDto qrReqDto = redisService.getData(token, QRReqDto.class);
		if (qrReqDto == null) {
			throw new ExpiredQRException();
		}
		if (qrReqDto.getMarketId() == null || !marketId.equals(qrReqDto.getMarketId())) {
			throw new MismatchMarketId();
		}
		return qrReqDto;
	}

	private boolean isValidBase64Image(String base64String) {
		log.debug("base64(partial) = {}", base64String.substring(0, 10));
		try {
			Base64.getDecoder().decode(base64String);
			return true;
		} catch (IllegalArgumentException e) {
			return false;
		}
	}

	public String storeQrData(QRReqDto qrReqDto, long redisQrTtlSeconds) {
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

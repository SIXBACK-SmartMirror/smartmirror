package com.sixback.backend.domain.service;

import java.awt.image.BufferedImage;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.util.Base64;
import java.util.List;

import javax.imageio.ImageIO;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import com.google.zxing.BarcodeFormat;
import com.google.zxing.WriterException;
import com.google.zxing.client.j2se.MatrixToImageWriter;
import com.google.zxing.common.BitMatrix;
import com.google.zxing.qrcode.QRCodeWriter;
import com.sixback.backend.common.service.JwtService;
import com.sixback.backend.domain.dto.OptionInfoDto;
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
public class QrService {
	private final JwtService jwtService;
	private final MarketService marketService;
	private final GoodsOptionRepository goodsOptionRepository;
	@Value("${spring.data.qr.base.url}")
	private String qrBaseUrl;

	public QRDto generateQRCode(Long marketId, QRReqDto qrReqDto) {
		// 마켓 id 검증
		marketService.validateMarket(marketId);
		isValidBase64Image(qrReqDto.getResultImage());
		qrReqDto.setMarketId(marketId);
		// JWT 토큰 생성
		String token = jwtService.generateToken(qrReqDto);
		// QR 코드 생성
		String qrUrl = qrBaseUrl + "/result?user=" + token;
		// QR 코드 생성 로직은 동일하나, Base64 인코딩 대신 byte[] 반환
		return QRDto.builder()
			.qrImageBytes(generateQRImageBytes(qrUrl))
			.build();
	}

	private byte[] generateQRImageBytes(String content) {
		int size = 300;
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		try {
			QRCodeWriter qrCodeWriter = new QRCodeWriter();
			BitMatrix bitMatrix = qrCodeWriter.encode(content, BarcodeFormat.QR_CODE, size, size);
			BufferedImage qrImage = MatrixToImageWriter.toBufferedImage(bitMatrix);
			ImageIO.write(qrImage, "PNG", baos);
		} catch (WriterException | IOException e) {
			log.error(e.getMessage());
		}
		return baos.toByteArray();
	}

	public ResultPageDTO getProductInfo(Long marketId, String token) {
		// 마켓 id 검증
		Market market = marketService.validateMarket(marketId);
		// 토큰 검증 및 디코딩
		QRReqDto qrData = jwtService.validateAndDecodeToken(token);
		// if(marketId != qrData.getMarketId()) new
		List<UseOptionDetailDto> goodsList = goodsOptionRepository.findAllUseOptionId(marketId, qrData.getOptionIdList());
		return ResultPageDTO.builder()
			.marketName(market.getMarketName())
			.goodsList(goodsList)
			.resultImage(qrData.getResultImage())
			.blueprintImage(market.getBlueprintImage())
			.build();
	}

	private boolean isValidBase64Image(String base64String) {
		try {
			Base64.getDecoder().decode(base64String);
			return true;
		} catch (IllegalArgumentException e) {
			return false;
		}
	}
}

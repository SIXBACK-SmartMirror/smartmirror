package com.sixback.backend.domain.controller;

import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

import com.sixback.backend.common.dto.ResponseDto;
import com.sixback.backend.domain.dto.QrDto;
import com.sixback.backend.domain.dto.QrReqDto;
import com.sixback.backend.domain.dto.ResultPageDto;
import com.sixback.backend.domain.service.QrService;

import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

/**
 * QR 코드 생성 및 결과 페이지 관리를 담당하는 컨트롤러.
 */
@Slf4j
@Controller
@RequiredArgsConstructor
@RequestMapping("/market/{marketId}/result")
public class QrController {
	private final QrService qrService;

	/**
	 * QR 코드를 생성하는 메서드.
	 *
	 * @param marketId QR 코드를 요청한 마켓의 ID.
	 * @param request QR 코드 생성 정보(마켓ID, 사용한 상품 목록, 결과 이미지) DTO.
	 * @return 생성된 QR 코드 정보를 담은 ResponseDto와 상태 코드.
	 */
	@PostMapping(value = "/generate-qr", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public ResponseEntity<?> generateQR(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute QrReqDto request) {
		QrDto qrDto = qrService.generateQRCode(marketId, request);
		return new ResponseEntity<>(new ResponseDto<>("A00", qrDto), HttpStatus.OK);
	}

	/**
	 * QR 페이지를 조회하는 메서드.
	 *
	 * @param marketId QR 페이지에 관련된 마켓의 ID.
	 * @param token 사용자 인증을 위한 토큰.
	 * @param model 뷰에 전달할 모델 객체.
	 * @return 결과 페이지.
	 */
	@GetMapping
	public String getResultPage(@PathVariable("marketId") Long marketId, @RequestParam("user") String token,
		Model model) {
		try {
			// QR 코드와 관련된 정보를 가져옴
			ResultPageDto result = qrService.getOptionInfoList(marketId, token);
			model.addAttribute("result", result);
			return "result-page";
		} catch (Exception e) {
			// 오류 발생 시 로깅 후 에러 페이지로 리디렉션
			log.error("getResultPage error : {}", e.getMessage());
			return "redirect:/error";
		}
	}

	/**
	 * 에러 페이지를 조회하는 메서드.
	 *
	 * @return 에러 페이지.
	 */
	@GetMapping("/error")
	public String errorPage() {
		return "error";
	}
}

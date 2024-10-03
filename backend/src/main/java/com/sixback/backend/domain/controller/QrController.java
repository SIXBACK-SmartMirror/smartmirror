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
import com.sixback.backend.domain.dto.QRDto;
import com.sixback.backend.domain.dto.QRReqDto;
import com.sixback.backend.domain.dto.ResultPageDTO;
import com.sixback.backend.domain.service.QrService;

import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;

@Controller
@RequiredArgsConstructor
@RequestMapping("/market/{marketId}/result")
public class QrController {

	private final QrService qrService;

	@PostMapping(value = "/generate-qr", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public ResponseEntity<?> generateQR(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute QRReqDto request) {
		QRDto qrDto = qrService.generateQRCode(marketId, request);
		return new ResponseEntity<>(new ResponseDto<>("A00", qrDto), HttpStatus.OK);
	}

	@GetMapping
	public String getResultPage(@PathVariable("marketId") Long marketId, @RequestParam("user") String token, Model model) {
		try {
			ResultPageDTO result = qrService.getProductInfo(marketId, token);
			model.addAttribute("result", result);
			return "result-page";
		} catch (Exception e) {
			return "redirect:/error";
		}
	}

	@GetMapping("/error")
	public String errorPage() {
		return "error";
	}
}

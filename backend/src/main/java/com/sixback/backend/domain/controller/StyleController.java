package com.sixback.backend.domain.controller;

import java.util.ArrayList;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.sixback.backend.common.dto.ResponseDto;
import com.sixback.backend.domain.dto.StyleInfoListDto;
import com.sixback.backend.domain.dto.VirtualMakeupReqDto;
import com.sixback.backend.domain.service.StyleService;

import jakarta.validation.Valid;
import jakarta.validation.constraints.Min;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

@RestController
@RequiredArgsConstructor
@RequestMapping("/market/{marketId}/styles")
@Slf4j
public class StyleController {

	private final StyleService styleService;

	// 화장 스타일 목록 조회
	@GetMapping
	public ResponseEntity<?> findAllStyle(@PathVariable("marketId") Long marketId,
		@Min(0) @RequestParam("page") int page,
		@Min(1) @RequestParam("size") int size) {
		StyleInfoListDto styleInfoListDto = new StyleInfoListDto(styleService.findAllStyle(marketId, page, size));
		return new ResponseEntity<>(new ResponseDto<>("A00", styleInfoListDto), HttpStatus.OK);
	}

	// 가상 화장 하기
	@PostMapping(consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public Mono<ResponseEntity<?>> creatVirtualMakeup(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute VirtualMakeupReqDto virtualMakeupReqDto) {
		return styleService.createVirtualMakeup(marketId, virtualMakeupReqDto)
			.map(styleResultDto -> new ResponseEntity<>(new ResponseDto<>("A00", styleResultDto), HttpStatus.OK));
	}

	@PostMapping(value = "/test", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public ResponseEntity<?> testCreatVirtualMakeup(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute VirtualMakeupReqDto virtualMakeupReqDto) {
		String base64Image = """
			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
			""";
		base64Image = base64Image.replaceAll("\\s+", "");
		byte[] imageBytes = Base64.getDecoder().decode(base64Image);
		log.debug(String.format("request : marketId = %d\nbody = %s", marketId, virtualMakeupReqDto));
		// StyleResultDto 생성
		StyleResultDto styleResultDto = StyleResultDto.builder()
			.styleId(virtualMakeupReqDto.getStyleId())
			.makeupImage("https://i.ibb.co/Dg0DFfb/XMY-014.png") // MultipartFile
			.goodsOptionList(new ArrayList<>()) // 빈 리스트 초기화
			.qrImage(imageBytes) // MultipartFile
			.build();
		return new ResponseEntity<>(new ResponseDto<>("A00", styleResultDto), HttpStatus.OK);
	}

	@PostMapping(value = "/testAi", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public Mono<ResponseEntity<?>> testAICreatVirtualMakeup(@PathVariable("marketId") Long marketId,
		@Valid @ModelAttribute VirtualMakeupReqDto virtualMakeupReqDto) {
		return styleService.testAIcreateVirtualMakeup(marketId, virtualMakeupReqDto)
			.map(styleResultDto -> new ResponseEntity<>(new ResponseDto<>("A00", styleResultDto), HttpStatus.OK));
	}

}

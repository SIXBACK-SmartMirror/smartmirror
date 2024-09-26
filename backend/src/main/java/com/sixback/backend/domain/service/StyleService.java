package com.sixback.backend.domain.service;

import java.util.List;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import com.sixback.backend.common.dto.gan.GanRequestDto;
import com.sixback.backend.common.exception.EmptyFileException;
import com.sixback.backend.common.exception.OptionNotFoundException;
import com.sixback.backend.common.exception.StyleNotFoundException;
import com.sixback.backend.common.exception.StyleUseOptionNotFoundException;
import com.sixback.backend.common.service.GanClientService;
import com.sixback.backend.domain.dto.OptionInfoDto;
import com.sixback.backend.domain.dto.StyleInfoDto;
import com.sixback.backend.domain.dto.StyleInfoListDto;
import com.sixback.backend.domain.dto.StyleResultDto;
import com.sixback.backend.domain.dto.UseOptionDetailDto;
import com.sixback.backend.domain.dto.UseOptionLocationListDto;
import com.sixback.backend.domain.dto.VirtualMakeupReqDto;
import com.sixback.backend.domain.entity.Style;
import com.sixback.backend.domain.repository.GoodsOptionRepository;
import com.sixback.backend.domain.repository.StyleRepository;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;

@Service
@RequiredArgsConstructor
@Slf4j
public class StyleService {

	private final MarketService marketService;
	private final GanClientService ganClientService;
	private final StyleRepository styleRepository;
	private final GoodsOptionRepository goodsOptionRepository;

	public StyleInfoListDto findAllStyle(Long marketId, int page, int size) {
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// 화장 스타일 식별번호 순으로 정렬
		Pageable pageable = PageRequest.of(page, size, Sort.by("styleId").ascending());
		Page<StyleInfoDto> styleInfoDtoPage = styleRepository.findAllDto(pageable);
		return new StyleInfoListDto(styleInfoDtoPage);
	}

	public Mono<StyleResultDto> createVirtualMakeup(Long marketId, VirtualMakeupReqDto virtualMakeupReqDto) {
		validateFileSize(virtualMakeupReqDto.getInputImage());
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// 스타일 식별 번호 검증 && 사용된 상품 정보 가져 오기
		Style style = styleRepository.findById(virtualMakeupReqDto.getStyleId())
			.orElseThrow(StyleNotFoundException::new);
		// GAN AI 서버 요청 (비동기)
		return ganClientService.sendRequest(GanRequestDto.builder()
				.inputImage(virtualMakeupReqDto.getInputImage())
				.styleImage(style.getStyleImage())
				.build())
			.flatMap(makeupImage -> createQRImage().map(qrImage -> StyleResultDto.builder()
				.styleId(style.getStyleId())
				.goodsOptionList(style.getGoodsOptionList())
				.makeupImage(makeupImage)
				.qrImage(qrImage)
				.build()));
	}

	public Mono<StyleResultDto> testAIcreateVirtualMakeup(Long marketId, VirtualMakeupReqDto virtualMakeupReqDto) {
		validateFileSize(virtualMakeupReqDto.getInputImage());
		// GAN AI 서버 요청 (비동기)
		return ganClientService.sendRequest(GanRequestDto.builder()
				.inputImage(virtualMakeupReqDto.getInputImage())
				.styleImage("https://i.ibb.co/Dg0DFfb/XMY-014.png")
				.build())
			.flatMap(makeupImage -> createQRImage().map(qrImage -> StyleResultDto.builder()
				.styleId(1L)
				.goodsOptionList(List.of())
				.makeupImage(makeupImage)
				.qrImage(qrImage)
				.build()));
	}

	public StyleResultDto testCreatVirtualMakeup(Long marketId, VirtualMakeupReqDto virtualMakeupReqDto) {
		validateFileSize(virtualMakeupReqDto.getInputImage());
		String base64Image = """
			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
			""";
		base64Image = base64Image.replaceAll("\\s+", "");
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// 스타일 식별 번호 검증 && 사용된 상품 정보 가져 오기
		Style style = styleRepository.findById(virtualMakeupReqDto.getStyleId())
			.orElseThrow(StyleNotFoundException::new);
		log.debug("JPA 완료");
		// GAN AI 서버 요청 (비동기)
		return StyleResultDto.builder()
			.styleId(style.getStyleId())
			.goodsOptionList(style.getGoodsOptionList())
			.makeupImage(base64Image)
			.qrImage(base64Image)
			.build();
	}

	public Mono<String> createQRImage() {
		String base64Image = """
			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
			""";
		base64Image = base64Image.replaceAll("\\s+", "");
		return Mono.just(base64Image);
	}

	public void validateFileSize(MultipartFile file) {
		if (file.getSize() < 0) {
			throw new EmptyFileException();
		}
	}

	public Object findUseGoodsOptionInfo(Long marketId, Long styleId, Long optionId) {
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		if (optionId == null) {
			return findAllUseOptonLocationList(marketId, styleId);
		} else {
			return findByOptionId(marketId, styleId, optionId);
		}
	}

	public UseOptionLocationListDto findAllUseOptonLocationList(Long marketId, Long styleId) {
		// 스타일 식별 번호 검증 & 사용된 상품 위치 정보 가져오기
		List<OptionInfoDto> results = styleRepository.findAllUseOptionLocationList(marketId, styleId);
		return new UseOptionLocationListDto(results);
	}

	public UseOptionDetailDto findByOptionId(Long marketId, Long styleId, Long optionId) {
		// 스타일 식별 번호 & 옵션 ID 검증
		styleRepository.findByStyleIdAndOptionId(styleId, optionId).orElseThrow(StyleUseOptionNotFoundException::new);
		// 사용된 상품 정보 가져 오기
		return goodsOptionRepository.findTopByMarketIdAndOptionId(marketId, optionId)
			.orElseThrow(OptionNotFoundException::new);
	}
}

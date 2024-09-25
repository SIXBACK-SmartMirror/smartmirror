package com.sixback.backend.domain.service;

import java.util.List;
import java.util.stream.Collectors;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;

import com.sixback.backend.common.exception.GoodsNotFoundException;
import com.sixback.backend.common.exception.OptionNotFoundException;
import com.sixback.backend.domain.dto.GoodsDetailDto;
import com.sixback.backend.domain.dto.GoodsDto;
import com.sixback.backend.domain.dto.OptionDto;
import com.sixback.backend.domain.dto.OptionInfoDto;
import com.sixback.backend.domain.dto.SearchResultDto;
import com.sixback.backend.domain.repository.GoodsOptionRepository;

import lombok.RequiredArgsConstructor;

@Service
@RequiredArgsConstructor
public class GoodsService {

	private final MarketService marketService;
	private final GoodsOptionRepository goodsOptionRepository;

	public SearchResultDto findAllGoodsByKeyword(Long marketId, String keyword, int page, int size) {
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// 화장 스타일 식별번호 순으로 정렬
		Pageable pageable = PageRequest.of(page, size, Sort.by("goods.releaseAt").descending());
		Page<GoodsDto> goodsDtoPage = goodsOptionRepository.findAllGoodsByKeyword(keyword, pageable);
		return new SearchResultDto(keyword, goodsDtoPage);
	}

	public GoodsDetailDto findByGoodsId(Long marketId, Long goodsId) {
		// 매장 유효성 검사
		marketService.validateMarket(marketId);
		// goods_id 검사
		goodsOptionRepository.findByValidGoodsId(goodsId).orElseThrow(GoodsNotFoundException::new);
		List<OptionInfoDto> locationList = goodsOptionRepository.findAllOptionByGoodsId(marketId, goodsId);
		if(locationList.isEmpty()) {
			throw new OptionNotFoundException();
		}
		List<OptionDto> optionDtoList = locationList.stream()
			.map(OptionInfoDto -> new OptionDto().builder()
				.optionId(OptionInfoDto.getOptionId())
				.optionName(OptionInfoDto.getOptionName())
				.optionImage(OptionInfoDto.getOptionImage())
				.optionPrice(OptionInfoDto.getOptionPrice())
				.optionDiscountPrice(OptionInfoDto.getOptionDiscountPrice())
				.stock(OptionInfoDto.getStock())
				.isInMarket(OptionInfoDto.getIsInMarket())
				.build())
			.collect(Collectors.toList());
		return new GoodsDetailDto(optionDtoList, locationList);
	}

}
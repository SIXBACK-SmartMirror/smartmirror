package com.sixback.backend.domain.service;

import org.springframework.stereotype.Service;

import com.sixback.backend.common.exception.InvalidMarketException;
import com.sixback.backend.common.exception.MarketNotFoundException;
import com.sixback.backend.domain.entity.Market;
import com.sixback.backend.domain.repository.MarketRepository;

import lombok.RequiredArgsConstructor;

@Service
@RequiredArgsConstructor
public class MarketService {

	private final MarketRepository marketRepository;
	
	public Market validateMarket(Long marketId) {
		Market market = marketRepository.findById(marketId).orElseThrow(MarketNotFoundException::new);
		if (market.isClosed()) {
			throw new InvalidMarketException();
		}
		return market;
	}
}

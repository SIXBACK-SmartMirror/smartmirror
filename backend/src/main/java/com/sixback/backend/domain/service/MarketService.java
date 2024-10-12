package com.sixback.backend.domain.service;

import org.springframework.stereotype.Service;

import com.sixback.backend.common.exception.InvalidMarketException;
import com.sixback.backend.common.exception.MarketNotFoundException;
import com.sixback.backend.domain.entity.Market;
import com.sixback.backend.domain.repository.MarketRepository;

import lombok.RequiredArgsConstructor;

/**
 * 매장 관련 서비스.
 */
@Service
@RequiredArgsConstructor
public class MarketService {
	// 매장 리포지토리
	private final MarketRepository marketRepository;

	/**
	 * 주어진 매장 ID로 매장을 검증하고, 매장이 유효한지 검사 메서드.
	 *
	 * @param marketId 매장 ID.
	 * @return 유효한 매장 객체.
	 * @throws MarketNotFoundException 매장을 찾을 수 없는 경우.
	 * @throws InvalidMarketException 매장이 페업한 경우.
	 */
	public Market validateMarket(Long marketId) {
		// 매장 ID로 매장 조회
		Market market = marketRepository.findById(marketId).orElseThrow(MarketNotFoundException::new);
		// 매장이 폐쇄된 경우 예외 발생
		if (market.isClosed()) {
			throw new InvalidMarketException();
		}
		return market; // 유효한 매장 반환
	}
}

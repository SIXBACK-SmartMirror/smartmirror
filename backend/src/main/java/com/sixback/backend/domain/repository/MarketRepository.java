package com.sixback.backend.domain.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.sixback.backend.domain.entity.Market;
/**
 * 매장 리포지토리.
 */
public interface MarketRepository extends JpaRepository<Market, Long> {
}

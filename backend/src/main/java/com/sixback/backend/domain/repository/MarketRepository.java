package com.sixback.backend.domain.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.sixback.backend.domain.entity.Market;

public interface MarketRepository extends JpaRepository<Market, Long> {
}

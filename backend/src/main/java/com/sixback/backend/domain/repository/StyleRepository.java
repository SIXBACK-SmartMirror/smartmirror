package com.sixback.backend.domain.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.sixback.backend.domain.entity.Style;

public interface StyleRepository extends JpaRepository<Style, Long> {
	// Page<StyleInfoDto> findAllDto(Pageable pageable);
}

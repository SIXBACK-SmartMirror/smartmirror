package com.sixback.backend.domain.repository;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import com.sixback.backend.domain.dto.StyleInfoDto;
import com.sixback.backend.domain.entity.Style;

public interface StyleRepository extends JpaRepository<Style, Long> {
	@Query(value = """
		select new com.sixback.backend.domain.dto.StyleInfoDto
		(s.styleId, s.styleImage, s.styleName)
		from Style s
		""",
		countQuery = "select count(s) from Style s")
	Page<StyleInfoDto> findAllDto(Pageable pageable);
}

package com.sixback.backend.domain.repository;

import java.util.List;
import java.util.Optional;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import com.sixback.backend.domain.dto.OptionInfoDto;
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

	@Query(value = """
		SELECT
		    jt.option_id as option_id,
		    jt.option_name as option_name,
		    jt.goods_name as goods_name,
		    jt.option_image as option_image,
		    CASE WHEN s.stock_id IS NOT NULL THEN true ELSE false END AS is_in_market_raw,
		    s.location as location_raw,
		    COALESCE(s.count, 0) as stock
		FROM style st
		JOIN JSON_TABLE(st.goods_option_list, '$[*]'
		    COLUMNS(
		        option_id INT PATH '$.option_id',
		        goods_name VARCHAR(100) PATH '$.goods_name',
		    	option_name VARCHAR(100) PATH '$.option_name',
		    	option_image VARCHAR(255) PATH '$.option_image'
		    	)
		) AS jt ON st.style_id = :styleId
		LEFT JOIN Stock s ON s.market_id = :marketId AND s.option_id = jt.option_id
		""", nativeQuery = true)
	List<OptionInfoDto> findAllUseOptionInfoList(@Param("marketId") Long marketId, @Param("styleId") Long styleId);

	@Query(value = """
			SELECT *
			FROM Style s
			WHERE s.style_id = :styleId
			AND JSON_CONTAINS(s.goods_option_list, CONCAT('{"option_id":', :optionId, '}')) = TRUE
		""", nativeQuery = true)
	Optional<Style> findByStyleIdAndOptionId(Long styleId, Long optionId);
}

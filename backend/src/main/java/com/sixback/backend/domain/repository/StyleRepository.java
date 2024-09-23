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

	@Query(value = """
		SELECT 
		    jt.option_name as option_name,
		    CASE WHEN s.location IS NOT NULL THEN true ELSE false END AS is_in_market_raw,
		    s.location as location_raw,
		    SUM(CASE WHEN s.is_selling = 0 THEN 1 ELSE 0 END) AS stock
		FROM Style st 
		JOIN JSON_TABLE(st.goods_option_list, '$[*]' 
		    COLUMNS(
		        option_id INT PATH '$.option_id', 
		        option_name VARCHAR(100) PATH '$.option_name')
		) AS jt ON st.style_id = :styleId
		LEFT JOIN Stock s ON s.market_id = :marketId AND s.option_id = jt.option_id 
		GROUP BY jt.option_name, s.location
		""", nativeQuery = true)
	List<OptionInfoDto> findAllUseOptionLocationList(@Param("marketId") Long marketId, @Param("styleId") Long styleId);

}

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

/**
 * 스타일 리포지토리.
 */
public interface StyleRepository extends JpaRepository<Style, Long> {
	/**
	 * 모든 스타일 정보를 DTO로 조회.
	 *
	 * @param pageable 페이지 정보.
	 * @return 스타일 DTO의 페이지.
	 */
	@Query(value = """
		SELECT new com.sixback.backend.domain.dto.StyleInfoDto
		(s.styleId, s.styleImage, s.styleName)
		FROM Style s
		""",
		countQuery = "SELECT count(s) FROM Style s")
	Page<StyleInfoDto> findAllDto(Pageable pageable);

	/**
	 * 특정 스타일 ID와 마켓 ID에 대한 사용된 상품 옵션 정보 조회.
	 *
	 * @param marketId 마켓 식별 번호.
	 * @param styleId 스타일 식별 번호.
	 * @return 사용된 상품 옵션 정보 DTO 리스트.
	 */
	@Query(value = """
		SELECT
		    jt.option_id AS option_id,
		    jt.option_name AS option_name,
		    jt.goods_name AS goods_name,
		    jt.option_image AS option_image,
		    CASE WHEN s.stock_id IS NOT NULL THEN TRUE ELSE FALSE END AS is_in_market_raw,
		    s.location AS location_raw,
		    COALESCE(s.count, 0) AS stock
		FROM style st
		JOIN JSON_TABLE(st.goods_option_list, '$[*]'
		    COLUMNS(
		        option_id INT PATH '$.option_id',
		        goods_name VARCHAR(100) PATH '$.goods_name',
		        option_name VARCHAR(100) PATH '$.option_name',
		        option_image VARCHAR(255) PATH '$.option_image'
		    )
		) AS jt ON st.style_id = :styleId
		LEFT JOIN stock s ON s.market_id = :marketId AND s.option_id = jt.option_id
		""", nativeQuery = true)
	List<OptionInfoDto> findAllUseOptionInfoList(@Param("marketId") Long marketId,
		@Param("styleId") Long styleId);

	/**
	 * 주어진 스타일 ID와 상품 옵션 ID가 모두 해당하는 스타일 조회.
	 * 스타일 ID에 해당 상품 옵션 ID가 포함되었는지 검사.
	 *
	 * @param styleId 스타일 식별 번호.
	 * @param optionId 옵션 식별 번호.
	 * @return 스타일 정보 (주어진 스타일 ID에 해당 상품 옵션 ID가 존재하면).
	 */
	@Query(value = """
		    SELECT *
		    FROM Style s
		    WHERE s.style_id = :styleId
		    AND JSON_CONTAINS(s.goods_option_list, CONCAT('{"option_id":', :optionId, '}')) = TRUE
		""", nativeQuery = true)
	Optional<Style> findByStyleIdAndOptionId(Long styleId, Long optionId);

	/**
	 * 주어진 스타일 ID와 다른 스타일들 조회.
	 *
	 * @param styleId 제외할 스타일 ID.
	 * @return 제외된 스타일 ID와 다른 스타일 목록.
	 */
	@Query(value = """
		    SELECT s
		    FROM Style s
		    WHERE s.styleId != :styleId
		""")
	List<Style> findNotStyleId(Long styleId);
}

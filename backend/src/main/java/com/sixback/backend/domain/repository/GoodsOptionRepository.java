package com.sixback.backend.domain.repository;

import java.util.Optional;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import com.sixback.backend.domain.dto.UseOptionDetailDto;
import com.sixback.backend.domain.entity.GoodsOption;

public interface GoodsOptionRepository extends JpaRepository<GoodsOption, Long> {

	@Query(value = """
		   SELECT
				b.brand_name_kr,
				o.option_price,
				o.option_price * CAST((1 - o.option_discount) AS DECIMAL(10, 2)) AS option_discount_price,
				CASE WHEN s.location IS NOT NULL THEN true ELSE false END AS is_in_market_raw,
				SUM(CASE WHEN s.is_selling = 0 THEN 1 ELSE 0 END) as stock,
				s.location as location_raw
		   FROM goods_option o
		   JOIN Goods g ON g.goods_id = o.goods_id AND o.option_id = :optionId
		   JOIN brand b ON b.brand_id = g.brand_id
		   LEFT JOIN Stock s ON s.market_id = :marketId 
		       AND s.option_id = o.option_id
		   GROUP BY o.option_id, b.brand_name_kr, o.option_price, o.option_discount, s.location
		   ORDER BY stock DESC
		   LIMIT 1
		""", nativeQuery = true)
	Optional<UseOptionDetailDto> findTopByMarketIdAndOptionId(@Param("marketId") Long marketId,
		@Param("optionId") Long optionId);
}

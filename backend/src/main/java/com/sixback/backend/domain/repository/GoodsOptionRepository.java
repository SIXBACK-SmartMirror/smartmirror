package com.sixback.backend.domain.repository;

import java.util.List;
import java.util.Optional;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.EntityGraph;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import com.sixback.backend.domain.dto.GoodsDto;
import com.sixback.backend.domain.dto.OptionInfoDto;
import com.sixback.backend.domain.dto.UseOptionDetailDto;
import com.sixback.backend.domain.entity.Goods;
import com.sixback.backend.domain.entity.GoodsOption;

public interface GoodsOptionRepository extends JpaRepository<GoodsOption, Long> {
	@EntityGraph(attributePaths = {"goods", "goods.brand"})
	@Query("""
		    SELECT new com.sixback.backend.domain.dto.UseOptionDetailDto(
		            b.brandNameKr,
		            g.goodsName,
		            o.optionName,
		            o.optionPrice,
		            CAST(o.optionPrice * (1 - o.optionDiscount) AS long),
		            CASE WHEN s.location IS NOT NULL THEN true ELSE false END,
		           	CAST(COALESCE(s.count, 0) AS int ),
		            s.location
		        )
		    FROM GoodsOption o
		    JOIN o.goods g ON o.optionId = :optionId
		    JOIN g.brand b
		    LEFT JOIN Stock s ON s.market.marketId = :marketId
		        AND s.option.optionId = o.optionId
		    ORDER BY COALESCE(s.count, 0) DESC
		    LIMIT 1
		""")
	Optional<UseOptionDetailDto> findTopByMarketIdAndOptionId(@Param("marketId") Long marketId,
		@Param("optionId") Long optionId);

	@Query(value = """
		SELECT g.goods_id,
			g.goods_image,
			g.goods_name,
			g.goods_price,
			g.goods_price * CAST((1 - g.max_discount) AS DECIMAL(10, 2)) AS goods_discount_price,
			b.brand_name_kr
		FROM goods_option o
			JOIN goods g ON o.goods_id = g.goods_id
			JOIN brand b ON g.brand_id = b.brand_id
			JOIN goods_type t ON g.type_id = t.type_id
		WHERE g.is_possible = true
			AND (MATCH(o.option_name) AGAINST(:keyword IN BOOLEAN MODE)
				OR MATCH(t.type_name) AGAINST(:keyword IN BOOLEAN MODE)
				OR MATCH(g.goods_name) AGAINST(:keyword IN BOOLEAN MODE)
				OR MATCH(b.brand_name_kr, b.brand_name_eng) AGAINST(:keyword IN BOOLEAN MODE))
		""", nativeQuery = true,
		countQuery = """
			select count(distinct g.goods_id)
			FROM goods_option o
				JOIN goods g ON o.goods_id = g.goods_id
				JOIN brand b ON g.brand_id = b.brand_id
				JOIN goods_type t ON g.type_id = t.type_id
			WHERE g.is_possible = true
				AND (MATCH(o.option_name) AGAINST(:keyword IN BOOLEAN MODE)
					OR MATCH(t.type_name) AGAINST(:keyword IN BOOLEAN MODE)
					OR MATCH(g.goods_name) AGAINST(:keyword IN BOOLEAN MODE)
					OR MATCH(b.brand_name_kr, b.brand_name_eng) AGAINST(:keyword IN BOOLEAN MODE))
		""")
	Page<GoodsDto> findAllGoodsByKeyword(@Param("keyword") String keyword, Pageable pageable);

	@EntityGraph(attributePaths = {"goods.brand", "goods.type"})
	@Query("""
			select g
			from Goods g
			where g.goodsId = :goodsId
			and g.isPossible = true
		""")
	Optional<Goods> findByValidGoodsId(Long goodsId);

	@Query(value = """
		SELECT co.option_id,
			o.option_name,
			o.option_image,
			o.option_price,
			o.option_price * CAST((1 - o.option_discount) AS DECIMAL(10, 2)) AS option_discount_price,
			CASE WHEN s.stock_id IS NOT NULL THEN true ELSE false END AS is_in_market_raw,
			COALESCE(s.count, 0) as stock,
			s.location as location_raw
		FROM (SELECT go.goods_id, go.option_id, FALSE AS isGroup
				FROM goods_option go
				WHERE go.goods_id = :goodsId
			UNION ALL
				SELECT gro.goods_id, gro.option_id, TRUE AS isGroup
				FROM group_option gro
				WHERE gro.goods_id = :goodsId) as co
		LEFT JOIN goods_option o ON o.option_id = co.option_id
		LEFT JOIN stock s ON s.option_id = co.option_id AND s.market_id = :marketId AND s.is_possible = 1
		ORDER BY
			(CASE WHEN stock > 0 THEN 1 ELSE 0 END) DESC,
			co.isGroup,
			stock DESC;
	""", nativeQuery = true)
	List<OptionInfoDto> findAllOptionByGoodsId(@Param("marketId") Long marketId, @Param("goodsId") Long goodsId);

	@Query(value = """
				WITH RankedOptions AS (
		                  SELECT o.color_rgb,
		                      o.color_hsv,
		                      o.release_at,
		                      o.option_id,
		                      g.goods_name,
		                      o.option_name,
		                      o.option_image,
		                      CASE WHEN s.stock_id IS NOT NULL THEN TRUE ELSE FALSE END AS is_in_market_raw,
		                      s.location as location_raw,
		                      COALESCE(s.count, 0) AS stock,
		                      pt.type_name AS option_type_name_raw,
		                      ROW_NUMBER() OVER (PARTITION BY pt.type_id ORDER BY o.release_at DESC) AS rn
		                  FROM goods_option o
		                  JOIN goods g ON g.goods_id = o.goods_id
		                  JOIN goods_type AS ct ON g.type_id = ct.type_id
		                  JOIN goods_type AS pt ON ct.parent_id = pt.type_id
		                  LEFT JOIN stock s ON s.option_id = o.option_id
		                      AND s.market_id = 1
		                      AND s.is_possible = 1
		                  WHERE g.is_possible = 1
		                      AND o.color_rgb IS NOT NULL
		                      AND (
		                          pt.type_id IN (5)
		                          OR ct.type_id IN (15, 16)
		                          OR ct.type_id IN (17)
		                      )
		              )
		              SELECT color_rgb as option_color,
		                     option_id,
		                     goods_name,
		                     option_name,
		                     option_image,
		                     is_in_market_raw,
		                     location_raw,
		                     option_type_name_raw,
		                     stock
		              FROM RankedOptions
		              WHERE rn > :offset AND rn <= (:offset + :size)
		              ORDER BY option_type_name_raw, color_hsv, release_at DESC
		""",
          nativeQuery = true
	)
	List<OptionInfoDto> findAllCustomOption(@Param("size") int size, @Param("offset") int offset);
	@EntityGraph(attributePaths = {"goods", "goods.brand"})
	@Query("""
		    SELECT new com.sixback.backend.domain.dto.UseOptionDetailDto(
		            b.brandNameKr,
		            g.goodsName,
		            o.optionName,
		            o.optionPrice,
		            CAST(o.optionPrice * (1 - o.optionDiscount) AS long),
		            CASE WHEN s.stockId IS NOT NULL THEN true ELSE false END,
		           	CAST(coalesce(s.count, 0) AS int),
		            s.location
		        )
		    FROM GoodsOption o
		    JOIN o.goods g ON o.optionId in :optionId
		    JOIN g.brand b
		    LEFT JOIN Stock s ON s.market.marketId = :marketId
		        AND s.option.optionId = o.optionId
		""")
	List<UseOptionDetailDto> findAllUseOptionId(@Param("marketId") Long marketId,
		@Param("optionIdList") List<Long> optionIdList);

}

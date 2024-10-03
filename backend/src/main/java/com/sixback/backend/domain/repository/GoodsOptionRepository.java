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
		           	CAST(SUM(CASE WHEN s.isSelling = false THEN 1 ELSE 0 END) AS int ),
		            s.location
		        )
		    FROM GoodsOption o
		    JOIN o.goods g ON o.optionId = :optionId
		    JOIN g.brand b
		    LEFT JOIN Stock s ON s.market.marketId = :marketId
		        AND s.option.optionId = o.optionId
		    GROUP BY o.optionId, b.brandNameKr, o.optionPrice, o.optionDiscount, s.location
		    ORDER BY SUM(CASE WHEN s.isSelling = false THEN 1 ELSE 0 END) DESC
		    LIMIT 1
		""")
	Optional<UseOptionDetailDto> findTopByMarketIdAndOptionId(@Param("marketId") Long marketId,
		@Param("optionId") Long optionId);

	@EntityGraph(attributePaths = {"goods", "goods.brand", "goods.type"})
	@Query(value = """
		    select new com.sixback.backend.domain.dto.GoodsDto(
		         g.goodsId,
		         g.goodsImage,
		         g.goodsName,
		         g.goodsPrice,
		         CAST(g.goodsPrice * (1 - g.maxDiscount) AS long),
		         b.brandNameKr
		     )
		    from GoodsOption o
		       join o.goods g
		       join g.brand b
		       join g.type t
		    where g.isPossible = true
		       and (o.optionName like %:keyword%
		       OR t.typeName like %:keyword%
		       OR g.goodsName like %:keyword%
		       OR b.brandNameKr like %:keyword%
		       OR b.brandNameEng like %:keyword%)
		    group by g.goodsId, g.releaseAt
		""",
		countQuery = """
			    select count(distinct g.goodsId)
			    from GoodsOption o
			       join o.goods g
			       join g.brand b
			       join g.type t
			    where g.isPossible = true
			       and (o.optionName like %:keyword%
			       OR t.typeName like %:keyword%
			       OR g.goodsName like %:keyword%
			       OR b.brandNameKr like %:keyword%
			       OR b.brandNameEng like %:keyword%)
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
			CASE WHEN MAX(s.market_id) IS NOT NULL THEN TRUE ELSE FALSE END AS is_in_market_raw,
			COUNT(CASE WHEN s.is_selling = 0 THEN 1 END) AS stock,
			s.location as location_raw
		FROM (SELECT go.goods_id, go.option_id, FALSE AS isGroup
				FROM goods_option go
				WHERE go.goods_id = :goodsId
			UNION ALL
				SELECT gro.goods_id, gro.option_id, TRUE AS isGroup
				FROM group_option gro
				WHERE gro.goods_id = :goodsId) as co
		LEFT JOIN goods_option o ON o.option_id = co.option_id
		LEFT JOIN stock s ON s.option_id = co.option_id AND s.market_id = :marketId
		GROUP BY co.option_id,
			co.isGroup,
			o.option_name,
			o.option_image,
			o.option_price,
			o.option_discount,
			s.location
		ORDER BY
			(CASE WHEN stock > 0 THEN 1 ELSE 0 END) DESC,
			co.isGroup,
			stock DESC;
	""", nativeQuery = true)
	List<OptionInfoDto> findAllOptionByGoodsId(@Param("marketId") Long marketId, @Param("goodsId") Long goodsId);

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
		    ORDER BY SUM(CASE WHEN s.isSelling = false THEN 1 ELSE 0 END) DESC
		    LIMIT 1
		""")
	List<UseOptionDetailDto> findAllUseOptionId(@Param("marketId") Long marketId,
		@Param("optionIdList") List<Long> optionIdList);

}

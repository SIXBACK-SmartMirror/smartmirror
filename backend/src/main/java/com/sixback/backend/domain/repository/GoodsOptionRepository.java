package com.sixback.backend.domain.repository;

import java.util.Optional;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.EntityGraph;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import com.sixback.backend.domain.dto.GoodsDto;
import com.sixback.backend.domain.entity.Goods;
import com.sixback.backend.domain.entity.GoodsOption;

public interface GoodsOptionRepository extends JpaRepository<GoodsOption, Long> {

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

	// List<OptionDto> findOptionDtoByGoodsId(Long goodsId);
}

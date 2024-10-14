package com.sixback.backend.domain.repository;

import com.sixback.backend.domain.entity.ProductLog;
import org.springframework.data.mongodb.repository.MongoRepository;

public interface ProductLogRepository extends MongoRepository<ProductLog, String> {
}

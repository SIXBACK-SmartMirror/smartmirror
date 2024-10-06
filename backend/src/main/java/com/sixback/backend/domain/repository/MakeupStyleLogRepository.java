package com.sixback.backend.domain.repository;


import com.sixback.backend.domain.entity.MakeupStyleLog;
import org.springframework.data.mongodb.repository.MongoRepository;

public interface MakeupStyleLogRepository extends MongoRepository<MakeupStyleLog, String> {
}

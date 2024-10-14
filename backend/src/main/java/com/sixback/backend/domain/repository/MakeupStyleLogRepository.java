package com.sixback.backend.domain.repository;

import org.springframework.data.mongodb.repository.MongoRepository;

import com.sixback.backend.domain.entity.MakeupStyleLog;

public interface MakeupStyleLogRepository extends MongoRepository<MakeupStyleLog, String> {
}

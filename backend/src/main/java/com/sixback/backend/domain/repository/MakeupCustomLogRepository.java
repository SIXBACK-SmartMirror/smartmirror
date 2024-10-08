package com.sixback.backend.domain.repository;

import com.sixback.backend.domain.entity.MakeupCustomLog;
import org.springframework.data.mongodb.repository.MongoRepository;

public interface MakeupCustomLogRepository extends MongoRepository<MakeupCustomLog, String> {
}

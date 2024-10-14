package com.sixback.backend.common.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.data.mongodb.MongoDatabaseFactory;
import org.springframework.data.mongodb.core.convert.DbRefResolver;
import org.springframework.data.mongodb.core.convert.DefaultDbRefResolver;
import org.springframework.data.mongodb.core.convert.DefaultMongoTypeMapper;
import org.springframework.data.mongodb.core.convert.MappingMongoConverter;
import org.springframework.data.mongodb.core.mapping.MongoMappingContext;

/**
 * MongoDB 초기 설정 config.
 */
@Configuration
public class MongoDBConfig {
    /**
     * MappingMongoConverter를 생성하는 빈 메서드.
     *
     * @param mongoDatabaseFactory MongoDB 데이터베이스에 연결하기 위한 팩토리.
     * @param mongoMappingContext MongoDB와 Java 객체 간 매핑을 정의하는 컨텍스트.
     * @return 불필요한 메타데이터가 MongoDB에 저장되지 않도록 MappingMongoConverter 반환.
     */
    @Bean
    public MappingMongoConverter mappingMongoConverter(MongoDatabaseFactory mongoDatabaseFactory, MongoMappingContext mongoMappingContext) {
        DbRefResolver dbRefResolver = new DefaultDbRefResolver(mongoDatabaseFactory);
        MappingMongoConverter converter = new MappingMongoConverter(dbRefResolver, mongoMappingContext);
        converter.setTypeMapper(new DefaultMongoTypeMapper(null));
        return converter;
    }
}

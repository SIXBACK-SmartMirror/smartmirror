package com.sixback.backend.domain.service;

import com.sixback.backend.domain.entity.MakeupCustomLog;
import com.sixback.backend.domain.entity.MakeupStyleLog;
import com.sixback.backend.domain.entity.ProductLog;
import com.sixback.backend.domain.repository.MakeupCustomLogRepository;
import com.sixback.backend.domain.repository.MakeupStyleLogRepository;
import com.sixback.backend.domain.repository.ProductLogRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.Map;

@Service
@RequiredArgsConstructor
public class LogService {
    private final MakeupStyleLogRepository makeupStyleLogRepository;
    private final MakeupCustomLogRepository makeupCustomLogRepository;
    private final ProductLogRepository productLogRepository;

    // 화장 스타일 로그 저장
    public void saveMakeupStyleLog(String event, Long styleId, Long marketId, Map<String, Object> additionalInfo) {
        MakeupStyleLog logEntity = MakeupStyleLog.builder()
                .event(event)
                .styleId(styleId)
                .marketId(marketId)
                .timestamp(LocalDateTime.now())  // 현재 시간으로 저장
                .additionalInfo(additionalInfo)
                .build();

        makeupStyleLogRepository.save(logEntity);  // MongoDB에 저장
    }

    // 커스텀 합성 로그 저장
    public void saveMakeupCustomLog(String event, Long marketId, String eyebrowColor, String skinColor, String lipColor, String lipMode, Map<String, Object> additionalInfo) {
        MakeupCustomLog logEntity = MakeupCustomLog.builder()
                .event(event)
                .marketId(marketId)
                .eyebrowColor(eyebrowColor)
                .skinColor(skinColor)
                .lipColor(lipColor)
                .lipMode(lipMode)
                .timestamp(LocalDateTime.now())  // 현재 시간으로 저장
                .additionalInfo(additionalInfo)
                .build();

        makeupCustomLogRepository.save(logEntity);  // MongoDB에 저장
    }

    // 제품 입출고 로그 저장
    public void saveProductLog(String event, Long stockId, int quantity, Long marketId, Map<String, String> location, String description, Map<String, Object> additionalInfo) {
        ProductLog logEntity = ProductLog.builder()
                .event(event)
                .stockId(stockId)
                .quantity(quantity)
                .marketId(marketId)
                .location(location)
                .timestamp(LocalDateTime.now())  // 현재 시간으로 저장
                .description(description)
                .additionalInfo(additionalInfo)
                .build();

        productLogRepository.save(logEntity);  // MongoDB에 저장
    }

}

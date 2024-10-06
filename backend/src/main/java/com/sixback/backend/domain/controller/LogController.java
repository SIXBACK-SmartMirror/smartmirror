package com.sixback.backend.domain.controller;

import com.sixback.backend.domain.service.CustomService;
import com.sixback.backend.domain.service.LogService;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import reactor.core.publisher.Mono;

import java.util.Map;

@RestController
@RequiredArgsConstructor
@RequestMapping("/market/{marketId}/logs")
@Slf4j
public class LogController {
    private final LogService logService;

    // 커스텀 화장 로그 저장 및 처리
    @PostMapping("/custom")
    public void createCustomMakeup(@PathVariable("marketId") Long marketId) {
        // 로그 저장
        logService.saveMakeupCustomLog("custom_synthesis", marketId, "96,89,83", "239,204,172", "151,58,68", "full", Map.of());
        log.info("custom");
    }

    // 스타일 메이크업 로그 저장
    @PostMapping("/style")
    public void saveStyleLog(@PathVariable("marketId") Long marketId) {
        logService.saveMakeupStyleLog("style_synthesis", 123L, marketId, Map.of());
        log.info("Style makeup log saved");
    }

    // 제품 입출고 로그 저장
    @PostMapping("/product")
    public void saveProductLog(@PathVariable("marketId") Long marketId) {
        logService.saveProductLog("stock_in", 1L, 100, marketId, Map.of("name", "A", "row", "0", "col", "0"), "New stock in", Map.of());
        log.info("Product log saved");
    }
}

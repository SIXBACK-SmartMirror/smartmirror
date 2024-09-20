package com.sixback.backend;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class TestController {

    @GetMapping("/api-test")
    public static String getTest() {
        return "OK";
    }

    @GetMapping("/api-test2")
    public static String getTest2() {
        return "OK2";
    }
}

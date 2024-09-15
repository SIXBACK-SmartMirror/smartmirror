package com.sixback.backend;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/test")
public class TestController {

    @GetMapping("/get")
    public static ResponseEntity<?> getTestRes() {
        String message = "Spring API Success";
        return new ResponseEntity<>(message, HttpStatus.OK);
    }
}

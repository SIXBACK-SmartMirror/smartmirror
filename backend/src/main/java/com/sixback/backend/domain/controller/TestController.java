package com.sixback.backend.domain.controller;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class TestController {

	@GetMapping("/api-test")
	public static String getTest() {
		return "OK";
	}
}

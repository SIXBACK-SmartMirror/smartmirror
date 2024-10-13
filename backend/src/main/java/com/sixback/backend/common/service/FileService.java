package com.sixback.backend.common.service;

import java.io.IOException;
import java.util.Base64;

import org.springframework.core.io.ByteArrayResource;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import com.sixback.backend.common.exception.EmptyFileException;
import com.sixback.backend.common.exception.NoSearchKeywordException;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

/**
 * 파일 처리와 관련된 기능을 제공하는 서비스 클래스.
 * 파일 변환, 크기 검증, Base64 이미지 유효성 검증 등의 메서드 포함.
 */
@Slf4j
@Service
@RequiredArgsConstructor
public class FileService {
	/**
	 * MultipartFile을 ByteArrayResource로 변환하는 메서드.
	 *
	 * @param file 변환할 MultipartFile.
	 * @return ByteArrayResource로 변환된 이미지.
	 * @throws RuntimeException(IOException) 파일 IO 중 발생 예외.
	 */
	public ByteArrayResource multipartFileToByteArray(MultipartFile file) {
		try {
			return new ByteArrayResource(file.getBytes()) {
				@Override
				public String getFilename() {
					// 파일 이름 제공
					return file.getOriginalFilename();
				}
			};
		} catch (IOException ex) {
			throw new RuntimeException(ex); // IOException 발생 시 런타임 예외로 변환
		}
	}

	/**
	 * 파일의 크기를 검증하는 메서드.
	 *
	 * @param file 검증할 MultipartFile.
	 * @throws EmptyFileException 빈 파일일 경우.
	 */
	public void validateFileSize(MultipartFile file) {
		if (file.getSize() < 0) {
			throw new EmptyFileException();
		}
	}

	/**
	 * Base64 문자열이 유효한지 검증하는 메서드.
	 *
	 * @param base64String 검증할 Base64 문자열.
	 * @return 유효성 여부.
	 */
	public boolean isValidBase64Image(String base64String) {
		try {
			Base64.getDecoder().decode(base64String);
			return true;
		} catch (IllegalArgumentException e) {
			return false;
		}
	}

	/**
	 * 오디오 파일의 유효성을 검증하는 메서드.
	 *
	 * @param audioFile 검증할 MultipartFile.
	 * @throws NoSearchKeywordException 파일이 null 일 경우.
	 * @throws EmptyFileException 빈 파일일 경우.
	 */
	public void checkValidAudioFile(MultipartFile audioFile) {
		if (audioFile == null) {
			throw new NoSearchKeywordException(); // 검색 입력 없음
		} else if (audioFile.isEmpty()) {
			throw new EmptyFileException(); // 빈 파일 보냄
		}
	}

	/**
	 * MultipartFile을 Base64 문자열로 변환하는 메서드.
	 *
	 * @param file 변환할 MultipartFile.
	 * @return Base64로 인코딩된 문자열.
	 * @throws RuntimeException(IOException) 변환 중 발생하는 예외.
	 */
	public String convertFileToBase64(MultipartFile file) {
		try {
			byte[] bytes = file.getBytes();
			return Base64.getEncoder().encodeToString(bytes);
		} catch (IOException e) {
			log.error("Failed to read input image file {}", e.getMessage());
			throw new RuntimeException(e);
		}
	}
}

# 1. Java 17 베이스 이미지 사용 (JDK)
FROM openjdk:17-jdk-alpine

# 2. JAR 파일을 컨테이너 내에서 실행할 디렉토리 설정
WORKDIR /app

# 3. ARG 변수 설정
ARG JAR_FILE_PATH=./build/libs/backend-0.0.1-SNAPSHOT.jar

# 4. 디렉토리 복사
COPY . .

# 5. 권한 부여 및 Gradle 빌드 실행
RUN chmod +x ./gradlew && ./gradlew clean build

# 경로 확인
RUN ls build/libs

# 6. JAR 파일을 컨테이너의 /app으로 복사
COPY ${JAR_FILE_PATH} ./app/app.jar

# 7. 포트 설정 (스프링 부트 기본 포트: 8080)
EXPOSE 8080

# 8. 컨테이너가 시작되면 실행할 명령어 (JAR 실행)
ENTRYPOINT ["java", "-jar", "./app/app.jar"]

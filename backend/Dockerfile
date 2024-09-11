# Java 17 베이스 이미지 사용 (JDK)
FROM openjdk:17-jdk-alpine

# JAR 파일을 컨테이너 내에서 실행할 디렉토리 설정
WORKDIR /app

# 디렉토리 복사
COPY . .

# 권한 부여 및 Gradle 빌드 실행
RUN chmod +x ./gradlew && ./gradlew clean build --info --stacktrace

# 빌드 후 JAR 파일이 생성되었는지 확인
RUN ls -l build/libs/

# ARG 변수 설정
ARG JAR_FILE=backend-0.0.1-SNAPSHOT.jar

# JAR 파일을 컨테이너의 /app으로 복사
COPY ./build/libs/${JAR_FILE} ./app.jar

# 포트 설정 (스프링 부트 기본 포트: 8080)
EXPOSE 8080

# 컨테이너가 시작되면 실행할 명령어 (JAR 실행)
ENTRYPOINT ["java", "-jar", "app.jar"]

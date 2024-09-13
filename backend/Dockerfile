FROM openjdk:17-jdk-alpine

WORKDIR /app

RUN chmod +x ./gradlew && ./gradlew clean build

ARG JAR_FILE=bild/libs/backend-0.0.1-SNAPSHOT.jar

COPY ${JAR_FILE} app.jar

ENTRYPOINT ["java", "-jar", "./app.jar"]

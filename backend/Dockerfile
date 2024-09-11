FROM openjdk:17-jdk-alpine

WORKDIR /app

COPY . .

RUN chmod +x ./gradlew && ./gradlew clean build
RUN ls -al ./build/libs/

ARG JAR_FILE=backend-0.0.1-SNAPSHOT.jar
RUN mv ./build/libs/${JAR_FILE} ./build/libs/app.jar
RUN ls -al ./build/libs/

RUN cp ./build/libs/app.jar ./app.jar

ENTRYPOINT ["java", "-jar", "./app.jar"]

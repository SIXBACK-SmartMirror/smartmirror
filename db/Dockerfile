# 공식 MySQL 이미지에서 기반 이미지 설정
FROM mysql:latest

# 환경 변수 설정
ENV MYSQL_ROOT_PASSWORD=${MYSQL_ROOT_PASSWORD}
ENV MYSQL_DATABASE=${MYSQL_DATABASE}
ENV MYSQL_USER=${MYSQL_USER}
ENV MYSQL_PASSWORD=${MYSQL_PASSWORD}

# 초기화 스크립트를 컨테이너에 복사
COPY ./init.sql /docker-entrypoint-initdb.d/

# 컨테이너에서의 작업 디렉터리 설정
WORKDIR /app

# 기본 명령어 설정 (이미지에서 기본으로 설정된 명령어 사용)
CMD ["mysqld"]

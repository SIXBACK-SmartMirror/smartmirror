# Miniconda 베이스 이미지 사용 (Python 3.8 버전)
FROM continuumio/miniconda3

# 작업 디렉터리 생성
WORKDIR /app

# environment.yml 파일을 컨테이너로 복사
COPY environment.yml .

# Conda 환경 생성 및 패키지 설치
RUN conda env create -f environment.yml

# Conda 환경 활성화
RUN echo "source activate my_env" > ~/.bashrc

# FastAPI 앱 파일을 복사
COPY . .

# Conda 환경에서 FastAPI 서버 실행
CMD ["conda", "run", "--no-capture-output", "-n", "my_env", "uvicorn", "main:app", "--host", "0.0.0.0", "--port", "8000", "--reload"]

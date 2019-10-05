FROM ubuntu:19.04
RUN mkdir /app
WORKDIR /app
COPY . /app
RUN apt update && apt install wget -y && wget -q https://packages.microsoft.com/config/ubuntu/19.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && dpkg -i packages-microsoft-prod.deb && apt update && apt install apt-transport-https -y && apt update && apt install dotnet-sdk-3.0 -y
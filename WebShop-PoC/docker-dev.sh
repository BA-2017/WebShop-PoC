#!/usr/bin/env bash
set -e
set -v

docker build -t dev-image -f Dockerfile.dev .

docker run -it --rm --name dotnet-dev \
    -v $(pwd):/app \
    -e ASPNETCORE_URLS="http://*:9000" \
    -e ASPNETCORE_ENVIRONMENT=Development \
    -p 9000:9000 \
    dev-image
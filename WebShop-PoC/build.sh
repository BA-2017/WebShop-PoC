#!/usr/bin/env bash
set -v
set -e

# Config
APP_NAME=WebShop-PoC

DOCKER_REPO=ba2017
DOCKER_IMAGE=$DOCKER_REPO"/"$APP_NAME
DOCKER_TAG=$TRAVIS_BUILD_NUMBER

cd $APP_NAME

# Build the builder image
docker build --force-rm -t build-image -t build-image:$DOCKER_TAG -f Dockerfile.build .
# Create a container from the built image
docker create --name build-container build-image
# Copy binaries
docker cp build-container:/out ./PublishOutput
# Build the productin image
docker build --force-rm -t $DOCKER_IMAGE -f Dockerfile.prod .
# Login to docker hub
docker login -u="$DOCKER_HUB_USER" -p="$DOCKER_HUB_PASSWORD"
# Push docker image
docker push ba2017/webshop-poc:$DOCKER_TAG
docker push ba2017/webshop-poc:latest
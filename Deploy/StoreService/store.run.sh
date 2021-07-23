#!/usr/bin/env bash
docker-compose -f docker-compose.store.yml build
docker-compose -f docker-compose.store.yml up -d

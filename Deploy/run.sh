docker-compose -f ./Infrastructure/Mongo/docker-compose.mongo.yml up -d
docker-compose -f ./Infrastructure/Redis/docker-compose.redis.yml up -d
docker-compose -f ./StoreService/docker-compose.store.internal.yml up -d
docker-compose -f ./StoreService/docker-compose.store.auth.yml up -d
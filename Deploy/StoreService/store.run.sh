docker-compose -f docker-compose.store.internal.yml build
docker-compose -f docker-compose.store.internal.yml up -d
docker-compose -f docker-compose.store.auth.yml build
docker-compose -f docker-compose.store.auth.yml up -d

# MicroHackOTrain (.NET 9, Dapper, YARP)

Debug-first (VS / VS Code), deploy with Dockerfile per service.

## Projects
- CatalogService (Dapper + Swagger + HealthCheck)
- OrderService   (Dapper + transaction + Swagger + HealthCheck)
- ApiGateway     (YARP Reverse Proxy)

## Debug
- CatalogService → http://localhost:5081 (Swagger: /swagger)
- OrderService   → http://localhost:5082 (Swagger: /swagger)
- ApiGateway     → http://localhost:5090 (set saat run)

Set connection string di `appsettings.Development.json` atau `dotnet user-secrets`.

## Deploy (Dockerfile per service)
```bash
# Catalog
docker build -t micro/catalog:1.0 ./CatalogService
docker run -d -p 8081:8080 --name catalog   -e ConnectionStrings__SqlServer="Server=<host>,1433;Database=CatalogDb;User ID=sa;Password=<pwd>;TrustServerCertificate=True;Encrypt=True"   micro/catalog:1.0

# Order
docker build -t micro/order:1.0 ./OrderService
docker run -d -p 8082:8080 --name order   -e ConnectionStrings__SqlServer="Server=<host>,1433;Database=OrderDb;User ID=sa;Password=<pwd>;TrustServerCertificate=True;Encrypt=True"   micro/order:1.0

# ApiGateway (ubah destination Address ke URL container prod, ex: http://catalog:8080/)
docker build -t micro/gateway:1.0 ./ApiGateway
docker run -d -p 8080:8080 --name gateway micro/gateway:1.0
```

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Source/Store.Core.Host/Store.Core.Host.csproj", "Source/Store.Core.Host/"]
COPY ["Source/Store.Core.Contracts/Store.Core.Contracts.csproj", "Source/Store.Core.Contracts/"]
COPY ["Source/Store.Core.Services/Store.Core.Services.csproj", "Source/Store.Core.Services/"]
COPY ["Source/Store.Core.Cache/Store.Core.Cache.csproj", "Source/Store.Core.Cache/"]
COPY ["Source/Store.Core.Database/Store.Core.Database.csproj", "Source/Store.Core.Database/"]
COPY ["Source/Store.WebApi.Authorization/Store.WebApi.Authorization.csproj", "Store.WebApi.Authorization/"] 
RUN dotnet restore "Source/Store.WebApi.Authorization/Store.WebApi.Authorization.csproj"
COPY . .
WORKDIR "/src/Store.WebApi.Authorization"
RUN dotnet build "Store.WebApi.Authorization.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Store.WebApi.Authorization.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Store.WebApi.Authorization.dll"]

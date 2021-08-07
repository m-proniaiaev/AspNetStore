FROM mcr.microsoft.com/dotnet/aspnet:5.0.2-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0.102-1-buster-slim AS build
WORKDIR /src
COPY ["Source/Store.Core.Services/Store.Core.Services.csproj", "Source/Store.Core.Services/"]
COPY ["Source/Store.Core.Cache/Store.Core.Cache.csproj", "Source/Store.Core.Cache/"]
COPY ["Source/Store.Core.Contracts/Store.Core.Contracts.csproj", "Source/Store.Core.Contracts/"]
COPY ["Source/Store.Core.Database/Store.Core.Database.csproj", "Source/Store.Core.Database/"]
COPY ["Source/Store.Core.Extensions/Store.Core.Extensions.csproj", "Source/Store.Core.Extensions/"]
COPY ["Source/Store.Core.Host/Store.Core.Host.csproj", "Source/Store.Core.Host/"]
COPY ["Source/Store.WebApi.Internal/Store.WebApi.Internal.csproj", "Source/Store.WebApi.Internal/"]
RUN dotnet restore "Source/Store.WebApi.Internal/Store.WebApi.Internal.csproj"
COPY . .
WORKDIR "/src/Source/Store.WebApi.Internal"
RUN dotnet build "Store.WebApi.Internal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Store.WebApi.Internal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Store.WebApi.Internal.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:5.0.2-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0.102-1-buster-slim AS build
WORKDIR /src
COPY ["Source/Store.Core/Store.Core.csproj", "Source/Store.Core/"]
COPY ["Source/Store.Core.Contracts/Store.Contracts.csproj", "Source/Store.Core.Contracts/"]
COPY ["Source/Store.Core.Database/Store.Database.csproj", "Source/Store.Core.Database/"]
COPY ["Source/Store.Core.Extensions/Store.Extensions.csproj", "Source/Store.Core.Extensions/"]
COPY ["Source/Store.Core.Host/Store.Host.csproj", "Source/Store.Core.Host/"]
COPY ["Source/Store/SomeStore.csproj", "Source/Store/"]
RUN dotnet restore "Source/Store/SomeStore.csproj"
COPY . .
WORKDIR "/src/Source/Store"
RUN dotnet build "SomeStore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SomeStore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SomeStore.dll"]

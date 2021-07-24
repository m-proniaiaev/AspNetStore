FROM mcr.microsoft.com/dotnet/aspnet:5.0.2-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0.102-1-buster-slim AS build
WORKDIR /src
COPY ["Source/Store.Contracts/Store.Contracts.csproj", "Source/Store.Contracts/"]
COPY ["Source/Store.Core/Store.Core.csproj", "Source/Store.Core/"]
COPY ["Source/Store.Database/Store.Database.csproj", "Source/Store.Database/"]
COPY ["Source/Store.Extensions/Store.Extensions.csproj", "Source/Store.Extensions/"]
COPY ["Source/Store.Host/Store.Host.csproj", "Source/Store.Host/"]
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

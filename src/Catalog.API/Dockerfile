FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG source
WORKDIR /app
EXPOSE 60080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY "nuget.config" "nuget.config"
COPY "Directory.Packages.props" "Directory.Packages.props"
COPY "Sprmon.Shop.sln" "Sprmon.Shop.sln"
COPY "src/Catalog.API/Catalog.API.csproj" "src/Catalog.API/Catalog.API.csproj"
COPY "src/EventBus/EventBus.csproj" "src/EventBus/EventBus.csproj"
COPY "src/EventBusRabbitMQ/EventBusRabbitMQ.csproj" "src/EventBusRabbitMQ/EventBusRabbitMQ.csproj"
COPY "src/IntegrationEventLogEF/IntegrationEventLogEF.csproj" "src/IntegrationEventLogEF/IntegrationEventLogEF.csproj"
COPY "src/ServiceDefaults/ServiceDefaults.csproj" "src/ServiceDefaults/ServiceDefaults.csproj"
COPY "tests/Catalog.FunctionalTests/Catalog.FunctionalTests.csproj" "tests/Catalog.FunctionalTests/Catalog.FunctionalTests.csproj"
RUN dotnet workload restore
RUN dotnet restore Sprmon.Shop.sln

COPY . .
WORKDIR /src/src/Catalog.API
RUN dotnet publish Catalog.API.csproj --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]

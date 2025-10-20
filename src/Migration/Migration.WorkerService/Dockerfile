# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base

WORKDIR /app


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Migration/Migration.WorkerService/Migration.WorkerService.csproj", "src/Migration/Migration.WorkerService/"]
COPY ["src/Currency/Currency.Infrastructure/Currency.Infrastructure.csproj", "src/Currency/Currency.Infrastructure/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]
COPY ["src/Currency/Currency.Application/Currency.Application.csproj", "src/Currency/Currency.Application/"]
COPY ["src/Currency/Currency.Domain/Currency.Domain.csproj", "src/Currency/Currency.Domain/"]
COPY ["src/User/User.Infrastructure/User.Infrastructure.csproj", "src/User/User.Infrastructure/"]
COPY ["src/User/User.Application/User.Application.csproj", "src/User/User.Application/"]
COPY ["src/User/User.Domain/User.Domain.csproj", "src/User/User.Domain/"]
RUN dotnet restore "./src/Migration/Migration.WorkerService/Migration.WorkerService.csproj"
COPY . .
WORKDIR "/src/src/Migration/Migration.WorkerService"
RUN dotnet build "./Migration.WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Migration.WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Migration.WorkerService.dll"]
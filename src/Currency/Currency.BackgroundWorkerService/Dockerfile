# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Currency/Currency.BackgroundWorkerService/", "src/Currency/Currency.BackgroundWorkerService/"]
COPY ["src/Currency/Currency.Application/", "src/Currency/Currency.Application/"]
COPY ["src/Currency/Currency.Domain/", "src/Currency/Currency.Domain/"]
COPY ["src/Shared/", "src/Shared/"]
COPY ["src/Currency/Currency.Infrastructure/", "src/Currency/Currency.Infrastructure/"]
RUN dotnet restore "./src/Currency/Currency.BackgroundWorkerService/Currency.BackgroundWorkerService.csproj"
COPY . .
WORKDIR "/src/src/Currency/Currency.BackgroundWorkerService"
RUN dotnet build "./Currency.BackgroundWorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Currency.BackgroundWorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Currency.BackgroundWorkerService.dll"]
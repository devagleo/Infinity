# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Gateway/Gateway.Grpc/", "src/Gateway/Gateway.Grpc/"]
COPY ["src/Shared/", "src/Shared/"]
COPY ["src/Gateway/Gateway.Application/", "src/Gateway/Gateway.Application/"]
COPY ["src/Gateway/Gateway.Infrastructure/", "src/Gateway/Gateway.Infrastructure/"]

# Копируем сами proto файлы в то же место, где их ищет csproj
COPY ["src/User/User.Grpc/Protos/", "/src/User/User.Grpc/Protos/"]
COPY ["src/Currency/Currency.Grpc/Protos/", "/src/Currency/Currency.Grpc/Protos/"]

RUN dotnet restore "./src/Gateway/Gateway.Grpc/Gateway.Grpc.csproj"
COPY . .
WORKDIR "/src/src/Gateway/Gateway.Grpc"
RUN dotnet build "./Gateway.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Gateway.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.Grpc.dll"]
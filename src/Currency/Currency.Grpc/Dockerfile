# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Currency/Currency.Grpc/", "src/Currency/Currency.Grpc/"]
COPY ["src/Currency/Currency.Application/", "src/Currency/Currency.Application/"]
COPY ["src/Currency/Currency.Domain/", "src/Currency/Currency.Domain/"]
COPY ["src/Shared/", "src/Shared/"]
COPY ["src/Currency/Currency.Infrastructure/", "src/Currency/Currency.Infrastructure/"]
COPY ["src/Currency/Currency.Grpc/Protos", "src/Currency/Currency.Grpc/Protos"]
RUN dotnet restore "./src/Currency/Currency.Grpc/Currency.Grpc.csproj"
COPY . .
WORKDIR "/src/src/Currency/Currency.Grpc"
RUN dotnet build "./Currency.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Currency.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Currency.Grpc.dll"]
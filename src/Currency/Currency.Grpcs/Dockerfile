# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app

# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpie AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Currency/Currency.Grpcs/Currency.Grpcs.csproj", "Currency/Currency.Grpcs/"]
COPY ["Currency/Currency.Application/Currency.Application.csproj", "Currency/Currency.Application/"]
COPY ["Currency/Currency.Domain/Currency.Domain.csproj", "Currency/Currency.Domain/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]
COPY ["Currency/Currency.Infrastructure/Currency.Infrastructure.csproj", "Currency/Currency.Infrastructure/"]
RUN dotnet restore "./Currency/Currency.Grpcs/Currency.Grpcs.csproj"
COPY . .
WORKDIR "/src/Currency/Currency.Grpcs"
RUN dotnet build "./Currency.Grpcs.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Currency.Grpcs.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Currency.Grpcs.dll"]
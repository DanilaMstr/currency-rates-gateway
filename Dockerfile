FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["CurrencyRatesGateway.API/CurrencyRatesGateway.API.csproj", "CurrencyRatesGateway.API/"]
COPY ["CurrencyRatesGateway.Application/CurrencyRatesGateway.Application.csproj", "CurrencyRatesGateway.Application/"]
COPY ["CurrencyRatesGateway.Domain/CurrencyRatesGateway.Domain.csproj", "CurrencyRatesGateway.Domain/"]
COPY ["CurrencyRatesGateway.CbRFAdapter/CurrencyRatesGateway.CbRFAdapter.csproj", "CurrencyRatesGateway.CbRFAdapter/"]

RUN dotnet restore "CurrencyRatesGateway.API/CurrencyRatesGateway.API.csproj"

COPY . .

WORKDIR "/src/CurrencyRatesGateway.API"
RUN dotnet build "CurrencyRatesGateway.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CurrencyRatesGateway.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrencyRatesGateway.API.dll"]
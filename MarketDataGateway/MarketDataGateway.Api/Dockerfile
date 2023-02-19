FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MarketDataGateway.Api/MarketDataGateway.Api.csproj", "MarketDataGateway.Api/"]
RUN dotnet restore "MarketDataGateway.Api/MarketDataGateway.Api.csproj"
COPY . .
WORKDIR "/src/MarketDataGateway.Api"
RUN dotnet build "MarketDataGateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarketDataGateway.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarketDataGateway.Api.dll"]

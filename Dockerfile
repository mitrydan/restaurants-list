FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY . .
RUN dotnet restore RestaurantsList.Api/RestaurantsList.Api.csproj
COPY . .
WORKDIR /src
RUN dotnet build RestaurantsList.Api/RestaurantsList.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RestaurantsList.Api/RestaurantsList.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RestaurantsList.Api.dll"]
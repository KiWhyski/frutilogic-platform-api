FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["KiWhisky.FrutiLogicPlatform.FruitFreshness.API/KiWhisky.FrutiLogicPlatform.FruitFreshness.API.csproj", "./"]
RUN dotnet restore "KiWhisky.FrutiLogicPlatform.FruitFreshness.API.csproj"
COPY . .
WORKDIR "/src/KiWhisky.FrutiLogicPlatform.FruitFreshness.API"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
# Copia modelos (asegúrate de agregar el onnx fuera del repo o en una carpeta models)
COPY models/ ./models/
ENTRYPOINT ["dotnet", "KiWhisky.FrutiLogicPlatform.FruitFreshness.API.dll"]


FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY frutilogic-platform-api.sln ./
COPY global.json ./
COPY KiWhisky.FrutiLogicPlatform.API/KiWhisky.FrutiLogicPlatform.API.csproj KiWhisky.FrutiLogicPlatform.API/

RUN dotnet restore KiWhisky.FrutiLogicPlatform.API/KiWhisky.FrutiLogicPlatform.API.csproj

COPY KiWhisky.FrutiLogicPlatform.API/ KiWhisky.FrutiLogicPlatform.API/

RUN dotnet publish KiWhisky.FrutiLogicPlatform.API/KiWhisky.FrutiLogicPlatform.API.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "KiWhisky.FrutiLogicPlatform.API.dll"]

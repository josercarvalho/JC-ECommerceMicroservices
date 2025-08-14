FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Stock/Stock.API/Stock.API.csproj", "src/Stock/Stock.API/"]
COPY ["src/Stock/Stock.Application/Stock.Application.csproj", "src/Stock/Stock.Application/"]
COPY ["src/Stock/Stock.Domain/Stock.Domain.csproj", "src/Stock/Stock.Domain/"]
COPY ["src/Stock/Stock.Infrastructure/Stock.Infrastructure.csproj", "src/Stock/Stock.Infrastructure/"]
RUN dotnet restore "src/Stock/Stock.API/Stock.API.csproj"

WORKDIR /src/src/Stock/Stock.API
RUN dotnet build "Stock.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stock.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stock.API.dll"]

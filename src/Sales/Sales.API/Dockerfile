FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Sales/Sales.API/Sales.API.csproj", "src/Sales/Sales.API/"]
COPY ["src/Sales/Sales.Application/Sales.Application.csproj", "src/Sales/Sales.Application/"]
COPY ["src/Sales/Sales.Domain/Sales.Domain.csproj", "src/Sales/Sales.Domain/"]
COPY ["src/Sales/Sales.Infrastructure/Sales.Infrastructure.csproj", "src/Sales/Sales.Infrastructure/"]
RUN dotnet restore "src/Sales/Sales.API/Sales.API.csproj"

WORKDIR /src/src/Sales/Sales.API
RUN dotnet build "Sales.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sales.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sales.API.dll"]

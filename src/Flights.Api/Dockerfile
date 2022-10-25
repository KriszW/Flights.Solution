#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Flights.Api/Flights.Api.csproj", "src/Flights.Api/"]
COPY ["src/Core/Flights.Application/Flights.Application.csproj", "src/Core/Flights.Application/"]
COPY ["src/Core/Flights.Contracts/Flights.Contracts.csproj", "src/Core/Flights.Contracts/"]
COPY ["src/Core/Flights.Domain/Flights.Domain.csproj", "src/Core/Flights.Domain/"]
COPY ["src/Core/Flights.Infrastructure/Flights.Infrastructure.csproj", "src/Core/Flights.Infrastructure/"]
RUN dotnet restore "src/Flights.Api/Flights.Api.csproj"
COPY . .
WORKDIR "/src/src/Flights.Api"
RUN dotnet build "Flights.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Flights.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Flights.Api.dll"]
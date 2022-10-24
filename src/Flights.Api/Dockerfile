#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Flights.Api/Flights.Api.csproj", "src/Flights.Api/"]
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
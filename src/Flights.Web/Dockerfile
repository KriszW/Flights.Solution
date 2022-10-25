FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["src/Flights.Web/Flights.Web.csproj", "/src/Flights.Web/"]
RUN dotnet restore "/src/Flights.Web/Flights.Web.csproj"
COPY . .
RUN dotnet build "/src/Flights.Web/Flights.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/src/Flights.Web/Flights.Web.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
COPY --from=publish /app/publish/wwwroot /usr/share/nginx/html
COPY src/Flights.Web/nginx.conf /etc/nginx/nginx.conf
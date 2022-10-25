Set-Location src
docker build -t kriszw/flights.web:dev -f Flights.Web/Dockerfile ..
docker build -t kriszw/flights.api:dev -f Flights.Api/Dockerfile ..

docker run -d -p 10880:80 kriszw/flights.web:dev

docker run -d -p 11880:80 kriszw/flights.api:dev

Set-Location ..

Start-Process "http://localhost:10880"
Set-Location .\deploy

docker-compose up -d

Set-Location ..

Start-Process "http://localhost:10800"
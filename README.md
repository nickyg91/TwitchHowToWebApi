# TwitchHowToWebApi
Teaching twitch chat how to create a ASPNET Core WebAPI!

# Getting Started
I am making the assumption you have:
- Visual Studio 2022, VS Code, or Rider.
- .NET 6 installed
- Postman installed
- NodeJS/NPM and VS Code (whenever the frontend comes along)
- Docker (Desktop on Windows or just docker containerd on Linux)

# Dev Setup
## Docker pull commands to run:
- docker pull postgres
- docker pull redis

## Docker container run commands:
- docker run -p 6379:6379  --restart unless-stopped  --name dev-redis -d redis
- docker run -p 5432:5432  --restart unless-stopped  --name dev-postgres -v dev-postgres -e POSTGRES_PASSWORD=<password> -e POSTGRES_INITDB_ARGS="--auth-host=scram-sha-256 --auth-local=scram-sha-256" -d postgres

## Dotnet commands to run
- dotnet tool install --global dotnet-ef
- dotnet ef migrations add <migration name>
- dotnet tool install -g dotnet-aspnet-codegenerator


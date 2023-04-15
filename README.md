# TwitchHowToWebApi

Teaching twitch chat how to create a ASPNET Core WebAPI!

# Getting Started

I am making the assumption you have:

- Visual Studio 2022, VS Code, or Rider.
- .NET 7 SDK installed
- Postman installed
- NodeJS/NPM and VS Code (whenever the frontend comes along)
- Docker (Desktop on Windows or just docker containerd on Linux)
- Git

# Dev Setup

## Installing Docker on Windows

Download this and run the installer:
https://www.docker.com/products/docker-desktop/ (works on linux too, but I generally use CLI on Linux)

## Installing Docker on Linux

LINUX CLI - there is a new convinience script Docker provides, so maybe skip to that section if you dont want to run all these commands.
For Ubuntu follow these directions
https://docs.docker.com/engine/install/ubuntu/

For Fedora follow these directions
https://docs.docker.com/engine/install/fedora/

Post linux install steps:
https://docs.docker.com/engine/install/linux-postinstall/#manage-docker-as-a-non-root-user

## Docker pull commands to run:

- docker pull postgres
- docker pull redis

## Docker container run commands:

- docker run -p 6379:6379 --restart unless-stopped --name dev-redis -d redis
- docker run -p 5432:5432 --restart unless-stopped --name dev-postgres -v dev-postgres -e POSTGRES_PASSWORD=<password> -e POSTGRES_INITDB_ARGS="--auth-host=scram-sha-256 --auth-local=scram-sha-256" -d postgres

## Dotnet commands to run

- dotnet tool install --global dotnet-ef
- dotnet ef migrations add <migration name>
- dotnet tool install -g dotnet-aspnet-codegenerator

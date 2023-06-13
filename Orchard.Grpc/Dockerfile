FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Orchard.Grpc/Orchard.Grpc.csproj", "Orchard.Grpc/"]
RUN dotnet restore "Orchard.Grpc/Orchard.Grpc.csproj"
COPY . .
WORKDIR "/src/Orchard.Grpc"
RUN dotnet build "Orchard.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Orchard.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orchard.Grpc.dll"]

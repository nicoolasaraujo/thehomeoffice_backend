#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["TheHomeOffice.Api/TheHomeOffice.Api.csproj", "TheHomeOffice.Api/"]
RUN dotnet restore "TheHomeOffice.Api/TheHomeOffice.Api.csproj"

COPY . .
WORKDIR "/src/TheHomeOffice.Api"
RUN dotnet build "TheHomeOffice.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheHomeOffice.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheHomeOffice.Api.dll"]

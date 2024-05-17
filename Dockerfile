<<<<<<< HEAD
<<<<<<< HEAD
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
=======
>>>>>>> b57bf1406d7dca600907c00c4d268787db1db1bc
=======
>>>>>>> development_branch
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

USER app

WORKDIR /app

EXPOSE 8080

EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["Slush.csproj", "."]

RUN dotnet restore "./Slush.csproj"

COPY . .

WORKDIR "/src/."

RUN dotnet build "./Slush.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish

ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "./Slush.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Slush.dll"]

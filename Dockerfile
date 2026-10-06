FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Pre-create the upload dir owned by "app": the uploads-data volume mounts here
# empty on first run, and Docker seeds a new volume from whatever already exists
# at the mount path in the image — if it's missing here, Docker creates it as
# root instead, and the app user can never write to it.
RUN mkdir -p /app/wwwroot/uploads && chown -R app:app /app/wwwroot

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

COPY --from=publish --chown=app:app /app/publish .

ENTRYPOINT ["dotnet", "Slush.dll"]

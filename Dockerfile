FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /repo

COPY *.sln .
COPY ./src/*.csproj ./source
RUN dotnet restore

COPY ./src ./source
WORKDIR /repo/source
RUN dotnet publish -c release -o /artifacts --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /artifacts ./
ENTRYPOINT [ "dotnet", "aspnetapp.dll" ]
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /repo

COPY Project.sln ./
COPY ./src ./src
RUN dotnet restore

WORKDIR /repo/src
RUN dotnet publish -c release -o /artifacts --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /artifacts .


EXPOSE 8080
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT [ "dotnet", "GetPhoneAPI.dll" ]
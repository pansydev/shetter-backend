FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

WORKDIR /source

COPY Directory.Build.props .

COPY src/PansyDev.Common/Directory.Build.props                                       ./src/PansyDev.Common/
COPY src/PansyDev.Common/src/PansyDev.Common.Domain/*.csproj                         ./src/PansyDev.Common/src/PansyDev.Common.Domain/
COPY src/PansyDev.Common/src/PansyDev.Common.Application/*.csproj                    ./src/PansyDev.Common/src/PansyDev.Common.Application/
COPY src/PansyDev.Common/src/PansyDev.Common.Infrastructure/*.csproj                 ./src/PansyDev.Common/src/PansyDev.Common.Infrastructure/
COPY src/PansyDev.Common/src/PansyDev.Common.Infrastructure.Authentication/*.csproj  ./src/PansyDev.Common/src/PansyDev.Common.Infrastructure.Authentication/
COPY src/PansyDev.Common/src/PansyDev.Common.Infrastructure.EntityFramework/*.csproj ./src/PansyDev.Common/src/PansyDev.Common.Infrastructure.EntityFramework/
COPY src/PansyDev.Common/src/PansyDev.Common.Web/*.csproj                            ./src/PansyDev.Common/src/PansyDev.Common.Web/
COPY src/PansyDev.Common/src/PansyDev.Common.Web.GraphQL/*.csproj                    ./src/PansyDev.Common/src/PansyDev.Common.Web.GraphQL/

COPY src/PansyDev.Shetter/PansyDev.Shetter.Domain/*.csproj         ./src/PansyDev.Shetter/PansyDev.Shetter.Domain/
COPY src/PansyDev.Shetter/PansyDev.Shetter.Application/*.csproj    ./src/PansyDev.Shetter/PansyDev.Shetter.Application/
COPY src/PansyDev.Shetter/PansyDev.Shetter.Infrastructure/*.csproj ./src/PansyDev.Shetter/PansyDev.Shetter.Infrastructure/
COPY src/PansyDev.Shetter/PansyDev.Shetter.Web/*.csproj            ./src/PansyDev.Shetter/PansyDev.Shetter.Web/

WORKDIR /source/src/PansyDev.Shetter/PansyDev.Shetter.Web
RUN dotnet restore -p:SolutionDir=/source/

WORKDIR /source
COPY . .

WORKDIR /source/src/PansyDev.Shetter/PansyDev.Shetter.Web

RUN dotnet publish -p:SolutionDir=/source/ --no-restore -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine

RUN apk update && apk add libgdiplus --repository https://dl-3.alpinelinux.org/alpine/edge/testing/

WORKDIR /app

COPY --from=build /app ./
ENTRYPOINT ["dotnet", "PansyDev.Shetter.Web.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN curl -sL https://deb.nodesource.com/setup_14.x |  bash -
RUN apt-get update -y && apt-get install -y nodejs yarn

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
RUN curl -sL https://deb.nodesource.com/setup_14.x |  bash -
RUN apt-get update -y && apt-get install -y nodejs yarn
WORKDIR /src
COPY ["src/Web/WebSPA/WebSPA.csproj", "src/Web/WebSPA/"]
RUN dotnet restore "src/Web/WebSPA/WebSPA.csproj"
COPY . .
WORKDIR "/src/src/Web/WebSPA"
RUN dotnet build "WebSPA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebSPA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebSPA.dll"]

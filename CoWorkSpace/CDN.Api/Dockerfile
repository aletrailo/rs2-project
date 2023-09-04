#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CDN.Api/CDN.Api.csproj", "CDN.Api/"]
COPY ["CDN.Common/CDN.Common.csproj", "CDN.Common/"]
COPY ["CDN.Persistance/CDN.Persistance.csproj", "CDN.Persistance/"]
RUN dotnet restore "CDN.Api/CDN.Api.csproj"
COPY . .
WORKDIR "/src/CDN.Api"
RUN dotnet build "CDN.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CDN.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CDN.Api.dll"]
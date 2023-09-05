#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Advertising.Api/Advertising.Api.csproj", "Advertising.Api/"]
COPY ["Advertising.Common/Advertising.Common.csproj", "Advertising.Common/"]
RUN dotnet restore "Advertising.Api/Advertising.Api.csproj"
COPY . .
WORKDIR "/src/Advertising.Api"
RUN dotnet build "Advertising.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Advertising.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Advertising.Api.dll"]
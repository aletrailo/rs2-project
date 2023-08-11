#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Spaces.Api/Spaces.Api.csproj", "Spaces.Api/"]
COPY ["Spaces.Common/Spaces.Common.csproj", "Spaces.Common/"]
COPY ["Spaces.Persistance/Spaces.Persistance.csproj", "Spaces.Persistance/"]
RUN dotnet restore "Spaces.Api/Spaces.Api.csproj"
COPY . .
WORKDIR "/src/Spaces.Api"
RUN dotnet build "Spaces.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Spaces.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Spaces.Api.dll"]
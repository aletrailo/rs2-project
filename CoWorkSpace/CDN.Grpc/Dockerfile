#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CDN.Grpc/CDN.Grpc.csproj", "CDN.Grpc/"]
COPY ["CDN.Common/CDN.Common.csproj", "CDN.Common/"]
COPY ["CDN.Persistance/CDN.Persistance.csproj", "CDN.Persistance/"]
RUN dotnet restore "CDN.Grpc/CDN.Grpc.csproj"
COPY . .
WORKDIR "/src/CDN.Grpc"
RUN dotnet build "CDN.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CDN.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CDN.Grpc.dll"]
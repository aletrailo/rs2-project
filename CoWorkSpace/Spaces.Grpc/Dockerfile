#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Spaces.Grpc/Spaces.Grpc.csproj", "Spaces.Grpc/"]
COPY ["Spaces.Common/Spaces.Common.csproj", "Spaces.Common/"]
COPY ["Spaces.Persistance/Spaces.Persistance.csproj", "Spaces.Persistance/"]
RUN dotnet restore "Spaces.Grpc/Spaces.Grpc.csproj"
COPY . .
WORKDIR "/src/Spaces.Grpc"
RUN dotnet build "Spaces.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Spaces.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Spaces.Grpc.dll"]
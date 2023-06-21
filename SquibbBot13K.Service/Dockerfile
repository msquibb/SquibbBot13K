#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SquibbBot13K.Service/SquibbBot13K.Service.csproj", "SquibbBot13K.Service/"]
RUN dotnet restore "SquibbBot13K.Service/SquibbBot13K.Service.csproj"
COPY . .
WORKDIR "/src/SquibbBot13K.Service"
RUN dotnet build "SquibbBot13K.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SquibbBot13K.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SquibbBot13K.Service.dll"]
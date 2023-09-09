#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["src/Metro60.WebApi/Metro60.WebApi.csproj", "src/Metro60.WebApi/"]
COPY ["src/Metro60.Core/Metro60.Core.csproj", "src/Metro60.Core/"]
RUN dotnet restore "src/Metro60.WebApi/Metro60.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Metro60.WebApi"
RUN dotnet build "Metro60.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Metro60.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Metro60.WebApi.dll"]
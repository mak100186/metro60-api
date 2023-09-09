#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0-preview AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["tests/Metro60.FunctionalTests/Metro60.FunctionalTests.csproj", "tests/Metro60.FunctionalTests/"]
COPY ["src/Metro60.WebApi/Metro60.WebApi.csproj", "src/Metro60.WebApi/"]
COPY ["src/Metro60.Core/Metro60.Core.csproj", "src/Metro60.Core/"]
RUN dotnet restore "tests/Metro60.FunctionalTests/Metro60.FunctionalTests.csproj"
COPY . .
WORKDIR "/src/tests/Metro60.FunctionalTests"
RUN dotnet build "Metro60.FunctionalTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Metro60.FunctionalTests.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","test", "-v", "Metro60.FunctionalTests.dll"]

﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebApplication1/WebApplication1.csproj", "WebApplication1/"]
COPY ["project.Application.Contracts/project.Application.Contracts.csproj", "project.Application.Contracts/"]
COPY ["project.Application.Models/project.Application.Models.csproj", "project.Application.Models/"]
COPY ["project.Application.Abstractions/project.Application.Abstractions.csproj", "project.Application.Abstractions/"]
COPY ["project.Application/project.Application.csproj", "project.Application/"]
COPY ["Presentation.Kafka/Presentation.Kafka.csproj", "Presentation.Kafka/"]
COPY ["project.Infrastructure.Persistence/project.Infrastructure.Persistence.csproj", "project.Infrastructure.Persistence/"]
COPY ["project.Endpoints/project.Endpoints.csproj", "project.Endpoints/"]
RUN dotnet restore "WebApplication1/WebApplication1.csproj"
COPY . .
WORKDIR "/src/WebApplication1"
RUN dotnet build "WebApplication1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]

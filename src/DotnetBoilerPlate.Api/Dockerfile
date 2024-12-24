##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 8080

# Install cultures (same approach as Alpine SDK image)
RUN apk add --no-cache icu-libs

# Disable the invariant mode (set in base image)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src
COPY ["./Directory.Packages.props", "./"]
COPY ["src/DotnetBoilerPlate.Api/DotnetBoilerPlate.Api.csproj", "src/DotnetBoilerPlate.Api/"]
COPY ["src/DotnetBoilerPlate.Application/DotnetBoilerPlate.Application.csproj", "src/DotnetBoilerPlate.Application/"]
COPY ["src/DotnetBoilerPlate.Domain/DotnetBoilerPlate.Domain.csproj", "src/DotnetBoilerPlate.Domain/"]
COPY ["src/DotnetBoilerPlate.Infrastructure/DotnetBoilerPlate.Infrastructure.csproj", "src/DotnetBoilerPlate.Infrastructure/"]
COPY . .
WORKDIR "/src/src/DotnetBoilerPlate.Api"

FROM build AS publish
RUN dotnet publish "DotnetBoilerPlate.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER $APP_UID 
ENTRYPOINT ["dotnet", "DotnetBoilerPlate.Api.dll"]
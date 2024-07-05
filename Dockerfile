# Use the official ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the .NET SDK as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj file and restore dependencies as a distinct layer
COPY ["MovieCatalog.csproj", "./"]
RUN dotnet restore "MovieCatalog.csproj"

# Copy the remaining files and build the project
COPY . .
RUN dotnet build "MovieCatalog.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "MovieCatalog.csproj" -c Release -o /app/publish

# Final stage / production image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieCatalog.dll"]

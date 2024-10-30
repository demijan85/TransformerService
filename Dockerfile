# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution and projects files
COPY Transformer.sln ./
COPY src/Transformer.Api/*.csproj ./src/Transformer.Api/
COPY tests/Transformer.Tests/*.csproj ./tests/Transformer.Tests/

# Restore dependencies
RUN dotnet restore

# Copy the entire source code
COPY . ./

# Build the application
RUN dotnet publish src/Transformer.Api/Transformer.Api.csproj -c Release -o out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/out ./

# Set the ASP.NET Core URL to listen on port 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

# Expose the port the application runs on
EXPOSE 80

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "Transformer.Api.dll"]

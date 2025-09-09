#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Πρώτα αντιγράφουμε το .csproj (για καλύτερο caching)
COPY ["MindLog/MindLog.csproj", "MindLog/"]

# Restore dependencies
RUN dotnet restore "MindLog/MindLog.csproj"

# Μετά αντιγράφουμε όλο τον κώδικα
COPY . .

WORKDIR "/src/MindLog"

# Build
RUN dotnet build "MindLog.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "MindLog.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MindLog.dll"]


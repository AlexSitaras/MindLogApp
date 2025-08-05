#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

# base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-ltsc2022 AS base
WORKDIR /app
EXPOSE 8080

# build image
FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-ltsc2022 AS build
WORKDIR /src
COPY ["MindLog/MindLog.csproj", "MindLog/"]
RUN dotnet restore "MindLog/MindLog.csproj"
COPY . .
WORKDIR "/src/MindLog"
RUN dotnet build "MindLog.csproj" -c Release -o /app/build

# publish image
FROM build AS publish
RUN dotnet publish "MindLog.csproj" -c Release -o /app/publish /p:UseAppHost=false

# final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MindLog.dll"]

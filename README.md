# MindLog

MindLog is an ASP.NET Core 8.0 web application that allows users to log their mood entries, track emotional patterns, and reflect on their mental well-being. It is built using Clean Architecture principles and includes both a web interface and a RESTful API. The project supports Docker for containerization and uses SQL Server as the database.

## Features

- Add, view, edit, and delete mood entries
- Web MVC interface for user-friendly interaction
- REST API for programmatic access
- SQL Server database integration
- Entity Framework Core with migrations
- AutoMapper for mapping between models
- Custom middleware for error handling
- Docker support with Docker Compose
- Clean folder structure

## Technologies

- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server (Dockerized)
- AutoMapper
- Docker / Docker Compose
- Razor Views (MVC)

## Getting Started

### Prerequisites

- .NET 8 SDK
- Docker Desktop
- Git

### Clone the repository
git clone https://github.com/YourUsername/MindLogApp.git
cd MindLogApp


### Running with Docker

1. Make sure Docker is running.
2. Run the following command to start the application and the database:

docker-compose up -d

3. The application will be available at: http://localhost:8080


4. The Swagger UI (API documentation) will be available at: http://localhost:8080/swagger

### Entity Framework Core (migrations)

Run these commands from your host machine (not inside Docker):
dotnet ef migrations add InitialCreate
dotnet ef database update

Make sure your connection string in `appsettings.json` matches the Docker SQL Server service name (usually `sqlserver`).

Example:
"ConnectionStrings": {
"DefaultConnection": "Server=sqlserver;Database=MindLogDb;User=sa;Password=Your_password123;TrustServerCertificate=True;"
}


## Author

Alex Sitaras  
GitHub: https://github.com/AlexSitaras





# MyWebApp - ASP.NET Core CQRS Web Application

A modern web application built with ASP.NET Core, Razor Pages, and the CQRS (Command Query Responsibility Segregation) pattern with Entity Framework Core.

## Getting Started

### Prerequisites
- .NET 10.0 SDK or higher
- Visual Studio Code or Visual Studio
- SQLite (included with .NET)

### Installation
1. Clone or open this project
2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```
   The app will be available at `https://localhost:5001`

## Project Structure

### Layers
- **Domain/** - Business logic, entities, commands, queries, and handlers
  - `Models/` - Entity models
  - `Commands/` - Command definitions
  - `Queries/` - Query definitions
  - `Handlers/` - Logic for executing commands
  - `Interfaces/` - Service contracts
  
- **Data/** - Data access and persistence
  - `Context/` - Entity Framework Core database context
  - `Repositories/` - Generic repository implementation
  
- **Pages/** - Razor Pages UI layer
- **wwwroot/** - Static files (CSS, JavaScript, images)
- **Infra/** - Infrastructure services
- **Api/** - API endpoints (optional)

## Development

### Building
```bash
dotnet build
```

### Running in Development
```bash
dotnet run
```
Application starts with HTTPS on port 5001.

### Database Migrations
```bash
# Create a new migration
dotnet ef migrations add MigrationName

# Apply migrations to database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

### Publishing
```bash
dotnet publish -c Release
```

## CQRS Pattern

This project implements CQRS for clear separation of concerns:

- **Commands** (write operations) - Modify application state
  - Example: `CreateUserCommand` → `CreateUserCommandHandler` → Repository
  
- **Queries** (read operations) - Retrieve data
  - Example: `GetUserByIdQuery` → Query Handler

### Adding New Features

#### New Command:
1. Add command class in `Domain/Commands/`
2. Add handler in `Domain/Handlers/`
3. Register in `Program.cs`
4. Call from page or API

#### New Entity:
1. Create model in `Domain/Models/`
2. Add `DbSet<Model>` in `ApplicationDbContext`
3. Configure in `OnModelCreating()`
4. Create migration: `dotnet ef migrations add Add<Model>Table`

## Technology Stack
- ASP.NET Core 10.0
- Entity Framework Core 10.0.3
- SQLite
- Razor Pages
- C# 14

## License
MIT


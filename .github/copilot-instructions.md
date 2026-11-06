# Copilot Instructions for ASP.NET Core CQRS Web App

## Project Overview
This is an ASP.NET Core web application implementing the **CQRS (Command Query Responsibility Segregation)** pattern with Razor Pages. The architecture separates read operations (Queries) from write operations (Commands) for improved scalability and maintainability.

## Architecture & Layer Structure

### Domain Layer
- **Models/** - Entity models (e.g., `User.cs`)
- **Commands/** - Command objects that trigger state changes (e.g., `CreateUserCommand`)
- **Queries/** - Query objects that retrieve data without modification (e.g., `GetUserByIdQuery`)
- **Handlers/** - Handlers that execute commands and process business logic (e.g., `CreateUserCommandHandler`)
- **Interfaces/** - Service contracts like `IRepository<T>`

### Data Layer
- **Context/** - `ApplicationDbContext` - Entity Framework Core database context
- **Repositories/** - `GenericRepository<T>` - Generic repository implementing `IRepository<T>` for data access

### Presentation Layer
- **Pages/** - Razor Pages (.cshtml files) and code-behind (.cshtml.cs)
- **wwwroot/** - Static files (CSS, JavaScript, images)

### Infrastructure & API
- **Infra/** - Infrastructure services (logging, caching, etc.)
- **Api/** - API controllers and endpoints

## Key Files & Patterns

### CQRS Pattern Example Flow
1. **Command**: `Domain/Commands/CreateUserCommand.cs` - Defines what data to write
2. **Handler**: `Domain/Handlers/CreateUserCommandHandler.cs` - Executes the command
3. **Repository**: `Data/Repositories/GenericRepository.cs` - Persists data via EF Core
4. **Context**: `Data/Context/ApplicationDbContext.cs` - Database schema and configurations

### Dependency Injection Setup
All services registered in `Program.cs`:
```csharp
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<CreateUserCommandHandler>();
```

## Development Workflow

1. **Build**: `dotnet build`
2. **Run**: `dotnet run` (launches at https://localhost:5001)
3. **Migrations**: 
   - Create: `dotnet ef migrations add MigrationName`
   - Apply: `dotnet ef database update`
4. **Deploy**: `dotnet publish -c Release`

## Adding New Features (CQRS)

### To Add a New Command:
1. Create command class in `Domain/Commands/`
2. Create handler in `Domain/Handlers/`
3. Register handler in `Program.cs`
4. Use handler in Razor Pages code-behind

### To Add a New Query:
1. Create query class in `Domain/Queries/`
2. Create handler in `Domain/Handlers/`
3. Register handler in `Program.cs`
4. Use handler to fetch data in pages/views

### To Add a New Entity:
1. Create model in `Domain/Models/`
2. Add DbSet<Model> in `ApplicationDbContext`
3. Configure entity in `OnModelCreating` method
4. Create migration: `dotnet ef migrations add Add<Model>Table`

## Database Configuration
- **Default**: SQLite (`app.db`)
- **Connection String**: Set in `appsettings.json` under `ConnectionStrings:DefaultConnection`
- **Provider**: `Microsoft.EntityFrameworkCore.Sqlite`

## External Dependencies
- .NET 10.0 or higher
- ASP.NET Core framework
- Entity Framework Core 10.0.3
- SQLite provider for EF Core

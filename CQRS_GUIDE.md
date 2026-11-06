# CQRS Development Guide

## How to Add a New Entity

### 1. Create the Model
Create a new file in `Domain/Models/`:
```csharp
// Domain/Models/Product.cs
namespace Domain.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
```

### 2. Add DbSet to Context
Update `Data/Context/ApplicationDbContext.cs`:
```csharp
public DbSet<Product> Products { get; set; } = null!;

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Configure Product entity
    modelBuilder.Entity<Product>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
    });
}
```

### 3. Create Migration
```bash
dotnet ef migrations add Add<EntityName>Table
dotnet ef database update
```

---

## How to Add a New Command

### 1. Create Command
Create `Domain/Commands/Create<Entity>Command.cs`:
```csharp
namespace Domain.Commands;

public class CreateProductCommand
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
```

### 2. Create Handler
Create `Domain/Handlers/Create<Entity>CommandHandler.cs`:
```csharp
namespace Domain.Handlers;

public class CreateProductCommandHandler
{
    private readonly IRepository<Product> _repository;

    public CreateProductCommandHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Product> HandleAsync(CreateProductCommand command)
    {
        var product = new Product
        {
            Name = command.Name,
            Price = command.Price
        };
        
        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();
        return product;
    }
}
```

### 3. Register Handler
Update `Program.cs`:
```csharp
builder.Services.AddScoped<CreateProductCommandHandler>();
```

---

## How to Add a New Query

### 1. Create Query
Create `Domain/Queries/Get<Entity>ByIdQuery.cs`:
```csharp
namespace Domain.Queries;

public class GetProductByIdQuery
{
    public int ProductId { get; set; }
    
    public GetProductByIdQuery(int productId)
    {
        ProductId = productId;
    }
}
```

### 2. Create Handler (Optional)
For complex queries, create a handler in `Domain/Handlers/`.
For simple queries, use the repository directly.

---

## Common Patterns

### Using Commands in Razor Pages
```csharp
public class CreateProductModel : PageModel
{
    private readonly CreateProductCommandHandler _handler;

    public CreateProductModel(CreateProductCommandHandler handler)
    {
        _handler = handler;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var command = new CreateProductCommand { Name = "Product", Price = 99.99m };
        var result = await _handler.HandleAsync(command);
        return RedirectToPage();
    }
}
```

### Using Queries in Razor Pages
```csharp
public async Task OnGetAsync()
{
    var products = await _repository.GetAllAsync();
    Products = products;
}
```

---

## Key Rules
- **Commands** = Write operations (insert, update, delete)
- **Queries** = Read operations (no side effects)
- Always register handlers in `Program.cs`
- Use dependency injection for repositories
- Keep handlers stateless and testable

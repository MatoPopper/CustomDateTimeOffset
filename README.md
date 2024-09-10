
# CustomDateTime Library

**CustomDateTime** is a library for working with dates and time offsets in databases that do not natively support **`DateTimeOffset`**, such as **MySQL** and **PostgreSQL**. The library allows preserving and processing time offset information using a custom **CustomDateTime** class, which is designed to provide functionality similar to **`DateTimeOffset`** in databases without native support.

## Features
- **CustomDateTime** stores information about both the time (DateTime) and the time offset (Offset).
- It provides methods for converting between **DateTimeOffset** and **CustomDateTime**.
- Includes an Entity Framework Core **ValueConverter** to integrate **CustomDateTime** directly into your models.
- Supports JSON serialization using a custom **JsonConverter**.

## Installation
First, add the library to your project. If you're distributing the library via **NuGet**, you can install it as follows:

```bash
dotnet add package CustomDateTimeLibrary
```

If you're adding the library manually from GitHub, simply add it as a project reference.

## Usage in Entity Framework Core

### 1. Defining Models
To use **CustomDateTime** in your **Entity Framework Core** models, simply replace **`DateTime`** or **`DateTimeOffset`** properties with **CustomDateTime**. For example:

```csharp
using Core.Models.Shared;

public class Event
{
    public int Id { get; set; }
    
    // Using CustomDateTime instead of DateTimeOffset
    public CustomDateTime ValidFrom { get; set; }
}
```

### 2. Configuring `OnModelCreating` in DbContext
If you want to use **CustomDateTime** with Entity Framework Core, you'll need to add a custom **ValueConverter** in the **OnModelCreating** method in your **DbContext**:

```csharp
using Microsoft.EntityFrameworkCore;
using Core.Converters;
using Core.Models.Shared;

public class ApplicationDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>()
            .Property(e => e.ValidFrom)
            .HasConversion(new CustomDateTimeConverter());
    }
}
```

### 3. Registering in `Program.cs` or `Startup.cs`
Depending on which version of **ASP.NET Core** you're using, you’ll need to register **CustomDateTimeSerializer** in **Program.cs** or **Startup.cs** to enable the correct JSON serialization and deserialization of date-time fields:

#### Program.cs (for .NET 6 and above):

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new CustomDateTimeSerializer());
    });

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
```

#### Startup.cs (for older versions of .NET Core):

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new CustomDateTimeSerializer());
        });
}
```

### 4. Example Usage in a Controller

Once everything is set up, you can start using **CustomDateTime** in your **ASP.NET Core** API controllers. For example:

```csharp
[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EventsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _context.Events.ToListAsync();
        return Ok(events);
    }
}
```

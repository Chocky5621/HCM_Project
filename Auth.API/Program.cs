var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.MapGet("/", () => "Hello from Auth API!");
app.MapControllers();

app.Run();

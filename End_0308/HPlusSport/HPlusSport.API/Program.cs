using HPlusSport.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Registers controllers as services for dependency injection

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Adds support for endpoint exploration and API documentation

builder.Services.AddSwaggerGen();
// Registers the Swagger generator to produce OpenAPI specifications

builder.Services.AddDbContext<ShopContext>(options =>
{
    // Configures Entity Framework Core to use an in-memory database for the ShopContext
    options.UseInMemoryDatabase("Shop");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enables Swagger and Swagger UI only in development environment for API documentation and testing
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Enforces HTTPS for all incoming requests

app.UseAuthorization();
// Adds authorization middleware to the request pipeline

app.MapControllers();
// Maps the controller routes to the endpoints

app.Run();
// Runs the application

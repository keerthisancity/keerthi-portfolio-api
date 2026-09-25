
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortfolioApi;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext
// Prefer environment variable DEFAULTCONNECTION to avoid storing secrets in source-controlled config.
var connectionString = Environment.GetEnvironmentVariable("DEFAULTCONNECTION")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found. Set environment variable DEFAULTCONNECTION or add it to appsettings.json (not recommended for public repos).");
}

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    app.Run();
}
catch (Exception ex)
{
    // If host fails to start, log the exception so it's visible in logs
    var loggerFactory = app.Services.GetService<ILoggerFactory>();
    var logger = loggerFactory?.CreateLogger("Startup");
    logger?.LogCritical(ex, "Host terminated unexpectedly");
    throw;
}

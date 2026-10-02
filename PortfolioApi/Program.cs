
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortfolioApi;

var builder = WebApplication.CreateBuilder(args);

// Configure logging FIRST
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var loggerFactory = builder.Services.BuildServiceProvider().GetService<ILoggerFactory>();
var logger = loggerFactory?.CreateLogger("Startup") ?? throw new InvalidOperationException("Logger not configured");

// Register DbContext
// Prefer environment variable DEFAULTCONNECTION to avoid storing secrets in source-controlled config.
var connectionString = Environment.GetEnvironmentVariable("DEFAULTCONNECTION")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");

logger.LogInformation("=== ASP.NET Core Startup ===");
logger.LogInformation($"Environment: {builder.Environment.EnvironmentName}");
logger.LogInformation($"DEFAULTCONNECTION env var present: {!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("DEFAULTCONNECTION"))}");

if (string.IsNullOrWhiteSpace(connectionString))
{
    logger.LogCritical("CONNECTION STRING NOT FOUND!");
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found. Set environment variable DEFAULTCONNECTION or add it to appsettings.json (not recommended for public repos).");
}

logger.LogInformation($"Connection String (first 80 chars): {connectionString.Substring(0, Math.Min(80, connectionString.Length))}...");

try
{
    builder.Services.AddDbContext<PortfolioDbContext>(options =>
        options.UseSqlServer(connectionString));
    logger.LogInformation("DbContext registered successfully");
}
catch (Exception ex)
{
    logger.LogCritical(ex, "Failed to register DbContext");
    throw;
}

// Register controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    logger.LogInformation("Starting application...");
    app.Run();
}
catch (Exception ex)
{
    // If host fails to start, log the exception so it's visible in logs
    var appLoggerFactory = app.Services.GetService<ILoggerFactory>();
    var appLogger = appLoggerFactory?.CreateLogger("Startup");
    appLogger?.LogCritical(ex, "Host terminated unexpectedly");
    throw;
}

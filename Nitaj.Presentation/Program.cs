using Microsoft.EntityFrameworkCore;
using Nitaj.Infrastructure.Context;
using Nitaj.IoC;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DataContext");

builder.Services.AddDbContext<NitajDbContext>(options =>
    options.UseSqlServer(connectionString));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register services in the DI container
DependencyContainer.RegisterServices(builder.Services);
// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()                  // Log to console
    .WriteTo.File("logs/log.txt",       // Log to file (creates a new log file each day)
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Use Serilog for ASP.NET Core logging
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

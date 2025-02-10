using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

using Microsoft.Data.SqlClient;
using MyPortfolio.Services; // Or System.Data.SqlClient if you prefer

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add a database service using the connection string from appsettings.json
builder.Services.AddSingleton<UsersService>(); // Register your custom service

// Add CORS policy to allow the frontend (Angular) to connect
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()  // Allows all origins (for dev purposes)
               .AllowAnyMethod()  // Allows any HTTP method (GET, POST, etc.)
               .AllowAnyHeader(); // Allows any header
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS (allow frontend to make requests to the backend)
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

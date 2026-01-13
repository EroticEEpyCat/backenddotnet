
using Microsoft.EntityFrameworkCore;
using webdotnetapp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Modern OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Development tools
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // <-- modern replacement for UseSwagger + UseSwaggerUI
}

// Serve frontend
app.UseDefaultFiles(); // serves index.html
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Shared;
using MeterInspectionDB;
using System.Data;
using System.Data.SqlClient;
using MeterInspectionAPI;
using MeterInspectionAPI.Models;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);


// ===========================
// Register Config
// ===========================
builder.Services.AddSingleton<GpiConfig>(sp =>
{
    return GPI.GetConfig();
});


// ===========================
// Register Services
// ===========================
builder.Services.AddMeterInspectionDB();

builder.Services.AddScoped<OFFline_Online>();

builder.Services.AddScoped<
    ConnectionStatusService>();


// ===========================
// Dynamic DB Connection
// Decide Server / Local
// Per Request
// ===========================
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var config =
        sp.GetRequiredService<GpiConfig>();

    var connectionStatus =
        sp.GetRequiredService<
            ConnectionStatusService>();

    bool isOnline =
        connectionStatus.IsOnline();

    return new SqlConnection(
        isOnline
            ? config.ConnString_Server
            : config.ConnString_Local
    );
});


// ===========================
// Controllers
// ===========================
builder.Services.AddControllers();


// ===========================
// Swagger
// ===========================
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MeterInspection API",
        Version = "v1"
    });
});

var app = builder.Build();


// ===========================
// HTTP Pipeline
// ===========================

// Swagger
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "MeterInspection API V1");

    // Open Swagger مباشرة
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
using Microsoft.OpenApi;
using Shared;
using Microsoft.EntityFrameworkCore;
using MeterInspectionAPI.Models;
using MeterInspectionDB;
using System.Data;
using System.Data.SqlClient;


var builder = WebApplication.CreateBuilder(args);

//var connectionString_Server = GPI.GetConfig().ConnString_Server; //builder.Configuration.GetConnectionString("DefaultConnection");

////var connectionString_Local = GPI.GetConfig().ConnString_Local;

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(GPI.GetConfig().ConnString_Server));

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var config = sp.GetRequiredService<GpiConfig>();

    return new SqlConnection(
        Shared.GPI.Status == "Server"
            ? config.ConnString_Server
            : config.ConnString_Local
    );
});

builder.Services.AddSingleton<GpiConfig>(sp =>
{
    return GPI.GetConfig();
});

builder.Services.AddMeterInspectionDB();
// Register OFFline_Online
builder.Services.AddScoped<OFFline_Online>();


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MeterInspection API",
        Version = "v1"
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var offlineOnline = scope.ServiceProvider
            .GetRequiredService<OFFline_Online>();

       await offlineOnline.GetConnectionStatusAsync();

         
    }
    catch (Exception ex)
    {
         
    }
}
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeterInspection API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();

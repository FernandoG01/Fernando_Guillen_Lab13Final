using System.Text.Json.Serialization;
using Fernando_Guillen_Lab13Final.Data;
using Fernando_Guillen_Lab13Final.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LINQExampleDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<ExcelReportService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/", () => "API funcionando correctamente con SQL Server");

// PRUEBA DE CONEXIÓN A AZURE SQL
app.MapGet("/test-db", async (LINQExampleDbContext db) =>
{
    var totalUsuarios = await db.Users.CountAsync();

    return Results.Ok(new
    {
        TotalUsuarios = totalUsuarios
    });
});

app.Run();
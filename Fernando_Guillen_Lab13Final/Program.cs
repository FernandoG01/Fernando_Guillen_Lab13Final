using System.Text.Json.Serialization;
using Fernando_Guillen_Lab13Final.Data;
using Fernando_Guillen_Lab13Final.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LINQExampleDbContext>(options =>
    options.UseInMemoryDatabase("SistemaTicketsReportes")
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

// Swagger activo
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Endpoint base
app.MapGet("/", () => "API Fernando Guillen Lab13 Final funcionando correctamente.");

app.Run();
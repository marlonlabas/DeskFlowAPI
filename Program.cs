using DeskFlowAPI;
using DeskFlowAPI.Middlewares;
using DeskFlowAPI.Repositories;
using DeskFlowAPI.Repositories.Interfaces;
using DeskFlowAPI.Services;
using DeskFlowAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddScoped<ICategoriasService, CategoriasService>();
builder.Services.AddScoped<ICategoriasInterface, CategoriaRepository>();

var app = builder.Build();

app.UseMiddleware<ErrorMiddleware>();

app.MapOpenApi();

app.UseSwaggerUI (op =>
{
    op.SwaggerEndpoint("/openapi/v1.json", "v1");
});

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
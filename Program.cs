using DeskFlowAPI;
using DeskFlowAPI.Middlewares;
using DeskFlowAPI.Repositories;
using DeskFlowAPI.Repositories.Interfaces;
using DeskFlowAPI.Services;
using DeskFlowAPI.Services.Interfaces;
using DeskFlowAPI.Converters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(op =>
{
    op.AddDocumentTransformer((doc, _, _) =>
    {
        doc.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>();
        doc.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "Identity access token",
            In = ParameterLocation.Header,
            Description = "Cole apenas o accessToken retornado por /auth/login. O Swagger adiciona 'Bearer' automaticamente."
        };
        return Task.CompletedTask;
    });

    op.AddOperationTransformer((operation, context, _) =>
    {
        if (context.Description.ActionDescriptor.EndpointMetadata.Any(metadata => metadata is IAuthorizeData))
        {
            operation.Security ??= [];
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", context.Document, null)] = []
            });
        }
        return Task.CompletedTask;
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateTimeSemFracaoConverter());
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthorization();

builder.Services.AddScoped<ICategoriasService, CategoriasService>();
builder.Services.AddScoped<ICategoriasInterface, CategoriaRepository>();
builder.Services.AddScoped<IChamadosService, ChamadosService>();
builder.Services.AddScoped<IChamadosInterface, ChamadosRepository>();

var app = builder.Build();

app.UseMiddleware<ErrorMiddleware>();

app.MapOpenApi();

app.UseSwaggerUI (op =>
{
    op.SwaggerEndpoint("/openapi/v1.json", "v1");
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/auth").MapIdentityApi<IdentityUser>();

app.MapControllers();
app.Run();
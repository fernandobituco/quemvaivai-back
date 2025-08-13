using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuemVaiVai.Api.Configurations;
using QuemVaiVai.Api.Extensions;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration);

var frontendUrl = builder.Configuration["FRONT_END_URL"] ?? "http://localhost:3000";

ApiConfiguration.AddApiServices(builder.Services, frontendUrl, builder.Configuration);

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("Jwt"));

EmailConfiguration.AddEmailConfiguration(builder.Services, builder.Configuration);

DependencyInjection.AddDependencyInjections(builder.Services);

builder.Services.AddControllers();

var stringConnection = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

DatabaseConfiguration.AddDataBaseConfiguration(builder.Services, stringConnection);

SwaggerConfiguration.AddSwaggerConfiguration(builder.Services);

AutoMapperConfiguration.AddAutoMapperConfiguration(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}

ApiConfiguration.UseApiConfiguration(app);

app.MapControllers();

SwaggerConfiguration.UseSwaggerConfiguration(app);

app.UseHttpsRedirection();

app.Run();

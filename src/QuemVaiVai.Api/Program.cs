using Microsoft.EntityFrameworkCore;
using QuemVaiVai.Api.Configurations;
using QuemVaiVai.Api.Extensions;
using QuemVaiVai.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

var frontendUrl = builder.Configuration["FRONTEND_URL"] ?? "http://localhost:3000";

ApiConfiguration.AddApiServices(builder.Services);

DependencyInjection.AddDependencyInjections(builder.Services);

CorsConfiguration.AddCorsConfiguration(builder.Services, frontendUrl);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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

CorsConfiguration.UseCorsConfiguration(app);

app.MapControllers();

SwaggerConfiguration.UseSwaggerConfiguration(app);

app.UseHttpsRedirection();

app.Run();

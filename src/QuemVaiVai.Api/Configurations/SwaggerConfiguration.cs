namespace QuemVaiVai.Api.Extensions;

public static class  SwaggerConfiguration
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Quem Vai Vai API",
                Version = "v1",
                Description = "API for Quem Vai Vai application"
            });
        });
    }

    public static void UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quem Vai Vai API V1");
            c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
        });
    }
}
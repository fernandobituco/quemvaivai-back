namespace QuemVaiVai.Api.Configurations;

public static class CorsConfiguration
{
    public static void AddCorsConfiguration(this IServiceCollection services, string frontendUrl)
    {
        Console.WriteLine("frontenturl: " + frontendUrl);
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                policy => policy
                    .WithOrigins(frontendUrl)
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
    }

    public static void UseCorsConfiguration(this IApplicationBuilder app)
    {
        app.UseCors();
    }
}
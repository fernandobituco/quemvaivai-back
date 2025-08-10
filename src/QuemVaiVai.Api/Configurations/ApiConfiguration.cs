using QuemVaiVai.Api.Utils;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Api.Configurations;

public static class ApiConfiguration
{
    public static void AddApiServices(this IServiceCollection services, string frontendUrl)
    {
        // Adiciona o IHttpContextAccessor para acessar o contexto HTTP
        services.AddHttpContextAccessor();

        // Configura o AutoMapper para mapear DTOs e entidades
        //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Configura os controladores da API
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null; // Manter nomes de propriedades como estão
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true; //Tratar manualmente erros de modelstate
            });

        Console.WriteLine("frontenturl: " + frontendUrl);
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                policy => policy
                    .WithOrigins(frontendUrl.Split(';'))
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithMethods("GET", "POST", "PUT", "DELETE"));
        });
    }

    public static void UseApiConfiguration(this IApplicationBuilder app)
    {
        app.UseCors("AllowFrontend");
        app.UseRouting();
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Mapeia os controladores da API
        });
    }
}
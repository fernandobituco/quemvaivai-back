namespace QuemVaiVai.Api.Configurations;

public static class ApiConfiguration
{
    public static void AddApiServices(this IServiceCollection services)
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
            });
    }

    public static void UseApiConfiguration(this IApplicationBuilder app)
    {
        // Configura o pipeline de middleware da API
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Mapeia os controladores da API
        });
    }
}
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuemVaiVai.Api.Utils;
using QuemVaiVai.Domain.Entities;
using System.Text;

namespace QuemVaiVai.Api.Configurations;

public static class ApiConfiguration
{
    public static void AddApiServices(this IServiceCollection services, string frontendUrl, IConfiguration configuration)
    {
        // Adiciona o IHttpContextAccessor para acessar o contexto HTTP
        services.AddHttpContextAccessor();

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

        // CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                policy => policy
                    .WithOrigins(frontendUrl.Split(';'))
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithMethods("GET", "POST", "PUT", "DELETE"));
        });

        // Configuração JWT
        var tokenSettings = configuration.GetSection("Jwt").Get<TokenSettings>() ?? throw new ArgumentNullException("Token settings not found");
        var key = Encoding.UTF8.GetBytes(tokenSettings.Key);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = tokenSettings.Issuer,
                ValidAudience = tokenSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        services.AddAuthorization();
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
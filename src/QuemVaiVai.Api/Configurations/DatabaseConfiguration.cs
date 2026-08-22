using Microsoft.EntityFrameworkCore;
using Npgsql;
using QuemVaiVai.Infrastructure.Contexts;
using System.Data;

namespace QuemVaiVai.Api.Configurations;

public static class DatabaseConfiguration
{
    public static void AddDataBaseConfiguration(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
        });

        services.AddScoped<IDbConnection>(sp =>
            new NpgsqlConnection(connectionString)
        );

        services.AddSingleton<DapperQueryContext>();
    }
}
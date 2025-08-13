using Microsoft.EntityFrameworkCore;
using Npgsql;
using QuemVaiVai.Infrastructure.Contexts;
using System.Data;

namespace QuemVaiVai.Api.Configurations;

public static class DatabaseConfiguration
{
    public static void AddDataBaseConfiguration(this IServiceCollection services, string connectionString)
    {
        Console.WriteLine("Connection String: " + connectionString);
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IDbConnection>(sp =>
            new NpgsqlConnection(connectionString)
        );

        services.AddSingleton<DapperQueryContext>();
    }
}
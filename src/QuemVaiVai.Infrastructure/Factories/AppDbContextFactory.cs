using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using QuemVaiVai.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Infrastructure.Factories
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            var connectionString = Environment.GetEnvironmentVariable("QUEMVAIVAI_CONNECTION_STRING");
            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}

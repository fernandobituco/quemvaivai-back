using System.Reflection;

namespace QuemVaiVai.Api.Configurations;

public static class AutoMapperConfiguration
{
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        // Register AutoMapper with all profiles in the current assembly
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Optionally, you can specify a specific profile if needed
        // services.AddAutoMapper(typeof(YourProfileClass));
    }
}
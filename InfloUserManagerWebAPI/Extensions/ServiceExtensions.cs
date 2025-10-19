namespace InfloUserManagerWebAPI.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSqlConnections(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InfloUserManagerDbContext>(
            options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("InfloUserManager")));
    }

    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddTransient<IUserHandler, UserHandler>();
        services.AddTransient<IUserStatusHandler, UserStatusHandler>();
    }
}
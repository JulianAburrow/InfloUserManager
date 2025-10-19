namespace InfloUserManagerWebAPI.Data;

public class InfloUserManagerDbContext(DbContextOptions<InfloUserManagerDbContext> options) : DbContext(options)
{
    public DbSet<UserModel> Users => Set<UserModel>();

    public DbSet<UserStatusModel> UserStatuses => Set<UserStatusModel>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
            .Where(p => p.ClrType == typeof(string))))
        {
            property.SetIsUnicode(false);
        }

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserStatusConfiguration());
    }
}

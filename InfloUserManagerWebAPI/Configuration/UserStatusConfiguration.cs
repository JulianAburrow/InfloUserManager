namespace InfloUserManagerWebAPI.Configuration;

public class UserStatusConfiguration : IEntityTypeConfiguration<UserStatusModel>
{
    public void Configure(EntityTypeBuilder<UserStatusModel> builder)
    {
        builder.ToTable("UserStatus");
        builder.HasKey(nameof(UserStatusModel.StatusId));
        builder.HasMany(e => e.Users)
            .WithOne(e => e.Status)
            .HasForeignKey(e => e.StatusId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

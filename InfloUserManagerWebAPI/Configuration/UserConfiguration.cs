namespace InfloUserManagerWebAPI.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("User");
        builder.HasKey(nameof(UserModel.UserId));
        builder.HasOne(e => e.Status)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.StatusId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}

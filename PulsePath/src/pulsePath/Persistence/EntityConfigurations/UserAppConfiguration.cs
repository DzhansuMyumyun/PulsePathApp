using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserAppConfiguration : IEntityTypeConfiguration<UserApp>
{
    public void Configure(EntityTypeBuilder<UserApp> builder)
    {
        builder.ToTable("UserApps").HasKey(ua => ua.Id);

        builder.Property(ua => ua.Id).HasColumnName("Id").IsRequired();
        builder.Property(ua => ua.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(ua => ua.LastName).HasColumnName("LastName").IsRequired();
        builder.Property(ua => ua.Email).HasColumnName("Email").IsRequired();
        builder.Property(ua => ua.PasswordHash).HasColumnName("PasswordHash").IsRequired();
        builder.Property(ua => ua.CreatedAt).HasColumnName("CreatedAt").IsRequired();
        builder.Property(ua => ua.UpdatedAt).HasColumnName("UpdatedAt").IsRequired();
        builder.Property(ua => ua.Profile).HasColumnName("Profile").IsRequired();
        builder.Property(ua => ua.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ua => ua.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ua => ua.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ua => !ua.DeletedDate.HasValue);
    }
}
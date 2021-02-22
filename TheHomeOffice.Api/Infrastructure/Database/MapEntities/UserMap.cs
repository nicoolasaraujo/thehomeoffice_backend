using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheHomeOffice.Api.Domain.Models;

namespace TheHomeOffice.Api.Infrastructure.Database
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => new { x.IsAdmin }).IsUnique(true);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Password).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(x => x.UserAddress).HasColumnType("jsonb");

            builder.HasData(new User() { Id = -1, Email = "admin@admin.com", Name = "admin", Password = "admin", IsAdmin = true});
        }
    }
}
using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.API.Data.Mappings
{
    public class UserAccountMapping : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable("UserAccount");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.UserName).IsRequired(true).HasColumnType("NVARCHAR").HasMaxLength(100);
            builder.Property(c => c.Password).IsRequired(true).HasColumnType("NVARCHAR").HasMaxLength(100);
            builder.Property(c => c.Role).IsRequired(true).HasColumnType("VARCHAR").HasMaxLength(20);
        }
    }
}

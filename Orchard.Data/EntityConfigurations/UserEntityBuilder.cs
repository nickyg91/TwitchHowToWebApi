using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orchard.Data.Entities;

namespace Orchard.Data.EntityConfigurations;

public class UserEntityBuilder : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.HasKey(x => x.Id).HasName("pk_user");
        
        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(512)
            .HasColumnName("email");

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(64)
            .HasColumnName("first_name");
        
        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(64)
            .HasColumnName("last_name");

        builder
            .Property(x => x.BirthDate)
            .HasColumnName("birth_date");
        
        builder
            .Property(x => x.EmailVerificationGuid)
            .IsRequired()
            .HasColumnName("email_verification_guid")
            .HasDefaultValueSql("gen_random_uuid()");
        
        builder.Property(x => x.CreatedAdUtc)
            .HasColumnName("created_at_utc")
            .HasDefaultValueSql("timezone('UTC', now())");

        builder.Property(x => x.HashedPassword)
            .HasMaxLength(512)
            .HasColumnName("hashed_password");
    }
}
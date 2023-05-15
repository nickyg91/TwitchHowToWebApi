using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orchard.Data.Entities;

namespace Orchard.Data.EntityConfigurations;

public class BasketEntityBuilder : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.ToTable("basket");
        builder.HasKey(x => x.Id).HasName("pk_basket");
        builder.Property(x => x.Id).HasColumnName("id").UseIdentityColumn();

        builder.Property(x => x.UserId).HasColumnName("user_id");
        
        builder.Property(x => x.CreatedAdUtc)
            .HasColumnName("created_at_utc")
            .HasDefaultValueSql("timezone('UTC', now())");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Baskets)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_basket_user");
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orchard.Data.Entities;

namespace Orchard.Data.EntityConfigurations;

public class BasketFruitEntityBuilder : IEntityTypeConfiguration<BasketFruit>
{
    public void Configure(EntityTypeBuilder<BasketFruit> builder)
    {
        builder.ToTable("basket_fruit");
        builder.HasKey(x => x.Id).HasName("pk_basket_fruit_id");
        builder.Property(x => x.Id).IsRequired().UseIdentityColumn().HasColumnName("id");
        builder.Property(x => x.Amount).IsRequired().HasColumnName("amount");
        builder.Property(x => x.FruitId).IsRequired().HasColumnName("fruit_id");
        builder.Property(x => x.BasketId).IsRequired().HasColumnName("basket_id");
        builder.HasOne(x => x.Basket)
            .WithMany(x => x.Fruit)
            .HasForeignKey(x => x.BasketId)
            .HasConstraintName("fk_basket_basket_fruit")
            .IsRequired();
        builder.Property(x => x.CreatedAdUtc).HasColumnName("created_at_utc").HasDefaultValueSql("timezone('UTC', now())");
    }
}
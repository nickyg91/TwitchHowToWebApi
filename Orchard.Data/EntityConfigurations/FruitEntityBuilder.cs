using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orchard.Data.Entities;

namespace Orchard.Data.EntityConfigurations;

public class FruitEntityBuilder : IEntityTypeConfiguration<Fruit>
{
    public void Configure(EntityTypeBuilder<Fruit> builder)
    {
        builder.ToTable("fruit");
        builder.HasKey(x => x.Id).HasName("pk_fruit_id");
        builder.Property(x => x.Id).IsRequired().HasColumnName("id").UseIdentityColumn();
        
        builder.Property(x => x.Name)
            .IsRequired()
            .IsUnicode()
            .HasColumnName("name")
            .HasMaxLength(512);
        
        builder.Property(x => x.SkuNumber).IsRequired().HasColumnName("sku_number").HasMaxLength(512);
        builder.Property(x => x.CountryOfOrigin).IsRequired().HasColumnName("country_of_origin").HasMaxLength(128);

        builder.Property(x => x.PiecesInInventory)
            .IsRequired()
            .HasColumnName("pieces_in_inventory")
            .HasDefaultValue(0);

        builder.Property(x => x.IsArchived)
            .IsRequired()
            .HasColumnName("is_archived")
            .HasDefaultValue(false);
        
        builder.Property(x => x.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .HasDefaultValueSql("timezone('UTC', now())");
    }
}
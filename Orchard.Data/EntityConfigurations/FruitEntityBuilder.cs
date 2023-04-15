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
        builder.Property(x => x.FruitShape).IsRequired().HasColumnName("fruit_shape");
        builder.Property(x => x.FruitType).IsRequired().HasColumnName("fruit_type");
    }
}
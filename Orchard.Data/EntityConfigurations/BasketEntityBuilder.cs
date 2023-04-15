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
    }
}
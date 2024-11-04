
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.CatalogoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class CatalogoItemCatalogoEntityTypeConfiguration : IEntityTypeConfiguration<CatalogoItemCatalogo>
    {
        public void Configure(EntityTypeBuilder<CatalogoItemCatalogo> builder)
        {
            builder.ToTable("catalogoitemcatalogo", "programagestao");

            builder.HasKey(p => p.CatalogoItemCatalogoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.CatalogoItemCatalogoId)
                   .HasColumnName("catalogoitemcatalogoid")
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.CatalogoId).HasColumnName("catalogoid");

            builder.Property(p => p.ItemCatalogoId).HasColumnName("itemcatalogoid");

            builder.HasOne(p => p.Catalogo)
                   .WithMany(p => p.ItensCatalogo)
                   .HasForeignKey(p => p.CatalogoId)
                   .HasConstraintName("fk_catalogoitemcatalogo_catalogo");

            builder.HasOne(p => p.ItemCatalogo)
                   .WithMany(p => p.Catalogos)
                   .HasForeignKey(p => p.ItemCatalogoId)
                   .HasConstraintName("fk_catalogoitemcatalogo_itemcatalogo");

        }

    }
}

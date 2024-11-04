
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.CatalogoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class ItemCatalogoAssuntoEntityTypeConfiguration : IEntityTypeConfiguration<ItemCatalogoAssunto>
    {
        public void Configure(EntityTypeBuilder<ItemCatalogoAssunto> builder)
        {
            builder.ToTable("itemcatalogoassunto", "programagestao");

            builder.HasKey(p => p.ItemCatalogoAssuntoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.ItemCatalogoAssuntoId)
                   .HasColumnName("itemcatalogoassuntoid")
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.ItemCatalogoId).HasColumnName("itemcatalogoid");

            builder.Property(p => p.AssuntoId).HasColumnName("assuntoid");

            builder.HasOne(p => p.Assunto)
                   .WithMany()
                   .HasForeignKey(p => p.AssuntoId)
                   .HasConstraintName("fk_itemcatalogoassunto_assunto");

        }

    }
}

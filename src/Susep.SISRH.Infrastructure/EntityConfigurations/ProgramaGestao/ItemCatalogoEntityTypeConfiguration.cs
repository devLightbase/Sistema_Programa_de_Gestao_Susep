
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.CatalogoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class ItemCatalogoEntityTypeConfiguration : IEntityTypeConfiguration<ItemCatalogo>
    {
        public void Configure(EntityTypeBuilder<ItemCatalogo> builder)
        {
            builder.ToTable("itemcatalogo", "programagestao");

            builder.HasKey(p => p.ItemCatalogoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode)
                   .Ignore(p => p.TempoExecucaoPreviamenteDefinido);

            builder.Property(p => p.ItemCatalogoId).HasColumnName("itemcatalogoid");
            builder.Property(p => p.Titulo).HasColumnName("titulo");
            builder.Property(p => p.FormaCalculoTempoItemCatalogoId).HasColumnName("calculotempoid");
            builder.Property(p => p.PermiteTrabalhoRemoto).HasColumnName("permiteremoto");
            builder.Property(p => p.TempoExecucaoPresencial).HasColumnName("tempopresencial");
            builder.Property(p => p.TempoExecucaoRemoto).HasColumnName("temporemoto");
            builder.Property(p => p.Descricao).HasColumnName("descricao");
            builder.Property(p => p.Complexidade).HasColumnName("complexidade");
            builder.Property(p => p.DefinicaoComplexidade).HasColumnName("definicaocomplexidade");
            builder.Property(p => p.EntregasEsperadas).HasColumnName("entregasesperadas");

            builder.HasOne(p => p.FormaCalculoTempoItemCatalogo)
                   .WithMany(p => p.ItensCatalogo)
                   .HasForeignKey(p => p.FormaCalculoTempoItemCatalogoId)
                   .HasConstraintName("fk_itemcatalogo_calculotempo");

            builder.HasMany(p => p.Assuntos)
                   .WithOne()
                   .HasForeignKey(p => p.ItemCatalogoId)
                   .HasConstraintName("fk_itemcatalogoassunto_itemcatalogo");
        }

    }
}

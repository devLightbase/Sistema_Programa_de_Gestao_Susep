
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PlanoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PlanoTrabalhoAtividadeItemEntityTypeConfiguration : IEntityTypeConfiguration<PlanoTrabalhoAtividadeItem>
    {
        public void Configure(EntityTypeBuilder<PlanoTrabalhoAtividadeItem> builder)
        {
            builder.ToTable("planotrabalhoatividadeitem", "programagestao");

            builder.HasKey(p => p.PlanoTrabalhoAtividadeItemId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.PlanoTrabalhoAtividadeItemId)
                .HasColumnName("planotrabalhoatividadeitemid")
                .ValueGeneratedOnAdd(); ;
            
            builder.Property(p => p.PlanoTrabalhoAtividadeId).HasColumnName("planotrabalhoatividadeid");
            builder.Property(p => p.ItemCatalogoId).HasColumnName("itemcatalogoid");

            builder.HasOne(p => p.ItemCatalogo)
                   .WithMany(p => p.PlanosTrabalhoAtividadesItens)
                   .HasForeignKey(p => p.ItemCatalogoId)
                   .HasConstraintName("fk_planotrabalhoitematividade_itemcatalogo");

            builder.HasOne(p => p.PlanoTrabalhoAtividade)
                   .WithMany(p => p.ItensCatalogo)
                   .HasForeignKey(p => p.PlanoTrabalhoAtividadeId)
                   .HasConstraintName("fk_planotrabalhoitematividade_planotrabalhoatividade");

        }

    }
}

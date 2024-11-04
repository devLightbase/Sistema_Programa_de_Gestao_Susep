
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PlanoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PlanoTrabalhoAtividadeCriterioEntityTypeConfiguration : IEntityTypeConfiguration<PlanoTrabalhoAtividadeCriterio>
    {
        public void Configure(EntityTypeBuilder<PlanoTrabalhoAtividadeCriterio> builder)
        {
            builder.ToTable("planotrabalhoatividadecriterio", "programagestao");

            builder.HasKey(p => p.PlanoTrabalhoAtividadeCriterioId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.PlanoTrabalhoAtividadeCriterioId)
                .HasColumnName("planotrabalhoatividadecriterioid")
                .ValueGeneratedOnAdd(); ;
            
            builder.Property(p => p.PlanoTrabalhoAtividadeId).HasColumnName("planotrabalhoatividadeid");
            builder.Property(p => p.CriterioId).HasColumnName("criterioid");

            builder.HasOne(p => p.Criterio)
                   .WithMany(p => p.CriteriosAtividadesPlanos)
                   .HasForeignKey(p => p.CriterioId)
                   .HasConstraintName("fk_planotrabalhocriterioatividade_criterio");

            builder.HasOne(p => p.PlanoTrabalhoAtividade)
                   .WithMany(p => p.Criterios)
                   .HasForeignKey(p => p.PlanoTrabalhoAtividadeId)
                   .HasConstraintName("fk_planotrabalhocriterioatividade_planotrabalhoatividade");

        }

    }
}

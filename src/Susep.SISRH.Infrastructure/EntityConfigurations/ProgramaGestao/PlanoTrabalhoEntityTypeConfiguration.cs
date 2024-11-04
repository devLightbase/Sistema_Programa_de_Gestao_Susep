
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PlanoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PlanoTrabalhoEntityTypeConfiguration : IEntityTypeConfiguration<PlanoTrabalho>
    {
        public void Configure(EntityTypeBuilder<PlanoTrabalho> builder)
        {
            builder.ToTable("planotrabalho", "programagestao");

            builder.HasKey(p => p.PlanoTrabalhoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.PlanoTrabalhoId).HasColumnName("planotrabalhoid");
            builder.Property(p => p.UnidadeId).HasColumnName("unidadeid");
            builder.Property(p => p.DataInicio).HasColumnName("datainicio");
            builder.Property(p => p.DataFim).HasColumnName("datafim");
            builder.Property(p => p.SituacaoId).HasColumnName("situacaoid");
            builder.Property(p => p.PrazoComparecimento).HasColumnName("tempocomparecimento");
            builder.Property(p => p.TotalServidoresSetor).HasColumnName("totalservidoressetor");
            builder.Property(p => p.TempoFaseHabilitacao).HasColumnName("tempofasehabilitacao");
            builder.Property(p => p.TermoAceite).HasColumnName("termoaceite");


            builder.HasOne(p => p.Unidade)
                   .WithMany(p => p.PlanosTrabalho)
                   .HasForeignKey(p => p.UnidadeId)
                   .HasConstraintName("fk_planotrabalho_unidade");

            builder.HasOne(p => p.Situacao)
                   .WithMany(p => p.PlanosTrabalho)
                   .HasForeignKey(p => p.SituacaoId)
                   .HasConstraintName("fk_platotrabalho_situacao");

        }

    }
}

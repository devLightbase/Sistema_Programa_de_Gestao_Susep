
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PactoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PactoTrabalhoHistoricoEntityTypeConfiguration : IEntityTypeConfiguration<PactoTrabalhoHistorico>
    {
        public void Configure(EntityTypeBuilder<PactoTrabalhoHistorico> builder)
        {
            builder.ToTable("pactotrabalhohistorico", "programagestao");

            builder.HasKey(p => p.PactoTrabalhoHistoricoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.PactoTrabalhoHistoricoId)
                   .HasColumnName("pactotrabalhohistoricoid")
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.PactoTrabalhoId).HasColumnName("pactotrabalhoid");
            builder.Property(p => p.SituacaoId).HasColumnName("situacaoid");
            builder.Property(p => p.Observacoes).HasColumnName("observacoes");
            builder.Property(p => p.ResponsavelOperacao).HasColumnName("responsaveloperacao");
            builder.Property(p => p.DataOperacao).HasColumnName("dataoperacao");
                        

            builder.HasOne(p => p.PactoTrabalho)
                   .WithMany(p => p.Historico)
                   .HasForeignKey(p => p.PactoTrabalhoId)
                   .HasConstraintName("fk_pactotrabalhohistorico_pactotrabalho");

            builder.HasOne(p => p.Situacao)
                   .WithMany(p => p.HistoricoPactosTrabalho)
                   .HasForeignKey(p => p.SituacaoId)
                   .HasConstraintName("fk_pactotrabalhohistorico_situacao");


        }

    }
}

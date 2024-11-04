
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PactoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PactoTrabalhoEntityTypeConfiguration : IEntityTypeConfiguration<PactoTrabalho>
    {
        public void Configure(EntityTypeBuilder<PactoTrabalho> builder)
        {
            builder.ToTable("pactotrabalho", "programagestao");

            builder.HasKey(p => p.PactoTrabalhoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Ignore(p => p.DiasNaoUteis);

            builder.Property(p => p.PactoTrabalhoId).HasColumnName("pactotrabalhoid");
            builder.Property(p => p.PlanoTrabalhoId).HasColumnName("planotrabalhoid");
            builder.Property(p => p.UnidadeId).HasColumnName("unidadeid");
            builder.Property(p => p.PessoaId).HasColumnName("pessoaid");
            builder.Property(p => p.DataInicio).HasColumnName("datainicio");
            builder.Property(p => p.DataFim).HasColumnName("datafim");
            builder.Property(p => p.ModalidadeExecucaoId).HasColumnName("formaexecucaoid");
            builder.Property(p => p.SituacaoId).HasColumnName("situacaoid");
            builder.Property(p => p.TermoAceite).HasColumnName("termoaceite");
            builder.Property(p => p.CargaHorariaDiaria).HasColumnName("cargahorariadiaria");
            builder.Property(p => p.PercentualExecucao).HasColumnName("percentualexecucao");
            builder.Property(p => p.RelacaoPrevistoRealizado).HasColumnName("relacaoprevistorealizado");
            builder.Property(p => p.TempoTotalDisponivel).HasColumnName("tempototaldisponivel");            

            builder.HasOne(p => p.Unidade)
                   .WithMany(p => p.PactosTrabalho)
                   .HasForeignKey(p => p.UnidadeId)
                   .HasConstraintName("fk_pactotrabalho_unidade");

            builder.HasOne(p => p.Pessoa)
                   .WithMany(p => p.PactosTrabalho)
                   .HasForeignKey(p => p.PessoaId)
                   .HasConstraintName("fk_pactotrabalho_pessoa");

        }

    }
}

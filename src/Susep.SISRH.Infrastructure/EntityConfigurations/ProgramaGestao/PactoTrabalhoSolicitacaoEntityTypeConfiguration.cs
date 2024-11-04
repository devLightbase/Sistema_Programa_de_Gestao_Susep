
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PactoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PactoTrabalhoSolicitacaoEntityTypeConfiguration : IEntityTypeConfiguration<PactoTrabalhoSolicitacao>
    {
        public void Configure(EntityTypeBuilder<PactoTrabalhoSolicitacao> builder)
        {
            builder.ToTable("pactotrabalhosolicitacao", "programagestao");

            builder.HasKey(p => p.PactoTrabalhoSolicitacaoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.PactoTrabalhoSolicitacaoId)
                .HasColumnName("pactotrabalhosolicitacaoid")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.PactoTrabalhoId).HasColumnName("pactotrabalhoid");
            builder.Property(p => p.TipoSolicitacaoId).HasColumnName("tiposolicitacaoid");
            builder.Property(p => p.DataSolicitacao).HasColumnName("datasolicitacao");
            builder.Property(p => p.Solicitante).HasColumnName("solicitante");
            builder.Property(p => p.DadosSolicitacao).HasColumnName("dadossolicitacao");
            builder.Property(p => p.ObservacoesSolicitante).HasColumnName("observacoessolicitante");
            builder.Property(p => p.Analisado).HasColumnName("analisado");
            builder.Property(p => p.DataAnalise).HasColumnName("dataanalise");
            builder.Property(p => p.Analista).HasColumnName("analista");
            builder.Property(p => p.Aprovado).HasColumnName("aprovado");
            builder.Property(p => p.ObservacoesAnalista).HasColumnName("observacoesanalista");

            builder.HasOne(p => p.PactoTrabalho)
                   .WithMany(p => p.Solicitacoes)
                   .HasForeignKey(p => p.PactoTrabalhoId)
                   .HasConstraintName("fk_pactotrabalhosolicitacao_pactotrabalho");

            builder.HasOne(p => p.TipoSolicitacao)
                   .WithMany(p => p.PactosTrabalhoSolicitacoes)
                   .HasForeignKey(p => p.TipoSolicitacaoId)
                   .HasConstraintName("fk_pactotrabalhosolicitacao_tiposolicitacao");

        }

    }
}

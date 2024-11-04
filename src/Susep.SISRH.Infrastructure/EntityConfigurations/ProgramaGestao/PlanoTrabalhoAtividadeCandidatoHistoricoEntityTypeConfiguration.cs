
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PlanoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PlanoTrabalhoAtividadeCandidatoHistoricoEntityTypeConfiguration : IEntityTypeConfiguration<PlanoTrabalhoAtividadeCandidatoHistorico>
    {
        public void Configure(EntityTypeBuilder<PlanoTrabalhoAtividadeCandidatoHistorico> builder)
        {
            builder.ToTable("planotrabalhoatividadecandidatohistorico", "programagestao");

            builder.HasKey(p => p.PlanoTrabalhoAtividadeCandidatoHistoricoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.PlanoTrabalhoAtividadeCandidatoHistoricoId)
                .HasColumnName("planotrabalhoatividadecandidatohistoricoid")
                .ValueGeneratedOnAdd();
            
            builder.Property(p => p.PlanoTrabalhoAtividadeCandidatoId).HasColumnName("planotrabalhoatividadecandidatoid");
            builder.Property(p => p.SituacaoId).HasColumnName("situacaoid");
            builder.Property(p => p.Data).HasColumnName("data");
            builder.Property(p => p.Descricao).HasColumnName("descricao");
            builder.Property(p => p.ResponsavelOperacao).HasColumnName("responsaveloperacao");            

            builder.HasOne(p => p.PlanoTrabalhoAtividadeCandidato)
                   .WithMany(p => p.Historico)
                   .HasForeignKey(p => p.PlanoTrabalhoAtividadeCandidatoId)
                   .HasConstraintName("fk_planotrabalhoatividadecandidatohistorico_planotrabalhoativid");

            builder.HasOne(p => p.Situacao)
                   .WithMany(p => p.PlanoTrabalhoAtividadeCandidatoHistoricos)
                   .HasForeignKey(p => p.SituacaoId)
                   .HasConstraintName("fk_planotrabalhoatividadecandidatohistorico_situacao");

        }

    }
}

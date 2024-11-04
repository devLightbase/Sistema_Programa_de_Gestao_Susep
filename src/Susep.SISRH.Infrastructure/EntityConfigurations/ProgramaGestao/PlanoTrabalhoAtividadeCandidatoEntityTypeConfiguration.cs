
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PlanoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PlanoTrabalhoAtividadeCandidatoEntityTypeConfiguration : IEntityTypeConfiguration<PlanoTrabalhoAtividadeCandidato>
    {
        public void Configure(EntityTypeBuilder<PlanoTrabalhoAtividadeCandidato> builder)
        {
            builder.ToTable("planotrabalhoatividadecandidato", "programagestao");

            builder.HasKey(p => p.PlanoTrabalhoAtividadeCandidatoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.PlanoTrabalhoAtividadeCandidatoId)
                .HasColumnName("planotrabalhoatividadecandidatoid")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.PlanoTrabalhoAtividadeId).HasColumnName("planotrabalhoatividadeid");
            builder.Property(p => p.PessoaId).HasColumnName("pessoaid");
            builder.Property(p => p.SituacaoId).HasColumnName("situacaoid");
            builder.Property(p => p.TermoAceite).HasColumnName("termoaceite");

            builder.HasOne(p => p.PlanoTrabalhoAtividade)
                   .WithMany(p => p.Candidatos)
                   .HasForeignKey(p => p.PlanoTrabalhoAtividadeId)
                   .HasConstraintName("fk_planotrabalhoatividadecandidato_planotrabalhoatividade");

            builder.HasOne(p => p.Pessoa)
                   .WithMany(p => p.Candidaturas)
                   .HasForeignKey(p => p.PessoaId)
                   .HasConstraintName("fk_planotrabalhoatividadecandidato_pessoa");

            builder.HasOne(p => p.Situacao)
                   .WithMany(p => p.PlanoTrabalhoAtividadeCandidatos)
                   .HasForeignKey(p => p.SituacaoId)
                   .HasConstraintName("fk_planotrabalhoatividadecandidato_situacao");
        }

    }
}

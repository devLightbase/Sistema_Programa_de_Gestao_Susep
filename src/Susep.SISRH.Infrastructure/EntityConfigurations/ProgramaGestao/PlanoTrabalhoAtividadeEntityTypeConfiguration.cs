using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PlanoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PlanoTrabalhoAtividadeEntityTypeConfiguration : IEntityTypeConfiguration<PlanoTrabalhoAtividade>
    {
        public void Configure(EntityTypeBuilder<PlanoTrabalhoAtividade> builder)
        {
            builder.ToTable("planotrabalhoatividade", "programagestao");

            builder.HasKey(p => p.PlanoTrabalhoAtividadeId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.PlanoTrabalhoAtividadeId)
                .HasColumnName("planotrabalhoatividadeid")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.PlanoTrabalhoId).HasColumnName("planotrabalhoid");
            builder.Property(p => p.ModalidadeExecucaoId).HasColumnName("modalidadeexecucaoid");
            builder.Property(p => p.QuantidadeColaboradores).HasColumnName("quantidadecolaboradores");
            builder.Property(p => p.Descricao).HasColumnName("descricao");

            builder.HasOne(p => p.PlanoTrabalho)
                   .WithMany(p => p.Atividades)
                   .HasForeignKey(p => p.PlanoTrabalhoId)
                   .HasConstraintName("fk_planotrabalhoatividade_planotrabalho");

            builder.HasOne(p => p.ModalidadeExecucao)
                   .WithMany(p => p.PlanosTrabalhoAtividades)
                   .HasForeignKey(p => p.ModalidadeExecucaoId)
                   .HasConstraintName("fk_planotrabalhoatividade_modalidadeexecucao");

            builder.HasMany(p => p.Assuntos)
                   .WithOne()
                   .HasForeignKey(p => p.PlanoTrabalhoAtividadeId)
                   .HasConstraintName("fk_planoTrabalhoatividadeassunto_planotrabalhoatividade");

        }

    }
}

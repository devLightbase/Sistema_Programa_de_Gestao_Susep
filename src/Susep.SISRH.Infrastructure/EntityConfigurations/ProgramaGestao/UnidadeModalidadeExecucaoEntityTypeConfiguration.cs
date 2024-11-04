using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.UnidadeAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class UnidadeModalidadeExecucaoEntityTypeConfiguration : IEntityTypeConfiguration<UnidadeModalidadeExecucao>
    {
        public void Configure(EntityTypeBuilder<UnidadeModalidadeExecucao> builder)
        {
            builder.ToTable("unidademodalidadeexecucao", "programagestao");

            builder.HasKey(p => p.UnidadeModalidadeExecucaoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.UnidadeId).HasColumnName("unidadeid");
            builder.Property(p => p.ModalidadeExecucaoId).HasColumnName("modalidadeexecucaoid");


            builder.HasOne(p => p.Unidade)
                   .WithMany(p => p.ModalidadesExecucao)
                   .HasForeignKey(p => p.UnidadeId)
                   .HasConstraintName("fk_unidademodalidadeexecucao_unidade");

            builder.HasOne(p => p.ModalidadeExecucao)
                   .WithMany(p => p.UnidadesModalidadesExecucao)
                   .HasForeignKey(p => p.ModalidadeExecucaoId)
                   .HasConstraintName("fk_unidademodalidadeexecucao_modalidadeexecucao");

        }

    }
}

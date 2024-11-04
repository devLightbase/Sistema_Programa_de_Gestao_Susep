using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.SituacaoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations
{
    public class SituacaoEntityTypeConfiguration : IEntityTypeConfiguration<Situacao>
    {
        public void Configure(EntityTypeBuilder<Situacao> builder)
        {
            builder.ToTable("situacaopessoa");

            builder.HasKey(p => p.SituacaoPessoaId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.SituacaoPessoaId).HasColumnName("situacaopessoaid");
            builder.Property(p => p.Descricao).HasColumnName("spsdescricao");
            builder.Property(p => p.situacao).HasColumnName("situacao");

        }

    }
}

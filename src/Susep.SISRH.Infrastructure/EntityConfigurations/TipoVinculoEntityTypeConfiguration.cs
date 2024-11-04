using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.TipoVinculoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations
{
    internal class TipoVinculoEntityTypeConfiguration : IEntityTypeConfiguration<TipoVinculo>
    {
        public void Configure(EntityTypeBuilder<TipoVinculo> builder)
        {
            builder.ToTable("tipovinculo");

            builder.HasKey(p => p.TipoVinculoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.TipoVinculoId).HasColumnName("tipovinculoid");
            builder.Property(p => p.Descricao).HasColumnName("tvndescricao");
            builder.Property(p => p.Situacao).HasColumnName("situacao");
        }

    }
}

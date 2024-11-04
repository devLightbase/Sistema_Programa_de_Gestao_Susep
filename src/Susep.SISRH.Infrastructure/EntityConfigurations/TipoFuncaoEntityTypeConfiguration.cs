using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.TipoFuncaoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations
{
    internal class TipoFuncaoEntityTypeConfiguration : IEntityTypeConfiguration<TipoFuncao>
    {
        public void Configure(EntityTypeBuilder<TipoFuncao> builder)
        {
            builder.ToTable("tipofuncao");

            builder.HasKey(p => p.TipoFuncaoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.TipoFuncaoId).HasColumnName("tipofuncaoid");
            builder.Property(p => p.Descricao).HasColumnName("tfndescricao");
            builder.Property(p => p.CodigoFuncao).HasColumnName("tfncodigofuncao");
            builder.Property(p => p.IndicadorChefia).HasColumnName("tfnindicadorchefia");
            builder.Property(p => p.Situacao).HasColumnName("situacao");
        }

    }
}

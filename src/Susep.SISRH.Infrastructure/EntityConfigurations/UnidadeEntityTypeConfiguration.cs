
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.UnidadeAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations
{
    public class UnidadeEntityTypeConfiguration : IEntityTypeConfiguration<Unidade>
    {
        public void Configure(EntityTypeBuilder<Unidade> builder)
        {
            builder.ToTable("vw_unidadesiglacompleta");

            builder.HasKey(p => p.UnidadeId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.UnidadeId).HasColumnName("unidadeid");
            builder.Property(p => p.Sigla).HasColumnName("undsigla");
            builder.Property(p => p.SiglaCompleta).HasColumnName("undsiglacompleta");            
            builder.Property(p => p.Nome).HasColumnName("unddescricao");
            builder.Property(p => p.UfId).HasColumnName("ufid");

        }

    }
}

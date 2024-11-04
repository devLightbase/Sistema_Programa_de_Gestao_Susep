
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.FeriadoAggregate;
using Susep.SISRH.Domain.AggregatesModel.PessoaAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations
{
    public class FeriadoEntityTypeConfiguration : IEntityTypeConfiguration<Feriado>
    {
        public void Configure(EntityTypeBuilder<Feriado> builder)
        {
            builder.ToTable("feriado");

            builder.HasKey(p => p.FeriadoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.FeriadoId).HasColumnName("feriadoid");
            builder.Property(p => p.Data).HasColumnName("ferdata");
            builder.Property(p => p.Fixo).HasColumnName("ferfixo");
            builder.Property(p => p.Descricao).HasColumnName("ferdescricao");
            builder.Property(p => p.UfId).HasColumnName("ufid");
            builder.Property(p => p.Situacao).HasColumnName("situacao");





        }

    }
}

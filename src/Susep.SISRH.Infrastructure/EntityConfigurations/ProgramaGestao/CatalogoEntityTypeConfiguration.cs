
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.CatalogoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class CatalogoEntityTypeConfiguration : IEntityTypeConfiguration<Catalogo>
    {
        public void Configure(EntityTypeBuilder<Catalogo> builder)
        {
            builder.ToTable("catalogo", "programagestao");

            builder.HasKey(p => p.CatalogoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.CatalogoId)
                   .HasColumnName("catalogoid");
                   
            builder.Property(p => p.UnidadeId)
                   .HasColumnName("unidadeid");

            builder.HasOne(p => p.Unidade)
                   .WithMany(p => p.Catalogos)
                   .HasForeignKey(p => p.UnidadeId)
                   .HasConstraintName("fk_catalogo_unidade");

        }

    }
}

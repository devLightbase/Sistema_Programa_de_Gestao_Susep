using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.UnidadesAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations
{
    public class UnidadeDBEntityTypeConfiguration : IEntityTypeConfiguration<UnidadeDB>
    {
        public void Configure(EntityTypeBuilder<UnidadeDB> builder)
        {
            builder.ToTable("unidade");

            builder.HasKey(p => p.Unidadeid);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.Unidadeid).HasColumnName("unidadeid");
            builder.Property(p => p.Sigla).HasColumnName("undsigla");
            builder.Property(p => p.Nome).HasColumnName("unddescricao");
            builder.Property(p => p.UfId).HasColumnName("ufid");
            builder.Property(p => p.Email).HasColumnName("email");
            builder.Property(p => p.UnidadeIdPai).HasColumnName("unidadeidpai");
            builder.Property(p => p.Nivel).HasColumnName("undnivel");
            builder.Property(p => p.PessoaIdChefe).HasColumnName("pessoaidchefe");
            builder.Property(p => p.PessoaIdChefeSubstituto).HasColumnName("pessoaidchefesubstituto");
            builder.Property(p => p.TipoFuncaoUnidadeId).HasColumnName("tipofuncaounidadeid");
            builder.Property(p => p.SituacaoUnidadeId).HasColumnName("situacaounidadeid");
            builder.Property(p => p.TipoUnidadeId).HasColumnName("tipounidadeid");
            builder.Property(p => p.CodSiorg).HasColumnName("undcodigosiorg");
            builder.Property(p => p.CodSgc).HasColumnName("undcodigosgc");
        }
    }
}

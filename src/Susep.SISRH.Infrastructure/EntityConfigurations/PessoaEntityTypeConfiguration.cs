using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PessoaAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations
{
    public class PessoaEntityTypeConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("pessoa");

            builder.HasKey(p => p.PessoaId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode)
                   .Ignore(p => p.CargaHoraria);

            builder.Property(p => p.PessoaId).HasColumnName("pessoaid");
            builder.Property(p => p.Nome).HasColumnName("pesnome");
            builder.Property(p => p.CargaHorariaDb).HasColumnName("cargahoraria");
            builder.Property(p => p.TipoFuncaoId).HasColumnName("tipofuncaoid");
            builder.Property(p => p.UnidadeId).HasColumnName("unidadeid");
            builder.Property(p => p.Cpf).HasColumnName("pescpf");
            builder.Property(p => p.Email).HasColumnName("pesemail");
            builder.Property(p => p.MatriculaSiape).HasColumnName("pesmatriculasiape");
            builder.Property(p => p.SituacaoPessoaId).HasColumnName("situacaopessoaid");
            builder.Property(p => p.TipoVinculoId).HasColumnName("tipovinculoid");
            builder.Property(p => p.DataNascimento).HasColumnName("pesdatanascimento");
        }

    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PlanoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PlanoTrabalhoHistoricoEntityTypeConfiguration : IEntityTypeConfiguration<PlanoTrabalhoHistorico>
    {
        public void Configure(EntityTypeBuilder<PlanoTrabalhoHistorico> builder)
        {
            builder.ToTable("planotrabalhohistorico", "programagestao");

            builder.HasKey(p => p.PlanoTrabalhoHistoricoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.PlanoTrabalhoHistoricoId)
                   .HasColumnName("planotrabalhohistoricoid")
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.PlanoTrabalhoId).HasColumnName("planotrabalhoid");
            builder.Property(p => p.SituacaoId).HasColumnName("situacaoid");
            builder.Property(p => p.Observacoes).HasColumnName("observacoes");
            builder.Property(p => p.ResponsavelOperacao).HasColumnName("responsaveloperacao");
            builder.Property(p => p.DataOperacao).HasColumnName("dataoperacao");



            builder.HasOne(p => p.PlanoTrabalho)
                   .WithMany(p => p.Historico)
                   .HasForeignKey(p => p.PlanoTrabalhoId)
                   .HasConstraintName("fk_planotrabalhohistorico_planotrabalho");

            builder.HasOne(p => p.Situacao)
                   .WithMany(p => p.HistoricoPlanosTrabalho)
                   .HasForeignKey(p => p.SituacaoId)
                   .HasConstraintName("fk_planotrabalhohistorico_situacao");


        }

    }
}

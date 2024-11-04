
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PlanoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PlanoTrabalhoCustoEntityTypeConfiguration : IEntityTypeConfiguration<PlanoTrabalhoCusto>
    {
        public void Configure(EntityTypeBuilder<PlanoTrabalhoCusto> builder)
        {
            builder.ToTable("planotrabalhocusto", "programagestao");

            builder.HasKey(p => p.PlanoTrabalhoCustoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.PlanoTrabalhoCustoId)
                .HasColumnName("planotrabalhocustoid")
                .ValueGeneratedOnAdd();
            
            builder.Property(p => p.PlanoTrabalhoId).HasColumnName("planotrabalhoid");
	        builder.Property(p => p.PlanoTrabalhoObjetoId).HasColumnName("planotrabalhoobjetoid");
            builder.Property(p => p.Valor).HasColumnName("valor");
            builder.Property(p => p.Descricao).HasColumnName("descricao");
            builder.Property(p => p.PlanoTrabalhoObjetoId).HasColumnName("planotrabalhoobjetoid");

            builder.HasOne(p => p.PlanoTrabalho)
                   .WithMany(p => p.Custos)
                   .HasForeignKey(p => p.PlanoTrabalhoId)
                   .HasConstraintName("fk_planotrabalhocusto_planotrabalho");

            builder.HasOne(p => p.PlanoTrabalhoObjeto)
                   .WithMany(p => p.Custos)
                   .HasForeignKey(p => p.PlanoTrabalhoObjetoId)
                   .HasConstraintName("fk_planotrabalhocusto_planotrabalhoobjeto");

        }
    }
}

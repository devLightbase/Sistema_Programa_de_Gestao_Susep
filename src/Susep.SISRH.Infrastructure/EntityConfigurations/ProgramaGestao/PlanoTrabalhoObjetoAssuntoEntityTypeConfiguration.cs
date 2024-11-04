
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PlanoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PlanoTrabalhoObjetoAssuntoEntityTypeConfiguration : IEntityTypeConfiguration<PlanoTrabalhoObjetoAssunto>
    {
        public void Configure(EntityTypeBuilder<PlanoTrabalhoObjetoAssunto> builder)
        {
            builder.ToTable("planotrabalhoobjetoassunto", "programagestao");

            builder.HasKey(p => p.PlanoTrabalhoObjetoAssuntoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.PlanoTrabalhoObjetoAssuntoId)
                   .HasColumnName("planotrabalhoobjetoassuntoid")
                   .ValueGeneratedOnAdd(); ;
            
            builder.Property(p => p.PlanoTrabalhoObjetoId).HasColumnName("planotrabalhoobjetoid");
            builder.Property(p => p.AssuntoId).HasColumnName("assuntoid");

            builder.HasOne(p => p.PlanoTrabalhoObjeto)
                   .WithMany(p => p.Assuntos)
                   .HasForeignKey(p => p.PlanoTrabalhoObjetoId)
                   .HasConstraintName("fk_planotrabalhoobjetoassunto_planotrabalhoobjeto");

            builder.HasOne(p => p.Assunto)
                   .WithMany()
                   .HasForeignKey(p => p.AssuntoId)
                   .HasConstraintName("fk_planotrabalhoobjetoassunto_assunto");

        }

    }
}

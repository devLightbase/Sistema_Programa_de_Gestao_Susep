
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PactoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PactoAtividadePlanoObjetoEntityTypeConfiguration : IEntityTypeConfiguration<PactoAtividadePlanoObjeto>
    {
        public void Configure(EntityTypeBuilder<PactoAtividadePlanoObjeto> builder)
        {
            builder.ToTable("pactoatividadeplanoobjeto", "programagestao");

            builder.HasKey(p => p.PactoAtividadePlanoObjetoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.PactoAtividadePlanoObjetoId)
                .HasColumnName("pactoatividadeplanoobjetoid")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.PlanoTrabalhoObjetoId).HasColumnName("planotrabalhoobjetoid");
            builder.Property(p => p.PactoTrabalhoAtividadeId).HasColumnName("pactotrabalhoatividadeid");

            builder.HasOne(p => p.PactoTrabalhoAtividade)
                   .WithMany(p => p.Objetos)
                   .HasForeignKey(p => p.PactoTrabalhoAtividadeId)
                   .HasConstraintName("fk_pactoatividadeplanoobjeto_pactotrabalhoatividade");

            builder.HasOne(p => p.PlanoTrabalhoObjeto)
                   .WithMany()
                   .HasForeignKey(p => p.PlanoTrabalhoObjetoId)
                   .HasConstraintName("fk_pactoatividadeplanoobjeto_planotrabalhoobjeto");

        }

    }
}

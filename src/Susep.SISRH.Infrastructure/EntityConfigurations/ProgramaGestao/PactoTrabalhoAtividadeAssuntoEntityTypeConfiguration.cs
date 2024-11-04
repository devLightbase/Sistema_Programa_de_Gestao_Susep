using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PactoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PactoTrabalhoAtividadeAssuntoEntityTypeConfiguration : IEntityTypeConfiguration<PactoTrabalhoAtividadeAssunto>
    {
        public void Configure(EntityTypeBuilder<PactoTrabalhoAtividadeAssunto> builder)
        {
            builder.ToTable("pactotrabalhoatividadeassunto", "programagestao");

            builder.HasKey(p => p.PactoTrabalhoAtividadeAssuntoId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.PactoTrabalhoAtividadeAssuntoId)
                   .HasColumnName("pactotrabalhoatividadeassuntoid")
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.PactoTrabalhoAtividadeId).HasColumnName("pactotrabalhoatividadeid");

            builder.Property(p => p.AssuntoId).HasColumnName("assuntoid");

            builder.HasOne(p => p.Assunto)
                   .WithMany()
                   .HasForeignKey(p => p.AssuntoId)
                   .HasConstraintName("fk_pactotrabalhoatividadeassunto_assunto");

        }

    }
}

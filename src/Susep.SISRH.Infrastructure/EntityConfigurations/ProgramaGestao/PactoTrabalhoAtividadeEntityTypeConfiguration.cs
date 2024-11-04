
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Susep.SISRH.Domain.AggregatesModel.PactoTrabalhoAggregate;

namespace Susep.SISRH.Infrastructure.EntityConfigurations.ProgramaGestao
{
    public class PactoTrabalhoAtividadeEntityTypeConfiguration : IEntityTypeConfiguration<PactoTrabalhoAtividade>
    {
        public void Configure(EntityTypeBuilder<PactoTrabalhoAtividade> builder)
        {
            builder.ToTable("pactotrabalhoatividade", "programagestao");

            builder.HasKey(p => p.PactoTrabalhoAtividadeId);

            builder.Ignore(p => p.Id)
                   .Ignore(p => p.RequestedHashCode);

            builder.Property(p => p.PactoTrabalhoAtividadeId)
                .HasColumnName("pactotrabalhoatividadeid")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.PactoTrabalhoId).HasColumnName("pactotrabalhoid");
            builder.Property(p => p.ItemCatalogoId).HasColumnName("itemcatalogoid");
            builder.Property(p => p.Quantidade).HasColumnName("quantidade");
            builder.Property(p => p.TempoPrevistoPorItem).HasColumnName("tempoprevistoporitem");
            builder.Property(p => p.TempoPrevistoTotal).HasColumnName("tempoprevistototal");
            builder.Property(p => p.DataInicio).HasColumnName("datainicio");
            builder.Property(p => p.DataFim).HasColumnName("datafim");
            builder.Property(p => p.TempoRealizado).HasColumnName("temporealizado");
            builder.Property(p => p.TempoHomologado).HasColumnName("tempohomologado");            
            builder.Property(p => p.SituacaoId).HasColumnName("situacaoid");
            builder.Property(p => p.Descricao).HasColumnName("descricao");
            builder.Property(p => p.ConsideracoesConclusao).HasColumnName("consideracoesconclusao");
            builder.Property(p => p.Nota).HasColumnName("nota");
            builder.Property(p => p.Justificativa).HasColumnName("justificativa");
            builder.Property(p => p.ModalidadeExecucaoId).HasColumnName("modalidadeexecucaoid");

            builder.HasOne(p => p.PactoTrabalho)
                   .WithMany(p => p.Atividades)
                   .HasForeignKey(p => p.PactoTrabalhoId)
                   .HasConstraintName("fk_pactotrabalhoatividade_pactotrabalho");

            builder.HasOne(p => p.ItemCatalogo)
                   .WithMany(p => p.PactosTrabalhoAtividades)
                   .HasForeignKey(p => p.ItemCatalogoId)
                   .HasConstraintName("fk_pactotrabalhoatividade_itemcatalogo");

            builder.HasOne(p => p.Situacao)
                   .WithMany(p => p.PactosTrabalhoAtividades)
                   .HasForeignKey(p => p.SituacaoId)
                   .HasConstraintName("fk_pactotrabalhoatividade_situacao");

            builder.HasMany(p => p.Assuntos)
                   .WithOne()
                   .HasForeignKey(p => p.PactoTrabalhoAtividadeId)
                   .HasConstraintName("fk_pactotrabalhoatividadeassunto_pactotrabalhoatividade");

        }

    }
}

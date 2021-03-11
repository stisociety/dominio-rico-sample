using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCompany.InscricoesContexto.Cursos;
using MyCompany.InscricoesContexto.Dominio.Turmas;

namespace MyCompany.InscricoesContexto.Infraestrutura.EntityTypeConfigurations
{
    public class TurmaEntityTypeConfiguration : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turma", "Inscricoes");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Descricao);
            builder.Property(p => p.ValorMensal);
            builder.OwnsOne(p => p.LimiteIdade, limite =>
            {
                limite.Property(c => c.Ativa).HasColumnName("RegraLimiteIdadeAtiva");
                limite.Property(c => c.LimiteMinimo).HasColumnName("RegraLimiteIdadeMinima");
                limite.Property(c => c.LimiteMaximo).HasColumnName("RegraLimiteIdadeMaxima");
            });
            
            // Relacionamento entre Turma e Curso (* para 1)
            builder
                .HasOne<Curso>()
                .WithMany()
                .HasForeignKey(c=> c.CursoId);
            
            // Relacionamento entre Turma e Horarios (1 para *)
            builder
                .HasMany(c => c.Horarios)
                .WithOne()
                .HasForeignKey("CursoId")
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata
                .PrincipalToDependent
                .SetField("_horarios");
        }
    }
}
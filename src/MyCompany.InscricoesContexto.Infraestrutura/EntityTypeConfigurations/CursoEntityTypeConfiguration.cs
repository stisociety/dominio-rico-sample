using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCompany.InscricoesContexto.Cursos;

namespace MyCompany.InscricoesContexto.Infraestrutura.EntityTypeConfigurations
{
    public class CursoEntityTypeConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso", "Inscricoes");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Descricao);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyCompany.InscricoesContexto.Dominio.Alunos;
using MyCompany.InscricoesContexto.Dominio.Inscricoes;
using MyCompany.InscricoesContexto.Dominio.Turmas;

namespace MyCompany.InscricoesContexto.Infraestrutura.EntityTypeConfigurations
{
    public class InscricaoEntityTypeConfiguration : IEntityTypeConfiguration<Inscricao>
    {
        public void Configure(EntityTypeBuilder<Inscricao> builder)
        {
            builder.ToTable("InscricaoRealizada", "Inscricoes");
            builder.HasKey(p => p.Id);
            builder.Property(c => c.Data);
            builder.Property(c => c.Situacao).HasConversion(new EnumToStringConverter<ESituacao>());
            
            // Relacionamento entre Inscricao e Turma
            builder
                .HasOne<Turma>()
                .WithMany()
                .HasForeignKey(c=> c.TurmaId);
            
            // Relacionamento entre Inscricao e Aluno
            builder
                .HasOne<Aluno>()
                .WithMany()
                .HasForeignKey(c=> c.AlunoId);
        }
    }
}
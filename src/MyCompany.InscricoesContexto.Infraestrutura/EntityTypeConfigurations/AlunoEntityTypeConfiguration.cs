using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCompany.InscricoesContexto.Dominio.Alunos;

namespace MyCompany.InscricoesContexto.Infraestrutura.EntityTypeConfigurations
{
    public class AlunoEntityTypeConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Aluno", "Inscricoes");
            builder.HasKey(p => p.Id);
            builder.OwnsOne(p => p.Nome, nome =>
            {
                nome.Property(c => c.Nome).HasColumnName("Nome").HasColumnType("varchar(30)");
                nome.Property(c => c.Sobrenome).HasColumnName("Sobrenome").HasColumnType("varchar(30)");
            });
            builder.Property(p => p.Cpf).HasConversion(EFTypeConverters.CpfConverter);
            builder.Property(p => p.DataNascimento);
            builder.OwnsOne(p => p.Endereco, endereco =>
            {
                endereco.Property(c => c.Rua).HasColumnName("Rua").HasColumnType("varchar(40)");
                endereco.Property(c => c.Numero).HasColumnName("Numero").HasColumnType("varchar(10)");
                endereco.Property(c => c.Complemento).HasColumnName("Complemento").HasColumnType("varchar(20)");
                endereco.Property(c => c.Bairro).HasColumnName("Bairro").HasColumnType("varchar(40)");
                endereco.Property(c => c.Cidade).HasColumnName("Cidade").HasColumnType("varchar(40)");
                endereco.Property(c => c.Cep).HasColumnName("Cep").HasColumnType("varchar(15)");
                endereco.Property(c => c.Uf).HasColumnName("Uf").HasColumnType("varchar(2)");
            });
        }
    }
}
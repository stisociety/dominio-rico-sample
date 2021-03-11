using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyCompany.Inscricoes.Shared;
using MyCompany.InscricoesContexto.Dominio.Turmas;

namespace MyCompany.InscricoesContexto.Infraestrutura.EntityTypeConfigurations
{
    public class HorarioEntityTypeConfiguration : IEntityTypeConfiguration<HorarioDisponivel>
    {
        public void Configure(EntityTypeBuilder<HorarioDisponivel> builder)
        {
            builder.ToTable("Horario", "Inscricoes");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DiaSemana).HasConversion(new EnumToStringConverter<EDiaSemana>());
            builder.OwnsOne(p => p.Inicio, inicio =>
            {
                inicio.Property(c => c.Hora).HasColumnName("HoraInicio");
                inicio.Property(c => c.Minuto).HasColumnName("MinutoInicio");
            });
            builder.OwnsOne(p => p.Fim, fim =>
            {
                fim.Property(c => c.Hora).HasColumnName("HoraFim");
                fim.Property(c => c.Minuto).HasColumnName("MinutoFim");
            });
        }
    }
}
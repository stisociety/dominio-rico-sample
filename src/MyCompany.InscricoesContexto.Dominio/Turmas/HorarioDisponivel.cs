using CSharpFunctionalExtensions;
using MyCompany.Inscricoes.Shared;

namespace MyCompany.InscricoesContexto.Dominio.Turmas
{
    public sealed class HorarioDisponivel : Entity<int>
    {
        private HorarioDisponivel(){}
        private HorarioDisponivel(in int id, in EDiaSemana diaSemana, in Horario inicio, in Horario fim)
            :base(id)
        {
            DiaSemana = diaSemana;
            Inicio = inicio;
            Fim = fim;
        }
        
        public EDiaSemana DiaSemana { get; }
        public Horario Inicio { get; }
        public Horario Fim { get; }

        public static Result<HorarioDisponivel> Criar(in EDiaSemana diaSemana, in Horario inicio, in Horario fim,
            in int id = 0)
        {
            return new HorarioDisponivel(id, diaSemana, inicio, fim);
        }
    }
}
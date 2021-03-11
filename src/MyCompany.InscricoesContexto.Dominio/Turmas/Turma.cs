using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using MyCompany.Inscricoes.Shared;
using MyCompany.InscricoesContexto.Dominio.SeedWork;

namespace MyCompany.InscricoesContexto.Dominio.Turmas
{
    public sealed class Turma : Entity<int>, IAgreggateRoot
    {
        private Turma() {}
        private Turma(in int id,in int cursoId, in string descricao, in decimal valorMensal, 
            in RegraParaLimiteIdade limiteIdade, in bool disponivelParaInscricao, 
            IReadOnlyCollection<HorarioDisponivel> horarios)
            :base(id)
        {
            CursoId = cursoId;
            Descricao = descricao;
            ValorMensal = valorMensal;
            LimiteIdade = limiteIdade;
            DisponivelParaInscricao = disponivelParaInscricao;
            _horarios = horarios == null
                ? new List<HorarioDisponivel>()
                : horarios.ToList();
        }

        private List<HorarioDisponivel> _horarios;
        
        public int CursoId { get; }
        public string Descricao { get; }
        public decimal ValorMensal { get; }
        public RegraParaLimiteIdade LimiteIdade { get; }
        public bool DisponivelParaInscricao { get; private set; }
        public IEnumerable<HorarioDisponivel> Horarios => _horarios.AsReadOnly();
        
        public Result IdadeEhValidaParaInscricao(in int idade)
        {
            if (!LimiteIdade.IdadeEhValida(idade))
                return Result.Failure(MensagensErro.IdadeInvalidaParaTurma);
            return Result.Success();
        }

        public void AdicionarHorario(HorarioDisponivel horario)
        {
            
            _horarios.Add(horario);
        }

        public Result LiberarParaInscricoes()
        {
            if (Horarios.Count() == 0)
                return Result.Failure("Não é possivel disponibilizar turma pois não existem horários");
            DisponivelParaInscricao = true;
            return Result.Success();
        }
        
        public static Result<Turma> Criar(in int cursoId, in string descricao, in decimal valorMensal, in RegraParaLimiteIdade limiteIdade,
            IReadOnlyCollection<HorarioDisponivel> horarios, in int id = 0)
        {
            var validacao = Result.Combine(
                Result.FailureIf(string.IsNullOrEmpty(descricao), MensagensErro.DescricaoTurmaInvalida),
                Result.FailureIf(valorMensal <= 0, MensagensErro.ValorDaMensalidadeInvalido));
            if (validacao.IsFailure)
                return Result.Failure<Turma>(validacao.Error);
            return new Turma(id, cursoId, descricao, valorMensal, limiteIdade, false, horarios);
        }
    }
}
using CSharpFunctionalExtensions;
using MediatR;

namespace MyCompany.InscricoesContexto.Dominio.Inscricoes.Comandos
{
    public sealed class RealizarInscricaoCommand : IRequest<Result<int>>
    {
        private RealizarInscricaoCommand(in int alunoId, in int turmaId)
        {
            AlunoId = alunoId;
            TurmaId = turmaId;
        }
        
        public int AlunoId { get; }
        public int TurmaId { get; }

        public static Result<RealizarInscricaoCommand> Criar(in int alunoId, in int turmaId)
        {
            var validacao = Result.Combine(
                Result.FailureIf(alunoId <= 0, "Aluno inválido"),
                Result.FailureIf(turmaId <= 0, "Turma inválido"));
            if (validacao.IsFailure)
                return Result.Failure<RealizarInscricaoCommand>(validacao.Error);
            return new RealizarInscricaoCommand(alunoId, turmaId);
        }
    }
}
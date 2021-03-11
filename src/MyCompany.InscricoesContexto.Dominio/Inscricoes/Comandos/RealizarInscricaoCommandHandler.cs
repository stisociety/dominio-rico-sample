using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using MyCompany.InscricoesContexto.Dominio.Alunos;
using MyCompany.InscricoesContexto.Dominio.Turmas;

namespace MyCompany.InscricoesContexto.Dominio.Inscricoes.Comandos
{
    public sealed class RealizarInscricaoCommandHandler
        : IRequestHandler<RealizarInscricaoCommand, Result<int>>
    {
        private readonly IAlunosRepositorio _alunosRepositorio;
        private readonly ITurmasRepositorio _turmasRepositorio;
        private readonly IInscricoesRepositorio _inscricoesRepositorio;

        public RealizarInscricaoCommandHandler(
            IAlunosRepositorio alunosRepositorio,
            ITurmasRepositorio turmasRepositorio,
            IInscricoesRepositorio inscricoesRepositorio)
        {
            _alunosRepositorio = alunosRepositorio;
            _turmasRepositorio = turmasRepositorio;
            _inscricoesRepositorio = inscricoesRepositorio;
        }
        
        public async Task<Result<int>> Handle(RealizarInscricaoCommand request, CancellationToken cancellationToken)
        {
            var aluno = await _alunosRepositorio.RecuperarAsync(request.AlunoId, cancellationToken);
            if (aluno.HasNoValue)
                return Result.Failure<int>("Aluno não foi localizado");
            var turma = await _turmasRepositorio.RecuperarAsync(request.TurmaId, cancellationToken);
            if(turma.HasNoValue)
                return Result.Failure<int>("Turma não foi localizada");

            var inscricao = Inscricao.RealizarInscricao(aluno.Value, turma.Value);
            if(inscricao.IsFailure)
                return Result.Failure<int>(inscricao.Error);

            await _inscricoesRepositorio.AdicionarAsync(inscricao.Value, cancellationToken);
            await _inscricoesRepositorio.UnitOfWork.Commit(cancellationToken);
            
            return Result.Success(inscricao.Value.Id);
        }
    }
}
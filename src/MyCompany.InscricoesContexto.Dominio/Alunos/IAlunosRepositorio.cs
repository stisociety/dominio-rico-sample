using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MyCompany.InscricoesContexto.Dominio.SeedWork;

namespace MyCompany.InscricoesContexto.Dominio.Alunos
{
    public interface IAlunosRepositorio : IRepository<Aluno>
    {
        Task<Maybe<Aluno>> RecuperarAsync(int id, CancellationToken cancellationToken);
    }
}
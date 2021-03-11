using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MyCompany.InscricoesContexto.Dominio.SeedWork;

namespace MyCompany.InscricoesContexto.Dominio.Turmas
{
    public interface ITurmasRepositorio : IRepository<Turma>
    {
        Task<Maybe<Turma>> RecuperarAsync(int id, CancellationToken cancellationToken);
    }
}
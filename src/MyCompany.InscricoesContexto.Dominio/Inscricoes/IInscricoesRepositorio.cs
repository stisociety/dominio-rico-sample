using System.Threading;
using System.Threading.Tasks;
using MyCompany.InscricoesContexto.Dominio.SeedWork;

namespace MyCompany.InscricoesContexto.Dominio.Inscricoes
{
    public interface IInscricoesRepositorio : IRepository<Inscricao>
    {
        Task AdicionarAsync(Inscricao inscricao, CancellationToken cancellationToken);
    }
}
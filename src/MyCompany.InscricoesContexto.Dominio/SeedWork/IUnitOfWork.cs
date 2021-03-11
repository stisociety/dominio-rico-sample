using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.InscricoesContexto.Dominio.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit(CancellationToken cancellationToken = default);
    }
}
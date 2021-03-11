using System.Threading;
using System.Threading.Tasks;
using MyCompany.InscricoesContexto.Dominio.Inscricoes;
using MyCompany.InscricoesContexto.Dominio.SeedWork;

namespace MyCompany.InscricoesContexto.Infraestrutura
{
    public sealed class InscricaoRepositorio : IInscricoesRepositorio
    {
        private readonly InscricoesDbContext _contexto;

        public InscricaoRepositorio(InscricoesDbContext contexto)
        {
            _contexto = contexto;
        }

        public IUnitOfWork UnitOfWork => _contexto;
        
        public async Task AdicionarAsync(Inscricao inscricao, CancellationToken cancellationToken)
        {
            await _contexto.Inscricoes.AddAsync(inscricao, cancellationToken);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MyCompany.InscricoesContexto.Dominio.SeedWork;
using MyCompany.InscricoesContexto.Dominio.Turmas;

namespace MyCompany.InscricoesContexto.Infraestrutura
{
    public sealed class TurmasRepositorio : ITurmasRepositorio
    {
        private readonly InscricoesDbContext _contexto;

        public TurmasRepositorio(InscricoesDbContext contexto)
        {
            _contexto = contexto;
        }

        public IUnitOfWork UnitOfWork => _contexto;
        
        public async Task<Maybe<Turma>> RecuperarAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _contexto.Turmas.Include(c => c.Horarios)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MyCompany.InscricoesContexto.Dominio.Alunos;
using MyCompany.InscricoesContexto.Dominio.SeedWork;

namespace MyCompany.InscricoesContexto.Infraestrutura
{
    public sealed class AlunosRepositorio : IAlunosRepositorio
    {
        private readonly InscricoesDbContext _contexto;

        public AlunosRepositorio(InscricoesDbContext contexto)
        {
            _contexto = contexto;
        }

        public IUnitOfWork UnitOfWork => _contexto;
        
        public async Task<Maybe<Aluno>> RecuperarAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _contexto.Alunos.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
    }
}
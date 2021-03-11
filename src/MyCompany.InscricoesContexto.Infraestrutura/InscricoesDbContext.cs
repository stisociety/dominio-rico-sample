using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCompany.InscricoesContexto.Cursos;
using MyCompany.InscricoesContexto.Dominio.Alunos;
using MyCompany.InscricoesContexto.Dominio.Inscricoes;
using MyCompany.InscricoesContexto.Dominio.SeedWork;
using MyCompany.InscricoesContexto.Dominio.Turmas;
using MyCompany.InscricoesContexto.Infraestrutura.EntityTypeConfigurations;

namespace MyCompany.InscricoesContexto.Infraestrutura
{
    public class InscricoesDbContext : DbContext, IUnitOfWork
    {
        public InscricoesDbContext() { }
        public InscricoesDbContext(DbContextOptions<InscricoesDbContext> options) : base(options) { }
        
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Inscricao> Inscricoes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CursoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TurmaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new HorarioEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InscricaoEntityTypeConfiguration());
        }

        public Task<int> Commit(CancellationToken cancellationToken = default)
        {
            return SaveChangesAsync(cancellationToken);
        }
    }
}
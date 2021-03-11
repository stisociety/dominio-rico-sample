using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCompany.Inscricoes.Shared;
using MyCompany.InscricoesContexto.Cursos;
using MyCompany.InscricoesContexto.Dominio.Alunos;
using MyCompany.InscricoesContexto.Dominio.Inscricoes;
using MyCompany.InscricoesContexto.Dominio.Inscricoes.Comandos;
using MyCompany.InscricoesContexto.Dominio.Turmas;
using MyCompany.InscricoesContexto.Infraestrutura;
using Shouldly;
using Xunit;

namespace MyCompany.InscricoesContexto.Dominio.Testes.Inscricoes
{
    [Collection(nameof(InscricoesDbContext))]
    public sealed class RealizarInscricaoCommandHandlerTestes : IAsyncLifetime
    {
        private readonly SqlLiteDbContextFactory _dbContextFactory;
        private readonly CancellationToken _cancellationToken = CancellationToken.None;
        private int turmaId = 0;
        private int alunoId = 0;
        private RealizarInscricaoCommandHandler handler;
        
        public RealizarInscricaoCommandHandlerTestes(SqlLiteDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        
        [Fact]
        public async Task DadoComandoValido_QuandoExecutarHandlerDeInscricao_DevoTerUmAlunoInscrito()
        {
            using var dbContextHandler = await _dbContextFactory.CriarAsync();
            var alunosRepositorio = new AlunosRepositorio(dbContextHandler);
            var turmasRepositorio = new TurmasRepositorio(dbContextHandler);
            var inscricoesRepositorio = new InscricaoRepositorio(dbContextHandler);
            handler = new RealizarInscricaoCommandHandler(alunosRepositorio, turmasRepositorio, inscricoesRepositorio);
            var comando = RealizarInscricaoCommand.Criar(alunoId, turmaId).Value;
            
            var resultado = await handler.Handle(comando, _cancellationToken);
            
            resultado.IsSuccess.ShouldBeTrue();
            using var dbContext = await _dbContextFactory.CriarAsync();
            var inscricao = await dbContext.Inscricoes.FirstOrDefaultAsync(c => c.Id == resultado.Value, _cancellationToken);
            inscricao.ShouldNotBeNull();
            inscricao.AlunoId.ShouldBe(alunoId);
            inscricao.TurmaId.ShouldBe(turmaId);
            inscricao.Data.ShouldBe(DateTime.Now, TimeSpan.FromSeconds(20));
            inscricao.Situacao.ShouldBe(ESituacao.Ativo);
        }

        public async Task InitializeAsync()
        {
            using var dbContext = await _dbContextFactory.CriarAsync();
            var regraLimite = RegraParaLimiteIdade.CriarAtiva(18, 28).Value;
            var curso = Curso.Criar("Curso de C#").Value;
            await dbContext.Cursos.AddAsync(curso, _cancellationToken);
            await dbContext.SaveChangesAsync(_cancellationToken);
            
            var turma = Turma.Criar(curso.Id, "Teste", 150, regraLimite, new List<HorarioDisponivel>
            {
                HorarioDisponivel.Criar(EDiaSemana.Segunda, Horario.Criar(10, 0).Value, Horario.Criar(11, 0).Value)
                    .Value
            }).Value;
            await dbContext.Turmas.AddAsync(turma, _cancellationToken);
            
            var aluno = Aluno.Criar(Cpf.Create("47270357072").Value, NomeCompleto.Criar("Gabriel", "Da Silva").Value,
                DateTime.Now.AddYears(-20),
                new Endereco("Rua Paissandu", "36", "", "Vila Rosa", "Novo Hamburgo", "93315030", "RS")).Value;
            await dbContext.Alunos.AddAsync(aluno, _cancellationToken);
            
            await dbContext.SaveChangesAsync(_cancellationToken);
            alunoId = aluno.Id;
            turmaId = turma.Id;
        }

        public async Task DisposeAsync()
        {
            using var dbContext = await _dbContextFactory.CriarAsync();
            var inscricoes = await dbContext.Inscricoes.ToListAsync(_cancellationToken);
            dbContext.Inscricoes.RemoveRange(inscricoes);
            var turmas = await dbContext.Turmas.ToListAsync(_cancellationToken);
            dbContext.Turmas.RemoveRange(turmas);
            var alunos = await dbContext.Alunos.ToListAsync(_cancellationToken);
            dbContext.Alunos.RemoveRange(alunos);
            await dbContext.SaveChangesAsync(_cancellationToken);
        }
    }
}
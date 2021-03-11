using System;
using CSharpFunctionalExtensions;
using MyCompany.InscricoesContexto.Dominio.Alunos;
using MyCompany.InscricoesContexto.Dominio.SeedWork;
using MyCompany.InscricoesContexto.Dominio.Turmas;

namespace MyCompany.InscricoesContexto.Dominio.Inscricoes
{
    public sealed class Inscricao : Entity<int>, IAgreggateRoot
    {
        private Inscricao(){}
        private Inscricao(in int id, in int turmaId, in int alunoId, in DateTime data, in ESituacao situacao) : base(id)
        {
            TurmaId = turmaId;
            AlunoId = alunoId;
            Data = data;
            Situacao = situacao;
        }
        
        public int TurmaId { get; }
        public int AlunoId { get; }
        public DateTime Data { get; }
        public ESituacao Situacao { get;  }

        public static Result<Inscricao> RealizarInscricao(Aluno aluno, Turma turma, in int id = 0)
        {
            if (turma.IdadeEhValidaParaInscricao(aluno.Idade) is var resultado && resultado.IsFailure)
                return Result.Failure<Inscricao>(resultado.Error);
            return new Inscricao(id, turma.Id, aluno.Id, DateTime.Now, ESituacao.Ativo);
        }
    }

    public enum ESituacao
    {
        Ativo,
        Cancelado
    }
}
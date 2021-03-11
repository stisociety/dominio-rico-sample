using CSharpFunctionalExtensions;
using MyCompany.Inscricoes.Shared;

namespace MyCompany.InscricoesContexto.Cursos
{
    public sealed class Curso : Entity<int>
    {
        private Curso(){}
        public Curso(in int id, in string descricao): base(id)
        {
            Descricao = descricao;
        }
        
        public string Descricao { get; }

        public static Result<Curso> Criar(in string descricao, in int id = 0)
        {
            var validacao = Result.FailureIf(string.IsNullOrEmpty(descricao), MensagensErro.DescricaoCursoInvalida);
            if (validacao.IsFailure)
                return Result.Failure<Curso>(validacao.Error);
            return new Curso(id, descricao);
        }
    }
}
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace MyCompany.Inscricoes.Shared
{
    public sealed class NomeCompleto : ValueObject
    {
        private NomeCompleto(){}
        private NomeCompleto(in string nome, in string sobrenome)
        {
            Nome = nome;
            Sobrenome = sobrenome;
        }
        
        public string Nome { get; }
        public string Sobrenome { get; }
        
        public override string ToString() => $"{Nome} {Sobrenome}";

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Nome;
            yield return Sobrenome;
        }

        public static Result<NomeCompleto> Criar(in string nome, in string sobrenome)
        {
            var validacao = Result.Combine(
                Result.FailureIf(string.IsNullOrEmpty(nome), MensagensErro.NomeInvalido),
                Result.FailureIf(string.IsNullOrEmpty(sobrenome), MensagensErro.SobrenomeInvalido));

            if (validacao.IsFailure)
                return Result.Failure<NomeCompleto>(validacao.Error);

            return new NomeCompleto(nome, sobrenome);
        }
    }
}
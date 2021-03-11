using System.Collections.Generic;
using CSharpFunctionalExtensions;
using MyCompany.Inscricoes.Shared;

namespace MyCompany.InscricoesContexto.Dominio.Turmas
{
    public sealed class RegraParaLimiteIdade : ValueObject
    {
        private RegraParaLimiteIdade(){}
        private RegraParaLimiteIdade(in bool ativa, in int limiteMinimo, in int limiteMaximo)
        {
            Ativa = ativa;
            LimiteMinimo = limiteMinimo;
            LimiteMaximo = limiteMaximo;
        }
        
        public bool Ativa { get; }
        public int LimiteMinimo { get; }
        public int LimiteMaximo { get; }

        public bool IdadeEhValida(in int idade)
        {
            if (!Ativa)
                return true;
            return idade >= LimiteMinimo && idade <= LimiteMaximo;

            #region Teste
            //idade >= LimiteMinimo && idade <= LimiteMaximo;
            #endregion
        }
        
        public static RegraParaLimiteIdade CriarInativa()
            => new RegraParaLimiteIdade(false, 0, 0);

        public static Result<RegraParaLimiteIdade> CriarAtiva(in int limiteMinimo, in int limiteMaximo)
        {
            var validacao = Result.Combine(
                Result.FailureIf(limiteMinimo < 0, MensagensErro.LimiteMinimoDeIdadeInvalido),
                Result.FailureIf(limiteMaximo < 0, MensagensErro.LimiteMaximoDeIdadeInvalido),
                Result.FailureIf(limiteMinimo > limiteMaximo, MensagensErro.LimiteMinimoDeveSerMenor));
            if (validacao.IsFailure)
                return Result.Failure<RegraParaLimiteIdade>(validacao.Error);

            return new RegraParaLimiteIdade(true, limiteMinimo, limiteMaximo);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Ativa;
            yield return LimiteMinimo;
            yield return LimiteMaximo;
        }
    }
}
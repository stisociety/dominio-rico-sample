using System.Collections.Generic;
using CSharpFunctionalExtensions;
using MyCompany.InscricoesContexto.Dominio.Turmas;

namespace MyCompany.Inscricoes.Shared
{
    public sealed class Horario : ValueObject
    {
        private Horario(){}
        private Horario(in int hora, in int minuto)
        {
            Hora = hora;
            Minuto = minuto;
        }
        
        public int Hora { get; }
        public int Minuto { get; }

        public static Result<Horario> Criar(in int hora, in int minuto)
        {
            var validacao = Result.Combine(
                Result.SuccessIf(hora > 0 && hora <= 23, MensagensErro.HoraInvalida),
                Result.SuccessIf(minuto >= 0 && minuto <= 59, MensagensErro.MinutoInvalido));
            if (validacao.IsFailure)
                return Result.Failure<Horario>(validacao.Error);
            return new Horario(hora, minuto);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hora;
            yield return Minuto;
        }
    }
}
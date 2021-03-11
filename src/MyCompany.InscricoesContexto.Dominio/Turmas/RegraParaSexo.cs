using System.Collections.Generic;
using CSharpFunctionalExtensions;
using MyCompany.Inscricoes.Shared;

namespace MyCompany.InscricoesContexto.Dominio.Turmas
{
    public sealed class RegraParaSexo: ValueObject
    {
        private RegraParaSexo() { }

        private RegraParaSexo(in ESexo sexo)
        {
            Sexo = sexo;
        }
        
        public ESexo Sexo { get; }

        public bool EhSexoValido(ESexo sexo)
            => Sexo == sexo;
        
        public static RegraParaSexo CriarMasculino()
            => new RegraParaSexo(ESexo.Masculino);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Sexo;
        }
    }
}
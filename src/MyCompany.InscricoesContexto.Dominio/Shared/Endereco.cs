using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace MyCompany.Inscricoes.Shared
{
    public sealed class Endereco : ValueObject
    {
        private Endereco(){}
        public Endereco(in string rua, in string numero, in string complemento, in string bairro, 
            in string cidade, in string cep, in string uf)
        {
            Rua = rua;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
            Uf = uf;
        }
        
        public string Rua { get; }
        public string Numero { get;  }
        public string Complemento { get; }
        public string Bairro { get; }
        public string Cidade { get; }
        public string Cep { get; }
        public string Uf { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Rua;
            yield return Numero;
            yield return Complemento;
            yield return Bairro;
            yield return Cidade;
            yield return Cep;
            yield return Uf;
        }
    }
}
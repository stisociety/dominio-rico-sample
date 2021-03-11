using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyCompany.Inscricoes.Shared;

namespace MyCompany.InscricoesContexto.Infraestrutura
{
    public static class EFTypeConverters
    {
        public static readonly ValueConverter<Cpf, string> CpfConverter = new ValueConverter<Cpf, string>(
            cpf => cpf.Value,
            cpfNumber => Cpf.CreateWithExcetion(cpfNumber));
    }
}
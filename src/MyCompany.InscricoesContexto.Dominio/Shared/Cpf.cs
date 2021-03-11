using System;
using System.Text;
using CSharpFunctionalExtensions;

namespace MyCompany.Inscricoes.Shared
{
    public sealed class Cpf
    {
        private Cpf(){}
        private Cpf(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<Cpf> Create(string value)
        {
            if (IsValid(value) is var result && result.IsFailure)
                return Result.Failure<Cpf>(result.Error);
            return new Cpf(value);
        }

        public static Cpf CreateWithExcetion(string value)
        {
            var (_, isFailure, error) = IsValid(value);
            if (isFailure)
                throw new InvalidCpfException(error);
            return new Cpf(value);
        }

        public static implicit operator Cpf(string valor) => CreateWithExcetion(valor);

        public override string ToString()
            => string.Format(new CpfFormatter(), "{0}", this);

        #region Métodos de validação

        private static Result IsValid(string value)
        {
            if (!SequenceIsValid(value))
                return Result.Failure("Valor inválido para CPF");
            return !DigitIsValid(value)
                ? Result.Failure("Valor inválido para CPF")
                : Result.Success();
        }

        private static bool SequenceIsValid(string value)
        {
            var isEqualSequence = true;
            for (var i = 1; i < value.Length && isEqualSequence; i++)
                if (value[i] != value[0])
                    isEqualSequence = false;

            return !(isEqualSequence || value == "12345678909");
        }

        private static bool DigitIsValid(string value)
        {
            var tempCpf = value.Substring(0, 9);
            var d1 = GetMod11(tempCpf);
            var d2 = GetMod11($"{tempCpf}{d1}");
            return value.EndsWith($"{d1}{d2}");
        }

        private static string GetMod11(string value)
        {
            var sum = 0;
            for (int i = value.Length - 1, multiplier = 2; i >= 0; i--)
            {
                sum += (int)char.GetNumericValue(value[i]) * multiplier;
                ++multiplier;
            }
            var mod = (sum % 11);
            if (mod == 0 || mod == 1) return "0";
            return (11 - mod).ToString();
        }
        #endregion
    }

    internal sealed class CpfFormatter : ICustomFormatter, IFormatProvider
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg is Cpf cpf)
                return new StringBuilder(cpf.Value).Insert(3, '.').Insert(7, '.').Insert(11, '-').ToString();
            throw new FormatException($"Não é possível formatar objetos de tipos diferentes de CPF ou string");
        }
        public object GetFormat(Type formatType)
            => (formatType == typeof(ICustomFormatter)) ? this : null;
    }

    public sealed class InvalidCpfException : Exception
    {
        public InvalidCpfException() { }

        public InvalidCpfException(string message) : base(message) { }

        public InvalidCpfException(string message, Exception inner) : base(message, inner) { }
    }
}
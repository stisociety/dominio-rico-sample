using System;
using CSharpFunctionalExtensions;
using MyCompany.Inscricoes.Shared;
using MyCompany.InscricoesContexto.Dominio.SeedWork;

namespace MyCompany.InscricoesContexto.Dominio.Alunos
{    
    public sealed class Aluno : Entity<int>, IAgreggateRoot
    {
        private Aluno(){}
        private Aluno(in int id, Cpf cpf, NomeCompleto nomeCompleto, DateTime dataNascimento, Endereco endereco) 
            : base(id)
        {
            Cpf = cpf;
            Nome = nomeCompleto;
            DataNascimento = dataNascimento;
            Endereco = endereco;
        }

        public Cpf Cpf { get;  }
        public NomeCompleto Nome { get; }
        public DateTime DataNascimento { get;  }
        public int Idade => ObterIdadeAtual();
        public Endereco Endereco { get; private set; }

        public void AlterarEndereco(Endereco novoEndereco)
        {
            Endereco = novoEndereco;
        }

        private int ObterIdadeAtual()
        {
            var hoje = DateTime.Now;
            var idade = hoje.Year - DataNascimento.Year;
            if (DataNascimento > hoje.AddYears(-idade)) idade--;
            return idade;
        }
        
        public static Result<Aluno> Criar(Cpf cpf, NomeCompleto nomeCompleto, DateTime dataNascimento,
            Endereco endereco, in int id = 0)
        {
            return new Aluno(id, cpf, nomeCompleto, dataNascimento, endereco);
        }
    }
}
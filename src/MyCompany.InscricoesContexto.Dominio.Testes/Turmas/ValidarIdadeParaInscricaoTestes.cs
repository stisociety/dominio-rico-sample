using System.Collections.Generic;
using MyCompany.Inscricoes.Shared;
using MyCompany.InscricoesContexto.Dominio.Turmas;
using Shouldly;
using Xunit;

namespace MyCompany.InscricoesContexto.Dominio.Testes.Turmas
{
    public class ValidarIdadeParaInscricaoTestes
    {
        [Fact]
        public void
            DadoTurmaComRegraDeIdadeEInformarIdadeValida_QuandoSolicitarValidacaoDeIdade_DevoReceberSucesso()
        {
            var regra = RegraParaLimiteIdade.CriarAtiva(10, 18).Value;
            var turma = Turma.Criar(12, "Teste", 23m, regra, new List<HorarioDisponivel>()).Value;
            
            turma.IdadeEhValidaParaInscricao(10).IsSuccess.ShouldBeTrue();
            turma.IdadeEhValidaParaInscricao(15).IsSuccess.ShouldBeTrue();
            turma.IdadeEhValidaParaInscricao(18).IsSuccess.ShouldBeTrue();
        }
        
        [Fact]
        public void
            DadoTurmaComRegraDeIdadeEInformarIdadeForaDoLimite_QuandoSolicitarValidacaoDeIdade_DevoReceberFalha()
        {
            var regra = RegraParaLimiteIdade.CriarAtiva(10, 18).Value;
            var turma = Turma.Criar(12,"Teste", 23m, regra, new List<HorarioDisponivel>()).Value;

            var resultado = turma.IdadeEhValidaParaInscricao(25);
            
            resultado.IsFailure.ShouldBeTrue();
            resultado.Error.ShouldBe(MensagensErro.IdadeInvalidaParaTurma);
        }
    }
}
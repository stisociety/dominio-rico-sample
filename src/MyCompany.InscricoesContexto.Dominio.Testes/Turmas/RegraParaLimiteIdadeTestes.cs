using MyCompany.InscricoesContexto.Dominio.Turmas;
using Shouldly;
using Xunit;

namespace MyCompany.InscricoesContexto.Dominio.Testes.Turmas
{
    public class RegraParaLimiteIdadeTestes
    {
        [Fact]
        public void
            DadoRegraAtivaComLimitesDeIdadesEIdadeMaiorQueLimiteMaximo_QuandoSolicitarValidacaoDeIdade_DevoReceberIdadeInvalida()
        {
            //Arrange
            var regra = RegraParaLimiteIdade.CriarAtiva(10, 18).Value;

            //Act
            var validacaoIdadeMaior = regra.IdadeEhValida(20);

            //Assert
            validacaoIdadeMaior.ShouldBeFalse();
        }
        
        [Fact]
        public void
            DadoRegraAtivaComLimitesDeIdadesEIdadeMaiorQueLimiteMinimo_QuandoSolicitarValidacaoDeIdade_DevoReceberIdadeInvalida()
        {
            var regra = RegraParaLimiteIdade.CriarAtiva(10, 18).Value;

            var validacaoIdadeMenor = regra.IdadeEhValida(9);

            validacaoIdadeMenor.ShouldBeFalse();
        }
        
        [Fact]
        public void
            DadoRegraAtivaComLimitesDeIdadesEIdadeDentroDoLimite_QuandoSolicitarValidacaoDeIdade_DevoReceberIdadeValida()
        {
            var regra = RegraParaLimiteIdade.CriarAtiva(10, 18).Value;

            regra.IdadeEhValida(10).ShouldBeTrue();
            regra.IdadeEhValida(15).ShouldBeTrue();
            regra.IdadeEhValida(18).ShouldBeTrue();
        }

        [Fact]
        public void DadoRegraInativa_QuandoSolicitarValidacaoDeIdade_DevoReceberIdadeValida()
        {
            var regra = RegraParaLimiteIdade.CriarInativa();

            var resultado = regra.IdadeEhValida(20);
            
            resultado.ShouldBeTrue();
        }
    }
}
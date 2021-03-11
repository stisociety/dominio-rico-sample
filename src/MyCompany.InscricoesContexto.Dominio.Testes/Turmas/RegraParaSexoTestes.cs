using MyCompany.Inscricoes.Shared;
using MyCompany.InscricoesContexto.Dominio.Turmas;
using Shouldly;
using Xunit;

namespace MyCompany.InscricoesContexto.Dominio.Testes.Turmas
{
    public class RegraParaSexoTestes
    {
        [Fact]
        public void DadoRegraParaMeninosEInformarSexoMasculino_QuandoSolicitarValidacaoDeSexo_DevoRetornarSexoValido()
        {
            var regra = RegraParaSexo.CriarMasculino();

            var resultado = regra.EhSexoValido(ESexo.Masculino);

            resultado.ShouldBeTrue();
        }
    }
}
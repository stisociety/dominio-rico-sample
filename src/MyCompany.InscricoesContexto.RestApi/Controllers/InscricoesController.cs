using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using MyCompany.InscricoesContexto.Dominio.Inscricoes.Comandos;
using MyCompany.InscricoesContexto.RestApi.Models;

namespace MyCompany.InscricoesContexto.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscricoesController : ControllerBase
    {
        private readonly IMediator _mediator;

        // Injeção de Dependencia (DI) + Inversão de controle
        public InscricoesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody]NovaInscricaoInputModel novaInscricaoInputModel)
        {
            var comando =
                RealizarInscricaoCommand.Criar(novaInscricaoInputModel.AlunoId, novaInscricaoInputModel.TurmaId);
            if (comando.IsFailure)
                return BadRequest(comando.Error);

            var resultado = await _mediator.Send(comando.Value);
            if (resultado.IsFailure)
                return BadRequest(resultado.Error);

            return Ok(resultado.Value);
        }
    }
}

using System.Security.Permissions;

namespace MyCompany.InscricoesContexto.RestApi.Models
{
    public class NovaInscricaoInputModel
    {
        public int TurmaId { get; set; }
        public int AlunoId { get; set; }
    }
}
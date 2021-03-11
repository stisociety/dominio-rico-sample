namespace MyCompany.InscricoesContexto.RestApi.Models
{
    public class NovaTurmaInputModel
    {
        public int CursoId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public NovoHorarioInputModel[] Horarios { get; set; }
    
        public class NovoHorarioInputModel
        {
            public string DiaDaSemana { get; set; }
            public string HoraInicio { get; set; }
            public string HoraFim { get; set; }
        }
    }
}
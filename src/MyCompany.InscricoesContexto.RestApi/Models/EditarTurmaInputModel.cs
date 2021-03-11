namespace MyCompany.InscricoesContexto.RestApi.Models
{
    public class EditarTurmaInputModel
    {
        
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public HorarioInputModel[] Horarios { get; set; }
        
        public class HorarioInputModel
        {
            public int Id { get; set; }
            public string DiaDaSemana { get; set; }
            public string HoraInicio { get; set; }
            public string HoraFim { get; set; }
            public bool EhAlteracao { get; set; }
            public bool EhDelecao { get; set; }
        }
    }
}
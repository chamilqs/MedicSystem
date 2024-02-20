namespace MedicSystem.Core.Application.ViewModels.ResultadosDeLaboratorio
{
    public class ResultadoDeLaboratorioViewModel
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string NombrePaciente { get; set; }
        public string Resultado { get; set; }
        public int EstadoResultado { get; set; }
        public int PruebaDeLaboratorioId { get; set; }
        public string NombrePrueba { get; set; }
    }
}

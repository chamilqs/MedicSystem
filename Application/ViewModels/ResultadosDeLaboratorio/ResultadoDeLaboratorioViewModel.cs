using MedicSystem.Core.Domain.Enum;

namespace MedicSystem.Core.Application.ViewModels.ResultadosDeLaboratorio
{
    public class ResultadoDeLaboratorioViewModel
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public string CedulaPaciente { get; set; }
        public string? Resultado { get; set; }
        public EstadoResultado EstadoResultado { get; set; }
        public int PruebaDeLaboratorioId { get; set; }
        public string NombrePrueba { get; set; }
        public int CitaId { get; set; }
    }
}

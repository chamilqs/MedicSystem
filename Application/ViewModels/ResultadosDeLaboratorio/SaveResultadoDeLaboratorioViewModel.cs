using MedicSystem.Core.Application.ViewModels.Pacientes;
using MedicSystem.Core.Application.ViewModels.PruebasDeLaboratorio;
using MedicSystem.Core.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace MedicSystem.Core.Application.ViewModels.ResultadosDeLaboratorio
{
    public class SaveResultadoDeLaboratorioViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int PacienteId { get; set; }
        public string? Resultado { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public EstadoResultado EstadoResultado { get; set; }
        public int PruebaDeLaboratorioId { get; set; }
        [Required(ErrorMessage = "Seleccione una prueba de laboratorio.")]
        public List<int> PruebasDeLaboratorioIds { get; set; }
        public int CitaId { get; set; }

        public List<PruebaDeLaboratorioViewModel>? PruebasDeLaboratorio { get; set; }
        public List<PacienteViewModel>? Pacientes { get; set; }
    }
}

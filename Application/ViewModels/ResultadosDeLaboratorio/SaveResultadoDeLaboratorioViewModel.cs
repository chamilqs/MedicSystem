using MedicSystem.Core.Application.ViewModels.Pacientes;
using MedicSystem.Core.Domain.Entities;
using MedicSystem.Core.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace MedicSystem.Core.Application.ViewModels.ResultadosDeLaboratorio
{
    public class SaveResultadoDeLaboratorioViewModel
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        [Required(ErrorMessage = "El resultado de la prueba es requerido.")]
        public string Resultado { get; set; }
        public EstadoResultado EstadoResultado { get; set; }
        public int PruebaDeLaboratorioId { get; set; }

        public List<PruebaDeLaboratorio>? PruebaDeLaboratorio { get; set; }
        public List<PacienteViewModel>? Pacientes { get; set; }
    }
}

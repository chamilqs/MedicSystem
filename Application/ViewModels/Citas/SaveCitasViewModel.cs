using MedicSystem.Core.Application.ViewModels.Medicos;
using MedicSystem.Core.Application.ViewModels.Pacientes;
using MedicSystem.Core.Application.ViewModels.ResultadosDeLaboratorio;
using MedicSystem.Core.Domain.Entities;
using MedicSystem.Core.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace MedicSystem.Core.Application.ViewModels.Citas
{
    public class SaveCitasViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo del paciente es requerido.")]
        public int PacienteId { get; set; }
        [Required(ErrorMessage = "El campo del médico es requerido.")]
        public int MedicoId { get; set; }
        [Required(ErrorMessage = "El campo de la fecha es requerido.")]
        public string Fecha { get; set; }
        [Required(ErrorMessage = "El campo de la hora es requerido.")]
        public string Hora { get; set; }
        [Required(ErrorMessage = "El campo de la causa es requerido.")]
        public string Causa { get; set; }
        [Required(ErrorMessage = "El campo del estado de la cita es requerido.")]
        public EstadoCita? EstadoCita { get; set; }
        public int UsuarioId { get; set; }

        public List<ResultadoDeLaboratorioViewModel> ResultadosDeLaboratorio { get; set; }
        public List<PacienteViewModel>? Pacientes { get; set; }
        public List<MedicoViewModel>? Medicos { get; set; }


    }
}

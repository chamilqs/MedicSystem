using MedicSystem.Core.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace MedicSystem.Core.Application.ViewModels.Citas
{
    public class CitaViewModel
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public int MedicoId { get; set; }
        public string NombreMedico { get; set; }
        public string ApellidoMedico { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Causa { get; set; }
        public EstadoCita? EstadoCita { get; set; }
        public List<string> ResultadosDeLaboratorio { get;}

    }
}

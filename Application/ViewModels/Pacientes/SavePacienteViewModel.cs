using MedicSystem.Core.Application.ViewModels.Citas;
using MedicSystem.Core.Application.ViewModels.Medicos;
using MedicSystem.Core.Application.ViewModels.ResultadosDeLaboratorio;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MedicSystem.Core.Application.ViewModels.Pacientes
{
    public class SavePacienteViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "La direccion es requerida")]
        [DataType(DataType.Text)]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El telefono es requerido")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "La cedula es requerida")]
        [DataType(DataType.Text)]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        [DataType(DataType.Date)]
        public string FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo fumador es requerido")]
        public bool Fumador { get; set; }
        [Required(ErrorMessage = "El campo alergias es requerido")]
        public bool Alergias { get; set; }
        [Required(ErrorMessage = "La foto es requerida")]
        public string? Foto { get; set; }
        public IFormFile? File { get; set; }
        public int UsuarioId { get; set; }

        public List<ResultadoDeLaboratorioViewModel>? ResultadosDeLaboratorio { get; set; }
        public List<CitaViewModel>? Citas { get; set; }
        public List<MedicoViewModel>? Medicos { get; set; }
    }
}

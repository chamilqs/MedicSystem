using MedicSystem.Core.Application.ViewModels.Citas;
using MedicSystem.Core.Application.ViewModels.Medicos;
using MedicSystem.Core.Application.ViewModels.Pacientes;
using MedicSystem.Core.Domain.Enum;

namespace MedicSystem.Core.Application.ViewModels.Usuarios
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public ICollection<PacienteViewModel>? Pacientes { get; set; }
        public ICollection<MedicoViewModel>? Medicos { get; set; }
        public ICollection<CitaViewModel>? Citas { get; set; }
    }
}

using MedicSystem.Core.Domain.Common;
using MedicSystem.Core.Domain.Enum;

namespace MedicSystem.Core.Domain.Entities
{
    public class Usuario : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        // Relaciones: Al crear un paciente, medico o cita, se le asigna un usuario.
        public ICollection<Paciente>? Pacientes { get; set; }
        public ICollection<Medico>? Medicos { get; set; }
        public ICollection<Cita>? Citas { get; set; }
    }
}

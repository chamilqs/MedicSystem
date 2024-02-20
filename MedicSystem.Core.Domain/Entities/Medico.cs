using MedicSystem.Core.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MedicSystem.Core.Domain.Entities
{
    public class Medico : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }
        public string? Foto { get; set; }

        // Un medico puede tener muchos pacientes
        public ICollection<Paciente>? Pacientes { get; set; }

        // Un medico puede tener muchas citas
        public ICollection<Cita> Citas { get; set; }

        // Relacion con la tabla Usuario
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}

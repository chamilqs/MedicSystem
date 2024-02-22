using MedicSystem.Core.Domain.Common;

namespace MedicSystem.Core.Domain.Entities
{
    public class Paciente : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }
        public string FechaNacimiento { get; set; }
        public bool Fumador { get; set; }
        public bool Alergias { get; set; }
        public string? Foto { get; set; }

        // Un paciente puede tener muchos resultados de laboratorio
        public ICollection<ResultadoDeLaboratorio>? ResultadosDeLaboratorio { get; set; }

        // Un paciente puede tener muchas citas
        public ICollection<Cita>? Citas { get; set; }

        // Relacion con la tabla Usuario
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}

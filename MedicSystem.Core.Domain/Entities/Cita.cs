using MedicSystem.Core.Domain.Common;
using MedicSystem.Core.Domain.Enum;

namespace MedicSystem.Core.Domain.Entities
{
    public class Cita : AuditableBaseEntity
    {
        // Navigation property de la tabla Paciente
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }

        // Navigation property de la tabla Medico
        public int MedicoId { get; set; }
        public Medico? Medico { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Causa { get; set; }
        public EstadoCita? EstadoCita { get; set; }

        // Relacion con la tabla Usuario
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        // Many to many 
        public ICollection<ResultadoDeLaboratorio> ResultadosDeLaboratorio { get; set; }

    }
}

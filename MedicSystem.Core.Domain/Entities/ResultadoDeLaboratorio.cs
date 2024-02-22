using MedicSystem.Core.Domain.Common;
using MedicSystem.Core.Domain.Enum;

namespace MedicSystem.Core.Domain.Entities
{
    public class ResultadoDeLaboratorio : AuditableBaseEntity
    {
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }
        public string? Resultado { get; set; }
        public EstadoResultado EstadoResultado { get; set; }

        // Navigation property
        public int PruebaDeLaboratorioId { get; set; }
        public PruebaDeLaboratorio? PruebaDeLaboratorio { get; set; }
        public ICollection<PruebaDeLaboratorio>? PruebasDeLaboratorio { get; set; }
        
        // Many to many
        public int CitaId { get; set; }
        public Cita? Cita { get; set; }
        public ICollection<Cita>? Citas { get; set; }
    }
}

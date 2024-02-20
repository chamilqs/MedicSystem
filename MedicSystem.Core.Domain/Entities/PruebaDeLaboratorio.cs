using MedicSystem.Core.Domain.Common;

namespace MedicSystem.Core.Domain.Entities
{
    public class PruebaDeLaboratorio : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public ICollection<ResultadoDeLaboratorio>? ResultadosDeLaboratorio { get; set; }

    }
}

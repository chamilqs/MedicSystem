using System.ComponentModel.DataAnnotations;

namespace MedicSystem.Core.Application.ViewModels.PruebasDeLaboratorio
{
    public class SavePruebaDeLaboratorioViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la prueba de laboratorio es requerido")]
        public string Nombre { get; set; }

    }
}

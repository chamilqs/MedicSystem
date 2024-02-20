using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace MedicSystem.Core.Application.ViewModels.Medicos
{
    public class SaveMedicoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El correo es requerido")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        [Required(ErrorMessage = "El telefono es requerido")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "La cedula es requerida")]
        [DataType(DataType.Text)]
        public string Cedula { get; set; }
        public string? Foto { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public int UsuarioId { get; set; }
    }
}

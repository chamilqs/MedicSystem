using MedicSystem.Core.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace MedicSystem.Core.Application.ViewModels.Usuarios
{
    public class LoginViewModel
    {        
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        [DataType(DataType.Text)]
        public string NombreUsuario { get; set; }
                
        [Required(ErrorMessage = "La contraseña es requerida.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}

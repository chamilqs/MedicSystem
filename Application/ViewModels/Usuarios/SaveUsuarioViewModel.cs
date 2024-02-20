using MedicSystem.Core.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace MedicSystem.Core.Application.ViewModels.Usuarios
{
    public class SaveUsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre para el usuario es requerido.")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "El apellido para el usuario es requerido.")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }
        
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        [DataType(DataType.Text)]
        public string NombreUsuario { get; set; }
        
        [Required(ErrorMessage = "El correo para el usuario es requerido.")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        
        [Required(ErrorMessage = "La contraseña es requerida.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden.")]
        [Required(ErrorMessage = "Debe confirmar la contraseña.")]
        [DataType(DataType.Password)]
        public string ConfirmarPassword { get; set; }

        [DataType(DataType.Password)]
        public string? PasswordActual { get; set; }

        [Range(0, 2, ErrorMessage = "El tipo de usuario es requerido.")]
        public TipoUsuario TipoUsuario { get; set; }
    }
}

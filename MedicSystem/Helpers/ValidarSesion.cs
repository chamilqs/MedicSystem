using MedicSystem.Core.Application.ViewModels.Usuarios;
using MedicSystem.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace MedicSystem.Helpers
{
    public class ValidarSesion
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidarSesion(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UsuarioViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("CurrentUser");

            if (userViewModel == null)
            {
                return false;
            }
            return true;
        }

        
    }
}

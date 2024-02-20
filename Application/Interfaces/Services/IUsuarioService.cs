using MedicSystem.Core.Application.ViewModels.Usuarios;

namespace MedicSystem.Core.Application.Interfaces.Services
{
    public interface IUsuarioService : IGenericService<SaveUsuarioViewModel, UsuarioViewModel>
    {
        Task<UsuarioViewModel> Login(LoginViewModel loginVm);
    }
}

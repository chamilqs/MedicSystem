using MedicSystem.Core.Application.ViewModels.Usuarios;
using MedicSystem.Core.Domain.Entities;

namespace MedicSystem.Core.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
       Task<Usuario> LoginAsync(LoginViewModel loginVm);

    }
}

using MedicSystem.Core.Application.Interfaces.Repositories;
using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.ViewModels.Usuarios;
using MedicSystem.Core.Domain.Entities;

namespace MedicSystem.Core.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<SaveUsuarioViewModel> Add(SaveUsuarioViewModel vm)
        {        
            Usuario usuario = new();

            usuario.Id = vm.Id;
            usuario.Nombre = vm.Nombre;
            usuario.Apellido = vm.Apellido;
            usuario.NombreUsuario = vm.NombreUsuario;
            usuario.Correo = vm.Correo;
            usuario.Password = vm.Password;
            usuario.TipoUsuario = vm.TipoUsuario;

            usuario = await _usuarioRepository.AddAsync(usuario);

            if (usuario == null)
            {
                return null;
            }
            else
            {
                SaveUsuarioViewModel vmUsuario = new();

                vmUsuario.Id = usuario.Id;
                vmUsuario.Nombre = usuario.Nombre;
                vmUsuario.Apellido = usuario.Apellido;
                vmUsuario.NombreUsuario = usuario.NombreUsuario;
                vmUsuario.Correo = usuario.Correo;
                vmUsuario.Password = usuario.Password;
                vmUsuario.TipoUsuario = usuario.TipoUsuario;

                return vmUsuario;
            }

            
        }
        
        public async Task Update(SaveUsuarioViewModel vm)
        {
            Usuario usuario = await _usuarioRepository.GetByIdAsync(vm.Id);
            usuario.Nombre = vm.Nombre;
            usuario.Apellido = vm.Apellido;
            usuario.NombreUsuario = vm.NombreUsuario;
            usuario.Correo = vm.Correo;
            usuario.Password = vm.Password;
            usuario.TipoUsuario = vm.TipoUsuario;

            await _usuarioRepository.UpdateAsync(usuario);

        }

        public async Task Delete(int id)
        {
            var product = await _usuarioRepository.GetByIdAsync(id);
            await _usuarioRepository.DeleteAsync(product);
        }

        public async Task<List<UsuarioViewModel>> GetAllViewModel()
        {
            // Cambiar por GetAllWithIncludeAsync
            var userList = await _usuarioRepository.GetAllAsync();

            return userList.Select(user => new UsuarioViewModel
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                NombreUsuario = user.NombreUsuario,                
                Correo = user.Correo,
                Password = user.Password,
                TipoUsuario = user.TipoUsuario

            }).ToList();
        }

        public async Task<SaveUsuarioViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _usuarioRepository.GetByIdAsync(id);

            SaveUsuarioViewModel vm = new();

            vm.Id = user.Id;
            vm.Nombre = user.Nombre;
            vm.Apellido = user.Apellido;
            vm.NombreUsuario = user.NombreUsuario;
            vm.Correo = user.Correo;
            vm.Password = user.Password;
            vm.ConfirmarPassword = user.Password;
            vm.TipoUsuario = user.TipoUsuario;

            return vm;
        }

        public async Task<UsuarioViewModel> Login (LoginViewModel loginVm)
        {
            try
            {
                Usuario user = await _usuarioRepository.LoginAsync(loginVm);

                if (user == null)
                {
                    return null;
                }
                else
                {
                    UsuarioViewModel vm = new();

                    vm.Id = user.Id;
                    vm.Nombre = user.Nombre;
                    vm.Apellido = user.Apellido;
                    vm.NombreUsuario = user.NombreUsuario;
                    vm.Correo = user.Correo;
                    vm.Password = user.Password;
                    vm.TipoUsuario = user.TipoUsuario;

                    return vm;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }

        }

    }
}

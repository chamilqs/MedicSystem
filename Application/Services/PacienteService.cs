using MedicSystem.Core.Application.Helpers;
using MedicSystem.Core.Application.Interfaces.Repositories;
using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.ViewModels.Medicos;
using MedicSystem.Core.Application.ViewModels.Pacientes;
using MedicSystem.Core.Application.ViewModels.Usuarios;
using MedicSystem.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace MedicSystem.Core.Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsuarioViewModel userViewModel;

        public PacienteService(IPacienteRepository pacienteRepository, IHttpContextAccessor httpContextAccessor)
        {
            _pacienteRepository = pacienteRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("CurrentUser");
        }

        public async Task Update(SavePacienteViewModel vm)
        {
            Paciente paciente = await _pacienteRepository.GetByIdAsync(vm.Id);

            paciente.Id = vm.Id;
            paciente.Nombre = vm.Nombre;
            paciente.Apellido = vm.Apellido;
            paciente.Direccion = vm.Direccion;
            paciente.FechaNacimiento = vm.FechaNacimiento;
            paciente.Cedula = vm.Cedula;
            paciente.Fumador = vm.Fumador;
            paciente.Alergias = vm.Alergias;
            paciente.Telefono = vm.Telefono;
            paciente.Foto = vm.Foto;

            await _pacienteRepository.UpdateAsync(paciente);
        }

        public async Task<SavePacienteViewModel> Add(SavePacienteViewModel vm)
        {
            Paciente paciente = new();

            paciente.Id = vm.Id;
            paciente.Nombre = vm.Nombre;
            paciente.Apellido = vm.Apellido;
            paciente.Direccion = vm.Direccion;
            paciente.FechaNacimiento = vm.FechaNacimiento;
            paciente.Cedula = vm.Cedula;
            paciente.Fumador = vm.Fumador;
            paciente.Alergias = vm.Alergias;
            paciente.Telefono = vm.Telefono;
            paciente.Foto = vm.Foto;
            paciente.UsuarioId = userViewModel.Id;

            paciente = await _pacienteRepository.AddAsync(paciente);

            SavePacienteViewModel pacienteVm = new();

            pacienteVm.Id = paciente.Id;
            pacienteVm.Nombre = paciente.Nombre;
            pacienteVm.Apellido = paciente.Apellido;
            pacienteVm.Direccion = paciente.Direccion;
            pacienteVm.FechaNacimiento = paciente.FechaNacimiento;
            pacienteVm.Cedula = paciente.Cedula;
            pacienteVm.Fumador = paciente.Fumador;
            pacienteVm.Alergias = paciente.Alergias;
            pacienteVm.Telefono = paciente.Telefono;
            pacienteVm.Foto = paciente.Foto;

            return pacienteVm;
        }

        public async Task Delete(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);
            await _pacienteRepository.DeleteAsync(paciente);
        }

        public async Task<SavePacienteViewModel> GetByIdSaveViewModel(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);

            SavePacienteViewModel pacienteVm = new();

            pacienteVm.Id = paciente.Id;
            pacienteVm.Nombre = paciente.Nombre;
            pacienteVm.Apellido = paciente.Apellido;
            pacienteVm.Direccion = paciente.Direccion;
            pacienteVm.FechaNacimiento = paciente.FechaNacimiento;
            pacienteVm.Cedula = paciente.Cedula;
            pacienteVm.Fumador = paciente.Fumador;
            pacienteVm.Alergias = paciente.Alergias;
            pacienteVm.Telefono = paciente.Telefono;
            pacienteVm.Foto = paciente.Foto;

            return pacienteVm;
        }

        public async Task<List<PacienteViewModel>> GetAllViewModel()
        {
            var pacienteList = await _pacienteRepository.GetAllAsync();

            return pacienteList.Select(paciente => new PacienteViewModel
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                Direccion = paciente.Direccion,
                FechaNacimiento = paciente.FechaNacimiento,
                Cedula = paciente.Cedula,
                Fumador = paciente.Fumador,
                Alergias = paciente.Alergias,
                Telefono = paciente.Telefono,
                Foto = paciente.Foto

            }).ToList();
        }
    }
}

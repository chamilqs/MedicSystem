using MedicSystem.Core.Application.Helpers;
using MedicSystem.Core.Application.Interfaces.Repositories;
using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.ViewModels.Medicos;
using MedicSystem.Core.Application.ViewModels.Usuarios;
using MedicSystem.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace MedicSystem.Core.Application.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsuarioViewModel userViewModel;

        public MedicoService(IMedicoRepository medicoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _medicoRepository = medicoRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("CurrentUser");
        }

        public async Task Update(SaveMedicoViewModel vm)
        {
            Medico medico = await _medicoRepository.GetByIdAsync(vm.Id);
            medico.Id = vm.Id;
            medico.Nombre = vm.Nombre;
            medico.Apellido = vm.Apellido;
            medico.Cedula = vm.Cedula;
            medico.Correo = vm.Correo;
            medico.Telefono = vm.Telefono;
            medico.Foto = vm.Foto;

            await _medicoRepository.UpdateAsync(medico);
        }

        public async Task<SaveMedicoViewModel> Add(SaveMedicoViewModel vm)
        {
            Medico medico = new();
            medico.Id = vm.Id;
            medico.Nombre = vm.Nombre;
            medico.Apellido = vm.Apellido;
            medico.Cedula = vm.Cedula;
            medico.Correo = vm.Correo;
            medico.Telefono = vm.Telefono;
            medico.Foto = vm.Foto;
            medico.UsuarioId = userViewModel.Id;

            medico = await _medicoRepository.AddAsync(medico);

            SaveMedicoViewModel medicoVm = new();

            medicoVm.Id = medico.Id;
            medicoVm.Nombre = medico.Nombre;
            medicoVm.Apellido = medico.Apellido;
            medicoVm.Cedula = medico.Cedula;
            medicoVm.Correo = medico.Correo;
            medicoVm.Telefono = medico.Telefono;
            medicoVm.Foto = medico.Foto;

            return medicoVm;
        }

        public async Task Delete(int id)
        {
            var medico = await _medicoRepository.GetByIdAsync(id);
            await _medicoRepository.DeleteAsync(medico);
        }

        public async Task<SaveMedicoViewModel> GetByIdSaveViewModel(int id)
        {
            var medico = await _medicoRepository.GetByIdAsync(id);

            SaveMedicoViewModel medicoVm = new();

            medicoVm.Id = medico.Id;
            medicoVm.Nombre = medico.Nombre;
            medicoVm.Apellido = medico.Apellido;
            medicoVm.Cedula = medico.Cedula;
            medicoVm.Correo = medico.Correo;
            medicoVm.Telefono = medico.Telefono;
            medicoVm.Foto = medico.Foto;

            return medicoVm;
        }

        public async Task<List<MedicoViewModel>> GetAllViewModel()
        {
            var medicoList = await _medicoRepository.GetAllAsync();

            return medicoList.Select(medico => new MedicoViewModel
            {
                Id = medico.Id,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Cedula = medico.Cedula,
                Correo = medico.Correo,
                Telefono = medico.Telefono,
                Foto = medico.Foto

            }).ToList();
        }
    }
}

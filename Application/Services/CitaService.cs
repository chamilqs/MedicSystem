﻿using MedicSystem.Core.Application.Helpers;
using MedicSystem.Core.Application.Interfaces.Repositories;
using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.ViewModels.Citas;
using MedicSystem.Core.Application.ViewModels.Usuarios;
using MedicSystem.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicSystem.Core.Application.Services
{
    public class CitaService : ICitaService
    {

        private readonly ICitaRepository _citaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsuarioViewModel userViewModel;

        public CitaService(ICitaRepository citaRepository, IHttpContextAccessor httpContextAccessor)
        {
            _citaRepository = citaRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("CurrentUser");
        }

        public async Task<SaveCitasViewModel> Add(SaveCitasViewModel vm)
        {
            Cita cita = new();
            cita.Id = vm.Id;
            cita.Fecha = vm.Fecha;
            cita.Hora = vm.Hora;
            cita.Causa = vm.Causa;
            cita.EstadoCita = vm.EstadoCita;
            cita.PacienteId = vm.PacienteId;
            cita.MedicoId = vm.MedicoId;
            cita.UsuarioId = userViewModel.Id;

            cita = await _citaRepository.AddAsync(cita);

            SaveCitasViewModel citaVm = new();
            citaVm.Id = vm.Id;
            citaVm.Fecha = vm.Fecha;
            citaVm.Hora = vm.Hora;
            citaVm.Causa = vm.Causa;
            citaVm.EstadoCita = vm.EstadoCita;
            citaVm.PacienteId = vm.PacienteId;
            citaVm.MedicoId = vm.MedicoId;

            return citaVm;

        }
        public Task Update(SaveCitasViewModel vm)
        {
            Cita cita = new();
            cita.Id = vm.Id;
            cita.Fecha = vm.Fecha;
            cita.Hora = vm.Hora;
            cita.Causa = vm.Causa;
            cita.EstadoCita = vm.EstadoCita;
            cita.PacienteId = vm.PacienteId;
            cita.MedicoId = vm.MedicoId;

            return _citaRepository.UpdateAsync(cita);
        }
        public Task Delete(int id)
        {
            var cita = _citaRepository.GetByIdAsync(id);
            return _citaRepository.DeleteAsync(cita.Result);
        }

        public async Task<List<CitaViewModel>> GetAllViewModel()
        {
            var citaList = await _citaRepository.GetAllWithIncludeAsync(new List<string> { "ResultadoDeLaboratorio", "Medico", "Usuario" });

            return citaList.Where(cita => cita.UsuarioId == userViewModel.Id).Select(c => new CitaViewModel
            {
                Id = c.Id,
                Fecha = c.Fecha,
                Hora = c.Hora,
                Causa = c.Causa,
                EstadoCita = c.EstadoCita,
                PacienteId = c.PacienteId,
                MedicoId = c.MedicoId,
               
            }).ToList();
        }

        public async Task<SaveCitasViewModel> GetByIdSaveViewModel(int id)
        {
            var cita = await _citaRepository.GetByIdAsync(id);

            SaveCitasViewModel citaVm = new();

            citaVm.Id = cita.Id;
            citaVm.Fecha = cita.Fecha;
            citaVm.Hora = cita.Hora;
            citaVm.Causa = cita.Causa;
            citaVm.EstadoCita = cita.EstadoCita;
            citaVm.PacienteId = cita.PacienteId;
            citaVm.MedicoId = cita.MedicoId;

            return citaVm;

        }


    }
}
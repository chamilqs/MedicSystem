using MedicSystem.Core.Application.Helpers;
using MedicSystem.Core.Application.Interfaces.Repositories;
using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.ViewModels.ResultadosDeLaboratorio;
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
    public class ResultadoDeLaboratorioService : IResultadoDeLaboratorioService
    {
        private readonly IResultadoDeLaboratorioRepository _resultadoDeLaboratorioRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsuarioViewModel userViewModel;

        public ResultadoDeLaboratorioService(IResultadoDeLaboratorioRepository resultadoDeLaboratorioRepository, IHttpContextAccessor httpContextAccessor)
        {
            _resultadoDeLaboratorioRepository = resultadoDeLaboratorioRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("CurrentUser");
        }

        public async Task<SaveResultadoDeLaboratorioViewModel> Add(SaveResultadoDeLaboratorioViewModel vm)
        {
            ResultadoDeLaboratorio resultadoDeLaboratorio = new();

            resultadoDeLaboratorio.Id = vm.Id;
            resultadoDeLaboratorio.PacienteId = vm.PacienteId;
            resultadoDeLaboratorio.Resultado = vm.Resultado;
            resultadoDeLaboratorio.EstadoResultado = vm.EstadoResultado;
            resultadoDeLaboratorio.PruebaDeLaboratorioId = vm.PruebaDeLaboratorioId;

            resultadoDeLaboratorio = await _resultadoDeLaboratorioRepository.AddAsync(resultadoDeLaboratorio);

            SaveResultadoDeLaboratorioViewModel vmResultadoDeLaboratorio = new();

            vmResultadoDeLaboratorio.Id = resultadoDeLaboratorio.Id;
            vmResultadoDeLaboratorio.PacienteId = resultadoDeLaboratorio.PacienteId;
            vmResultadoDeLaboratorio.Resultado = resultadoDeLaboratorio.Resultado;
            vmResultadoDeLaboratorio.EstadoResultado = resultadoDeLaboratorio.EstadoResultado;
            vmResultadoDeLaboratorio.PruebaDeLaboratorioId = resultadoDeLaboratorio.PruebaDeLaboratorioId;

            return vmResultadoDeLaboratorio;

        }

        public async Task Delete(int id)
        {
            var resultadoDeLaboratorio = await _resultadoDeLaboratorioRepository.GetByIdAsync(id);
            await _resultadoDeLaboratorioRepository.DeleteAsync(resultadoDeLaboratorio);          
        }

        public async Task<List<ResultadoDeLaboratorioViewModel>> GetAllViewModel()
        {
            var resultadosList = await _resultadoDeLaboratorioRepository.GetAllWithIncludeAsync(new List<string> { "Cita" });

            return resultadosList.Select(resultado => new ResultadoDeLaboratorioViewModel
            {
                Id = resultado.Id,
                PacienteId = resultado.PacienteId,
                NombrePaciente = resultado.Paciente.Nombre,
                ApellidoPaciente = resultado.Paciente.Apellido,
                Resultado = resultado.Resultado,
                EstadoResultado = resultado.EstadoResultado,
                PruebaDeLaboratorioId = resultado.PruebaDeLaboratorioId,
                NombrePrueba = resultado.PruebaDeLaboratorio.Nombre

            }).ToList();
        }

        public async Task<SaveResultadoDeLaboratorioViewModel> GetByIdSaveViewModel(int id)
        {
            var resultado = await _resultadoDeLaboratorioRepository.GetByIdAsync(id);
        
            SaveResultadoDeLaboratorioViewModel vm = new();

            vm.Id = resultado.Id;
            vm.PacienteId = resultado.PacienteId;
            vm.Resultado = resultado.Resultado;
            vm.EstadoResultado = resultado.EstadoResultado;
            vm.PruebaDeLaboratorioId = resultado.PruebaDeLaboratorioId;

            return vm;

        }

        public async Task Update(SaveResultadoDeLaboratorioViewModel vm)
        {
            ResultadoDeLaboratorio resultadoDeLaboratorio = new();

            resultadoDeLaboratorio.Id = vm.Id;
            resultadoDeLaboratorio.PacienteId = vm.PacienteId;
            resultadoDeLaboratorio.Resultado = vm.Resultado;
            resultadoDeLaboratorio.EstadoResultado = vm.EstadoResultado;
            resultadoDeLaboratorio.PruebaDeLaboratorioId = vm.PruebaDeLaboratorioId;

            await _resultadoDeLaboratorioRepository.UpdateAsync(resultadoDeLaboratorio);
        }
    }
}

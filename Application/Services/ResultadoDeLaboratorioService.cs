using MedicSystem.Core.Application.Helpers;
using MedicSystem.Core.Application.Interfaces.Repositories;
using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.ViewModels.ResultadosDeLaboratorio;
using MedicSystem.Core.Application.ViewModels.Usuarios;
using MedicSystem.Core.Domain.Entities;
using MedicSystem.Core.Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace MedicSystem.Core.Application.Services
{
    public class ResultadoDeLaboratorioService : IResultadoDeLaboratorioService
    {
        private readonly IResultadoDeLaboratorioRepository _resultadoDeLaboratorioRepository;

        public ResultadoDeLaboratorioService(IResultadoDeLaboratorioRepository resultadoDeLaboratorioRepository, IHttpContextAccessor httpContextAccessor)
        {
            _resultadoDeLaboratorioRepository = resultadoDeLaboratorioRepository;

        }

        public async Task<SaveResultadoDeLaboratorioViewModel> Add(SaveResultadoDeLaboratorioViewModel vm)
        {
            ResultadoDeLaboratorio resultado = new();

            // resultadoDeLaboratorio.Id = vm.Id;
            resultado.PacienteId = vm.PacienteId;
            resultado.Resultado = vm.Resultado;
            resultado.EstadoResultado = vm.EstadoResultado;
            resultado.PruebaDeLaboratorioId = vm.PruebaDeLaboratorioId;
            resultado.CitaId = vm.CitaId;

            resultado = await _resultadoDeLaboratorioRepository.AddAsync(resultado);

            SaveResultadoDeLaboratorioViewModel vmResultado = new();

            vmResultado.Id = resultado.Id;
            vmResultado.PacienteId = resultado.PacienteId;
            vmResultado.Resultado = resultado.Resultado;
            vmResultado.EstadoResultado = resultado.EstadoResultado;
            vmResultado.PruebaDeLaboratorioId = resultado.PruebaDeLaboratorioId;
            vmResultado.CitaId = resultado.CitaId;

            return vmResultado;

        }

        public async Task Delete(int id)
        {
            var resultadoDeLaboratorio = await _resultadoDeLaboratorioRepository.GetByIdAsync(id);
            await _resultadoDeLaboratorioRepository.DeleteAsync(resultadoDeLaboratorio);          
        }

        public async Task<List<ResultadoDeLaboratorioViewModel>> GetAllViewModel()
        {
            var resultadosList = await _resultadoDeLaboratorioRepository.GetAllWithIncludeAsync(new List<string> { "Paciente", "PruebaDeLaboratorio" });

            return resultadosList.Select(resultado => new ResultadoDeLaboratorioViewModel
            {
                Id = resultado.Id,
                PacienteId = resultado.PacienteId,
                NombrePaciente = resultado.Paciente.Nombre,
                ApellidoPaciente = resultado.Paciente.Apellido,
                CedulaPaciente = resultado.Paciente.Cedula,
                Resultado = resultado.Resultado,
                EstadoResultado = resultado.EstadoResultado,
                PruebaDeLaboratorioId = resultado.PruebaDeLaboratorioId,
                NombrePrueba = resultado.PruebaDeLaboratorio.Nombre,
                CitaId = resultado.CitaId

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
            vm.CitaId = resultado.CitaId;

            return vm;

        }

        public async Task<List<ResultadoDeLaboratorioViewModel>> GetByCitaIdAsync(int citaId)
        {
            var resultadoList = await _resultadoDeLaboratorioRepository.GetAllWithIncludeAsync(new List<string> { "Paciente", "PruebaDeLaboratorio" });

            var listViewModels = resultadoList.Select(resultado => new ResultadoDeLaboratorioViewModel
            {
                Id = resultado.Id,
                PacienteId = resultado.PacienteId,
                NombrePaciente = resultado.Paciente.Nombre,
                ApellidoPaciente = resultado.Paciente.Apellido,
                CedulaPaciente = resultado.Paciente.Cedula,
                Resultado = resultado.Resultado,
                EstadoResultado = resultado.EstadoResultado,
                PruebaDeLaboratorioId = resultado.PruebaDeLaboratorioId,
                NombrePrueba = resultado.PruebaDeLaboratorio.Nombre,
                CitaId = resultado.CitaId

            }).ToList();

            if (citaId != 0)
            {
                listViewModels = listViewModels.Where(cita => cita.CitaId.Equals(citaId)).ToList();
            }

            return listViewModels;
        }

        public async Task Update(SaveResultadoDeLaboratorioViewModel vm)
        {
            ResultadoDeLaboratorio resultado = new();

            resultado.Id = vm.Id;
            resultado.PacienteId = vm.PacienteId;
            resultado.Resultado = vm.Resultado;
            resultado.EstadoResultado = vm.EstadoResultado;
            resultado.PruebaDeLaboratorioId = vm.PruebaDeLaboratorioId;
            resultado.CitaId = vm.CitaId;

            await _resultadoDeLaboratorioRepository.UpdateAsync(resultado);
        }

        public async Task UpdateState(int id, EstadoResultado estadoResultado)
        {
            var resultadoDeLaboratorio = await _resultadoDeLaboratorioRepository.GetByIdAsync(id);
            resultadoDeLaboratorio.EstadoResultado = estadoResultado;
            await _resultadoDeLaboratorioRepository.UpdateAsync(resultadoDeLaboratorio);
        }

        public async Task UpdateResultado(int id, string resultado)
        {
            var resultadoDeLaboratorio = await _resultadoDeLaboratorioRepository.GetByIdAsync(id);
            resultadoDeLaboratorio.Resultado = resultado;
            await _resultadoDeLaboratorioRepository.UpdateAsync(resultadoDeLaboratorio);
        }

        public async Task<List<ResultadoDeLaboratorioViewModel>> GetByCedulaAsync(string cedula)
        {
            var resultadoList = await _resultadoDeLaboratorioRepository.GetAllWithIncludeAsync(new List<string> { "Paciente", "PruebaDeLaboratorio" });

            var listViewModels = resultadoList.Select(resultado => new ResultadoDeLaboratorioViewModel
            {
                Id = resultado.Id,
                PacienteId = resultado.PacienteId,
                NombrePaciente = resultado.Paciente.Nombre,
                ApellidoPaciente = resultado.Paciente.Apellido,
                CedulaPaciente = resultado.Paciente.Cedula,
                Resultado = resultado.Resultado,
                EstadoResultado = resultado.EstadoResultado,
                PruebaDeLaboratorioId = resultado.PruebaDeLaboratorioId,
                NombrePrueba = resultado.PruebaDeLaboratorio.Nombre,
                CitaId = resultado.CitaId

            }).ToList();

            if (cedula != null)
            {
                listViewModels = listViewModels.Where(resultado => resultado.CedulaPaciente.Trim().Contains(cedula.Trim())).ToList();
            }

            return listViewModels;
        }

    }
}

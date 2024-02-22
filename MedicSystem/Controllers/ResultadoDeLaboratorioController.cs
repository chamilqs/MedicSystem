using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.ViewModels.ResultadosDeLaboratorio;
using MedicSystem.Core.Domain.Entities;
using MedicSystem.Core.Domain.Enum;
using MedicSystem.Helpers;
using MedicSystem.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MedicSystem.Controllers
{
    public class ResultadoDeLaboratorioController : Controller
    {
        private readonly IResultadoDeLaboratorioService _resultadoDeLaboratorioService;
        private readonly IPruebaDeLaboratorioService _pruebaDeLaboratorioService;
        private readonly ICitaService _citaService;
        private readonly IPacienteService _pacienteService;
        private readonly ValidarSesion _validarsesion;

        public ResultadoDeLaboratorioController(ICitaService citaService, ValidarSesion validarSesion, IResultadoDeLaboratorioService resultadoDeLaboratorioService, IPruebaDeLaboratorioService pruebaDeLaboratorioService, IPacienteService pacienteService)
        {
            _resultadoDeLaboratorioService = resultadoDeLaboratorioService;
            _citaService = citaService;
            _validarsesion = validarSesion;
            _pruebaDeLaboratorioService = pruebaDeLaboratorioService;
            _pacienteService = pacienteService;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            var resultados = await _resultadoDeLaboratorioService.GetAllViewModel();
            return View(resultados);
        }

        public IActionResult NoAccess()
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            return View("~/Views/Shared/Error.cshtml");
        }

        public async Task<IActionResult> Search(string cedula)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            return View("Index", await _resultadoDeLaboratorioService.GetByCedulaAsync(cedula));
        }

        public async Task<IActionResult> Consultar(int id)
        {

            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            SaveResultadoDeLaboratorioViewModel vm = new();
            vm.PruebasDeLaboratorio = await _pruebaDeLaboratorioService.GetAllViewModel();
            vm.Pacientes = await _pacienteService.GetAllViewModel();

            return View("ConsultarCita", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Consultar(SaveResultadoDeLaboratorioViewModel result, int id)
        {

            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            if (result.PruebasDeLaboratorioIds == null || result.PruebasDeLaboratorioIds.Count == 0)
            {
                result.PruebasDeLaboratorio = await _pruebaDeLaboratorioService.GetAllViewModel();
                result.Pacientes = await _pacienteService.GetAllViewModel();
                return View("ConsultarCita", result);
            }
            
            var cita = await _citaService.GetByIdSaveViewModel(id);
            
            foreach (var item in result.PruebasDeLaboratorioIds)
            {
                result.CitaId = cita.Id;
                result.PacienteId = cita.PacienteId;
                result.EstadoResultado = EstadoResultado.Pendiente; 
                result.PruebaDeLaboratorioId = item;

                await _resultadoDeLaboratorioService.Add(result);
            }

            await _citaService.UpdateState(cita.Id, EstadoCita.PendienteResultados);
            return RedirectToRoute(new { controller = "Cita", action = "Index" });
        }

        public async Task<IActionResult> Reportar(int id)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            return View("ReportarResultado", new SaveResultadoDeLaboratorioViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Reportar(SaveResultadoDeLaboratorioViewModel result, int id)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            if (result.Resultado == null || result.Resultado.IsNullOrEmpty())
            {
                return View("ReportarResultado", result);
            }

            await _resultadoDeLaboratorioService.UpdateResultado(id, result.Resultado);
            await _resultadoDeLaboratorioService.UpdateState(id, EstadoResultado.Completado);

            return RedirectToRoute(new { controller = "ResultadoDeLaboratorio", action = "Index" });
        }
    }
}

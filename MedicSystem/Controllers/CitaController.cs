using MedicSystem.Core.Application.Helpers;
using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.ViewModels.Citas;
using MedicSystem.Core.Application.ViewModels.Usuarios;
using MedicSystem.Core.Domain.Entities;
using MedicSystem.Core.Domain.Enum;
using MedicSystem.Helpers;
using MedicSystem.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MedicSystem.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaService _citaService;
        private readonly IResultadoDeLaboratorioService _resultadoDeLaboratorioService;
        private readonly IMedicoService _medicoService;
        private readonly IPacienteService _pacienteService;
        private readonly ValidarSesion _validarsesion;
        private readonly ApplicationContext _dbContext;
        
        public CitaController(ICitaService citaService, ValidarSesion validarSesion, IMedicoService medicoService, IPacienteService pacienteService, IResultadoDeLaboratorioService resultadoDeLaboratorioService, ApplicationContext dbContext)
        {
            _citaService = citaService;
            _validarsesion = validarSesion;
            _medicoService = medicoService;
            _pacienteService = pacienteService;
            _resultadoDeLaboratorioService = resultadoDeLaboratorioService;
            _dbContext = dbContext;
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

            return View(await _citaService.GetAllViewModel());
        }

        public IActionResult NoAccess()
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            return View("~/Views/Shared/Error.cshtml");
        }

        public async Task<IActionResult> Add()
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            SaveCitasViewModel vm = new();
            vm.Medicos = await _medicoService.GetAllViewModel();
            vm.Pacientes = await _pacienteService.GetAllViewModel();

            return View("SaveCita", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SaveCitasViewModel vm)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            if (!ModelState.IsValid)
            {
                vm.Medicos = await _medicoService.GetAllViewModel();
                vm.Pacientes = await _pacienteService.GetAllViewModel();
                return View("SaveCita", vm);
            }

            if (!vm.EstadoCita.HasValue)
            {
                vm.EstadoCita = EstadoCita.PendienteConsulta;
                SaveCitasViewModel cita = await _citaService.Add(vm);

            }

            return RedirectToRoute(new { controller = "Cita", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            var paciente = await _dbContext.Citas
                                .Where(c => c.Id == id)
                                .Join(_dbContext.Pacientes,
                                        cita => cita.PacienteId,
                                        paciente => paciente.Id,
                                        (cita, paciente) => 
                                        new { Cita = cita, PacienteNombre = paciente.Nombre, PacienteApellido = paciente.Apellido })
                                .FirstOrDefaultAsync();

            ViewBag.Paciente = $"{paciente.PacienteNombre} {paciente.PacienteApellido}";
            return View(await _citaService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            await _citaService.Delete(id);
            return RedirectToRoute(new { controller = "Cita", action = "Index" });
        }

        public async Task<IActionResult> ConsultarResultados(int id)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            ViewBag.IdCita = id;
            return View("Resultados", await _resultadoDeLaboratorioService.GetByCitaIdAsync(id));
        }

        public async Task<IActionResult> FinalizarCita(int id)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            await _citaService.UpdateState(id, EstadoCita.Completada);
            return RedirectToRoute(new { controller = "Cita", action = "Index" });
        }

        public async Task<IActionResult> VerResultados(int id)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            return View("VerResultados", await _resultadoDeLaboratorioService.GetByCitaIdAsync(id));
        }

    }
}

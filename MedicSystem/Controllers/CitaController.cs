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
        private readonly IMedicoService _medicoService;
        private readonly IPacienteService _pacienteService;
        private readonly ValidarSesion _validarsesion;
        private readonly ApplicationContext _dbContext;
        
        public CitaController(ICitaService citaService, ValidarSesion validarSesion, IMedicoService medicoService, IPacienteService pacienteService, ApplicationContext dbContext)
        {
            _citaService = citaService;
            _validarsesion = validarSesion;
            _medicoService = medicoService;
            _pacienteService = pacienteService;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
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

            //if (!ModelState.IsValid)
            //{
            //    vm.Medicos = await _medicoService.GetAllViewModel();
            //    vm.Pacientes = await _pacienteService.GetAllViewModel();
            //    return View("SaveCita", vm);
            //}

            if (!vm.EstadoCita.HasValue)
            {
                vm.EstadoCita = EstadoCita.PendienteConsulta;
                Console.WriteLine(vm.Hora);
                SaveCitasViewModel cita = await _citaService.Add(vm);

            }

            return RedirectToRoute(new { controller = "Cita", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            SaveCitasViewModel vm = await _citaService.GetByIdSaveViewModel(id);

            vm.Medicos = await _medicoService.GetAllViewModel();
            vm.Pacientes = await _pacienteService.GetAllViewModel();

            return View("SaveCita", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveCitasViewModel vm, ResultadoDeLaboratorio vmR)
        {
            var cita = await _citaService.GetByIdSaveViewModel(vm.Id);

            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveCita", vm);
            }

            if(cita.EstadoCita == EstadoCita.PendienteConsulta)
            {
                // Tratar de hacerlo redireccionando a la vista de resultados de laboratorio
                await _citaService.Add(vm);
            }

            return RedirectToRoute(new { controller = "Cita", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
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

            await _citaService.Delete(id);
            return RedirectToRoute(new { controller = "Cita", action = "Index" });
        }

        public async Task<IActionResult> Consult(int id)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            var cita = await _citaService.GetByIdSaveViewModel(id);
            var paciente = await _pacienteService.GetByIdSaveViewModel(cita.PacienteId);
            var medico = await _medicoService.GetByIdSaveViewModel(cita.MedicoId);

            ViewBag.Paciente = $"{paciente.Nombre} {paciente.Apellido}";
            ViewBag.Medico = $"{medico.Nombre} {medico.Apellido}";
            ViewBag.IdCita = id;

            return View("~/Views/ResultadoDeLaboratorio/New.cshtml", new ResultadoDeLaboratorio());
        }

    }
}

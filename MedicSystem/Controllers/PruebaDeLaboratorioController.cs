using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.ViewModels.PruebasDeLaboratorio;
using MedicSystem.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace MedicSystem.Controllers
{
    public class PruebaDeLaboratorioController : Controller
    {
        private readonly IPruebaDeLaboratorioService _pruebaService;
        private readonly ValidarSesion _validarsesion;

        public PruebaDeLaboratorioController(IPruebaDeLaboratorioService pruebaService, ValidarSesion validarSesion)
        {
            _pruebaService = pruebaService;
            _validarsesion = validarSesion;
        }
        public async Task<IActionResult> Index()
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            return View(await _pruebaService.GetAllViewModel());
        }

        public IActionResult NoAccess()
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            return View("~/Views/Shared/Error.cshtml");
        }

        public IActionResult Add()
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            SavePruebaDeLaboratorioViewModel vm = new();
            return View("SavePrueba", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SavePruebaDeLaboratorioViewModel vm)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            if (!ModelState.IsValid)
            {
                return View("SavePrueba", vm);
            }

            var adding = await _pruebaService.Add(vm);

            if (adding != null)
            {
                return RedirectToRoute(new { controller = "PruebaDeLaboratorio", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("Validacion", "El nombre de la prueba ya existe.");
                return View("SavePrueba", vm);
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            SavePruebaDeLaboratorioViewModel vm = await _pruebaService.GetByIdSaveViewModel(id);
            return View("SavePrueba", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePruebaDeLaboratorioViewModel vm)
        {
            if (!_validarsesion.HasUser()   )
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            if (!ModelState.IsValid)
            {
                return View("SavePrueba", vm);
            }
            await _pruebaService.Update(vm);
            return RedirectToRoute(new { controller = "PruebaDeLaboratorio", action = "Index" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }


            return View(await _pruebaService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            await _pruebaService.Delete(id);
            return RedirectToRoute(new { controller = "PruebaDeLaboratorio", action = "Index" });
        }

    }
}

using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.ViewModels.Medicos;
using MedicSystem.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace MedicSystem.Controllers
{
    public class MedicoController : Controller
    {
        public readonly IMedicoService _medicoService;
        public readonly ValidarSesion _validarSession;
        public MedicoController(IMedicoService medicoService, ValidarSesion validarSession)
        {
            _medicoService = medicoService;
            _validarSession = validarSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validarSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarSession.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            return View(await _medicoService.GetAllViewModel());
        }

        public IActionResult NoAccess()
        {
            if (!_validarSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            return View("~/Views/Shared/Error.cshtml");
        }

        public IActionResult Add()
        {
            if (!_validarSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarSession.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            SaveMedicoViewModel vm = new();
            return View("SaveMedico", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SaveMedicoViewModel vm)
        {
            if (!_validarSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarSession.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            if (!ModelState.IsValid)
            {
                return View("SaveMedico", vm);
            }

            SaveMedicoViewModel medicoVm = await _medicoService.Add(vm);

            if (medicoVm.Id != 0 && medicoVm != null)
            {
                medicoVm.Foto = UploadFile(vm.File, medicoVm.Id);

                await _medicoService.Update(medicoVm);
            }

            return RedirectToRoute(new { controller = "Medico", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validarSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarSession.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            SaveMedicoViewModel vm = await _medicoService.GetByIdSaveViewModel(id);
            return View("SaveMedico", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveMedicoViewModel vm)
        {
            if (!_validarSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarSession.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            if (!ModelState.IsValid)
            {
                return View("SaveMedico", vm);
            }

            SaveMedicoViewModel medicoVm = await _medicoService.GetByIdSaveViewModel(vm.Id);
            vm.Foto = UploadFile(vm.File, vm.Id, true, medicoVm.Foto);
            await _medicoService.Update(vm);

            return RedirectToRoute(new { controller = "Medico", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validarSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarSession.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            return View(await _medicoService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validarSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarSession.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            await _medicoService.Delete(id);

            string basePath = $"/Images/Medicos/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Medico", action = "Index" });
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Medicos/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}

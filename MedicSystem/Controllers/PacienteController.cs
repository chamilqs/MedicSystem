using MedicSystem.Core.Application.Helpers;
using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.ViewModels.Medicos;
using MedicSystem.Core.Application.ViewModels.Pacientes;
using MedicSystem.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace MedicSystem.Controllers
{
    public class PacienteController : Controller
    {
        public readonly IPacienteService _pacienteService;
        public readonly ValidarSesion _validarSession;
        public PacienteController(IPacienteService pacienteService, ValidarSesion validarSession)
        {
            _pacienteService = pacienteService;
            _validarSession = validarSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validarSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            return View(await _pacienteService.GetAllViewModel());
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

            SavePacienteViewModel vm = new();
            return View("SavePaciente", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SavePacienteViewModel vm)
        {
            if (!_validarSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            SavePacienteViewModel pacienteVm = await _pacienteService.Add(vm);

            if (pacienteVm.Id != 0 && pacienteVm != null)
            {
                pacienteVm.Foto = UploadFile(vm.File, pacienteVm.Id);

                await _pacienteService.Update(pacienteVm);
            }

            return RedirectToRoute(new { controller = "Paciente", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validarSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            SavePacienteViewModel vm = await _pacienteService.GetByIdSaveViewModel(id);
            return View("SavePaciente", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePacienteViewModel vm)
        {
            if (!_validarSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            SavePacienteViewModel medicoVm = await _pacienteService.GetByIdSaveViewModel(vm.Id);
            vm.Foto = UploadFile(vm.File, vm.Id, true, medicoVm.Foto);
            await _pacienteService.Update(vm);

            return RedirectToRoute(new { controller = "Paciente", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validarSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            return View(await _pacienteService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validarSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }

            await _pacienteService.Delete(id);

            string basePath = $"/Images/Pacientes/{id}";
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

            return RedirectToRoute(new { controller = "Paciente", action = "Index" });
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
            string basePath = $"/Images/Pacientes/{id}";
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

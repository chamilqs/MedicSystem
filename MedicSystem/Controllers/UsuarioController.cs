using MedicSystem.Core.Application.Helpers;
using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.ViewModels.Usuarios;
using MedicSystem.Core.Domain.Entities;
using MedicSystem.Core.Domain.Enum;
using MedicSystem.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using static MedicSystem.Helpers.ValidarSesion;


namespace MedicSystem.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ValidarSesion _validarsesion;

        public UsuarioController(IUsuarioService usuarioService, ValidarSesion validarSesion)
        {
            _usuarioService = usuarioService;
            _validarsesion = validarSesion;
        }
        public IActionResult Index()
        {
            if (_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View();
        }

        public IActionResult NoAccess()
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            return View("~/Views/Shared/Error.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginvm)
        {
            if (_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            } 

            if (!ModelState.IsValid)
            {
                return View(loginvm);
            }

            UsuarioViewModel usuario = await _usuarioService.Login(loginvm);

            if(usuario != null)
            {
                HttpContext.Session.Set("CurrentUser", usuario);                
                HttpContext.Session.Set("Username", usuario.NombreUsuario);
                HttpContext.Session.Set("AccessLevel", usuario.TipoUsuario);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("Validacion", "Usuario o contraseña incorrectos.");                
                return View(loginvm);
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("CurrentUser");
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("AccessLevel");
            return RedirectToRoute(new { controller = "Usuario", action = "Index" });
        }

        public async Task<IActionResult> MantenimientoUsuario(UsuarioViewModel vm)
        {
            if (!_validarsesion.HasUser())
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }


            return View(await _usuarioService.GetAllViewModel());
        }

        public IActionResult Add()
        {
            SaveUsuarioViewModel vm = new ();

            ViewBag.TiposUsuario = Get.TiposUsuario();
            return View("SaveUsuario", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SaveUsuarioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TiposUsuario = Get.TiposUsuario();
                return View("SaveUsuario", vm);
            }

            var adding = await _usuarioService.Add(vm);

            if (adding != null)
            {
                return RedirectToRoute(new { controller = "Usuario", action = "MantenimientoUsuario" });
            }
            else
            {
                ViewBag.TiposUsuario = Get.TiposUsuario();
                ModelState.AddModelError("Validacion", "El nombre de usuario ya existe.");
                return View("SaveUsuario",vm);
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            if (_validarsesion.HasUser() == false)
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            SaveUsuarioViewModel vm = await _usuarioService.GetByIdSaveViewModel(id);

            ViewBag.TiposUsuario = Get.TiposUsuario(); 
            return View("SaveUsuario", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUsuarioViewModel vm)
        {
            var usuario = await _usuarioService.GetByIdSaveViewModel(vm.Id);

            if (_validarsesion.HasUser() == false)
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            if (!ModelState.IsValid)
            {          
                ViewBag.TiposUsuario = Get.TiposUsuario();
                return View("SaveUsuario", vm);
            }

            if (!vm.PasswordActual.IsNullOrEmpty())
            {
                if(vm.Password.IsNullOrEmpty() || vm.ConfirmarPassword.IsNullOrEmpty())
                {
                    ViewBag.TiposUsuario = Get.TiposUsuario();
                    ModelState.AddModelError("Validacion", "Las contraseñas no coiciden.");
                    return View("SaveUsuario", vm);
                }

                // aqui vamos a comparar la contraseña hash con la contraseña actual
                var verify = PasswordEncryption.Encrypt256Hash(vm.PasswordActual);

                if (usuario.Password != verify)
                {
                    ModelState.AddModelError("Validacion", "La contraseña actual no coincide.");
                    ViewBag.TiposUsuario = Get.TiposUsuario();
                    return View("SaveUsuario", vm);
                }

            }

            if (vm.PasswordActual.IsNullOrEmpty())
            {
                vm.Password = usuario.Password;
            }

            await _usuarioService.Update(vm);
            return RedirectToRoute(new { controller = "Usuario", action = "MantenimientoUsuario" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (_validarsesion.HasUser() == false)
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            return View(await _usuarioService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (_validarsesion.HasUser() == false)
            {
                return RedirectToRoute(new { controller = "Usuario", action = "Index" });
            }
            else if (!_validarsesion.isAdmin())
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            await _usuarioService.Delete(id);
            return RedirectToRoute(new { controller = "Usuario", action = "MantenimientoUsuario" });
        }

        public static class Get
        {
            public static IEnumerable<SelectListItem> TiposUsuario()
            {
                var tiposUsuario = Enum.GetValues(typeof(TipoUsuario))
                    .Cast<TipoUsuario>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = Enum.GetName(typeof(TipoUsuario), e)
                    });

                return tiposUsuario;
            }

        }

    }
}

using System.Reflection.Metadata;
using Dermatologia.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Dermatologia.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        private readonly SignInManager<IdentityUser> signInManager;

        public UsuarioController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registro(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = new IdentityUser() { Email = model.Email, UserName = model.Email };

            var Resultado = await userManager.CreateAsync(usuario, password: model.Password);
            if (Resultado.Succeeded)
            {
                await signInManager.SignInAsync(usuario, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in Resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }

        [AllowAnonymous]
        public IActionResult Login(string mensaje = null)
        {
            if (mensaje is not null)
            {
                ViewData["mensaje"] = mensaje;
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var Resultado = await signInManager.PasswordSignInAsync(model.Email,
            model.Password, model.Recuerdame, lockoutOnFailure: false);

            if (Resultado.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o password incorrecto");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Listado()
        {
            return View();
        }

        // [HttpGet]
        // [Authorize(Roles = Constantes.RolAdmin)]
        // public async Task<IActionResult> Listado(string mensaje = null)
        // {
        //     var usuarios = await _context.Users.Select(u => new UsuarioViewModel
        //     {
        //         Email = u.Email
        //     }).ToListAsync();

        //     var model = new UsuariosListadoViewModel();
        //     model.Usuarios = usuarios;
        //     model.Mensaje = mensaje;
        //     return View(model);
        // }
    }
}
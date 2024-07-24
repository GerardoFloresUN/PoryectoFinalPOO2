using System.Reflection.Metadata;
using Dermatologia.Models;
using Dermatologia.Services;
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

        public UsuarioController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._context = context;
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
            var lista = this._context.Users.Select(x => new UsuarioViewModel
            {
                Email = x.Email
            }).ToList();
            
            var userListViewModel = new UsuariosListadoViewModel();
            userListViewModel.Usuarios = lista;
            return View(userListViewModel);
        }

        [HttpPost]
        // [Authorize(Roles = Constantes.RolAdmin)]
        public  async Task<IActionResult> HacerAdmin(string email)
        {
            var usuario = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario is null)
            {
                return NotFound();
            }

            await userManager.AddToRoleAsync(usuario, MyConstants.RolAdmin);

            return RedirectToAction("Listado",
                routeValues: new { confirmed = "Rol asignado correctamente a " + email, remove = ""  });
        }

        [HttpPost]
        // [Authorize(Roles = Constantes.RolAdmin)]
        public async Task<IActionResult> RemoverAdmin(string email)
        {
            var usuario = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario is null)
            {
                return NotFound();
            }

            await userManager.RemoveFromRoleAsync(usuario, MyConstants.RolAdmin);

            return RedirectToAction("Listado",
                routeValues: new { confirmed = "", remove = "Rol removido correctamente a " + email });
        }

        
    }
}
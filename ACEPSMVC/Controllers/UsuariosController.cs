using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ACEPSMVC.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ACEPSMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ContextoDBAplicacao _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Usuarios Usuarios { get; set; }

        public UsuariosController(ContextoDBAplicacao db, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            Usuarios login = new Usuarios();
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuarios login)
        {
            if (ModelState.IsValid)
            {
                //Usuarios appUser = await userManager.FindByEmailAsync(login.Email);
                //if (appUser != null)
                //{
                //    await signInManager.SignOutAsync();
                //    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Senha, false, false);
                //    if (result.Succeeded)
                //        return Redirect(login.ReturnUrl ?? "/");
                //}
                //ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
            }
            return View(login);
        }
    }
}

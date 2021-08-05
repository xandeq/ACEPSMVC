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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace ACEPSMVC.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly ContextoDBAplicacao _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Usuarios Usuario { get; set; }

        public LoginController(ContextoDBAplicacao db, IWebHostEnvironment webHostEnvironment)
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
        [AllowAnonymous]
        public async Task<IActionResult> Login(Usuarios login)
        {
            var user = _db.Usuarios.Where(c => c.NomeUsuario == login.NomeUsuario && c.Senha == login.Senha).FirstOrDefault();

            if (!(user is null))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.NomeUsuario),
                new Claim("FullName", user.Nome),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    RedirectUri = "/Admin/Index",
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }

            return Redirect("/Accout/Error");
        }

        [AllowAnonymous]
        public IActionResult Upsert(int? id)
        {
            Usuario = new Usuarios();
            if (id == null)
            {
                //create
                return View(Usuario);
            }
            //update
            Usuario = _db.Usuarios.FirstOrDefault(u => u.Id == id);
            if (Usuario == null)
            {
                return NotFound();
            }
            return View(Usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Upsert(Usuarios usuario)
        {
            //if (ModelState.IsValid)
            //{
            if (Usuario.Id == 0)
            {
                //create
                _db.Usuarios.Add(Usuario);
            }
            else
            {
                _db.Usuarios.Update(Usuario);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
            //}
            return View(Usuario);
        }
    }

}

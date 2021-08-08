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
using System.IO;
using Utilidades;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace ACEPSMVC.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly ContextoDBAplicacao _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Usuarios Usuario { get; set; }

        public UsuariosController(ContextoDBAplicacao db, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Erro()
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
        public async Task<IActionResult> Login(Usuarios usuarioLogin)
        {
            try
            {
                List<Usuarios> lista = _db.Usuarios.ToList();
                var user = _db.Usuarios.Where(c => c.NomeUsuario == usuarioLogin.NomeUsuario && c.Senha == usuarioLogin.Senha).FirstOrDefault();

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

                return Redirect("/Admin/Index");
            }
            catch (Exception)
            {
                return Redirect("/Login/Erro");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Noticias.OrderByDescending(o => o.DataCriacao).ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioDB = await _db.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuarioDB == null)
            {
                return Json(new { success = false, message = "Erro ao deletar notícia." });
            }

            _db.Usuarios.Remove(usuarioDB);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Notícia deletada com sucesso." });
        }

        public IActionResult Detalhes(int id)
        {
            Usuario = _db.Usuarios.FirstOrDefault(u => u.Id == id);
            return View(Usuario);
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
                Usuario.DataCriacao = DateTime.Now;
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

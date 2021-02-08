using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACEPSMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ACEPSMVC.Controllers
{
    public class NoticiasController : Controller
    {
        private readonly ContextoDBAplicacao _db;

        [BindProperty]
        public Noticias Noticia { get; set; }

        public NoticiasController(ContextoDBAplicacao db)
        {
            _db = db;
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Noticias.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var noticiaBanco = await _db.Noticias.FirstOrDefaultAsync(u => u.Id == id);
            if (noticiaBanco == null)
            {
                return Json(new { success = false, message = "Erro ao deletar notícia." });
            }

            _db.Noticias.Remove(noticiaBanco);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Notícia deletada com sucesso." });
        }

        public IActionResult Upsert(int? id)
        {
            Noticia = new Noticias();
            if (id == null)
            {
                //create
                return View(Noticia);
            }
            //update
            Noticia = _db.Noticias.FirstOrDefault(u => u.Id == id);
            if (Noticia == null)
            {
                return NotFound();
            }
            return View(Noticia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Noticia.Id == 0)
                {
                    //create
                    _db.Noticias.Add(Noticia);
                }
                else
                {
                    _db.Noticias.Update(Noticia);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Noticia);
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }
    }
}

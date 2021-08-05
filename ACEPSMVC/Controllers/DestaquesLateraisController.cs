using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ACEPSMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utilidades;

namespace ACEPSMVC.Controllers
{
    [Authorize]
    public class DestaquesLateraisController : Controller
    {
        private readonly ContextoDBAplicacao _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public DestaquesLaterais DestaqueLateral { get; set; }

        public DestaquesLateraisController(ContextoDBAplicacao db, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.DestaqueLateral.OrderByDescending(o => o.Id).ToListAsync() });
        }

        public IActionResult Upsert(int? id)
        {
            DestaqueLateral = new DestaquesLaterais();
            if (id == null)
            {
                //create
                return View(DestaqueLateral);
            }
            //update
            DestaqueLateral = _db.DestaqueLateral.FirstOrDefault(u => u.Id == id);
            if (DestaqueLateral == null)
            {
                return NotFound();
            }
            return View(DestaqueLateral);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            //if (ModelState.IsValid)
            //{
            string caminho = _webHostEnvironment.WebRootPath + "\\destaqueslaterais";

            var filePath = Path.GetTempFileName();
            foreach (var formFile in Request.Form.Files)
            {
                if (formFile.Length > 0)
                {
                    string caminhoArquivo = Path.Combine(caminho, formFile.FileName);

                    caminhoArquivo = Helpers.ObterCaminhoArquivo(caminhoArquivo);
                    using (var inputStream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        // read file to stream
                        formFile.CopyToAsync(inputStream);


                        // stream to byte array
                        byte[] array = new byte[inputStream.Length];
                        inputStream.Seek(0, SeekOrigin.Begin);
                        inputStream.Read(array, 0, array.Length);
                        // get file name
                        string fName = formFile.FileName;
                        DestaqueLateral.Imagem = formFile.FileName;
                    }
                }
            }

            if (DestaqueLateral.Id == 0)
            {
                //create
                _db.DestaqueLateral.Add(DestaqueLateral);
            }
            else
            {
                _db.DestaqueLateral.Update(DestaqueLateral);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
            //}
            return View(DestaqueLateral);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objBD = await _db.DestaqueLateral.FirstOrDefaultAsync(u => u.Id == id);
            if (objBD == null)
            {
                return Json(new { success = false, message = "Erro ao deletar DestaqueLateral." });
            }

            _db.DestaqueLateral.Remove(objBD);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "DestaqueLateral deletada com sucesso." });
        }

        public IActionResult Detalhes(int id)
        {
            DestaquesLaterais destaque = _db.DestaqueLateral.FirstOrDefault(u => u.Id == id);
            return View(destaque);
        }
    }
}

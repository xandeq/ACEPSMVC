using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ACEPSMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ACEPSMVC.Controllers
{
    public class NoticiasController : Controller
    {

        private readonly ContextoDBAplicacao _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Noticias Noticia { get; set; }

        public NoticiasController(ContextoDBAplicacao db, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Noticias.OrderByDescending(o => o.DataCriacao).ToListAsync() });
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

        public IActionResult Detalhes(int id)
        {
            Noticia = _db.Noticias.FirstOrDefault(u => u.Id == id);
            return View(Noticia);
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
                string caminho = _webHostEnvironment.WebRootPath + "\\noticias";

                var filePath = Path.GetTempFileName();
                foreach (var formFile in Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        string caminhoArquivo = Path.Combine(caminho, formFile.FileName);

                        caminhoArquivo = GetUniqueFilePath(caminhoArquivo);

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
                            if (formFile.Name == "ImagemDestaque")
                                Noticia.ImagemDestaque = formFile.FileName;
                            else if (formFile.Name == "ImagemInterna")
                                Noticia.ImagemInterna = formFile.FileName;
                        }
                    }
                }

                var data = Noticia.DataCriacao;

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

        public static string GetUniqueFilePath(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                string folderPath = Path.GetDirectoryName(filePath);
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string fileExtension = Path.GetExtension(filePath);
                int number = 1;

                Match regex = Regex.Match(fileName, @"^(.+) \((\d+)\)$");

                if (regex.Success)
                {
                    fileName = regex.Groups[1].Value;
                    number = int.Parse(regex.Groups[2].Value);
                }

                do
                {
                    number++;
                    string newFileName = $"{fileName} ({number}){fileExtension}";
                    filePath = Path.Combine(folderPath, newFileName);
                }
                while (System.IO.File.Exists(filePath));
            }

            return filePath;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

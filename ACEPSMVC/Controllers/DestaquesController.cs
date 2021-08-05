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
    public class DestaquesController : Controller
    {
        private readonly ContextoDBAplicacao _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Destaques Destaque { get; set; }

        public DestaquesController(ContextoDBAplicacao db, IWebHostEnvironment webHostEnvironment)
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
            return Json(new { data = await _db.Destaque.OrderByDescending(o => o.Id).ToListAsync() });
        }

        public IActionResult Upsert(int? id)
        {
            Destaque = new Destaques();
            if (id == null)
            {
                //create
                return View(Destaque);
            }
            //update
            Destaque = _db.Destaque.FirstOrDefault(u => u.Id == id);
            if (Destaque == null)
            {
                return NotFound();
            }
            return View(Destaque);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            //if (ModelState.IsValid)
            //{
            string caminho = _webHostEnvironment.WebRootPath + "\\destaques";

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
                        Destaque.Imagem = formFile.FileName;
                    }
                }
            }

            if (Destaque.Id == 0)
            {
                //create
                _db.Destaque.Add(Destaque);
            }
            else
            {
                _db.Destaque.Update(Destaque);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
            //}
            return View(Destaque);
        }

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

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objBD = await _db.Destaque.FirstOrDefaultAsync(u => u.Id == id);
            if (objBD == null)
            {
                return Json(new { success = false, message = "Erro ao deletar Destaque." });
            }

            _db.Destaque.Remove(objBD);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Destaque deletada com sucesso." });
        }

        public IActionResult Detalhes(int id)
        {
            Destaques destaque = _db.Destaque.FirstOrDefault(u => u.Id == id);
            return View(destaque);
        }
    }
}

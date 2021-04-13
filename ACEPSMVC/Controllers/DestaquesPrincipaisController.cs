using ACEPSMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ACEPSMVC.Controllers
{
    public class DestaquesPrincipaisController : Controller
    {

        private readonly ContextoDBAplicacao _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public DestaquePrincipal DestaquePrincipal { get; set; }

        public DestaquesPrincipaisController(ContextoDBAplicacao db, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }

        // GET: DestaquesPrincipaisController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DestaquesPrincipaisController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DestaquesPrincipaisController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.DestaquePrincipal.OrderByDescending(o => o.Id).ToListAsync() });
        }

        public IActionResult Upsert(int? id)
        {
            DestaquePrincipal = new DestaquePrincipal();
            if (id == null)
            {
                //create
                return View(DestaquePrincipal);
            }
            //update
            DestaquePrincipal = _db.DestaquePrincipal.FirstOrDefault(u => u.Id == id);
            if (DestaquePrincipal == null)
            {
                return NotFound();
            }
            return View(DestaquePrincipal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert()
        {
            if (ModelState.IsValid)
            {
                string caminho = _webHostEnvironment.WebRootPath + "\\destaqueprincipal";

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
                            await formFile.CopyToAsync(inputStream);
                            // stream to byte array
                            byte[] array = new byte[inputStream.Length];
                            inputStream.Seek(0, SeekOrigin.Begin);
                            inputStream.Read(array, 0, array.Length);
                            // get file name
                            string fName = formFile.FileName;
                            DestaquePrincipal.Imagem = formFile.FileName;
                        }

                        DestaquePrincipal.DataCriacao = DateTime.Now;

                        if (DestaquePrincipal.Id == 0)
                        {
                            //create
                            _db.DestaquePrincipal.Add(DestaquePrincipal);
                        }
                        else
                        {
                            _db.DestaquePrincipal.Update(DestaquePrincipal);
                        }
                        _db.SaveChanges();
                    }
                    else
                    {
                        ViewData["Erro"] = "Arquivo ja existe. Renomeie.";
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                    .Where(y => y.Count > 0)
                    .ToList();
            }
            return View(DestaquePrincipal);
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


        // GET: DestaquesPrincipaisController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DestaquesPrincipaisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var destaqueBanco = await _db.DestaquePrincipal.FirstOrDefaultAsync(u => u.Id == id);
            if (destaqueBanco == null)
            {
                return Json(new { success = true, message = "Destaque deletado com sucesso." });
            }

            _db.DestaquePrincipal.Remove(destaqueBanco);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Destaque deletado com sucesso." });
        }
    }
}

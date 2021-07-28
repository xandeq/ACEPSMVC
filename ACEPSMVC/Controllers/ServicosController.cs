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
using Utilidades;

namespace ACEPSMVC.Controllers
{
    public class ServicosController : Controller
    {
        private readonly ContextoDBAplicacao _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Servicos Servicos { get; set; }

        public ServicosController(ContextoDBAplicacao db, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ListaServicos()
        {
            List<Servicos> listaServicos = _db.Servicos.ToList();
            return View(listaServicos);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Servicos.OrderByDescending(o => o.Id).ToListAsync() });
        }

        public IActionResult Upsert(int? id)
        {
            Servicos = new Servicos();
            if (id == null)
            {
                //create
                return View(Servicos);
            }
            //update
            Servicos = _db.Servicos.FirstOrDefault(u => u.Id == id);
            if (Servicos == null)
            {
                return NotFound();
            }
            return View(Servicos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            //if (ModelState.IsValid)
            //{
            try
            {
                string servicoDB = string.Empty;
                if (Servicos.Id !=null && Servicos.Id > 0)
                {
                    servicoDB = _db.Servicos.Where(o => o.Id == Servicos.Id).Select(s => s.Logo).First();
                }

                string caminho = _webHostEnvironment.WebRootPath + "\\servicos";

                var filePath = Path.GetTempFileName();
                foreach (var formFile in Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        string caminhoArquivo = Path.Combine(caminho, formFile.FileName);
                        // DELETA O ARQUIVO ANTERIOR COM MESMO NOME
                        if (System.IO.File.Exists(caminhoArquivo))
                        {
                            System.IO.File.Delete(caminhoArquivo);
                        }

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
                            Servicos.Logo = formFile.FileName;
                        }
                    }
                }

                Servicos.DataCriacao = DateTime.Now.ToString();
                if (Servicos.Id == 0)
                {
                    _db.Servicos.Add(Servicos);
                }
                else
                {
                    if(string.IsNullOrWhiteSpace(Servicos.Logo) && !string.IsNullOrWhiteSpace(servicoDB))
                    {
                        Servicos.Logo = servicoDB;
                    }
                    _db.Servicos.Update(Servicos);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                message += string.Format("<b>StackTrace:</b> {0}<br /><br />", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                //message += string.Format("<b>InnerException:</b> {0}<br /><br />", ex.InnerException.ToString().Replace(Environment.NewLine, string.Empty));
                message += string.Format("<b>Source:</b> {0}<br /><br />", ex.Source.Replace(Environment.NewLine, string.Empty));
                message += string.Format("<b>TargetSite:</b> {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                ViewData["ErrorMessage"] = "Erro : " + message;
            }
            //}
            return View(Servicos);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objBD = await _db.Servicos.FirstOrDefaultAsync(u => u.Id == id);
            if (objBD == null)
            {
                return Json(new { success = false, message = "Erro ao deletar Servicos." });
            }

            _db.Servicos.Remove(objBD);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Servicos deletada com sucesso." });
        }

        public IActionResult Detalhes(int id)
        {
            Servicos destaque = _db.Servicos.FirstOrDefault(u => u.Id == id);
            return View(destaque);
        }
    }
}

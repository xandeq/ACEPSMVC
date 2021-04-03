using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ACEPSMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ACEPSMVC.DataAccess.Data;

namespace ACEPSMVC.Controllers
{
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
            if (ModelState.IsValid)
            {
                string caminho = _webHostEnvironment.WebRootPath + "\\destaques";

                var filePath = Path.GetTempFileName();
                foreach (var formFile in Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        using (var inputStream = new FileStream(Path.Combine(caminho, formFile.FileName), FileMode.Create))
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
            }
            return View(Destaque);
        }
    }
}

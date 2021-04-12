using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ACEPSMVC.Models;
using System.Text.RegularExpressions;

namespace ACEPSMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ContextoDBAplicacao _db;
        
        public HomeController(ILogger<HomeController> logger, ContextoDBAplicacao db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            ViewData["Destaques"] = _db.Destaque.ToList();
            ViewData["Noticias"] = _db.Noticias.Take(10).Select( s => new Noticias
            {
                DataCriacao = s.DataCriacao,
                Id = s.Id,
                ImagemDestaque = s.ImagemDestaque,
                ImagemDestaqueArquivo = s.ImagemDestaqueArquivo,
                ImagemInterna = s.ImagemInterna,
                LinhaFina = s.LinhaFina,
                Subtitulo = s.Subtitulo,
                Texto = Regex.Replace(s.Texto, "<.*?>", String.Empty),
                Titulo = s.Titulo
            }).ToList();
            ViewData["DestaquePrincipal"] = _db.DestaquePrincipal.OrderByDescending(o => o.Id).ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

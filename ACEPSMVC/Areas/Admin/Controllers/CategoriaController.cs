using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACEPSMVC.DataAccess.Data.Repository.IRepository;
using ACEPSMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ACEPSMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Categoria categoria = new Categoria();
            if(id == null)
            {
                return View(categoria);
            }

            categoria = _unitOfWork.Categoria.Get(id.GetValueOrDefault());
            if(categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if(categoria.Id == 0)
                {
                    _unitOfWork.Categoria.Add(categoria);
                }
                else
                {
                    _unitOfWork.Categoria.Update(categoria);
                }

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }


        #region  API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Categoria.GetAll() });
        }

        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            var objBD = _unitOfWork.Categoria.Get(id);

            if(objBD == null)
            {
                return Json(new { success = false, message = "Erro ao deletar." } );
            }

            _unitOfWork.Categoria.Remove(objBD);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deletado com sucesso." });
        }

        #endregion
    }
}

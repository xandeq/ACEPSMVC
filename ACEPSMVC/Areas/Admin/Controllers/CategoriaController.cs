using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACEPSMVC.DataAccess.Data.Repository.IRepository;
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
    }
}

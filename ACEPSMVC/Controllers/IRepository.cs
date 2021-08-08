using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ACEPSMVC.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Utilidades;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace ACEPSMVC.Controllers
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IActionResult> GetAll();

        Task<IActionResult> Delete(int id);

        IActionResult Detalhes(int id);

        IActionResult Upsert(int? id);

        IActionResult Upsert();

        IActionResult Index();
    }
}

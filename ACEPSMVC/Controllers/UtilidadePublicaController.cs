﻿using System;
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
    public class UtilidadePublicaController : Controller
    {
        public IActionResult Index()
        {
            //TO DO
            return View();
        }
        //DestaqueLateral.DataCriacao = DateTime.Now;
    }
}

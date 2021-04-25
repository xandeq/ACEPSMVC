using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ACEPSMVC.classes;

namespace ACEPSMVC.Models
{
    public class ConfigurationSMTP
    {
        //SMTP parameters
        public static string smtpAdress = "smtp.mail.yahoo.com";
        public static int portNumber = 587;
        public static bool enableSSL = true;
        //need it for the secured connection
        public static string from = "sender email";
        public static string password = "password of the above email";
    }

    public class ContatoController : Controller
    {
        public IActionResult Index(int tipo = 0)
        {
            Enumeradores.TiposContato tipoContato = (Enumeradores.TiposContato)tipo;
            ViewData["TipoContato"] = tipoContato.ToString();

            return View();
        }

        public ActionResult EnviarMensagem()
        {
            try
            {
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                //Error response
                Response.StatusCode = 400;
            }
            return null;
        }
    }
}

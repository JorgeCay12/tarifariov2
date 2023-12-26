using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home--
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Index()
        {            
            //string autentificado = Session["autentificado"] != null ? Session["autentificado"].ToString() : ""; // load it from request
            string autentificado = "si";
            Session["ccod_usuario"] = "40417486";
            if (autentificado == "si")
            {
                WebApplication1.Models.Home obj = new WebApplication1.Models.Home();                
                ViewBag.cadena = obj.MenuHTML(@Session["ccod_usuario"].ToString());
                ViewBag.ccolor_fondo = WebConfigurationManager.AppSettings["ccolor_fondo"];
                ViewBag.cruta_logo = Url.Content("~/img/" + WebConfigurationManager.AppSettings["cimg_logo"].ToString());
                ViewBag.cversion = WebConfigurationManager.AppSettings["cversion"];
                ViewBag.cnomapp = WebConfigurationManager.AppSettings["cnomapp"];

                return View();                
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Salir()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }


    }
}
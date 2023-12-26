using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Web.Configuration;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.cversion = WebConfigurationManager.AppSettings["cversion"];
            ViewBag.cversion = WebConfigurationManager.AppSettings["cversion"];
            ViewBag.cnomapp = WebConfigurationManager.AppSettings["cnomapp"];
            ViewBag.ccolor_fondo = WebConfigurationManager.AppSettings["ccolor_fondo"];
            ViewBag.cimg_bg_ruta = Url.Content("~/img/" + WebConfigurationManager.AppSettings["cimg_bg"].ToString());
            ViewBag.cruta_logo = Url.Content("~/img/" + WebConfigurationManager.AppSettings["cimg_logo"].ToString());
            return View();
        }

        [HttpPost]
        public ActionResult Proceso()
        {
            //return- View();
            var ccod_usuario = Request["ccod_usuario"];
            var cpas_usuario = Request["cpas_usuario"];
            //return RedirectToAction("Index", "Home");
            //if (ccod_usuario == "carlos" && cpas_usuario == "123")
            

            WebApplication1.Models.Login obj = new WebApplication1.Models.Login();

            string crespuesta = "0";
            
            //ccod_usuario = "40814628";
            WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
            object[,] arreglo = new object[3, 2] { { "@accion", "ing" }, { "@ccod_usuario", ccod_usuario }, { "@cpas_usuario", cpas_usuario } };
            DataTable dt = new DataTable();
            dt = obj2.GetDatatable("db", "ads_genericos", arreglo);

            if (dt.Rows[0]["cstatus"].ToString() == "ok")
            {

                if (dt.Rows[0]["ctipo_ing"].ToString() == "1")
                {
                    if (obj.AutentificacionUPCH(ccod_usuario, cpas_usuario) == "si")
                    {
                        crespuesta = "1";
                    } else
                    {
                        crespuesta = "0";
                    }

                } else
                {
                    crespuesta = "1";
                }                
            } else
            {
                crespuesta = "0";
            }


            if (crespuesta == "1")
            {
                Session["cdsc_usuario"] = dt.Rows[0]["cdsc_usuario"].ToString();
                Session["cdsc_grupo"] = dt.Rows[0]["cdsc_grupo"].ToString();
                Session["ccod_usuario"] = ccod_usuario;
                Session["autentificado"] = "si";
                return Content("True" + System.Web.HttpContext.Current.Session.SessionID);
            } else
            {
                return Content("False");
            }          

        }
    }
}

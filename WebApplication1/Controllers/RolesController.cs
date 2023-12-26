using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebApplication1.Controllers
{
    public class RolesController : Controller
    {
        public ActionResult Lista(string id)
        {
            ViewBag.contenedor = id;
            return View();
        }

        [HttpPost]
        public ActionResult Grabar()
        {

            string xml = HttpUtility.UrlDecode(Request.Form["xml"], System.Text.Encoding.Default);
            string accionsql = Request.QueryString["accionsql"];
            string id = Request.QueryString["id"];

            WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
            object[,] arreglo = new object[3, 2] { { "@accion", accionsql }, { "@xml", xml }, { "@id", id } };
            DataTable dt = new DataTable();
            dt = obj2.GetDatatable("db", "ads_roles", arreglo);
            return Content(dt.Rows[0]["resultado"].ToString());
        }

        [HttpGet]
        public ActionResult Formulario()
        {
            //ViewBag.contenedor = Request.QueryString["contenedor"];
            string accionsql = Request.QueryString["accionsql"];
            string id = Request.QueryString["id"];

            WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();

            /*Nuevo*/
            DataSet ds = new DataSet();
            ds = null;
            object[,] arreglo = null;

            if (accionsql == "G")
            {
                //select a.ccod_menu,a.cdsc_menu,a.ccod_modulo,b.cdsc_modulo,a.ctipo from menu a cross apply (select cdsc_modulo from modulos where ccod_modulo = a.ccod_modulo) b where a.cstatus = 'A' order by a.ccod_modulo,a.ccod_menu
                arreglo = new object[5, 2] { { "@accion", "con" }, { "@campos", "a.ccod_menu,a.cdsc_menu,a.ccod_modulo,b.cdsc_modulo,a.ctipo" }, { "@tabla", "adt_menu a cross apply (select cdsc_modulo from adt_modulos where ccod_modulo = a.ccod_modulo) b" }, { "@condicion", " a.cstatus = 'A' " }, { "@campo_order", "a.ccod_modulo asc,a.ccod_menu asc" } };
                ds = obj2.GetDataSet("db", "ads_genericos", arreglo);
            }
            else if (accionsql == "E")
            {
                arreglo = new object[2, 2] { { "@accion", "M" }, { "@id", id } };
                ds = obj2.GetDataSet("db", "ads_roles", arreglo);
            }

            return View(ds);

        }

        [HttpGet]
        public ActionResult Json()
        {

            int page = Convert.ToInt32(Request["page"].ToString());
            int rows = Convert.ToInt32(Request["rows"].ToString());
            string sort = Request["sort"];
            string order = Request["order"];
            string filtro = Request["filtro"];
            string campobuscar = Request["campobuscar"];
            string textobuscar = Request["textobuscar"];

            string result;
            WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
            object[,] arreglo = new object[8, 2] { { "@accion", "L" }, { "@filtro", filtro }, { "@page", page }, { "@rows", rows }, { "@sort", sort }, { "@order", order }, { "@campobuscar", campobuscar }, { "@textobuscar", textobuscar } };
            DataSet ds = new DataSet();
            ds = obj2.GetDataSet("db", "ads_roles", arreglo);
            //result = "{\"total\":" + dt.Rows[0]["ntotal"].ToString() + ",\"rows\":" + (dt.Rows.Count > 0 ? obj2.DataTableToJSONWithStringBuilder(dt) : "[]") + "}";
            result = "{\"total\":" + ds.Tables[0].Rows[0]["ntotal"].ToString() + ",\"rows\":" + (ds.Tables[1].Rows.Count > 0 ? obj2.DataTableToJSONWithStringBuilder(ds.Tables[1]) : "[]") + "}";
            return Content(result);
        }
    }
}
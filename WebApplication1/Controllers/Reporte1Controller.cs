using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class Reporte1Controller : Controller
    {
        // GET: Reporte1
        public ActionResult Index(string id)
        {
            ViewBag.contenedor = id;
            return View();
        }

        [HttpGet]
        public ActionResult Excel(string id)
        {

            string cdsc_pago = Request.QueryString["cdsc_pago"];
            string cvalor = Request.QueryString["cvalor"];
            string ctipo = Request.QueryString["ctipo"];
            int page = 0; //Convert.ToInt32(Request.Form["page"].ToString());
            int rows = 0; // Convert.ToInt32(Request.Form["rows"].ToString());
            string sort = "nid"; //Request.Form["sort"];
            string order = "desc"; //Request.Form["order"];

            WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
            object[,] arreglo = new object[4, 2] { { "@accion", "RE1" }, { "@cdsc_pago", cdsc_pago }, { "@cvalor", cvalor }, { "@ctipo", ctipo } };
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = obj2.GetDatatable("db", "ads_genericos", arreglo);

            Response.AddHeader("content-disposition", "attachment; filename=Reporte.xls");
            Response.ContentType = "application/ms-excel";
            return View(dt);
        }

        [HttpPost]
        public ActionResult Result()
        {

            string cdsc_pago = Request.Form["cdsc_pago"];
            string cvalor = Request.Form["cvalor"];
            string ctipo = Request.Form["ctipo"];

            string result;
            WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
            object[,] arreglo = new object[4, 2] { { "@accion", "RE1" }, { "@cdsc_pago", cdsc_pago }, { "@cvalor", cvalor }, { "@ctipo", ctipo } };
            DataSet ds = new DataSet();
            ds = obj2.GetDataSet("db", "ads_genericos", arreglo);
            //result = "{\"total\":" + dt.Rows[0]["ntotal"].ToString() + ",\"rows\":" + (dt.Rows.Count > 0 ? obj2.DataTableToJSONWithStringBuilder(dt) : "[]") + "}";
            //result = "{\"total\":" + ds.Tables[0].Rows[0]["ntotal"].ToString() + ",\"rows\":" + (ds.Tables[1].Rows.Count > 0 ? obj2.DataTableToJSONWithStringBuilder(ds.Tables[1]) : "[]") + "}";
            result = "{\"total\":500,\"rows\":" + (ds.Tables[0].Rows.Count > 0 ? obj2.DataTableToJSONWithStringBuilder(ds.Tables[0]) : "[]") + "}";
            return Content(result);
        }

    }


}
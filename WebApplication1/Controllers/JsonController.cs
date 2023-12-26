using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Web.Configuration;

namespace WebApplication1.Controllers
{
    public class JsonController : Controller
    {
        // GET: Json
        [HttpPost]
        public ActionResult Index()
        {
            string campos = Request.Form["buscampos"];
            string tabla = Request.Form["bustabla"];
            string condicion = Request.Form["buscondicion"];
            int page = Convert.ToInt32(Request.Form["page"].ToString());
            int rows = Convert.ToInt32(Request.Form["rows"].ToString());
            string sort = Request.Form["sort"];
            string order = Request.Form["order"];
            string filtro = Request.Form["filtro"];
            string campobuscar = Request.Form["buscador_campo"];
            string textobuscar = Request.Form["buscador_valor"];
            string campos_mostrar = Request.Form["buscampos_mostrar"];
            string result;

            try
            {

                WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
                object[,] arreglo = new object[12, 2] { { "@accion", "jso" }, { "@campos_mostrar", campos_mostrar }, { "@campos", campos }, { "@tabla", tabla }, { "@condicion", condicion }, { "@page", page }, { "@rows", rows }, { "@sort", sort }, { "@order", order }, { "@filtro", filtro }, { "@campobuscar", campobuscar }, { "@textobuscar", textobuscar } };
                DataSet ds = new DataSet();
                ds = obj2.GetDataSet("db", "ads_genericos", arreglo);
                //result = "{\"total\":" + dt.Rows[0]["ntotal"].ToString() + ",\"rows\":" + (dt.Rows.Count > 0 ? obj2.DataTableToJSONWithStringBuilder(dt) : "[]") + "}";
                result = "{\"total\":" + ds.Tables[0].Rows[0]["ntotal"].ToString() + ",\"rows\":" + (ds.Tables[1].Rows.Count > 0 ? obj2.DataTableToJSONWithStringBuilder(ds.Tables[1]) : "[]") + "}";
            }
            catch(Exception e)
            {
                result = e.ToString();
            }

            return Content(result);


        }
    }
}
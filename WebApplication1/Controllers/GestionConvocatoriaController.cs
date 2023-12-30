using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebApplication1.Controllers
{
    public class GestionConvocatoriaController : Controller
    {
        // GET: Gestion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lista()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Formulario()
        {
            //ViewBag.contenedor = id;
            string accionsql = Request.QueryString["accionsql"];
            string id = Request.QueryString["id"];

            WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();

            /*Nuevo*/
            DataSet ds = new DataSet();
            ds = null;
            object[,] arreglo = null;

            if (accionsql == "EC")
            {
                arreglo = new object[2, 2] { { "@accion", "MC" }, { "@id", id } };
                ds = obj2.GetDataSet("db", "ads_genericos", arreglo);

                return View(ds);
            }

            return View();
        }

        [HttpGet]
        public ActionResult BuscarCentroCosto()
        {
            string bus = Request["q"];

            WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
            object[,] arreglo = new object[2, 2] { { "@accion", "LGC" }, { "@textobuscar", bus } };
            DataTable dt = new DataTable();
            dt = obj2.GetDatatable("db", "ads_genericos", arreglo);

            // Convertir DataTable a JSON manualmente
            var jsonData = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                var serializer = new JavaScriptSerializer();
                jsonData.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    jsonData.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        jsonData.AppendFormat("\"{0}\":\"{1}\"", dt.Columns[j].ColumnName, dt.Rows[i][j]);
                        if (j < dt.Columns.Count - 1)
                        {
                            jsonData.Append(",");
                        }
                    }
                    jsonData.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        jsonData.Append(",");
                    }
                }
                jsonData.Append("]");
            }

            // Devolver el resultado como un ContentResult con formato JSON
            return Content(jsonData.ToString(), "application/json");

        }


        [HttpPost]
        public ActionResult Grabar()
        {

            string xml = HttpUtility.UrlDecode(Request.Form["xml"], System.Text.Encoding.Default);
            string accionsql = Request.QueryString["accionsql"];
            string id = Request.QueryString["id"];

            WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
            object[,] arreglo = new object[4, 2] { { "@accion", accionsql }, { "@xml", xml }, { "@id", id }, { "@ccod_usuario", Session["ccod_usuario"] } };
            DataTable dt = new DataTable();
            dt = obj2.GetDatatable("db", "ads_genericos", arreglo);
            return Content(dt.Rows[0]["resultado"].ToString());
        }

        //[HttpGet]
        //public ActionResult Formulario()
        //{
        //    //ViewBag.contenedor = id;
        //    string accionsql = Request.QueryString["accionsql"];
        //    string id = Request.QueryString["id"];

        //    WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();

        //    /*Nuevo*/
        //    DataSet ds = new DataSet();
        //    ds = null;
        //    object[,] arreglo = null;

        //    if (accionsql == "E")
        //    {
        //        arreglo = new object[2, 2] { { "@accion", "M" }, { "@id", id }  };
        //        ds = obj2.GetDataSet("db", "ads_genericos", arreglo);
        //    }           

        //    return View(ds);

        //}

        //[HttpGet]
        //public ActionResult Json()
        //{

        //    int page = Convert.ToInt32(Request["page"].ToString());
        //    int rows = Convert.ToInt32(Request["rows"].ToString());
        //    string sort = Request["sort"];
        //    string order = Request["order"];
        //    string filtro = Request["filtro"];
        //    string campobuscar = Request["campobuscar"];
        //    string textobuscar = Request["textobuscar"];

        //    string result;
        //    WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
        //    object[,] arreglo = new object[8, 2] { { "@accion", "L" }, { "@filtro", filtro }, { "@page", page }, { "@rows", rows }, { "@sort", sort }, { "@order", order }, { "@campobuscar", campobuscar }, { "@textobuscar", textobuscar } };
        //    DataSet ds = new DataSet();
        //    ds = obj2.GetDataSet("db", "ads_genericos", arreglo);
        //    //result = "{\"total\":" + dt.Rows[0]["ntotal"].ToString() + ",\"rows\":" + (dt.Rows.Count > 0 ? obj2.DataTableToJSONWithStringBuilder(dt) : "[]") + "}";
        //    result = "{\"total\":" + ds.Tables[0].Rows[0]["ntotal"].ToString() + ",\"rows\":" + (ds.Tables[1].Rows.Count > 0 ? obj2.DataTableToJSONWithStringBuilder(ds.Tables[1]) : "[]") + "}";
        //    return Content(result);
        //}

        [HttpGet]
        public ActionResult JsonConvocatoria()
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
            object[,] arreglo = new object[8, 2] { { "@accion", "LC" }, { "@filtro", filtro }, { "@page", page }, { "@rows", rows }, { "@sort", sort }, { "@order", order }, { "@campobuscar", campobuscar }, { "@textobuscar", textobuscar } };
            DataSet ds = new DataSet();
            ds = obj2.GetDataSet("db", "ads_genericos", arreglo);
            //result = "{\"total\":" + dt.Rows[0]["ntotal"].ToString() + ",\"rows\":" + (dt.Rows.Count > 0 ? obj2.DataTableToJSONWithStringBuilder(dt) : "[]") + "}";
            result = "{\"total\":" + ds.Tables[0].Rows[0]["ntotal"].ToString() + ",\"rows\":" + (ds.Tables[1].Rows.Count > 0 ? obj2.DataTableToJSONWithStringBuilder(ds.Tables[1]) : "[]") + "}";
            return Content(result);
        }

    }
}
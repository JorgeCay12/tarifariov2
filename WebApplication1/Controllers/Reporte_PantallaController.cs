using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using WebApplication1.Entidades;
using System.Linq.Dynamic;
using Newtonsoft.Json;


namespace WebApplication1.Controllers
{
    public class Reporte_PantallaController : Controller
    {
        // GET: Reporte_Pantalla
        public ActionResult Index()
        {
            return View();
        }

        public class Filtros
        {
            public string field { get; set; }
            public string value { get; set; }
        }

        public class Data
        {
            public int total { get; set; }
            //public decimal test { get; set; }
            public List<vis_data> rows { get; set; }
        }


        [HttpGet]
        public JsonResult Json()
        {

            int page = Convert.ToInt32(Request["page"].ToString());
            int rows = Convert.ToInt32(Request["rows"].ToString());
            string sort = Request["sort"];
            string order = Request["order"];
            string filterRules = Request["filterRules"];

            //JavaScriptSerializer j = new JavaScriptSerializer();
            //object a = j.Deserialize(filterRules, typeof(object));
            //filtros filtros = new filtros();
            if (filterRules != null) {
                
                //var filtros = JsonConvert.DeserializeObject<Filtros>(filterRules);
                List<Filtros> filtros = JsonConvert.DeserializeObject<List<Filtros>>(filterRules);
                foreach (var item in filtros)
                {
                    Console.WriteLine(item.field);
                }
            }

            Data data = new Data();
            /*
            using (dbtarifarioEntities db = new dbtarifarioEntities())
            {
                IQueryable<vis_data> query;
                query = db.vis_data;
                    //.Where(c => c.ccod_ficha.Contains("5"))
                    //.Where("ccod_ficha.Contains(@0)", "5")
                    //.Where("cnro_doc.Contains(@0)", "40")
                    //.Where(c => c.ccod_ficha.Contains("5"))
                    //.Where(c => c.cnro_doc.Contains("40"))
                if (filterRules != null)
                {
                    List<Filtros> filtros = JsonConvert.DeserializeObject<List<Filtros>>(filterRules);
                    foreach (var item in filtros)
                    {
                        //query = query.Where(p => p.ccod_ficha.Contains(item.value));
                        query = query.Where(item.field + ".Contains(@0)", item.value);
                    }
                }                
                data.total = query.Count();
            }
            */
            /*
            var query = "";
            if (1 == 1)
            {
              query = query.Where("ccod_ficha.Contains(@0)", "5");
            }
            //var result = query.ToList();
            */

            //List<vis_data> lst;
            //string ccod_ficha = "0000006591";


            /*
             IQueryable<Person> query = context.People;

                if (Country != "All")
                {
                    query = query.Where(p => p.Country == Country);
                }

                if (Income != "All")
                {
                    query = query.Where(p => p.Income == Income);
                }

                if (Age != "All")
                {
                    query = query.Where(p => p.Age == Age);
                }

                List<Person> fetchedPeople = query.ToList();
            */


            int skipRows = (page - 1) * rows;
            using (dbtarifarioEntities1 db = new dbtarifarioEntities1())
            {

                //var query = db.vis_data;
                IQueryable<vis_data> query;
                query = db.vis_data;
                //query = query.Where(p => p.ccod_ficha == "0000002925");
                query = query.Where(p => p.ctipo == "1");
                if (filterRules != null)
                {
                    List<Filtros> filtros = JsonConvert.DeserializeObject<List<Filtros>>(filterRules);
                    foreach (var item in filtros)
                    {
                        //query = query.Where(p => p.ccod_ficha.Contains(item.value));
                        query = query.Where( item.field +".Contains(@0)", item.value);
                    }
                }
                data.total = query.Count();
                query = query.OrderBy(sort + " " + order);
                query = query.Skip(skipRows);
                query = query.Take(rows);
                //query = query.ToList();

                /*
                int skipRows = (page - 1) * rows;
                var query = db.vis_data
                .Where(x => x.ccod_ficha == "0000006591")
                .ToList();    
                */

                //query = query.OrderBy(sort + " " + order);


                /*
                int skipRows = (page - 1) * rows;
                lst = db.vis_data
                //.Where("ccod_ficha.Contains(@0)", "5")                
                .OrderBy(sort + " " + order)
                //.Where(c => c.ccod_ficha.Contains("5"))
                //.Where(c => c.cnro_doc.Contains("40"))
                //.Where("ccod_ficha.Contains(@0)", "5")
                //.Where("cnro_doc.Contains(@0)", "40")
                //.Where("")
                //.OrderByDescending(p => p.ccod_ficha) //.OrderBy(p => p.ccod_ficha)
                .OrderBy(sort + " " + order)
                .Skip(skipRows)
                .Take(rows)
                //.Select(x => new { ccod_ficha = x.ccod_ficha, cnro_doc = x.cnro_doc })                   
                //.Where("ccod_ficha = \"0000002925\"")
                .ToList();
                */                
                data.rows = query.ToList();
            }
            //return lst;
            //return Json(lst);
            
            return new JsonResult() { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            /*
            string result;
            WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
            object[,] arreglo = new object[8, 2] { { "@accion", "R1" }, { "@filtro", filtro }, { "@page", page }, { "@rows", rows }, { "@sort", sort }, { "@order", order }, { "@campobuscar", campobuscar }, { "@textobuscar", textobuscar } };
            DataSet ds = new DataSet();
            ds = obj2.GetDataSet("db", "ads_genericos", arreglo);
            //result = "{\"total\":" + dt.Rows[0]["ntotal"].ToString() + ",\"rows\":" + (dt.Rows.Count > 0 ? obj2.DataTableToJSONWithStringBuilder(dt) : "[]") + "}";
            result = "{\"total\":" + ds.Tables[0].Rows[0]["ntotal"].ToString() + ",\"rows\":" + (ds.Tables[1].Rows.Count > 0 ? obj2.DataTableToJSONWithStringBuilder(ds.Tables[1]) : "[]") + "}";
            return Content(result);
            */

        }
    }
}
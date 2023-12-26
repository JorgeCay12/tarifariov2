using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entidades;

namespace WebApplication1.Controllers
{
    public class Reporte_derecho_de_admision_pregradoController : Controller
    {
        // GET: Reporte_derecho_de_admision_pregrado
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
            string ctipo = Request["ctipo"]; //0 es derecho de admision pregrado // 2 es derecho de admision posgrado

            if (filterRules != null)
            {
                List<Filtros> filtros = JsonConvert.DeserializeObject<List<Filtros>>(filterRules);
                foreach (var item in filtros)
                {
                    Console.WriteLine(item.field);
                }
            }

            Data data = new Data();

            int skipRows = (page - 1) * rows;
            using (dbtarifarioEntities1 db = new dbtarifarioEntities1())
            {

                //var query = db.vis_data;
                IQueryable<vis_data> query;
                query = db.vis_data;
                query = query.Where(p => p.ctipo == ctipo);
                if (filterRules != null)
                {
                    List<Filtros> filtros = JsonConvert.DeserializeObject<List<Filtros>>(filterRules);
                    foreach (var item in filtros)
                    {
                        query = query.Where(item.field + ".Contains(@0)", item.value);
                    }
                }
                data.total = query.Count();
                query = query.OrderBy(sort + " " + order);
                query = query.Skip(skipRows);
                query = query.Take(rows);
                data.rows = query.ToList();
            }

            return new JsonResult() { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }
    }
}
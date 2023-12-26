using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Home
    {
        public string MenuHTML(string ccod_usuario)
        {
            WebApplication1.Models.Generico obj = new WebApplication1.Models.Generico();
            object[,] arreglo = new object[2, 2] { { "@accion", "MEN" }, { "@ccod_usuario", ccod_usuario } };
            DataTable dt = new DataTable();
            dt = obj.GetDatatable("db", "ads_genericos", arreglo);

            string basura2 = "00";
            string basura = "";
            string siguiente = "";
            string actual = "";
            string nomactual = "";
            string actualmodulo = "";
            string siguientemodulo = "";
            string cadena = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (basura2 != dt.Rows[i]["ccod_modulo"].ToString())
                {
                    cadena += "<div title=" + dt.Rows[i]["cdsc_modulo"].ToString() + " data-options='' style='padding: 1px; background-color:#E9EAE5'>";
                    cadena += " <ul class='easyui-tree' data-options='animate: true,lines: true' >";
                    basura = "00";
                    siguiente = "";
                    basura2 = dt.Rows[i]["ccod_modulo"].ToString();
                }

                actual = dt.Rows[i]["ccod_menu"].ToString();
                nomactual = dt.Rows[i]["cdsc_menu"].ToString();
                actualmodulo = dt.Rows[i]["ccod_modulo"].ToString();


                siguiente = i + 1 < dt.Rows.Count ? dt.Rows[i + 1]["ccod_menu"].ToString() : "00";  //IIf(not fila.eof,fila("ccod_menu"),"00") 
                siguientemodulo = i + 1 < dt.Rows.Count ? dt.Rows[i + 1]["ccod_modulo"].ToString() : "00";//IIf(not fila.eof,fila("ccod_modulo"),"00") 

                if (actual.Length == siguiente.Length)
                {
                    cadena += "<li><div ondblclick = addPanel('" + nomactual + "','" + actualmodulo + actual + "')>" + nomactual + "</div></li><br>";
                }
                else if (siguiente.Length > actual.Length)
                {
                    cadena += "<li><br>";
                    cadena += "<span> " + nomactual + "</span><br>";
                    cadena += "<ul>";
                }
                else if (siguiente.Length < actual.Length)
                {
                    cadena += "<li><div ondblclick = addPanel('" + nomactual + "','" + actualmodulo + actual + "')>" + nomactual + "</div></li><br>";
                    for (int z = 0; z < (actual.Length / 2) - (siguiente.Length / 2); z++)
                    { //for i = 1 to (len(actual) / 2) - (len(siguiente) / 2)
                        cadena += "</ul><br>";
                        cadena += "</li><br>";
                    }

                }

                if (actualmodulo != siguientemodulo)
                {
                    cadena += " </ul>";
                    cadena += "</div>";
                }
            }

            return cadena;

        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Log
    {
        private string Path = "";

        public Log()
        {
            this.Path = System.Web.Hosting.HostingEnvironment.MapPath("~/log/");//Path;
        }

        public void Add(string sLog)
        {
            string nombre = GetNameFile();
            string cadena = "";

            cadena += sLog + Environment.NewLine;

            StreamWriter sw = new StreamWriter(Path + "/" + nombre, true);
            sw.Write(cadena);
            sw.Close();

        }

        #region HELPER
        private string GetNameFile()
        {
            string nombre = "";

            nombre = "log_" + DateTime.Now.Year + "_" + DateTime.Now.Month + ".log";

            return nombre;
        }

        #endregion
    }
}
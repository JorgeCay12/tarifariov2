using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Xml;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RegisterController : Controller
    {

        Log oLog = new Log();
        // GET: Register
        public ActionResult Index(string id,string id2) //id = tipo 0 = por ccod_art / tipo =  1 por centro de costo
        {

            //Poner en mantenimiento///////////////////////////
            /*
            string mensaje_mantenimiento = @"<!doctype html>
                <title>Ficha de pago</title>
                <style>
                  body { text-align: center; padding: 150px; }
                  h1 { font-size: 50px; }
                  body { font: 20px Helvetica, sans-serif; color: #333; }
                  article { display: block; text-align: left; width: 650px; margin: 0 auto; }
                  a { color: #dc8100; text-decoration: none; }
                  a:hover { color: #333; text-decoration: none; }
                </style>

                <article>
                    <h1>Regresamos en breve.</h1>
                    <div>
                        <p>
                                Estimado usuario, para mejorar los servicios, estamos realizando un mantenimiento programado. 
                                En breves momentos volveremos a activar la tienda, disculpen los inconvenientes.
                        </p>
                    </div>
                </article>
                ";
            return Content(mensaje_mantenimiento);
            */
            /////////////////////////////////////////////////////


            WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
            DataSet ds = new DataSet();
            ds = null;
            object[,] arreglo = null;
            //string mostrar = ""; //
            //string mostrar2 = "";
            string mensaje_error = "<!DOCTYPE html><meta name='viewport' content='width = device-width, initial-scale = 1, maximum-scale = 1'><script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js'></script><link href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css' rel='stylesheet'/><body><div id='content'><div class='alert alert-danger text-center' role='alert'>El enlace no existe</div></div></body>";
            string mensaje_error_creo = "<!DOCTYPE html><meta name='viewport' content='width = device-width, initial-scale = 1, maximum-scale = 1'><script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js'></script><link href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css' rel='stylesheet'/><body><div id='content'><div class='alert alert-danger text-center' role='alert'>Esta pagina se habilitará próximamente.</div></div></body>";
            string mensaje_cerrar = "<!DOCTYPE html><meta name='viewport' content='width = device-width, initial-scale = 1, maximum-scale = 1'><script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js'></script><link href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css' rel='stylesheet'/><body><div id='content'><div class='alert alert-danger text-center' role='alert'>Gracias por su preferencia, el curso ya no se encuentra en convocatoria</div></div></body>";
            string mensaje_limite_vacante = "<!DOCTYPE html><meta name='viewport' content='width = device-width, initial-scale = 1, maximum-scale = 1'><script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js'></script><link href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css' rel='stylesheet'/><body><div id='content'><div class='alert alert-danger text-center' role='alert'>Gracias por su preferencia, el curso ya alcanzo a su limite de vacantes</div></div></body>";
            //string mensaje_mantenimiento = "<!DOCTYPE html><meta name='viewport' content='width = device-width, initial-scale = 1, maximum-scale = 1'><script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js'></script><link href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css' rel='stylesheet'/><body><div id='content'><div class='alert alert-primary text-center' role='alert'>La pagina se encuentra en mantenimiento, regresamos a las 14:00 horas, disculpen los inconvenientes</div></div></body>";

            ViewBag.cfactura = "N";

            if (id == null)
            {
                return Content(mensaje_error);
            }
            else
            {
                if (id2 == null)
                {
                    return Content(mensaje_error);
                }
                else
                {                  

                    string cstatus_ficha = "";
                    string ccod_grupo = "";
                    arreglo = new object[4, 2] { { "@accion", "LTC" }, { "@ctipo", id }, { "@cvalor", id2 }, { "@ccod_cia", "U16" } };
                    ds = obj2.GetDataSet("upch299", "usp_dgagenericos", arreglo);                                                
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewBag.ctipo = id;
                        ViewBag.cvalor = id2;

                        if (id == "1") // solo curso, se busca por centro de costo
                        {
                            cstatus_ficha = ds.Tables[0].Rows[0]["cstatus_ficha"].ToString();
                            
                            if (cstatus_ficha == "X")
                            {
                                return Content(mensaje_error);
                            }
                            else if (cstatus_ficha == "I")
                            {
                                return Content(mensaje_cerrar);
                            }
                            else if (cstatus_ficha == "C")
                            {
                                return Content(mensaje_limite_vacante);
                            }
                            else
                            {
                                return View(ds);
                                //return Content(mensaje_mantenimiento);
                            }
                        }
                        else if (id == "3") //cualquier tarifario que no sea curso, derecho de admision pregrado o posgrado, debe tener el codigo de grupo en tarifarios 10 solo boleta 20 boleta y factura
                        {
                            ccod_grupo = ds.Tables[0].Rows[0]["ccod_grupo"].ToString();
                            if (ccod_grupo == "20")
                            {
                                ViewBag.cfactura = "S";
                            }
                            return View(ds);
                            //return Content(mensaje_error_creo);
                        } else
                        {
                            
                           return Content(mensaje_error);                            
                            
                        }

                    } else
                    {
                        //if (id2.Trim() == "PAQ202234" || id2.Trim() == "PAQ202334" || id2.Trim() == "PAQ202311")
                        if (id2.Trim() == "PAQ202234" || id2.Trim() == "PAQ202334" || id2.Trim() == "PAQ202311" || id2.Trim() == "PAQ112023" || id2.Trim() == "PAQ202401")  //Se añadio Tarifario 
                        {
                            return Content(mensaje_error_creo);
                        }
                        else
                        {
                            return Content(mensaje_error);
                        }
                    }                                                
                    //}
                }               
            }                     
        }

        public class DataAPIResponseDTO
        {
            public bool success { get; set; }
        }

        [HttpPost]
        public ActionResult Process()
        {
            string validadni = WebConfigurationManager.AppSettings["cconsume_api_dni"];

            oLog.Add("-----------------------------------------------------------------------------------------------------------------------");
            oLog.Add("Inicio de proceso para grabar registro - Valida dni: "+ validadni +" - " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));            

            string crespuesta = "0";
            string ccod_ficha = "";
            string cemail = "";
            string cdsc_coa = "";
            string chash = "";
            string cdsc_art = "";
            string ctipo = "";
            string cvalor = "";
            try
            {

                string xml = HttpUtility.UrlDecode(Request["xml"], System.Text.Encoding.Default);

                /*Obtener del xml el numero de documento*/
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                XmlNode node = xmlDoc.SelectSingleNode("/root/cabecera");
                string cnro_doc = node["cnro_doc"].InnerText;
                string cdoc = node["cdoc"].InnerText;
                /**************************************/
                
                /*
                oLog.Add("Tipo de documento: " + cdoc + " - Numero de documento: " + cnro_doc);
                //Validar DNI con el servicio de api-peru/////////////////
                if (cdoc == "01" && validadni == "1")
                {                    
                    string urlWebService = "https://consulta.api-peru.com/api/dni/" + cnro_doc.Trim();
                    string urlWebServiceResponse = "";
                    WebClient weatherClient = new WebClient();
                    weatherClient.Headers.Add("Authorization", "Bearer edc1e8105963af8a3add2065816955651cfbf7de0faeef94f36b17c49822bdc1");
                    weatherClient.Headers.Add("Content-Type", "application/json");
                    urlWebServiceResponse = weatherClient.DownloadString(urlWebService);                                        
                    DataAPIResponseDTO dataapiresponsedto = JsonConvert.DeserializeObject<DataAPIResponseDTO>(urlWebServiceResponse);
                    if (dataapiresponsedto.success ==  false)
                    {
                        oLog.Add("El api entrego la respuesta: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - NO existe el DNI " + cnro_doc.Trim());
                        oLog.Add("Respuesta api: ");
                        oLog.Add(urlWebServiceResponse);
                        oLog.Add("-----------------------------------------------------------------------------------------------------------------------");
                        crespuesta = "0" + "<b>El DNI que registro NO existe, recordar que el DNI es un documento solo para Peruanos</b><br><br>Si Ud es extranjero seleccione PASAPORTE o CARNET DE EXTRANJERIA<br><br>Si Ud es extranjero y NO tiene PASAPORTE o CARNET DE EXTRANJERIA seleccione OTROS DOCUMENTOS y coloque el numero de documento de su pais<br><br>Cualquier consulta o duda con este mensaje escriba a daef.pensiones@oficinas-upch.pe";
                        return Content(crespuesta);
                    }
                    oLog.Add("El api entrego la respuesta: "+ DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - SI existe el DNI " + cnro_doc.Trim());
                }
                //////////////////////////////////////////////////////////////////


                //Obtener del xml el numero de ruc
                string ctipo_doc = node["ctipo_doc"].InnerText;
                string cruc = node["cruc"].InnerText;
                //////////////////////////////////////////////////

                oLog.Add("Inicio de validacion de Numero de ruc: " + cruc );
                //Validar RUC con el servicio de api-peru/////////////////
                if (ctipo_doc == "F" && validadni == "1")
                {
                    string urlWebService = "https://consulta.api-peru.com/api/ruc/" + cruc.Trim();
                    string urlWebServiceResponse = "";
                    WebClient weatherClient = new WebClient();
                    weatherClient.Headers.Add("Authorization", "Bearer edc1e8105963af8a3add2065816955651cfbf7de0faeef94f36b17c49822bdc1");
                    weatherClient.Headers.Add("Content-Type", "application/json");
                    urlWebServiceResponse = weatherClient.DownloadString(urlWebService);
                    DataAPIResponseDTO dataapiresponsedto = JsonConvert.DeserializeObject<DataAPIResponseDTO>(urlWebServiceResponse);
                    if (dataapiresponsedto.success == false)
                    {
                        oLog.Add("El api entrego la respuesta: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - NO existe el RUC " + cruc.Trim());
                        oLog.Add("Respuesta api: ");
                        oLog.Add(urlWebServiceResponse);
                        oLog.Add("-----------------------------------------------------------------------------------------------------------------------");
                        crespuesta = "0" + "<b>El numero de RUC que registro NO existe, por favor verificar<br><br>Cualquier consulta o duda con este mensaje escriba a daef.pensiones@oficinas-upch.pe";
                        return Content(crespuesta);
                    }
                    oLog.Add("El api entrego la respuesta: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - SI existe el RUC " + cnro_doc.Trim());
                }
                //////////////////////////////////////////////////////////////////
                */


                WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
                object[,] arreglo = new object[2, 2] { { "@accion", "REG" }, { "@xml", xml } };
                DataTable dt = new DataTable();
                dt = obj2.GetDatatable("upch299", "usp_dgagenericos", arreglo);
                crespuesta = dt.Rows[0]["crespuesta"].ToString();

                if (crespuesta.Substring(0, 1) == "1")
                {

                    ccod_ficha = dt.Rows[0]["ccod_ficha"].ToString();
                    cemail = dt.Rows[0]["cemail"].ToString();
                    cdsc_coa = dt.Rows[0]["cdsc_coa"].ToString();
                    chash = dt.Rows[0]["chash"].ToString();
                    cdsc_art = dt.Rows[0]["cdsc_art"].ToString();
                    ctipo = dt.Rows[0]["ctipo"].ToString();
                    cvalor = dt.Rows[0]["cvalor"].ToString();

                    if (crespuesta.Substring(0, 1) == "1")
                    {
                        obj2.SMail("UPCH - Gestión de Pagos <gestiondepagos@upch.pe>", cemail, "Su registro se completo satisfactoriamente - " + cdsc_art, "Estimado(a) " + cdsc_coa + ":<br><br>Gracias por registrarte. Tu información ha sido recibida satisfactoriamente.<br><br><b>Para completar su proceso, puede proceder a pagar desde este enlace </b><br><a href='https://dga2.upch.edu.pe/app/tarifario/register/sucess/" + ctipo + "/" + cvalor + "/" + chash + "' style = ''>" + "https://dga2.upch.edu.pe/app/tarifario/register/sucess/" + ctipo + "/" + cvalor + "/" + chash + "</a><br><br><b>Se agradecera no responder a este correo por que solo es un email de envio de alertas, si tiene cualquier consulta u observacion por favor escribir a los correos que estan en copia, ellos gustosos los atenderan</b><br><br>Si Ud ya cancelo el concepto seleccionado, hacer caso omiso a este mensaje<br><br>UNIVERSIDAD PERUANA CAYETANO HEREDIA",cvalor);
                    }
                    

                    //return RedirectToAction("Sucess", "Register");
                }
                oLog.Add("Termino el proceso de grabar la ficha");
                oLog.Add("-----------------------------------------------------------------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                crespuesta = "0" + ex.ToString();
                oLog.Add("Se produjo un error:" + ex.ToString());
                oLog.Add("-----------------------------------------------------------------------------------------------------------------------");
            }

            return Content(crespuesta + ccod_ficha);

        }

        [HttpPost]
        public ActionResult UpdateProcess()
        {
            string crespuesta = "0";
            string ccod_ficha = "";
            try
            {

                string xml = HttpUtility.UrlDecode(Request["xml"], System.Text.Encoding.Default);
                ccod_ficha = Request["ccod_ficha"];

                WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
                object[,] arreglo = new object[3, 2] { { "@accion", "ACT" }, { "@xml", xml }, { "@ccod_ficha", ccod_ficha } };
                DataTable dt = new DataTable();
                dt = obj2.GetDatatable("upch299", "usp_dgagenericos", arreglo);
                crespuesta = dt.Rows[0]["crespuesta"].ToString();

            }
            catch (Exception ex)
            {
                crespuesta = "0" + ex.ToString();
            }

            return Content(crespuesta);

        }

        public ActionResult Sucess(string id, string id2,string id3)
        {

            //Poner en mantenimiento////////////////
            /*
            string mensaje_mantenimiento = @"<!doctype html>
                <title>Ficha de pago</title>
                <style>
                  body { text-align: center; padding: 150px; }
                  h1 { font-size: 50px; }
                  body { font: 20px Helvetica, sans-serif; color: #333; }
                  article { display: block; text-align: left; width: 650px; margin: 0 auto; }
                  a { color: #dc8100; text-decoration: none; }
                  a:hover { color: #333; text-decoration: none; }
                </style>

                <article>
                    <h1>Regresamos en breve.</h1>
                    <div>
                        <p>
                                Estimado usuario, para mejorar los servicios, estamos realizando un mantenimiento programado. 
                                En breves momentos volveremos a activar la tienda, disculpen los inconvenientes.
                        </p>
                    </div>
                </article>
                ";
            return Content(mensaje_mantenimiento);
            */
            /////////////////////////////////////////


            //chash = "997867ED74A68577218A31B474A288E4C54B0253EE1C421AF1B0BB8F0C875AD6162DC1C3B1B38389F67D27DFB48FD02F47E076C25DEBF07A74F6EA069363D358";

            string mensaje_error = "<!DOCTYPE html><meta name='viewport' content='width = device-width, initial-scale = 1, maximum-scale = 1'><script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js'></script><link href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css' rel='stylesheet'/><body><div id='content'><div class='alert alert-danger text-center' role='alert'>El enlace no existe</div></div></body>";
            //string mensaje_mantenimiento = "<!DOCTYPE html><meta name='viewport' content='width = device-width, initial-scale = 1, maximum-scale = 1'><script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js'></script><link href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css' rel='stylesheet'/><body><div id='content'><div class='alert alert-primary text-center' role='alert'>La pagina se encuentra en mantenimiento, regresamos a las 14:00 horas, disculpen los inconvenientes</div></div></body>";

            string crespuesta = "";
            string ccod_grupo = "";
            ViewBag.ccod_grupo = "";
            //string ccod_curso = "";
            WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
            object[,] arreglo = new object[2, 2] { { "@accion", "MOS" }, { "@chash", id3 } };
            DataTable dt = new DataTable();
            dt = obj2.GetDatatable("upch299", "usp_dgagenericos", arreglo);

            //ccod_curso = dt.Rows[0]["ccod_curso"].ToString();
            crespuesta = dt.Rows[0]["crespuesta"].ToString();
           

            if (crespuesta == "ok")
            {
                ccod_grupo = dt.Rows[0]["ccod_grupo"].ToString();
                ViewBag.ccod_grupo = ccod_grupo;
                ViewBag.ctipo = dt.Rows[0]["ctipo"].ToString();
                ViewBag.cvalor = dt.Rows[0]["ccod_ccosto"].ToString();
                return View(dt);
                //return Content(mensaje_mantenimiento);
            }
            else if (crespuesta == "ne") //no existe
            {
                return Content(mensaje_error);
            }
            else if (crespuesta == "er") //error
            {
                return Content("<font color=red>Se produjo un inconveniente, intente otra vez</font>");
            }
            else if (crespuesta == "ps") //pago satisfactorio
            {
               return Content("<!DOCTYPE html><meta name='viewport' content='width = device-width, initial-scale = 1, maximum-scale = 1'><script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js'></script><link href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css' rel='stylesheet'/><body><div id='content'><div class='alert alert-success text-center' role='alert'>Felicitaciones, Ud. ya cancelo el concepto seleccionado</div></div></body>");
            }
            else
            {
                return Content("");
            }

        }

        [HttpPost]
        public ActionResult SavePreLiquidacion()
        {
            string respuesta = "";
            string ccodigo = "";
            string cnropedido = "";
            try
            {
                string xml = HttpUtility.UrlDecode(Request["xml"], System.Text.Encoding.Default);

                WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
                object[,] arreglo = new object[3, 2] { { "@ccod_cia", "U16" }, { "@accion", "GEN" }, { "@xml", xml } };
                DataTable dt = new DataTable();
                dt = obj2.GetDatatable("upch299", "usp_tiendavirtual", arreglo);
                respuesta = dt.Rows[0]["result"].ToString(); //2 caracteres
                ccodigo = dt.Rows[0]["ccodigo"].ToString(); //10 caracteres
                cnropedido = dt.Rows[0]["cnropedido"].ToString(); //6 caracteres, en alignet le agregan los 3                 


            }
            catch
            {
                respuesta = "er";
                ccodigo = "";
                cnropedido = "";
            }

            return Content(respuesta + ccodigo + cnropedido);

        }
    }
}
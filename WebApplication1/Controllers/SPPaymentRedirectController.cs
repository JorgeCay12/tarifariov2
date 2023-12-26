using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class SPPaymentRedirectController : Controller
    {
        private Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public ActionResult Index()
        {

            decimal Amount = Convert.ToDecimal(Request.Form["ntotal"]);
            string CustomMerchantName = Request.Form["capepat"] + " " + Request.Form["capemat"] + " " + Request.Form["cnom"];
            string ShopperEmail = Request.Form["cemail"];
            string cod_ficha = Request.Form["cod_ficha"];
            string ccodigo = Request.Form["ccodigo"];

            string curl = "";
            WebApplication1.Models.Generico obj2 = new WebApplication1.Models.Generico();
            object[,] arreglo = null;
            string MerchantSalesID = "";

            /*Obtengo el numerador del MerchantSalesID que se encuentra en upch299.dateasy.dga_tabcorrelativos*/
            arreglo = new object[5, 2] { { "@accion", "SS2" }, { "@corigen", "PreCayetano" }, { "@cubicacion_base", "testdatcor2016.dbprecayetano.pct_registro" }, { "@cficha", "S" }, { "@ccodigo", ccodigo } };
            DataTable dt2 = new DataTable();
            dt2 = obj2.GetDatatable("upch299", "usp_dgagenericos", arreglo);

            WebApplication1.ServiceReference1.MerchantExpressApiOperationsClient client = new WebApplication1.ServiceReference1.MerchantExpressApiOperationsClient();

            if (dt2.Rows[0]["cstatus"].ToString() == "ok")
            {

                MerchantSalesID = dt2.Rows[0]["cnnum_corre"].ToString();
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12
                       | SecurityProtocolType.Ssl3;
                
                WebApplication1.ServiceReference1.ExpressTokenResponse ExpressTokenResponse = client.CreateExpressToken(new ServiceReference1.ExpressTokenRequest()
                {
                    //ApiKey = "803e5838e554606ba6519c3bcfe662cb",//"0dafa909d1e48d38c484754d80fc47fe", //e1df6298e7042ee80f01a108f0237f61 //tienda 803e5838e554606ba6519c3bcfe662cb
                    ApiKey = "0dafa909d1e48d38c484754d80fc47fe",//Comercio 2
                    RequestDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss").ToString(),//"2018-07-13T11:04:00",
                    CurrencyID = (dt2.Rows[0]["ccod_mon"].ToString() == "01" ? "PEN" : "USD"), //"PEN",
                    Amount = Amount,
                    MerchantSalesID = MerchantSalesID,
                    Language = WebApplication1.ServiceReference1.LanguageCodeType.ES, //ES
                    LanguageSpecified = true,
                    TrackingCode = "",
                    ExpirationTime = "480", //Es en minutos// se divide entre 60
                    FilterBy = (dt2.Rows[0]["ccod_mon"].ToString() == "01" ? "PEN" : "USD"), //"PEN",
                    //ProductID = "2",
                    TransactionOkURL = "http://www.upch.edu.pe",
                    TransactionErrorURL = "http://www.upch.edu.pe",
                    TransactionExpirationTime = "",
                    CustomMerchantName = CustomMerchantName,
                    ShopperEmail = email_bien_escrito(ShopperEmail) == true ? ShopperEmail : "a@a.com",
                    Signature = obj2.sha256(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss").ToString() + (dt2.Rows[0]["ccod_mon"].ToString() == "01" ? "PEN" : "USD") + Amount + MerchantSalesID.ToString() + "ES480" + "http://www.upch.edu.pe" + "http://www.upch.edu.pe" + "eea01bd829e49a4a3ea49565f53f8cc0").ToString().ToUpper() //a00b02d00f94d5ea67c54d88345f97c6                    
                    //Signature = obj2.sha256(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss").ToString() + "PEN" + Amount + MerchantSalesID.ToString() + "ES480" + "http://www.upch.edu.pe" + "http://www.upch.edu.pe" + "ce98e50a1c24ce2a88b73efc140efe17").ToString().ToUpper() //a00b02d00f94d5ea67c54d88345f97c6 //tienda
                });
                curl = ExpressTokenResponse.ShopperRedirectURL.ToString();




            }

            return Redirect(curl);
            //return Content(MerchantSalesID);
        }
    }
}
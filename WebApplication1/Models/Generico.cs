using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace WebApplication1.Models
{
    public class Generico
    {
        public static SqlConnection GetConnection(string name)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings[name].ConnectionString);
            //con.ConnectionString = "Data Source = 172.17.100.220; Initial Catalog = imtavh; User ID = siga; Password =ByhvsUk88xfjdeSK";
            return con;
        }

        public string sha256(string texto)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(texto));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }

        public DataTable GetDatatable(string name, string uspnombre, object[,] arreglo)
        {
            SqlConnection con = GetConnection(name);
            SqlCommand sqlComm = new SqlCommand(uspnombre, con);

            int i;
            for (i = 0; i < arreglo.GetLength(0); i++)
            {
                sqlComm.Parameters.AddWithValue(arreglo[i, 0].ToString(), arreglo[i, 1]);
            }

            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlComm;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

        public DataSet GetDataSet(string name, string uspnombre, object[,] arreglo)
        {
            try
            {
                SqlConnection con = GetConnection(name);
                SqlCommand sqlComm = new SqlCommand(uspnombre, con);
                int i;
                for (i = 0; i < arreglo.GetLength(0); i++)
                {
                    sqlComm.Parameters.AddWithValue(arreglo[i, 0].ToString(), arreglo[i, 1]);
                }
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (System.Exception ex)
            {
                return null;
            }

        }

        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }

        public String SMail(string desde, string para, string asunto, string cuerpo, string cvalor)
        {            
            string ccod_ug = cvalor.Substring(2, 2); //saco la ug del centro de costo
            string resultado = "";
            try
            {


                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = false;


                //smtpClient.Credentials = new System.Net.NetworkCredential("upchnt\\42111122", "12345j");
                //smtpClient.Credentials = new System.Net.NetworkCredential("cepu.matricula@oficinas-upch.pe", "matriculacepu");
                //smtpClient.Credentials = new System.Net.NetworkCredential("carlos.callupe.c@upch.pe", "$Iori87@1");
                smtpClient.Credentials = new System.Net.NetworkCredential("gestiondepagos@upch.pe", "v~2aX=m/{UL8EB7P");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.UseDefaultCredentials = true;

                smtpClient.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(desde.Trim());
                //mail.To.Add(new MailAddress("xcarlosperu@hotmail.com"));
                mail.To.Add(new MailAddress(para.Trim()));
                //mail.To.Add(new MailAddress(capo_email.Trim()));                
                /*Con copia de todas maneras*/
                if (ccod_ug == "02" || ccod_ug == "04" || ccod_ug == "06")
                {
                    mail.CC.Add(new MailAddress("elizabeth.aramburu.l@upch.pe"));
                    mail.CC.Add(new MailAddress("sandra.wester.d@upch.pe"));
                    
                } else if (ccod_ug == "09")
                {
                    mail.CC.Add(new MailAddress("favez.postgrado@oficinas-upch.pe"));
                    mail.CC.Add(new MailAddress("favez.marketing@oficinas-upch.pe"));
                } else if (ccod_ug == "10")
                {
                    mail.CC.Add(new MailAddress("epgvac.cursovirtual@oficinas-upch.pe"));
                    mail.CC.Add(new MailAddress("comunicaciones2@oficinas-upch.pe"));
                } else if (ccod_ug == "07")
                {
                    mail.CC.Add(new MailAddress("garry.garibay@upch.pe"));
                    mail.CC.Add(new MailAddress("giannina.cotrina@upch.pe"));
                    mail.CC.Add(new MailAddress("katerin.villanueva@upch.pe"));
                    mail.CC.Add(new MailAddress("jahir.franco@upch.pe"));
                    mail.CC.Add(new MailAddress("giulia.vargas@upch.pe"));
                }
                mail.Bcc.Add(new MailAddress("carlos.callupe.c@upch.pe"));
                //mail.Bcc.Add(new MailAddress("ivan@upch.pe"));
                //mail.Bcc.Add(new MailAddress("mmasias @gotuzzos.com"));               
                /****************************/
                mail.Subject = asunto.ToString();
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = cuerpo;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                smtpClient.Send(mail);

                resultado = "ok";

            }
            catch (Exception ex)
            {
                resultado = "er" + ex.ToString();
            }
            return resultado;

        }
    }
}
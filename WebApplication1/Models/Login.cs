using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Web.Configuration;
using System.DirectoryServices;

namespace WebApplication1.Models
{
    public class Login
    {
        public string AutentificacionUPCH(string usuario, string password)
        {
            string authenticated = "no";
            try
            {
                DirectoryEntry de = new DirectoryEntry("LDAP://172.17.100.4", usuario, password, AuthenticationTypes.Secure);
                DirectorySearcher ds = new DirectorySearcher(de);
                ds.FindOne();
                authenticated = "si";
            }
            catch (Exception ex)
            {
                authenticated = "no";

            }
            return authenticated;
        }
    }
}
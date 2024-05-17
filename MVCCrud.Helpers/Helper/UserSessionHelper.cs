using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCCrud.Sessions
{
    public class UserSession
    {
        public static int UserID 
        {
            get {
                return (int)(HttpContext.Current.Session["UserID"] == null ? 0 : HttpContext.Current.Session["UserID"]);
            }
            set {
                HttpContext.Current.Session["UserID"] = value;
            }
        }
        public static string UserName
        {
            get
            {
                return (string)(HttpContext.Current.Session["UserName"] == null ? 0 : HttpContext.Current.Session["UserName"]);
            }
            set
            {
                HttpContext.Current.Session["UserName"] = value;
            }
        }
        public static string UserRole
        {
            get
            {
                return (string)(HttpContext.Current.Session["UserRole"] == null ? 0 : HttpContext.Current.Session["UserRole"]);
            }
            set
            {
                HttpContext.Current.Session["UserRole"] = value;
            }
        }
    }
}
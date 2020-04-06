using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMS.ViewModels.Common
{
    public static class SessionManager
    {
        public static int UserId
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            }
            set
            {
                HttpContext.Current.Session["UserId"] = value;
            }
        }

        public static int UserTypeId
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["UserTypeId"]);
            }
            set
            {
                HttpContext.Current.Session["UserTypeId"] = value;
            }
        }

        public static int BookSellerId
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["BookSellerId"]);
            }
            set
            {
                HttpContext.Current.Session["BookSellerId"] = value;
            }
        }
        public static int ParentsId
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["ParentsId"]);
            }
            set
            {
                HttpContext.Current.Session["ParentsId"] = value;
            }
        }
    }
}
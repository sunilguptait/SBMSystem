using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Common
{
    public static class AppConstants
    {
        public static string JWTKey = "";
        public static int JWTTokenTimeoutMinutes = 0;
        public static string TimeZone = "";
        public static string BookSellerRegistrationEmailSubject = "";
        
        static AppConstants()
        {
            Type type = typeof(AppConstants);
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                string value = ConfigurationManager.AppSettings[field.Name];
                if (!string.IsNullOrEmpty(value))
                {
                    if (typeof(int).IsAssignableFrom(field.FieldType))
                    {
                        field.SetValue(null, Convert.ToInt32(value));
                    }
                    else if (typeof(bool).IsAssignableFrom(field.FieldType))
                    {
                        field.SetValue(null, Convert.ToBoolean(value));
                    }
                    else
                    {
                        field.SetValue(null, value);
                    }
                }
            }
        }
        
    }
}

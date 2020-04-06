using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI;
using SBMS.Mobile.Models.Common;
using System.Collections.ObjectModel;
using System.IO;

namespace SBMS.Mobile.Common
{
    public static class CommonExtensions
    {
        private static double searchPanelHeight = 0;
        public static int ToInt(this object value)
        {
            return Convert.ToInt32(value);
        }
        public static bool ToBoolean(this object value)
        {
            if (value == null)
                return false;
            return Convert.ToBoolean(value);
        }
        public static Dictionary<string, string> ToDictionary(this object obj, bool considerOnlyValuedProperties = true)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (obj == null)
                return ret;

            foreach (PropertyInfo prop in obj.GetType().GetRuntimeProperties())
            {
                string propName = prop.Name;
                var val = obj.GetType().GetRuntimeProperty(propName).GetValue(obj, null);
                if (val != null)
                {
                    if (prop.PropertyType == typeof(int))
                    {
                        if ((!string.IsNullOrEmpty(Convert.ToString(val)) && Convert.ToString(val) != "0") || considerOnlyValuedProperties == false)
                            ret.Add(propName, Convert.ToString(val));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(val)) || considerOnlyValuedProperties == false)
                            ret.Add(propName, Convert.ToString(val));
                    }

                }
                //else
                //{
                //    ret.Add(propName, null);
                //}
            }
            return ret;
        }
        public static string ConvertDictionaryToString(Dictionary<string, string> objDictionary)
        {
            string paramStr = string.Empty;
            if (objDictionary != null)
            {
                paramStr = string.Join("&", objDictionary.Select(kv => kv.Key.ToString() + "=" + kv.Value.ToString()).ToArray());
            }
            return paramStr;
        }
        public static string GetPropertyValues<T>(T APIParams)
        {
            StringBuilder spAPIParams = new StringBuilder();
            string apiParams = string.Empty;
            Type t = APIParams.GetType();
            foreach (PropertyInfo pi in t.GetRuntimeProperties())
            {
                //string otherProperties = "MethodName,UseMemoryCache,IsRefreshData,MethodType";
                //if (!otherProperties.Contains(pi.Name))
                //{
                if (pi.PropertyType == typeof(string) || pi.PropertyType == typeof(int))
                {
                    spAPIParams.Append(pi.Name + "=" + Convert.ToString(pi.GetValue(APIParams, null)) + "&");
                }
                else if (pi.PropertyType == typeof(string[]))
                {
                    object[] array = (object[])pi.GetValue(APIParams);
                    if (array != null)
                        spAPIParams.Append(pi.Name + "=" + string.Join(",", array) + "&");
                }
                //}
            }
            apiParams = spAPIParams != null ? Convert.ToString(spAPIParams) : string.Empty;
            //  apiParams = apiParams.Length > 1 ? apiParams.Substring(0, apiParams.Length - 1) : apiParams;
            return apiParams;
        }
        public static int PageIndex<T>(this IEnumerable<T> list, int pageSize)
        {
            var pageIndex = (Convert.ToDouble(list?.Count()) / Convert.ToDouble(pageSize)) + 1;
            return Convert.ToInt32(pageIndex);
        }
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
        //public static void CollapseStackLayout(this object control, bool isHide = true)
        //{
        //    var layout = control as StackLayout;
        //    // get reference to the layout to animate

        //    // setup information for animation
        //    Action<double> callback = input => { layout.HeightRequest = input; }; // update the height of the layout with this callback
        //    searchPanelHeight = isHide ? layout.Height : searchPanelHeight;
        //    double startingHeight = isHide ? layout.Height : 0; // the layout's height when we begin animation
        //    double endingHeight = isHide ? 0 : searchPanelHeight; // final desired height of the layout
        //    uint rate = 16; // pace at which aniation proceeds
        //    uint length = 500; // one second animation
        //    Easing easing = Easing.CubicOut; // There are a couple easing types, just tried this one for effect


        //    // now start animation with all the setup information
        //    layout.Animate("invis", callback, startingHeight, endingHeight, rate, length, easing);
        //    if (isHide)
        //        layout.FadeTo(0, 500, Easing.CubicInOut);
        //    else
        //        layout.FadeTo(1, 500, Easing.CubicInOut);

        //}
        public static string CurrencySymbol(string culture)
        {
            CultureInfo cult = new CultureInfo(culture);
            return cult.NumberFormat.CurrencySymbol;
        }

        public static void Initialize(this ObservableCollection<DropdownModel> dropdpwns)
        {
            dropdpwns = new ObservableCollection<DropdownModel>();
            dropdpwns.Add(new DropdownModel() { Text = "" });
        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        public static Stream ConvertToBase64(this Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return new MemoryStream(Encoding.UTF8.GetBytes(base64));
        }
        public static string ConvertToBase64String(this Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
    }
}

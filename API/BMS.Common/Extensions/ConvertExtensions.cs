using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BMS.Common.Extensions
{
    public static class ConvertExtensions
    {
        public static DateTime ToDateTime(this string dateTime)
        {
            DateTime dt;
            if (DateTime.TryParseExact(dateTime, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                //valid date
            }
            else
            {
                //invalid date
            }
            return dt;
        }

        public static DateTime ToDateTimeFromUrl(this string dateTime)
        {
            DateTime dt;
            if (DateTime.TryParseExact(dateTime, "d-M-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                //valid date
            }
            else
            {
                //invalid date
            }
            return dt;
        }

        public static string ToDDMMYYYY(this DateTime dateTime)
        {
            string s = dateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            return s;
        }

        public static string ToDDMMYYYYForUrl(this DateTime dateTime)
        {
            string s = dateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            return s;
        }

        public static bool IsDDMMYYYY(this string dateTime)
        {
            DateTime dt;
            if (DateTime.TryParseExact(dateTime, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ToShortNo(this string no)
        {
            return no.Substring(0, 3) + (new string('X', 4)) + no.Substring(no.Length - 3, 3);
        }

        public static string ToFinancialYear(this DateTime dateTime)
        {
            return dateTime.Month >= 4 ? (dateTime.Year).ToString() + "-" + dateTime.AddYears(1).ToString("yy") : dateTime.AddYears(-1).Year.ToString() + "-" + dateTime.ToString("yy");
        }

        public static string Ordinal(this int number)
        {
            var work = number.ToString();
            if ((number % 100) == 11 || (number % 100) == 12 || (number % 100) == 13)
                return work + "th";
            switch (number % 10)
            {
                case 1: work += "st"; break;
                case 2: work += "nd"; break;
                case 3: work += "rd"; break;
                default: work += "th"; break;
            }
            return work;
        }

        public static string ToNewCode(this string lastCode)
        {
            return Regex.Replace(lastCode, "\\d+", m => (int.Parse(m.Value) + 1).ToString(new string('0', m.Value.Length)));
        }
    }
}

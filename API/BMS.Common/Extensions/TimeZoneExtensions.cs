using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Common.Extensions
{
    public static class TimeZoneExtensions
    {
        public static TimeZoneInfo TimeZone = TimeZoneInfo.FindSystemTimeZoneById(AppConstants.TimeZone);
        public static DateTime ToIST(this DateTime utcDate)
        {
            var istDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZone);
            return istDate;
        }
        public static string ToISTDDMMYYYY(this DateTime date)
        {
            return date.ToDDMMYYYY();
        }
    }
}

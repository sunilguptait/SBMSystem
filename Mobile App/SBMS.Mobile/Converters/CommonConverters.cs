using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using SBMS.Mobile.Common;
using System.Collections;

namespace SBMS.Mobile.Converters
{
    public class ProductNameConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = System.Convert.ToString(value);
            name = name.Length > 25 ? name.Substring(0, 25) + "...." : name;
            return name;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class PriceConverter : IValueConverter
    {
        public string CultureName { get; set; } = "hi-IN";
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return CurrencyLabel(System.Convert.ToDecimal(value), System.Convert.ToDouble(parameter));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        public string CurrencySymbol(string culture)
        {
            CultureInfo cult = new CultureInfo(culture);
            return cult.NumberFormat.CurrencySymbol;
        }
        public FormattedString CurrencyLabel(decimal? price, double fontsize = 16, string culture = "")
        {
            if (string.IsNullOrEmpty(culture))
                culture = CultureName;
            price = price ?? 0;
            return new FormattedString
            {
                Spans = {
                        new Span { Text = CurrencySymbol(culture), FontSize = fontsize },
                        new Span { Text = " "+ Math.Round(price.Value,2), FontSize = fontsize } }
            };
        }
    }

    public class IsVisibleConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.ToString() == "" || value.ToString() == "0")
                return false;
            return true;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class IsReverseVisibleConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.ToString() == "" || value.ToString() == "0")
                return true;
            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class IsReverseBoolConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !value.ToBoolean();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class IsListVisibleConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            return ((IList)value).Count == 0 ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class DataSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;
            return ((IList)value).Count == 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

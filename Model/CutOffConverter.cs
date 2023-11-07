using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Demo_var_6
{
    internal class CutOffConverterDecimal : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal a = 0;
            value = (value as string).Replace('.',',');
            if (decimal.TryParse(((string)value).Replace('.', ','), out a))
            {
                if (((string)value).Contains(','))
                    if (((string)value).Count(ch => ch == ',') > 1
                        || ((string)value).Substring(((string)value).IndexOf(','), ((string)value).Length - ((string)value).IndexOf(',')).Length > 3 
                        || (((string)value).Substring(((string)value).IndexOf(','), ((string)value).Length - ((string)value).IndexOf(','))).Length ==1)
                        return false;
                            return (decimal.Parse((string)value)) >= Cutoff;
            }
            else return false;
        }
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
            public int Cutoff { get; set; }
    }
    internal class CutOffConverterInteger : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse((string)value, out _))
            {
                return (int.Parse((string)value)) >= Cutoff;
            }
            else return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        public int Cutoff { get; set; }
    }
}

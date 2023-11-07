using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Demo_var_6
{
    internal class ButtonContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                          object parameter, CultureInfo culture) => (value as string) == "Войти" ? true : false;
        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

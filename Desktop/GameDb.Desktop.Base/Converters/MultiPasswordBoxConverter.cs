using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace GameDb.Desktop.Base.Converters
{
    [ValueConversion(typeof(object), typeof(bool))]
    public class MultiPasswordBoxConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null)
            {
                var res = new List<object>
                {
                    values[0],
                    values[1]
                };

                return res;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace GGGC.Admin.Support
{
    public class TicketStatusConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool currentlyRented = (bool)value;

            return (currentlyRented ? "ACTIVO" : "BAJA");
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class TicketDepartmentConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte bytID = (byte)value;
            string strValue = "";
                switch(bytID)
                {
                    case 1: strValue = "Sistemas";
                        break;
                    case 2: strValue = "Mantenimiento";
                         break;
                     default:
                        strValue= "No Disponible";
                         break;

                }
            //(currentlyRented ? "Currently Rented" : "Available");

                return strValue;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

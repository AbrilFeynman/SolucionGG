using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class TicketStatusConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte bytID = (byte)value;
            string strValue = "";
            switch (bytID)
            {
                case 1: strValue = "Nuevo";
                    break;
                case 2: strValue = "Pendiente";
                    break;
                case 3: strValue = "En Proceso";
                    break;
                case 4: strValue = "Solucionado";
                    break;
                case 5: strValue = "En Espera";
                    break;
                case 6: strValue = "Cerrado";
                    break;
                default:
                    strValue = "Sin Estatus";
                    break;
            }
            //1 nuevo 2 pendiente 3 en proceso 4 Solucionado 5 En Espera 6 Cerrado
            //(currentlyRented ? "Currently Rented" : "Available");

            return strValue;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class GarageBrandConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte bytID = (byte)value;
            string strValue = "";
            switch (bytID)
            {
                case 0: strValue = "NO IDENTIFICADA";
                    break;
                case 1: strValue = "MOBIL";
                    break;
                case 2: strValue = "PENZOIL";
                    break;
                case 3: strValue = "GONHER";
                    break;
                default:
                    strValue = "N/A";
                    break;

            }
            //(currentlyRented ? "Currently Rented" : "Available");

            return strValue;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private void cargarMarcas()
        {
            try
            {
                //AccesoDatos sCen = new AccesoDatos(6);
                ////System.Data.DataSet ds = new System.Data.DataSet();
                //string sSQL = "SELECT * FROM  mto_Marcas  ";

                //DataTable tbl = sCen.BaseDatos.Consulta(sSQL);
                //int i = 0;
                //foreach (var row in tbl.Rows)
                //{
                //    objects.Add(new Brand(tbl.Rows[i]["MarcaID"].ToString(), tbl.Rows[i]["Marca"].ToString()));
                //    i++;
                //}


            }
            catch (Exception ex)
            {
                //  MessageBox.Show("Error en cargarDatos: " + ex.Message);
            }

        }
    }
}

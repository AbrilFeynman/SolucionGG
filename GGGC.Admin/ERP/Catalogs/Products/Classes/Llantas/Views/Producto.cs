using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Catalogs.Products.Classes.Llantas.Views
{
    public class Producto: IDataErrorInfo
    {

        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        //public int RollNumber { get; set; }
        //public string Division { get; set; }

        //public List<string> Divisions { get; set; }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Codigo")
                {
                    if (string.IsNullOrEmpty(Codigo) || Codigo.Length < 3)
                        result = "Debe Ingresar el codigo";
                }

                if (columnName == "Descripcion")
                {
                    if (string.IsNullOrEmpty(Descripcion) || Descripcion.Length < 3)
                        result = "Debe Ingresar la desc";
                }
                //if (columnName == "RollNumber")
                //{
                //    if (RollNumber <= 0)
                //        result = "Please enter a Proper roll Number";
                //}
                //if (columnName == "Division")
                //{
                //    if (string.IsNullOrEmpty(Division))
                //        result = "Please select Division";
                //}
                return result;
            }
        }
    }
}

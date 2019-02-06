using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Catalogs.Products.Classes.Llantas.Views
{
    public class Student : IDataErrorInfo
    {
        public string FirstName { get; set; }
        public int RollNumber { get; set; }
        public string Division { get; set; }

        public List<string> Divisions { get; set; }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "FirstName")
                {
                    if (string.IsNullOrEmpty(FirstName) || FirstName.Length < 3)
                        result = "Please enter a Name";
                }
                if (columnName == "RollNumber")
                {
                    if (RollNumber <= 0)
                        result = "Please enter a Proper roll Number";
                }
                if (columnName == "Division")
                {
                    if (string.IsNullOrEmpty(Division))
                        result = "Please select Division";
                }
                return result;
            }
        }
    }
}

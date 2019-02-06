using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class Department
    {

        public Department(string key, string value)
        {
            this.Key = key;
            this.Value = value;
            //this.Phone = phone;
            //this.Valor = valor;
        }
        public string Key
        {
            get;
            set;
        }
        public string Value
        {
            get;
            set;
        }
        //public string Phone
        //{
        //    get;
        //    set;
        //}

        //public string Valor
        //{
        //    get;
        //    set;
        //}
    }
}

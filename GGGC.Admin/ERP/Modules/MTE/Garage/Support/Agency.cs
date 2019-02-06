using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class Agency
    {
        public Agency()
        {
        }
        public Agency(string name, string phone, string zip)
        {
            this.Name = name;
            this.Phone = phone;
            this.Zip = zip;
        }
        public string Name
        {
            get;
            set;
        }
        public string Phone
        {
            get;
            set;
        }
        public string Zip
        {
            get;
            set;
        }
    }
}

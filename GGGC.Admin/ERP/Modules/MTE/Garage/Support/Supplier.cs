using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class Supplier
    {

        public Supplier(int supplierID, string rfc, string tradename, string value)
        {
            this.SupplierID = supplierID;
            this.RFC = rfc;
            this.TradeName = tradename;
            this.Value = value;
        }
        public int SupplierID
        {
            get;
            set;
        }
        public string RFC
        {
            get;
            set;
        }
        public string TradeName
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }
    }
}

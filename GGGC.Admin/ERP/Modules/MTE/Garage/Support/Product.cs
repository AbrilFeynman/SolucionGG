using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class Product
    {

        public Product(int productID, string codeid, string description, string value)
        {
            this.ProductID = productID;
            this.CodeID = codeid;
            this.Description = description;
            this.Value = value;
        }
        public int ProductID
        {
            get;
            set;
        }
        public string CodeID
        {
            get;
            set;
        }
        public string Description
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

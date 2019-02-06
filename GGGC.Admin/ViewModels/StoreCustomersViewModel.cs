using GGGC.Admin.Sign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ViewModels
{
   public class StoreCustomersViewModel //: ERPViewModelBase<Customer>
    {
        //public override object GetNewItem()
        //{
        //    // Should be implemented for adding new items.
        //    return null;
        //}

        //protected override void GetQuery()
        //{
        //    //  MainRepository.GetCustomers(false, this.Callback);
        //}

        //public override string ItemName
        //{
        //    get
        //    {
        //        return "Store";
        //    }
        //}

        public override string ToString()
        {
            return "Store Customers";
        }

    }
}

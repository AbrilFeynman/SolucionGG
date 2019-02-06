using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Mobile.Model
{
    public class Customer
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set {

                _name = value;
                if (String.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Customer name is mandatory.");
                }
            }
        }

        private string _textProperty;
        public string TextProperty
        {
            get { return _textProperty; }
            set
            {
                if (value.Length > 5)
                {
                    throw new Exception("Too many characters");
                }
                _textProperty = value;
            }
        }


    }
}

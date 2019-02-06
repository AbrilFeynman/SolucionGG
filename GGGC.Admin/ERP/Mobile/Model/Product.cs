using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Mobile.Model
{
    public class Product : IDataErrorInfo
    {
        #region state properties

        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        #endregion

        public void Save()
        {
            //Insert code to save new Product to database etc 
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string propertyName]
        {
            get
            {
                string validationResult = null;
                switch (propertyName)
                {
                    case "ProductName":
                        validationResult = ValidateName();
                        break;
                    case "Height":
                        validationResult = ValidateHeight();
                        break;
                    case "Width":
                        validationResult = ValidateWidth();
                        break;
                    default:
                        throw new ApplicationException("Unknown Property being validated on Product.");
                }
                return validationResult;
            }
        }

        private string ValidateName()
        {
            if (String.IsNullOrEmpty(this.ProductName))
                return "Product Name needs to be entered.";
            else if (this.ProductName.Length < 5)
                return "Product Name should have more than 5 letters.";
            else
                return String.Empty;
        }

        private string ValidateHeight()
        {
            if (this.Height <= 0)
                return "Height should be greater than 0";
            if (this.Height > this.Width)
                return "Height should be less than Width.";
            else
                return String.Empty;
        }

        private string ValidateWidth()
        {
            if (this.Width <= 0)
                return "Width should be greater than 0";
            if (this.Width < this.Height)
                return "Width should be greater than Height.";
            else
                return String.Empty;
        }
    }
}

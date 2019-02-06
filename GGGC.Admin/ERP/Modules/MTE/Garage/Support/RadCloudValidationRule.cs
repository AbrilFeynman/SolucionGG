using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class RadCloudValidationRule:ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            var fileName = (string)value;
            if (fileName.Contains("_"))
            {
                return new ValidationResult(false, "You can not upload files containing underscore ('_') in their name.");
            }
            return new ValidationResult(true, null);
        }
    }
}

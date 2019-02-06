using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class StringLenghtFilteringBehavior : ComboBoxFilteringBehavior
    {


        private int charLenght;
        //public override List<int> FindMatchingIndexes(string text)
        //{
        //    //if (int.TryParse(text, out this.charLenght))
        //    //{
        //    //    return this.ComboBox.Items.OfType<DataItem>().Where(i => i.Title.Length >= this.charLenght).Select(i => this.ComboBox.Items.IndexOf(i)).ToList();
        //    //}
        //    //return new List<int>();
        //}
    }


}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.AZ.Llantas.Views
{
    public class RadGridViewSampleData
    {
        public RadGridViewSampleData()
        {
            Cars = new ObservableCollection<Class1>();
            Cars.Add(new Class1("AAA", "XXXXXXXXXXXXXXXXXXXXXXXXXX"));
            Cars.Add(new Class1("BBB", "XXXXXXXXXXXXXXXXXXXXXXXXXX"));
            Cars.Add(new Class1("CCC", "XXXXXXXXXXXXXXXXXXXXXXXXXX"));
            Cars.Add(new Class1("DDD", "XXXXXXXXXXXXXXXXXXXXXXXXXX"));
        }

        public ObservableCollection<Class1> Cars
        {
            get;
            set;
        }
    }
}

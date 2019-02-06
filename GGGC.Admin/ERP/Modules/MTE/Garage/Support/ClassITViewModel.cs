using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class ClassITViewModel
    {
        private ObservableCollection<ClassIT> objects;

        public ObservableCollection<ClassIT> ClassIT
        {
            get
            {
                if (objects == null)
                {
                    objects = new ObservableCollection<ClassIT>();
                    objects.Add(new ClassIT("1", "Alta"));
                    objects.Add(new ClassIT("2", "Media"));
                    objects.Add(new ClassIT("3", "Baja"));
                    

                }

                return objects;
            }
        }
    }
}

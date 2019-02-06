using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class GroupITViewModel
    {
        private ObservableCollection<GroupIT> objects;

        public ObservableCollection<GroupIT> GroupIT
        {
            get
            {
                if (objects == null)
                {
                    objects = new ObservableCollection<GroupIT>();
                    objects.Add(new GroupIT("1", "Alta"));
                    objects.Add(new GroupIT("2", "Media"));
                    objects.Add(new GroupIT("3", "Baja"));
                    

                }

                return objects;
            }
        }
    }
}

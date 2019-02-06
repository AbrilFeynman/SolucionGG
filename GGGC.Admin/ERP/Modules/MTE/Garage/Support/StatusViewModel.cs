using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class StatusViewModel
    {
        private ObservableCollection<Status> objects;

        public ObservableCollection<Status> Status
        {
            get
            {
                if (objects == null)
                {
                    objects = new ObservableCollection<Status>();
                    objects.Add(new Status("1", "Nuevo"));
                    objects.Add(new Status("2", "Pendiente"));
                    objects.Add(new Status("3", "En Proceso"));
                    objects.Add(new Status("4", "En Espera"));
                    objects.Add(new Status("5", "Solucionado"));
                    objects.Add(new Status("6", "Cerrado"));
                    

                }

                return objects;
            }
        }
    }
}

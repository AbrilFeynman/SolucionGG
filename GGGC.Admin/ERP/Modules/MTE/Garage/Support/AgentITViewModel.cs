using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class AgentITViewModel
    {
        private ObservableCollection<AgentIT> objects;

        public ObservableCollection<AgentIT> AgentIT
        {
            get
            {
                if (objects == null)
                {
                    objects = new ObservableCollection<AgentIT>();
                    objects.Add(new AgentIT("1", "Alta"));
                    objects.Add(new AgentIT("2", "Media"));
                    objects.Add(new AgentIT("3", "Baja"));
                    

                }

                return objects;
            }
        }
    }
}

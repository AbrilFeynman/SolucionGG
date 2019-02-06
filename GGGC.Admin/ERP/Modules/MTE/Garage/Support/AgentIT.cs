using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class AgentIT
    {
        public AgentIT(string name, string detail)
        {
            this.Name = name;
            this.Detail = detail;
            
        }
        public string Name
        {
            get;
            set;
        }

        public string Detail
        {
            get;
            set;
        }
        
        
    }
}

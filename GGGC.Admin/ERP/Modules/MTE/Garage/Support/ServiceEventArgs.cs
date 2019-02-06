using System;
using System.Collections.Generic;
using System.Linq;
using GGGC.Client.Entities;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class ServiceEventArgs : EventArgs
    {
        public ServiceEventArgs(Service _obj, bool isNew)
        {
            Servicios = _obj;
            IsNew = isNew;
        }

        public Service Servicios { get; set; }
        public bool IsNew { get; set; }
    }
}

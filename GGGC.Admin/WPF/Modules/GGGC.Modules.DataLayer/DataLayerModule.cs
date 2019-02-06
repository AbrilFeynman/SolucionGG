using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GGGC.UI.Infrastructure.Prism;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace GGGC.Modules.DataLayer
{
    public class DataLayerModule :ModuleBase
    {
        public DataLayerModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {

        }

        protected override void RegisterTypes()
        {
            //Container.RegisterTypeForNavigation<DefaultView>();
            //Container.RegisterTypeForNavigation<PuestosView>();
            //Container.RegisterTypeForNavigation<EmployeesView>();
            //Container.RegisterTypeForNavigation<viewEmployee>();
            //Container.RegisterType<IEmployeesViewViewModel, EmployeesViewViewModel>();
        }

        protected override void ResolveOutlookGroup()
        {
           // RegionManager.Regions[RegionNames.OutlookBarGroupRegion].Add(Container.Resolve<EmployeesMenu>());
        }

        //protected override void RegisterTypes()
        //{
        //   // throw new NotImplementedException();
        //    Container.RegisterTypeForNavigation<EmployeesView>();
        //}
    }
}

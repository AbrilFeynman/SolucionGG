using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InputViewModel: ViewModelBase
    {
          [ImportingConstructor]
        public InputViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;
            //EditObjectCommand = new DelegateCommand<Article>(OnEditObjectCommand);
            //NewObjectCommand = new DelegateCommand<object>(OnNewObjectCommand);
          //  DetailObjectCommand = new DelegateCommand<Article>(OnDetailObjectCommand);
          //this.buttonNewIsEnabled = true;
           // this.EntitySetIsLoading = true;
        }
        IServiceFactory _ServiceFactory;


        public override string ViewTitle
        {
            get { return "Compras (Entradas)"; }
        }


    }
}

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

namespace GGGC.Admin.ERP.Modules.B20.SO.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EditViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public EditViewModel()//IServiceFactory serviceFactory
        {
            //_ServiceFactory = serviceFactory;
            //EditObjectCommand = new DelegateCommand<Ticket>(OnEditObjectCommand);
            //NewObjectCommand = new DelegateCommand<object>(OnNewObjectCommand);
            //DetailObjectCommand = new DelegateCommand<Ticket>(OnDetailObjectCommand);
            //this.buttonNewIsEnabled = true;
            // this.EntitySetIsLoading = true;
        }

    }
}

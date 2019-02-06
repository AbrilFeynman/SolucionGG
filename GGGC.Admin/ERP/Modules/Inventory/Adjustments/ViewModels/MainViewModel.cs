using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.UI.Core;

namespace GGGC.Admin.ERP.Modules.Inventory.Adjustments.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MainViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }

        [Import]
        public QueryViewModel QueryViewModel { get; private set; }

        [Import]
        public NewViewModel NewViewModel { get; private set; }
    }
}

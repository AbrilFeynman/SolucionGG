using Core.Common.UI.Core;
using System.ComponentModel.Composition;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MainViewModel : ViewModelBase
    {
        [Import]
        public CatalogViewModel CatalogViewModel { get; private set; }

        [Import]
        public InputViewModel InputViewModel { get; private set; }

        [Import]
        public CatalogServicesViewModel CatalogServicesViewModel { get; private set; }

        [Import]
        public OutputViewModel OutputViewModel { get; private set; }
    }
}

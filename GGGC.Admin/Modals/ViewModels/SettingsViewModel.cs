using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.Modals.ViewModels
{
    class SettingsViewModel: ViewModelBaseTMP
    {
        public SettingsViewModel(Action closeAction)
        {
            OkCommand = new RelayCommand(_ => closeAction());
        }
        public RelayCommand OkCommand { get; private set; }
    }
}

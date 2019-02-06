using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace GGGC.Admin.ERP.Designs
{
    /// <summary>
    /// Interaction logic for CambiarPassword.xaml
    /// </summary>
    public partial class CambiarPassword : UserControl
    {
        public CambiarPassword()
        {
            InitializeComponent();
            StyleManager.SetTheme(radTabControl, new Windows8TouchTheme());
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((ContentControl)this.Parent).Content = null;
                       
        }
    }
}

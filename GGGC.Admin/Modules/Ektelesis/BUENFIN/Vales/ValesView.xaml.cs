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

namespace GGGC.Admin.Modules.Ektelesis.BUENFIN.Vales
{
    /// <summary>
    /// Interaction logic for ValesView.xaml
    /// </summary>
    public partial class ValesView : UserControl
    {
        private delegate void UpdateDelegate(string i);
        private delegate void NoArgDelegate();

        public delegate void UpdateStatusBarEventHandler(string message);
        public event UpdateStatusBarEventHandler UpdateStatusBar;
        public ValesView()
        {
            InitializeComponent();
           
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //var before = GC.GetTotalMemory(false);
            //MessageBox.Show(before.ToString("#,###"));

            //Shell.userControls.Remove("1951");
            //Shell.userControls["1951"] = null;

            ((ContentControl)this.Parent).Content = null;
            //Shell.

            // Shell.radTreeViewCatalogs_SelectionChanged(null, null);
            //Shell.rb
            //MessageBox.Show(before.ToString("antes de gc" + "#,###"));
            GC.Collect();
            //var after = GC.GetTotalMemory(false);
            //MessageBox.Show(after.ToString("#,###"));
            // GC.c

        }


    
    }
}

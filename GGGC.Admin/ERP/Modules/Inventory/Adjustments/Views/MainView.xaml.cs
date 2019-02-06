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
using Core.Common.Core;
using Core.Common.UI.Core;
using GGGC.Admin.ERP.Modules.Inventory.Adjustments.ViewModels;


namespace GGGC.Admin.ERP.Modules.Inventory.Adjustments.Views
{
    /// <summary>
    /// Lógica de interacción para MainView.xaml
    /// </summary>  
    public partial class MainView : UserControlViewBase
    {
        public MainView()
        {
            InitializeComponent();
            //main.DataContext = ObjectBase.Container.GetExportedValue<MainViewModel>();
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {



            //   ObjectBase.Container.Catalog.
            //(this.Parent as ContentControl)..Children.Remove(this);
            //Window.GetWindow(this).conta;

            //System.Windows.Controls.UserControl.

            //this.RemoveVisualChild();

            //var window = Window.GetWindow(this);
            //window.r

            //((Window)this.Parent).Close();

            ((ContentControl)this.Parent).Content = null;

            //window.con

            //  this.c


            //window.cl

            //var window = UserControl..GetWindow(this);

            //Panel panel = default(Panel);

            //panel = (Panel)this.Parent;

            //panel.Children.RemoveAt(0);
        }
    }
}

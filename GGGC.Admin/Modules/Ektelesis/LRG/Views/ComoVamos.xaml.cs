using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GGGC.Admin.Modules.Ektelesis.LRG.Views
{
    /// <summary>
    /// Lógica de interacción para ComoVamos.xaml
    /// </summary>
    public partial class ComoVamos : UserControl
    {
        ObservableCollection<TabItemModel> tabItemsModel = new ObservableCollection<TabItemModel>();

        public ComoVamos()
        {
            InitializeComponent();
            this.CreateTabItems();
            EventManager.RegisterClassHandler(typeof(RadTabItem), RoutedEventHelper.CloseTabEvent, new RoutedEventHandler(OnCloseClicked));
        }

        public void OnCloseClicked(object sender, RoutedEventArgs e)
        {
            var tabItem = sender as RadTabItem;
            // Remove the item from the collection the control is bound to
            tabItemsModel.Remove(tabItem.DataContext as TabItemModel);
        }
        private void CreateTabItems()
        {
            // Create items:
            for (int num = 0; num < 5; num++)
            {
                tabItemsModel.Add(new TabItemModel()
                {
                    Title = String.Format("Item {0}", num),
                    Content = String.Format("Item Content {0}", num)
                });
            }
            // Attach the items:
            tabControl.ItemsSource = tabItemsModel;
        }
    }

    public class TabItemModel
    {
        public String Title
        {
            get;
            set;
        }
        public String Content
        {
            get;
            set;
        }
    }
}

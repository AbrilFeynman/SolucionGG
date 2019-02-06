using GGGC.Admin.Menus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
using Syncfusion.Windows.Tools.Controls;

namespace GGGC.Admin.UI.Docking.Views
{
    /// <summary>
    /// Interaction logic for ServerExplorer.xaml
    /// </summary>
    public partial class ServerExplorer : UserControl
    {

        private ItemsViewModel iVM;
        Telerik.Windows.Controls.RadTreeView treeView;
        public static Dictionary<string, UserControl> userControls = new Dictionary<string, UserControl>();
        private TabItemExt lastSelectedItem;
        private List<TabItemExt> _tabItems;
        private TabItemExt _tabAdd;

        public ServerExplorer()
        {
            InitializeComponent();

            iVM = new ItemsViewModel(ItemsViewModel.Menu.GG);
            tvGG.ItemsSource = iVM;
            tvGG.ExpandAll();
        }

        private void radTreeViewCatalogs_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            // Get a reference to the treeview
            treeView = sender as Telerik.Windows.Controls.RadTreeView;



            ObservableCollection<Object> selectedItems = treeView.SelectedItems;
           List<TabItem> _tabItems;
         
            Type type = this.GetType();
            Assembly assembly = type.Assembly;
            string strElement = ((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Name;
            string strTag = ((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Tag;
            RadTreeViewItem item3 = treeView.SelectedItem as RadTreeViewItem;
            switch (strTag)
            {
                           
                case "8010":

                    UserControl uc8010 = null;

                    if (userControls.ContainsKey("8010"))
                    {
                        userControls.Remove("8010");
                        uc8010 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.AR.Catalogs.Clientes.Views.MainView", type.Namespace));
                        userControls.Add("8010", uc8010);

                        //  container.Content = userControls["801"];
                    }
                    else
                    {

                        //var before = GC.GetTotalMemory(false);

                        //MessageBox.Show(before.ToString("#,###"));

                        //GC.Collect();
                        //MessageBox.Show(before.ToString("e gc" + "#,###"));



                        // userControls.Remove("801");
                        uc8010 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.AR.Catalogs.Clientes.Views.MainView", type.Namespace));
                        userControls.Add("8010", uc8010);


                        AddDetail(uc8010, "Catálogo de Clientes");

                        //container.

                        //  ((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Parent.IsSelected = true;
                        // container.Content = userControls["801"];

                        // container.ta

                        //container.Focus();

                        // ((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Tag = "800";

                        //  treeView.SelectedItem = ((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Tag = "800";

                        //selectedItems.IS                           
                        //var after = GC.GetTotalMemory(false);
                        //MessageBox.Show(after.ToString("after" + "#,###"));

                        //if (userControls["801"] == null)
                        //{
                        //    // userControls["1501"].
                        //    userControls.Remove("801");
                        //    UserControl uc801 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Guadiana.Tickets.Views.MainView", type.Namespace));
                        //    userControls.Add("801", uc801);
                        //}
                        //else
                        //{
                        //    // container.Content = userControls["1501"];
                        //    //userControls["1501"].de
                        //}
                    }
                    break;


             


                case "7004":

                    UserControl uc7004 = null;

                    if (userControls.ContainsKey("7004"))
                    {
                        userControls.Remove("7004");
                        uc7004 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Spares.Views.SparesView", type.Namespace));
                        userControls.Add("7004", uc7004);


                    }
                    else
                    {

                        uc7004 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Spares.Views.SparesView", type.Namespace));
                        userControls.Add("7004", uc7004);
                        AddDetail(uc7004, "Catalogo Refacciones");


                    }

                    break;

  


            }



        }



        public void AddDetail(UserControl detail, string name)
        {

            //    make sure the passed in arguments are good
            // Debug.Assert(detail != null, "UserControl detail is null");
            //Debug.Assert(name != null, "string name is null");

            //    locate the TabControl that the tab will be added to


            //containerOK = (TabControlExt)this.FindName("containerOK");
            
            
            
            //Debug.Assert(itemsTab != null, "can't find ItemsTab");

            // add a tabItem with + in header 
            // _tabAdd = new TabItem();
            // _tabAdd.Header = "+";
            // tabAdd.MouseLeftButtonUp += new MouseButtonEventHandler(tabAdd_MouseLeftButtonUp);

            _tabItems.Add(_tabAdd);

            //    create and populate the new tab and add it to the tab control
            TabItemExt newTab = new TabItemExt();
            newTab.Content = detail;
            newTab.Header = name;
           // containerOK.Items.Add(newTab);
            _tabItems.Add(newTab);

            //    display the new tab to the user; if this line is missing
            //    you get a blank tab
         //   containerOK.SelectedItem = newTab;

           // containerOK.Visibility = Visibility.Visible;
            //containerOK.fil


            //containerOK.Items.Refresh
        }



    }
}

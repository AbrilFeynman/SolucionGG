using GGGC.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace GGGC.Admin.ViewModels
{
    public class ShellWindowViewModel : ViewModelBase
    {
        private NavigationNode navigationTreeSelectedItem;
        private ViewModelBase currentViewModel;
        private bool isBusy;

        public ShellWindowViewModel()
        {
            this.IsBusy = true;
            this.NavigationTree = this.BuildNavigationTree();

            this.Tabs = new ObservableCollection<TabViewModel>();
            this.AddItem(null);
        }

        public ObservableCollection<TabViewModel> Tabs
        {
            get;
            private set;
        }

        public void AddItem(TabViewModel sender)
        {
            TabViewModel newTabItem = new TabViewModel(this);
            newTabItem.Header = "New Tab";
            newTabItem.IsSelected = true;
            if (sender != null)
            {
                int insertIndex = this.Tabs.IndexOf(sender) + 1;
                this.Tabs.Insert(insertIndex, newTabItem);
            }
            else
            {
                this.Tabs.Add(newTabItem);
            }
        }

        public void RemoveItem(TabViewModel tabItem)
        {
            this.Tabs.Remove(tabItem);
            tabItem.Dispose();
        }

        public IList<NavigationNode> NavigationTree { get; set; }

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return this.currentViewModel;
            }

            set
            {
                if (this.currentViewModel != value)
                {
                    this.currentViewModel = value;
                    this.OnPropertyChanged(() => this.CurrentViewModel);
                }
            }
        }



        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            set
            {
                if (this.isBusy != value)
                {
                    this.isBusy = value;
                    this.OnPropertyChanged(() => this.IsBusy);
                }
            }
        }

        public NavigationNode NavigationTreeSelectedItem
        {
            get
            {
                return this.navigationTreeSelectedItem;
            }
            set
            {
                if (this.navigationTreeSelectedItem != value
                    && value != null
                    && value.Name != "HomeInicio")
                {
                    this.navigationTreeSelectedItem = value;
                    this.OnPropertyChanged(() => this.NavigationTreeSelectedItem);

                    if (this.navigationTreeSelectedItem.ViewModel != null)
                    {
                        this.CurrentViewModel = this.navigationTreeSelectedItem.ViewModel;
                        this.ExpandParentNodes(this.navigationTreeSelectedItem.Parent);
                    }
                }
            }
        }

        private void ExpandParentNodes(NavigationNode node)
        {
            while (node != null)
            {
                node.IsExpanded = true;
                node = node.Parent;
            }
        }

        private IList<NavigationNode> BuildNavigationTree()
        {
            var tree = new List<NavigationNode>();

            //  var homeItem = new NavigationNode { Name = "Inicio", ImagePath = "/GGGC.BIG.Client;component/Images/folder.png" };
            //  var secondNode = new NavigationNode { Name = "Configuración", Parent = homeItem };
            // // secondNode.ChildNodes.Add(new NavigationNode { Name = "Individuals", ViewModel = new IndividualCustomersViewModel(), Parent = secondNode });
            //  //secondNode.ChildNodes.Add(new NavigationNode { Name = "Stores", ViewModel = new StoreCustomersViewModel(), Parent = secondNode });
            //  //homeItem.ChildNodes.Add(secondNode);
            ////  homeItem.ChildNodes.Add(new NavigationNode { Name = "Orders", ViewModel = new OrdersViewModel(), Parent = homeItem, IsSelected = true });
            //  tree.Add(homeItem);



            //Home
            var homeItem = new NavigationNode { Name = "Inicio", ImagePath = "/GGGC.BIG.Client;component/Images/folder.png" };
            var secondNode = new NavigationNode { Name = "Configuración", Parent = homeItem };
          //  secondNode.ChildNodes.Add(new NavigationNode { Name = "Individuals", ViewModel = new IndividualCustomersViewModel(), Parent = secondNode });
            //secondNode.ChildNodes.Add(new NavigationNode { Name = "Stores", ViewModel = new StoreCustomersViewModel(), Parent = secondNode });
            //homeItem.ChildNodes.Add(secondNode);
            //homeItem.ChildNodes.Add(new NavigationNode { Name = "Orders", ViewModel = new OrdersViewModel(), Parent = homeItem, IsSelected = true });
            tree.Add(homeItem);

            ////Grupo Guadiana
            //var guadianaItem = new NavigationNode { Name = "Grupo Guadiana", ImagePath = "/GGGC.BIG.Client;component/Images/folder.png" };
            //var guadianaNode = new NavigationNode { Name = "Comunicados", Parent = guadianaItem };
            //guadianaNode.ChildNodes.Add(new NavigationNode { Name = "Individuals", ViewModel = new IndividualCustomersViewModel(), Parent = guadianaNode });
            //guadianaNode.ChildNodes.Add(new NavigationNode { Name = "Stores", ViewModel = new StoreCustomersViewModel(), Parent = guadianaNode });
            //guadianaItem.ChildNodes.Add(guadianaNode);
            //guadianaItem.ChildNodes.Add(new NavigationNode { Name = "Orders", ViewModel = new OrdersViewModel(), Parent = guadianaItem, IsSelected = true });
            //tree.Add(guadianaItem);

            var salesItem = new NavigationNode { Name = "Sales", ImagePath = "/GGGC.Admin;component/Resources/Images/App/folder.png" };
            //var customersNode = new NavigationNode { Name = "Customers", Parent = salesItem };
            //customersNode.ChildNodes.Add(new NavigationNode { Name = "Individuals", ViewModel = new IndividualCustomersViewModel(), Parent = customersNode });
            //customersNode.ChildNodes.Add(new NavigationNode { Name = "Stores", ViewModel = new StoreCustomersViewModel(), Parent = customersNode });
            //salesItem.ChildNodes.Add(customersNode);
            //salesItem.ChildNodes.Add(new NavigationNode { Name = "Orders", ViewModel = new OrdersViewModel(), Parent = salesItem, IsSelected = true });
            tree.Add(salesItem);

            var production = new NavigationNode { Name = "Production", ImagePath = "/GGGC.Admin;component/Resources/Images/App/folder.png" };
            var manufactoringProcess = new NavigationNode { Name = "Manufactoring Process", Parent = production };
            //manufactoringProcess.ChildNodes.Add(new NavigationNode { Name = "Bill of Materials", ViewModel = new BillOfMaterialsViewModel(), Parent = manufactoringProcess });
            //manufactoringProcess.ChildNodes.Add(new NavigationNode { Name = "Work orders", ViewModel = new WorkOrdersViewModel(), Parent = manufactoringProcess });
            //manufactoringProcess.ChildNodes.Add(new NavigationNode { Name = "Instructions", Parent = manufactoringProcess, ViewModel = new DocumentViewModel("Instructions") });
            //production.ChildNodes.Add(manufactoringProcess);
            //production.ChildNodes.Add(new NavigationNode { Name = "Product inventory", ViewModel = new ProductInventoryViewModel(), Parent = production });
            //production.ChildNodes.Add(new NavigationNode { Name = "Documentation", Parent = production, ViewModel = new DocumentViewModel("Documentation") });
            tree.Add(production);

            var vendors = new NavigationNode { Name = "Vendors", ImagePath = "/GGGC.Admin;component/Resources/Images/App/folder.png" };
            //vendors.ChildNodes.Add(new NavigationNode { Name = "Suppliers", ViewModel = new VendorsViewModel(), Parent = vendors });
            //vendors.ChildNodes.Add(new NavigationNode { Name = "Purchases", ViewModel = new PurchaseOrdersViewModel(), Parent = vendors });
            tree.Add(vendors);

            return tree;
        }


    }


}

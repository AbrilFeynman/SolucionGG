//using GGGC.Admin.Login.Views;
using GGGC.Admin.Menus;
using GGGC.Admin.Sign.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//using Microsoft.WindowsAzure;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Table;
using Telerik.Windows.Controls;
using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools.Controls;
using System.Windows.Input;
//using Telerik.Windows.Themes.
public enum Category
{
    Amphibians,
    Bears,
    Canines,
    Spiders,
    Primates,
    BigCats
}


public enum Menu : int
{
    SALES = 5,
    EKTELESIS = 2,
    CATALOGOS = 3,
    SHOPS = 4,
    INVENTORY = 1,
    CXC = 6,
    CXP = 7,
    AUD = 8,
    SISTEMAS = 9,
    GG = 500,
    HOME = 13,
    LRG = 15,
    CLT = 16,
    MTO = 20,
    BUENFIN = 2015

}

public enum Empresas
{
    LRG,
    CLT,
    GGGC,
    AJ,
    SJ,
    IT
}

namespace GGGC.Admin
{
    public partial class Shell : MetroWindow
    {
        private University uni = new University();
        public static Dictionary<string, UserControl> userControls = new Dictionary<string, UserControl>();

        private TabItemExt lastSelectedItem;
        //Type type1 = null;
        //Assembly assembly1 = null;
        //UserControl uc1501 = (UserControl)assembly1.CreateInstance(string.Format("{0}.ERP.Modules.LRG.Databooks.Views.DatabookView", type1.Namespace));
        private bool blnCrearUserControl = false;
        private bool bln153 = true;
        private readonly IDialogService dialogService;

        private List<TabItemExt> _tabItems;
        private TabItemExt _tabAdd;

        Telerik.Windows.Controls.RadTreeView treeView;

        #region Fields

        public LoginViewModel ViewModel;
        private SampleViewModel sampleVM;
        private ItemsViewModel iVM;
        Boolean _booleanValue;
        private bool blnInside = false;
        private object _lock;

        //public int MyProperty
        //{
        //    get { return _myProperty; }
        //    set
        //    {
        //        lock (_lock)
        //        {
        //            //The property changed event will get fired whenever
        //            //the value changes. The subscriber will do work if the value is
        //            //1. This way you can keep your business logic outside of the setter
        //            if (value != _myProperty)
        //            {
        //                _myProperty = value;
        //                NotifyPropertyChanged("MyProperty");
        //            }
        //        }
        //    }
        //}


        //bool _theVariable;
        public bool TheVariable
        {
            get { return blnInside; }
            set
            {
                blnInside = value;
                if (blnInside == true)
                {
                    //Do stuff here.
                    //MessageBox("changue");
                }
            }
        }

        #endregion

        public Shell()
        {
            //  StyleManager.ApplicationTheme = new Office_SilverTheme();
            _tabItems = new List<TabItemExt>();
         
          //  containerOK.re
            this.Loaded += OnLoaded;
            InitializeComponent();
          //  containerOK.Width = 0;
            GlobalModule.GetLocalIP();
            // robMain rob;
            // uct_requiredFields.UpdateStatusBar += updateStatusBar;
            sampleVM = new SampleViewModel();
            //myTreeView.ItemsSource = sampleVM;
            //iVM = new ItemsViewModel(1);
            //tvModulos.ItemsSource = iVM;

          //  StyleManager.SetTheme(robMain, new Windows8Theme());



            iVM = new ItemsViewModel(Menu.GG);
            tvGG.ItemsSource = iVM;
            tvGG.ExpandAll();


            //iVM = new ItemsViewModel(Menu.LRG);
            //this.tvLRG.ItemsSource = iVM;
            ////tvLRG.ExpandAll();


            //iVM = new ItemsViewModel(Menu.HOME);
            //tvHome.ItemsSource = iVM;
            //tvHome.ExpandAll();

            //iVM = new ItemsViewModel(Menu.CATALOGOS);
            //tvGG.ItemsSource = iVM;
            //tvGG.ExpandAll();

            //iVM = new ItemsViewModel(Menu.EKTELESIS);
            //tvGG.ItemsSource = iVM;
            //tvGG.ExpandAll();

            //iVM = new ItemsViewModel(Menu.INVENTORY);
            //tvInventario.ItemsSource = iVM;
            //tvInventario.ExpandAll();
            ////iVM = new ItemsViewModel(Menu.SALES);
            ////tvSales.ItemsSource = iVM;

            //iVM = new ItemsViewModel(Menu.CXC);
            //tvCxC.ItemsSource = iVM;
            //tvCxC.ExpandAll();

            //iVM = new ItemsViewModel(Menu.AUD);
            //tvAuditoria.ItemsSource = iVM;

            //iVM = new ItemsViewModel(Menu.SISTEMAS);
            //tvSistemas.ItemsSource = iVM;
            //tvSistemas.ExpandAll();

            //iVM = new ItemsViewModel(Menu.SALES);
            //tvSales.ItemsSource = iVM;
            //tvSales.ExpandAll();

            //iVM = new ItemsViewModel(Menu.MTO);
            //tvMto.ItemsSource = iVM;
            //tvMto.ExpandAll();

            //iVM = new ItemsViewModel(Menu.BUENFIN);
            //tvBF.ItemsSource = iVM;
            //tvBF.ExpandAll();

            // Convert.ToInt16()
            //main.DataContext = ObjectBase.Container.GetExportedValue<MainViewModel>();
            string lastUsedAccent;
            lastUsedAccent = "Blue";
            //  MahApps.Metro.ThemeManager.ChangeTheme(this, MahApps.Metro.ThemeManager.DefaultAccents.First(x => x.Name == lastUsedAccent), MahApps.Metro.Theme.Light);


            // Infragistics.Windows.OutlookBar.Resources.Customizer.SetCustomizedString("NavigationPaneMinimizedText", "Text when minimized");
            // CrearMenus();       
            // MenuModulos(this.rbModulos);


            //ref robMain,
            this.ViewModel = new LoginViewModel( ref blnInside);
            this.DataContext = this.ViewModel;
            // CrearArbol();
            //  TreeViewGrupoGuadiana(rbiGg);
            // TreeViewSistemas(rbiSys);
            cargarMenus();

            // robMain.Width = 200;

            this.tvGG.Width = 0;
           
           
            tvGG.Visibility = Visibility.Hidden;

            //robMain.ActiveItemsCount = 0;
            prcPerfilAdmin();


         //   robMain.Width = 0;
         //this.DataContext
            //robMain.Visibility = Visibility .Hidden;
           // DisplayVersion();
            //robMain.
            //this.DialogResult=
            // this.lblFecha.Content = DateTime.Now.ToLongDateString().ToUpperInvariant();
            //sUser = ViewModel.UserName;
        }

        private void ShowModal(object sender, RoutedEventArgs e)
        {
            this.ToggleFlyout(0);
        }

        public void ShowSettings(object sender, RoutedEventArgs e)
        {
            this.ToggleFlyout(0);
        }

        public void ShowAppBar(object sender, RoutedEventArgs e)
        {
            this.ToggleFlyout(1);
        }

        public void ShowAppBar2()
        {
            this.ToggleFlyout(1);
        }



        private void ToggleFlyout(int index)
        {
            var flyout = this.Flyouts.Items[index] as Flyout;
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
        }

        private void ShowSettingsLeft(object sender, RoutedEventArgs e)
        {
            var flyout = (Flyout)this.Flyouts.Items[6];
            flyout.Position = Position.Left;
        }

        private void ShowSettingsRight(object sender, RoutedEventArgs e)
        {
            var flyout = (Flyout)this.Flyouts.Items[6];
            flyout.Position = Position.Right;
        }

        //private void ShowDynamicFlyout(object sender, RoutedEventArgs e)
        //{
        //    var flyout = new DynamicFlyout
        //    {
        //        Header = "Dynamic flyout"
        //    };

        //    // when the flyout is closed, remove it from the hosting FlyoutsControl
        //    RoutedEventHandler closingFinishedHandler = null;
        //    closingFinishedHandler = (o, args) =>
        //    {
        //        flyout.ClosingFinished -= closingFinishedHandler;
        //        flyoutsControl.Items.Remove(flyout);
        //    };
        //    flyout.ClosingFinished += closingFinishedHandler;

        //    flyoutsControl.Items.Add(flyout);

        //    flyout.IsOpen = true;
        //}
        private void cargarMenus()
        {

            //switch (strMenu)
            //{
            //    case "":
            //        break;


            //}
            Type type = this.GetType();
            Assembly assembly = type.Assembly;

            //comentar

            //UserControl uc1 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Views.InventoryView", type.Namespace));
            //userControls.Add("100", uc1);

            ////GGGC.Admin.Inventory.Views

            //UserControl mathUC = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.MathUserControl", type.Namespace));
            //userControls.Add("200", mathUC);

            //UserControl csUC6 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.ucMarcas", type.Namespace));
            //userControls.Add("300", csUC6);

            //UserControl uc9 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.Catalogs.Inventory.Tires.TireView", type.Namespace));
            //userControls.Add("900", uc9);

            //UserControl uc905 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.Catalogs.Inventory.Services.ServicesView", type.Namespace));
            //userControls.Add("905", uc905);

            //UserControl uc330 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.Catalogs.Inventory.RefaccionesView", type.Namespace));
            //userControls.Add("330", uc330);

            ////Inventory Fixed Groups
            //UserControl uc110 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Views.LinesView", type.Namespace));
            //userControls.Add("110", uc110);
            //UserControl uc120 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Views.BrandsView", type.Namespace));
            //userControls.Add("120", uc120);

            //UserControl uc111 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Services.Views.LinesView", type.Namespace));
            //userControls.Add("111", uc111);
            //UserControl uc121 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Services.Views.BrandsView", type.Namespace));
            //userControls.Add("121", uc121);

            //Inventory Variable Groups
            //UserControl uc151 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Shocks.Views.ShocksView", type.Namespace));
            //userControls.Add("151", uc151);
            //UserControl uc152 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Miscellaneous.Views.MiscView", type.Namespace));
            //userControls.Add("152", uc152);
            //UserControl uc153 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Spares.Views.SparesView", type.Namespace));
            //userControls.Add("153", uc153);

            UserControl uc151 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Batteries.Views.BatteriesView", type.Namespace));
            userControls.Add("151", uc151);

            UserControl uc152 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Shocks.Views.ShocksView", type.Namespace));
            userControls.Add("152", uc152);

            UserControl uc153 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Miscellaneous.Views.MiscView", type.Namespace));
            userControls.Add("153", uc153);

            UserControl uc154 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Spares.Views.SparesView", type.Namespace));
            userControls.Add("154", uc154);

           // assembly.

            //UserControl uc155 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Catalogs.Production.Classes.Spares.G4.Views.MainView", type.Namespace));
            //userControls.Add("155", uc155);

            ////UserControl uc153 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Inventory.Catalogs.Classes.Spares.Groups.Spares.Views.MainView", type.Namespace));
            ////userControls.Add("153", uc153);

            ////UserControl uc153 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Views.MainView", type.Namespace));
            ////userControls.Add("153", uc153);

            //UserControl uc2099 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Test.Test01.Views.MainView", type.Namespace));
            //userControls.Add("2099", uc2099);

            ////UserControl uc3000 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Test.Test02.Views.MainView", type.Namespace));
            ////userControls.Add("3000", uc3000);

            //UserControl uc3000 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Migration.Prices.Views.MainView", type.Namespace));
            //userControls.Add("3000", uc3000);

            ////GGGC.Admin.Modules.Migration.Prices

            //UserControl uc3001 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Migration.Precios.MainView", type.Namespace));
            //userControls.Add("3001", uc3001);

            //UserControl uc1983 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Catalogs.Products.Tires.Views.MainView", type.Namespace));
            //userControls.Add("1983", uc1983);

            //UserControl uc70 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Catalogs.Products.Classes.Llantas.Views.MainView", type.Namespace));
            //userControls.Add("70", uc70);

            //UserControl uc80 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Catalogs.Products.Classes.Servicios.Views.MainView", type.Namespace));
            //userControls.Add("80", uc80);

            //UserControl uc90 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Catalogs.Sales.Prices.Views.MainView", type.Namespace));
            //userControls.Add("90", uc90);

            //UserControl uc7002 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Ektelesis.LRG.Views.History.HistoryView", type.Namespace));
            //userControls.Add("7002", uc7002);

           // AppDomain.Unload(this);

            //UserControl uc931 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Mobile.Views.MainView", type.Namespace));
            //userControls.Add("931", uc931);

            //UserControl uc352 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Mobile.Rines.Views.MainView", type.Namespace));
            //userControls.Add("352", uc352);

            //UserControl uc850 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Mobile.Compras.Views.MainView", type.Namespace));
            //userControls.Add("850", uc850);

            //descomentar

            //UserControl csUC = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.ucServicios", type.Namespace));
            //UserControl csUC2 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.ucRef", type.Namespace));
            //UserControl csUC3 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.ucRines", type.Namespace));
            //UserControl csUC4 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.ucArt", type.Namespace));
            //UserControl csUC5 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.ucGrupos", type.Namespace));
            //UserControl uc2 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.viewRequisiciones", type.Namespace));
            //userControls.Add("260", uc2);

            //UserControl uc3 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.RequisicionesView", type.Namespace));
            //userControls.Add("280", uc3);

            //ERP.MODULES.Inventario

            //UserControl uc5099 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Inventory.Purchases.Views.MainView", type.Namespace));
            //userControls.Add("5099", uc5099);
            //UserControl uc5100 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Inventory.PurchasesMichelin.Views.MainView", type.Namespace));
            //userControls.Add("5100", uc5100);

            //ajustes
            //UserControl uc301 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Inventory.Adjustments.Views.MainView", type.Namespace));
            //userControls.Add("301", uc301);

            ////Transferencias
            //UserControl uc401 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Inventory.Transferences.Views.MainView", type.Namespace));
            //userControls.Add("401", uc401);

            ////Dev
            //UserControl uc6400 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Inventory.Devolutions.Views.MainView", type.Namespace));
            //userControls.Add("6400", uc6400);
            //UserControl uc6401 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Inventory.DevolutionsOtherIncome.Views.MainView", type.Namespace));
            //userControls.Add("6401", uc6401);

            ////Fact
            //UserControl uc6500 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Inventory.Invoices.Views.MainView", type.Namespace));
            //userControls.Add("6500", uc6500);
            //UserControl uc6501 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Inventory.InvoicesOtherIncome.Views.MainView", type.Namespace));
            //userControls.Add("6501", uc6501);
            //UserControl uc680 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Sales.Cotizaciones.Views.CotizacionView", type.Namespace));
            //userControls.Add("680", uc680);
            //UserControl uc682 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Sales.Pedidos.Views.PedidoView", type.Namespace));
            //userControls.Add("682", uc682);
            //UserControl uc684 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Sales.Ordenes.Views.OrdenView", type.Namespace));
            //userControls.Add("684", uc684);

            //UserControl uc686 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Sales.Existencias.Views.ExistenciaView", type.Namespace));
            //userControls.Add("686", uc686);

            //UserControl uc686 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.CxC.Existencias.Views.ExistenciaView", type.Namespace));
            //userControls.Add("686", uc686);
            UserControl uccxc600 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Ektelesis.CXC.Views.Comisiones.ComisionesView", type.Namespace));
            userControls.Add("600", uccxc600);

            UserControl uccxc601 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Ektelesis.CXC.Views.VentasNotas.VentasNotasView", type.Namespace));
            userControls.Add("601", uccxc601);

            UserControl uccxc602 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Ektelesis.CXC.Views.Comisiones.ComisionesView", type.Namespace));
            userControls.Add("602", uccxc602);

            //UserControl uccxc41 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Ektelesis.AUD.Views.Cancelaciones.CancelacionesView", type.Namespace));
            //userControls.Add("41", uccxc41);           
            //UserControl uc201 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Designs.CambiarPassword", type.Namespace));
            //userControls.Add("201", uc201);           
            //UserControl uc204 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Designs.SucursalesLRG", type.Namespace));
            //userControls.Add("204", uc204);
            //UserControl uc206 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Designs.Correos", type.Namespace));
            // userControls.Add("206", uc206);           
            //UserControl uc801 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Guadiana.Tickets.Views.MainView", type.Namespace));
            //userControls.Add("801", uc801);
            //UserControl uc212 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Designs.TicketsAdmin", type.Namespace));
            //userControls.Add("212", uc212);
            //UserControl uc7001 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Ektelesis.LRG.Views.ComoVamosLRG", type.Namespace));
            //userControls.Add("8000", uc7001);
            //UserControl uc1501 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.LRG.Databooks.Views.DatabookView", type.Namespace));
            //userControls.Add("1501", uc1501);
            //ERP.Modules.LRG.Databooks.Views.DatabookView
        }


        private void prcPerfil(string perfilID)
        {
            switch (perfilID)
            {
                case "0":
                    prcPerfilSucursal();
                    break;
                case "1":
                    prcPerfilAdmin();
                    break;
                case "4":
                    prcPerfilCatalogos();
                    break;

                case "5":
                    prcPerfilCredito();
                    break;

                default:
                    prcPerfilSucursal();
                    break;
            }

            //robMain.Width = 203;



            tvGG.Width = 180;
            //containerOK.Width = 5;
            //containerOK.Visibility = Visibility.Visible;
            tvGG.Visibility = Visibility.Visible;

            this.dpMain.Visibility = Visibility.Visible;
           // this.dpMain.Width = 250;
            //1 ADMIN
            //2 CONSEJO
            //3 ADMINISTRATIVO
            //4 CATALOGOS
            //5 CREDITO
        }

        private void prcPerfilbefore(int userID)
        {
            switch (userID)
            {
                case 89461001:
                    prcPerfilSucursal();
                    break;
                case 578100907:
                   // prcPerfilAdministrativo();
                    break;

                case 834141027:
                   // prcPerfilAdministrativo();
                    break;

                case 740130314:
                   // prcPerfilAdministrativo();
                    break;

                default:
                    //   prcPerfilSucursal();
                    break;
            }
        }

        private void prcPerfilSucursal()
        {
            //this.rbiDH.Visibility = Visibility.Hidden;
            //this.rbiCXC.Visibility = Visibility.Hidden;
            //this.rbiConta.Visibility = Visibility.Hidden;
            //this.rbiComer.Visibility = Visibility.Hidden;
            //this.rbiAuditoria.Visibility = Visibility.Hidden;
            //this.rbiCat.Visibility = Visibility.Hidden;
            //this.rbiMantenimiento.Visibility = Visibility.Hidden;
            //this.rbiPatrimonio.Visibility = Visibility.Hidden;
            //this.rbiProcesos.Visibility = Visibility.Hidden;
            //this.rbiSales.Visibility = Visibility.Hidden;
            //this.rbiSistemas.Visibility = Visibility.Hidden;
            //this.rbiCompras.Visibility = Visibility.Hidden;
            //this.rbiInventario.Visibility = Visibility.Hidden;

            //this.rbiDH.Visibility = Visibility.Hidden;
            //this.rbiCXC.Visibility = Visibility.Hidden;
            //this.rbiConta.Visibility = Visibility.Hidden;
            //this.rbiComer.Visibility = Visibility.Hidden;
            //this.rbiAuditoria.Visibility = Visibility.Hidden;
            //this.rbiCat.IsEnabled = false;
            //this.rbiMantenimiento.Visibility = Visibility.Hidden;
            //this.rbiPatrimonio.Visibility = Visibility.Hidden;
            //this.rbiProcesos.Visibility = Visibility.Hidden;
            //this.rbiSales.IsEnabled = false;
            //this.rbiSistemas.Visibility = Visibility.Hidden;
            //this.rbiCompras.IsEnabled = false;
            //this.rbiInventario.IsEnabled = false;
            //this.rbiSucursales.Visibility = Visibility.Hidden;

            //this.robMain.ActiveItemsCount = 9;

         



        }

        private void prcPerfilCatalogos()
        {
            //this.rbiDH.Visibility = Visibility.Hidden;
            //this.rbiCXC.Visibility = Visibility.Hidden;
            //this.rbiConta.Visibility = Visibility.Hidden;
            //this.rbiComer.Visibility = Visibility.Hidden;
            //this.rbiAuditoria.Visibility = Visibility.Hidden;
            //this.rbiCat.Visibility = Visibility.Hidden;
            //this.rbiMantenimiento.Visibility = Visibility.Hidden;
            //this.rbiPatrimonio.Visibility = Visibility.Hidden;
            //this.rbiProcesos.Visibility = Visibility.Hidden;
            //this.rbiSales.Visibility = Visibility.Hidden;
            //this.rbiSistemas.Visibility = Visibility.Hidden;
            //this.rbiCompras.Visibility = Visibility.Hidden;
            //this.rbiInventario.Visibility = Visibility.Hidden;

            //this.rbiDH.Visibility = Visibility.Hidden;
            //this.rbiCXC.Visibility = Visibility.Hidden;
            //this.rbiConta.Visibility = Visibility.Hidden;
            //this.rbiComer.Visibility = Visibility.Hidden;
            //this.rbiAuditoria.Visibility = Visibility.Hidden;
            //this.rbiBF.Visibility = Visibility.Hidden;
            //this.rbiCat.IsEnabled = true;
            //this.rbiMantenimiento.Visibility = Visibility.Hidden;
            //this.rbiPatrimonio.Visibility = Visibility.Hidden;
            //this.rbiProcesos.Visibility = Visibility.Hidden;
            //this.rbiSales.IsEnabled = false;
            //this.rbiSistemas.Visibility = Visibility.Hidden;
            //this.rbiCompras.IsEnabled = false;
            //this.rbiInventario.IsEnabled = false;
            //this.rbiSucursales.Visibility = Visibility.Hidden;
            //this.robMain.ActiveItemsCount = 9;
        }

        private void prcPerfilCredito()
        {
            //this.rbiDH.Visibility = Visibility.Hidden;
            //this.rbiCXC.Visibility = Visibility.Hidden;
            //this.rbiConta.Visibility = Visibility.Hidden;
            //this.rbiComer.Visibility = Visibility.Hidden;
            //this.rbiAuditoria.Visibility = Visibility.Hidden;
            //this.rbiCat.Visibility = Visibility.Hidden;
            //this.rbiMantenimiento.Visibility = Visibility.Hidden;
            //this.rbiPatrimonio.Visibility = Visibility.Hidden;
            //this.rbiProcesos.Visibility = Visibility.Hidden;
            //this.rbiSales.Visibility = Visibility.Hidden;
            //this.rbiSistemas.Visibility = Visibility.Hidden;
            //this.rbiCompras.Visibility = Visibility.Hidden;
            //this.rbiInventario.Visibility = Visibility.Hidden;
            //this.rbiDH.Visibility = Visibility.Hidden;
            //this.rbiCXC.Visibility = Visibility.Visible;
            //this.rbiConta.Visibility = Visibility.Hidden;
            //this.rbiComer.Visibility = Visibility.Hidden;
            //this.rbiAuditoria.Visibility = Visibility.Hidden;
            //this.rbiCat.Visibility = Visibility.Hidden;
            //this.rbiGg.IsEnabled = false;
            //this.rbiMantenimiento.Visibility = Visibility.Hidden;
            //this.rbiPatrimonio.Visibility = Visibility.Hidden;
            //this.rbiProcesos.Visibility = Visibility.Hidden;
            //this.rbiSales.IsEnabled = false;
            //this.rbiSistemas.Visibility = Visibility.Hidden;
            //this.rbiCompras.Visibility= Visibility.Hidden;
            //this.rbiInventario.Visibility = Visibility.Hidden;
            //this.rbiSucursales.Visibility = Visibility.Hidden;
            //this.robMain.ActiveItemsCount = 10;

            //this.rbiCXC.IsEnabled = true;

            //this.rbiBF.Visibility = Visibility.Hidden;
        }

        private void prcPerfilAdmin()
        {
            //this.rbiDH.Visibility = Visibility.Visible;
            //this.rbiCXC.Visibility = Visibility.Visible;
            //this.rbiConta.Visibility = Visibility.Visible;
            //this.rbiComer.Visibility = Visibility.Visible;
            //this.rbiAuditoria.Visibility = Visibility.Visible;
            //this.rbiCat.Visibility = Visibility.Visible;
            //this.rbiMantenimiento.Visibility = Visibility.Visible;
            //this.rbiPatrimonio.Visibility = Visibility.Visible;
            //this.rbiProcesos.Visibility = Visibility.Visible;
            //this.rbiSales.Visibility = Visibility.Visible;
            //this.rbiSistemas.Visibility = Visibility.Visible;
            //this.rbiCompras.Visibility = Visibility.Visible;
            //this.rbiInventario.Visibility = Visibility.Visible;

            //this.rbiDH.IsEnabled = true;
            //this.rbiCXC.IsEnabled = true;
            //this.rbiConta.IsEnabled = true;
            //this.rbiComer.IsEnabled = true;
            //this.rbiAuditoria.IsEnabled = true;
            //this.rbiCat.IsEnabled = true;
            //this.rbiMantenimiento.IsEnabled = true;
            //this.rbiPatrimonio.IsEnabled = true;
            //this.rbiProcesos.IsEnabled = true;
            //this.rbiSales.IsEnabled = true;
            //this.rbiSistemas.IsEnabled = true;
            //this.rbiCompras.IsEnabled = true;
            //this.rbiInventario.IsEnabled = true;
            //this.rbiSucursales.IsEnabled = true;

            //this.robMain.ActiveItemsCount = 25;

        }

        private void radTreeViewCatalogs_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            // Get a reference to the treeview
            // Telerik.Windows.Controls.RadTreeView treeView = sender as Telerik.Windows.Controls.RadTreeView;

            treeView = sender as Telerik.Windows.Controls.RadTreeView;



            ObservableCollection<Object> selectedItems = treeView.SelectedItems;
           // selectedItems[0].
            RadTreeViewItem item = selectedItems[ 0 ] as RadTreeViewItem;
            RadTreeViewItem item2 = treeView.SelectedItems[0] as RadTreeViewItem;
            //  RadTreeViewItem item = selectedItems[0] as RadTreeViewItem;

            List<TabItem> _tabItems;
            TabItem _tabAdd;

            //// initialize tabItem array
            //_tabItems = new List<TabItem>();

            //// add a tabItem with + in header 
            //_tabAdd = new TabItem();
            //_tabAdd.Header = "+";
            //// tabAdd.MouseLeftButtonUp += new MouseButtonEventHandler(tabAdd_MouseLeftButtonUp);

            //_tabItems.Add(_tabAdd);

            //// add first tab
            //this.AddTabItem();

            //// bind tab control
            //tabDynamic.DataContext = _tabItems;

            //tabDynamic.SelectedIndex = 0;




            // Get the previous item and the previous sibling item
            //RadTreeViewItem previousItem = item.PreviousItem;
            //RadTreeViewItem previousSiblingItem = item.PreviousSiblingItem;
            //RadTreeViewItem selectedItem = e.OriginalSource as RadTreeViewItem;
            //RadTreeViewItem selectedItem2 = selectedItems[0] as RadTreeViewItem;
            //RadTreeViewItem item = this.radContextMenu.GetClickedElement<RadTreeViewItem>();
            //if (item != null)
            //{
            //    this.radTreeView.SelectedItem = item;
            //}
            Type type = this.GetType();
            Assembly assembly = type.Assembly;
            // Get the previous item and the previous sibling item
            //RadTreeViewItem previousItem = item.PreviousItem;
            //RadTreeViewItem previousSiblingItem = item.PreviousSiblingItem;
            // Get the currently selected items
            //ObservableCollection<Object> selectedItems = treeView.SelectedItems;
            //RadTreeViewItem item = selectedItems[0] as RadTreeViewItem;
            string strElement = ((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Name;
            string strTag = ((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Tag;
            RadTreeViewItem item3 = treeView.SelectedItem as RadTreeViewItem;
            switch (strTag)
            {
                //case "110": container.Content = userControls["110"]; break;
                //case "120": container.Content = userControls["120"]; break;
                //case "111": container.Content = userControls["111"]; break;
                //case "121": container.Content = userControls["121"]; break;
                //case "151": container.Content = userControls["151"]; break;
                //case "152": container.Content = userControls["152"]; break;
                //case "154": container.Content = userControls["154"]; break;
                //case "155": container.Content = userControls["155"]; break;
                //case "153": container.Content = userControls["153"]; break;
                ////fast
                //case "201": container.Content = userControls["201"]; break;
                //case "204": container.Content = userControls["204"]; break;
                //case "206": container.Content = userControls["206"]; break;
                ////case "801": container.Content = userControls["801"]; break;
                //case "212": container.Content = userControls["212"]; break;
                //break;





                //if (bln153)
                //{
                //    UserControl uc153 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Spares.Views.SparesView", type.Namespace));
                //    userControls.Add("153", uc153); 
                //}
                //else
                //{
                //}
                //blnCrearUserControl = false;





                //case "2099": container.Content = userControls["2099"]; break;
                //case "3000": container.Content = userControls["3000"]; break;
                //case "3001": container.Content = userControls["3001"]; break;

                //case "8000": container.Content = userControls["8000"]; break;
                ////case "8010": container.Content = userControls["8010"]; break;
                //case "8020": container.Content = userControls["8020"]; break;
                //case "7002": container.Content = userControls["7002"]; break;

                //case "1983": container.Content = userControls["1983"]; break;
                //case "70": container.Content = userControls["70"]; break;
                //case "80": container.Content = userControls["80"]; break;
                //case "90": container.Content = userControls["90"]; break;
                //case "931": container.Content = userControls["931"]; break;
                //case "352": container.Content = userControls["352"]; break;
                //case "850": container.Content = userControls["850"]; break;
                ////ERP.MODULES
                ////Inventario

                //case "301": container.Content = userControls["301"]; break; //Ajustes
                //case "401": container.Content = userControls["401"]; break; //Transferencias
                //case "5099": container.Content = userControls["5099"]; break; //Transferencias
                //case "5100": container.Content = userControls["5100"]; break; //Transferencias

                //case "6400": container.Content = userControls["6400"]; break; //Transferencias
                //case "6401": container.Content = userControls["6401"]; break; //Transferencias
                //case "6500": container.Content = userControls["6500"]; break; //Transferencias
                //case "6501": container.Content = userControls["6501"]; break; //Transferencias

                //case "680": container.Content = userControls["680"]; break;
                //case "682": container.Content = userControls["682"]; break;
                //case "684": container.Content = userControls["684"]; break;
                //case "686": container.Content = userControls["686"]; break;
                ////Credito
                //case "600": container.Content = userControls["600"]; break;
                //case "601": container.Content = userControls["601"]; break;
                //case "602": container.Content = userControls["602"]; break;

                ////auditoria
                //case "41": container.Content = userControls["41"]; break;
                ////lrg
                

                case "1501":

                    if (userControls.ContainsKey("1501"))
                    {
                        userControls.Remove("1501");
                    }
                    else
                    {

                        UserControl uc1501 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.LRG.Databooks.Views.DatabookView", type.Namespace));
                        userControls.Add("1501", uc1501);
                       // container.Content = userControls["1501"];

                    }

                    //userControls.TryGetValue("1501", out ival);

                    //userControls.ContainsKey("1501");


                    //                    var item;
                    //if(!userControls.TryGetValue("1501", out item))
                    //    return null;
                    //return item;

                    //userControls.ContainsKey

                    //if (userControls["1501"] == null)
                    //{
                    //    // userControls["1501"].
                    //    userControls.Remove("1501");
                    //    UserControl uc1501 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.LRG.Databooks.Views.DatabookView", type.Namespace));
                    //    userControls.Add("1501", uc1501);
                    //}
                    //else
                    //{
                    //    // container.Content = userControls["1501"];
                    //    //userControls["1501"].de

                    //}

                  

                    break;


                case "1502":

                    UserControl uc1502 = null;

                    if (userControls.ContainsKey("1502"))
                    {
                        userControls.Remove("1502");
                        uc1502 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.LRG.Mkt.Views.MainView", type.Namespace));
                        userControls.Add("1502", uc1502);

                      //  container.Content = userControls["1502"];
                    }
                    else
                    {

                        //var before = GC.GetTotalMemory(false);

                        //MessageBox.Show(before.ToString("#,###"));

                        //GC.Collect();
                        //MessageBox.Show(before.ToString("e gc" + "#,###"));



                        // userControls.Remove("801");
                        uc1502 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.LRG.Mkt.Views.MainView", type.Namespace));
                        userControls.Add("1502", uc1502);


                        //  ((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Parent.IsSelected = true;
                        //container.Content = userControls["1502"];
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


                case "801":

                    UserControl uc801 = null;

                    if (userControls.ContainsKey("801"))
                    {
                        userControls.Remove("801");
                        uc801 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Guadiana.Tickets.Views.MainView", type.Namespace));
                        userControls.Add("801", uc801);

                      //  container.Content = userControls["801"];
                    }
                    else
                    {

                        //var before = GC.GetTotalMemory(false);

                        //MessageBox.Show(before.ToString("#,###"));

                        //GC.Collect();
                        //MessageBox.Show(before.ToString("e gc" + "#,###"));

                       

                       // userControls.Remove("801");
                        uc801 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Guadiana.Tickets.Views.MainView", type.Namespace));
                        userControls.Add("801", uc801);


                        AddDetail(uc801, "Tickets");

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


                case "201":
                    UserControl uc20 = null;
                    if (userControls.ContainsKey("201"))
                    {
                        userControls.Remove("201");
                        uc20 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.B20.SO.Views.MainView", type.Namespace));
                        userControls.Add("201", uc20);
                        AddDetail(uc20, "ÓRDENES DE SERVICIO");
                    }
                    else
                    {
                        uc20 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.B20.SO.Views.MainView", type.Namespace));
                        userControls.Add("201", uc20);                      
                        AddDetail(uc20, "ÓRDENES DE SERVICIO");                      
                   }
                    break;

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


                case "7002":

                    UserControl uc7002 = null;

                    if (userControls.ContainsKey("7002"))
                    {
                        userControls.Remove("7002");
                        uc7002 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Ektelesis.LRG.Views.History.HistoryView", type.Namespace));
                        userControls.Add("7002", uc7002);

                        //  container.Content = userControls["801"];
                    }
                    else
                    {
                        //var before = GC.GetTotalMemory(false);
                        //MessageBox.Show(before.ToString("#,###"));
                        //GC.Collect();
                        //MessageBox.Show(before.ToString("e gc" + "#,###"));
                        // userControls.Remove("801");
                        uc7002 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Ektelesis.LRG.Views.History.HistoryView", type.Namespace));
                        userControls.Add("7002", uc7002);
                        AddDetail(uc7002, "Historial");
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

                case "7005":

                    UserControl uc151 = null;

                    if (userControls.ContainsKey("7005"))
                    {
                        userControls.Remove("7005");
                        uc151 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Batteries.Views.BatteriesView", type.Namespace));
                        userControls.Add("7005", uc151);


                    }
                    else
                    {

                        uc151 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Batteries.Views.BatteriesView", type.Namespace));
                        userControls.Add("7005", uc151);
                        AddDetail(uc151, "Catalogo Acumuladores");


                    }

                    break;

                case "7006":

                    UserControl uc152 = null;

                    if (userControls.ContainsKey("7006"))
                    {
                        userControls.Remove("7006");
                        uc152 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Shocks.Views.ShocksView", type.Namespace));
                        userControls.Add("7006", uc152);


                    }
                    else
                    {

                        uc152 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Shocks.Views.ShocksView", type.Namespace));
                        userControls.Add("7006", uc152);
                        AddDetail(uc152, "Catalogo Amortiguadores");


                    }

                    break;

                case "7007":

                    UserControl uc153 = null;

                    if (userControls.ContainsKey("7007"))
                    {
                        userControls.Remove("7007");
                        uc153 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Miscellaneous.Views.MiscView", type.Namespace));
                        userControls.Add("7007", uc153);


                    }
                    else
                    {

                        uc153 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Miscellaneous.Views.MiscView", type.Namespace));
                        userControls.Add("7007", uc153);
                        AddDetail(uc153, "Catalogo de Miscelaneos");


                    }

                    break;

                case "7008":

                    UserControl uc154 = null;

                    if (userControls.ContainsKey("7008"))
                    {
                        userControls.Remove("7008");
                        uc154 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Spares.Views.SparesView", type.Namespace));
                        userControls.Add("7008", uc154);


                    }
                    else
                    {

                        uc154 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Spares.Views.SparesView", type.Namespace));
                        userControls.Add("7008", uc154);
                        AddDetail(uc154, "Catalogo Refacciones");

                    }
                    break;
                case "0102":
                    //UserControl uc7004 = null;
                    //if (userControls.ContainsKey("7004"))
                    //{
                    //    userControls.Remove("7004");
                    //    uc7004 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Spares.Views.SparesView", type.Namespace));
                    //    userControls.Add("7004", uc7004);
                    //}
                    //else
                    //{
                    //    uc7004 = (UserControl)assembly.CreateInstance(string.Format("{0}.Inventory.Catalogs.Spares.Groups.Spares.Views.SparesView", type.Namespace));
                    //    userControls.Add("7004", uc7004);
                    //    AddDetail(uc7004, "Catalogo Refacciones");
                    //}
                    //break;
                case "802":
                    UserControl uc802 = null;
                    if (userControls.ContainsKey("802"))
                    {
                        userControls.Remove("802");
                        uc801 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.CXC.Tickets.Views.MainView", type.Namespace));
                        userControls.Add("802", uc802);

                        //  container.Content = userControls["801"];
                    }
                    else
                    {
                        //var before = GC.GetTotalMemory(false);
                        //MessageBox.Show(before.ToString("#,###"));
                        //GC.Collect();
                        //MessageBox.Show(before.ToString("e gc" + "#,###"));
                        // userControls.Remove("801");
                        uc802 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.CXC.Tickets.Views.MainView", type.Namespace));
                        userControls.Add("802", uc802);
                        AddDetail(uc802, "Tickets");
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

                case "805":

                    UserControl uc805 = null;

                    if (userControls.ContainsKey("805"))
                    {
                        userControls.Remove("805");
                        uc805 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Guadiana.Invoice.Views.MainView", type.Namespace));
                        userControls.Add("805", uc805);

                       // container.Content = userControls["805"];
                    }
                    else
                    {
                        //var before = GC.GetTotalMemory(false);
                        //MessageBox.Show(before.ToString("#,###"));
                        //GC.Collect();
                        //MessageBox.Show(before.ToString("e gc" + "#,###"));
                        //userControls.Remove("801");
                        uc805 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Guadiana.Invoice.Views.MainView", type.Namespace));
                        userControls.Add("805", uc805);
                        //((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Parent.IsSelected = true;


                        AddDetail(uc805, "uc 805");

                        // create new tab item
                        //TabItem tab = new TabItem();
                        //tab.Header = "hol";
                        //tab.Name = "equis";
                        //container.DataContext = tab;

                        //container.Content = userControls["805"];



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



                case "7003":

                    UserControl uc7003 = null;

                    if (userControls.ContainsKey("7003"))
                    {
                        userControls.Remove("7003");
                        uc7003 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Ektelesis.CXC.Views.Recibos.RecibosView", type.Namespace));
                        userControls.Add("7003", uc7003);

                        //container.Content = userControls["7003"];
                    }
                    else
                    {
                        //var before = GC.GetTotalMemory(false);
                        //MessageBox.Show(before.ToString("#,###"));
                        //GC.Collect();
                        //MessageBox.Show(before.ToString("e gc" + "#,###"));
                        //userControls.Remove("801");
                        uc7003 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Ektelesis.CXC.Views.Recibos.RecibosView", type.Namespace));
                        userControls.Add("7003", uc7003);
                        //((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Parent.IsSelected = true;


                        //container.Content = userControls["7003"];


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

                case "951":

                    UserControl uc951 = null;

                    if (userControls.ContainsKey("951"))
                    {
                        userControls.Remove("951");
                        uc951 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.MTE.Garage.Views.MainView", type.Namespace));
                        userControls.Add("951", uc951);

                      //  container.Content = userControls["951"];
                    }
                    else
                    {

                        uc951 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.MTE.Garage.Views.MainView", type.Namespace));
                        userControls.Add("951", uc951);

                       // container.Content = userControls["951"];
                        //container.Focus();

                    }

                    break;


                case "1951":

                    UserControl uc1951 = null;

                    if (userControls.ContainsKey("1951"))
                    {
                        //GGGC.Admin.Modules.Ektelesis.BUENFIN.Vales.ValesView
                        userControls.Remove("1951");
                        uc1951 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Ektelesis.BUENFIN.Vales.ValesView", type.Namespace));
                        userControls.Add("1951", uc1951);

                       // container.Content = userControls["1951"];
                    }
                    else
                    {

                        uc1951 = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Ektelesis.BUENFIN.Vales.ValesView", type.Namespace));
                        userControls.Add("1951", uc1951);

                      //  container.Content = userControls["1951"];
                        //container.Focus();

                    }

                    break;


                //case "8010":

                //    UserControl uc603 = null;

                //    if (userControls.ContainsKey("8010"))
                //    {
                //        //GGGC.Admin.Modules.Ektelesis.BUENFIN.Vales.ValesView
                //        userControls.Remove("8010");
                //        uc603 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.AR.Catalogs.Clientes.Views.MainView", type.Namespace));
                //        userControls.Add("8010", uc603);

                //       // container.Content = userControls["8010"];
                //    }
                //    else
                //    {

                //        uc603 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.AR.Catalogs.Clientes.Views.MainView", type.Namespace));
                //        userControls.Add("8010", uc603);

                //      //  container.Content = userControls["8010"];
                //        //container.Focus();

                //    }

                //    break;



            }


            //if (strelemento == "Grupos")
            //{
            //   container.Content = userControls["100"];

            //}


            //if (strelemento == "Lineas")
            //{
            //    container.Content = userControls["200"];

            //}


            //if (strelemento == "Marcas")
            //{
            //    container.Content = userControls["300"];

            //}


            //if (strelemento == "Llantas")
            //{
            //    container.Content = userControls["900"];

            //}

            //if (strelemento == "Servicios")
            //{
            //    container.Content = userControls["905"];
            //}

            //if (strelemento == "Refacciones")
            //{
            //    container.Content = userControls["330"];
            //}
            //  container.Content = userControls[subject.ID];

            //var newArgs = new System.Windows.Controls.SelectionChangedEventArgs(e);
            // Get a reference to the treeview
            //System.Windows.RoutedEventArgs

            // treeView.SelectedContainer
            // Telerik.Windows.Controls.RadTreeView treeView = sender as Telerik.Windows.Controls.RadTreeView;
            //// Get the currently selected items
            //ObservableCollection<Object> selectedItems = treeView.SelectedItems;
            //RadTreeViewItem item = selectedItems[0] as RadTreeViewItem;


            //BusinessItem item = this.sampleVM.GetItemByName(this.textBox.Text);
            //if (item != null)
            //{
            //    item.IsSelected = true;
            //    string path = item.GetPath();
            //    myTreeView.BringPathIntoView(path);
            //}


            //object item = treeView.SelectedItem;
            //if (item != null)
            //{
            //    RadTreeViewItem tvi = treeView.ContainerFromItemRecursive(treeView.SelectedItem);
            //    if (tvi != null)
            //    {
            //        // here we can deal with the tree-view item 
            //        // ... 
            //        tvi.IsSelected = true;
            //        //string path = item.GetPath();
            //        //myTreeView.BringPathIntoView(path);
            //    }
            //} 

        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string tabName = (sender as Button).CommandParameter.ToString();

            var item = containerOK.Items.Cast<TabItem>().Where(i => i.Name.Equals(tabName)).SingleOrDefault();

            TabItemExt tab = item as TabItemExt;

            if (tab != null)
            {
                if (_tabItems.Count < 3)
                {
                    MessageBox.Show("Cannot remove last tab.");
                }
                else if (MessageBox.Show(string.Format("Are you sure you want to remove the tab '{0}'?", tab.Header.ToString()),
                    "Remove Tab", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // get selected tab
                    TabItemExt selectedTab = containerOK.SelectedItem as TabItemExt;

                    // clear tab control binding
                    containerOK.DataContext = null;

                    //containerOK.Remove(tab);
                    _tabItems.Remove(tab);


                    // bind tab control
                    containerOK.DataContext = _tabItems;

                    // select previously selected tab. if that is removed then select first tab
                    if (selectedTab == null || selectedTab.Equals(tab))
                    {
                        selectedTab = _tabItems[0];
                    }
                    containerOK.SelectedItem = selectedTab;
                }
            }
        }


        public void AddDetail(UserControl detail, string name)
        {

            //    make sure the passed in arguments are good
            Debug.Assert(detail != null, "UserControl detail is null");
            Debug.Assert(name != null, "string name is null");

            //    locate the TabControl that the tab will be added to
           containerOK  = (TabControlExt)this.FindName("containerOK");
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
            containerOK.Items.Add(newTab);
            _tabItems.Add(newTab);

            //    display the new tab to the user; if this line is missing
            //    you get a blank tab
            containerOK.SelectedItem = newTab;

            containerOK.Visibility = Visibility.Visible;
            //containerOK.fil


            //containerOK.Items.Refresh
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            new DialogService(this);

            //LayoutRoot
            LoadImage();
        }

        private void LoadImage()
        {
            try
            {
                LayoutRoot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/App/Backgrounds/BG5.jpg")));
            }
            catch (Exception ex)
            {
                throw new Exception("" + ex.ToString());
            }


            //BitmapImage src = new BitmapImage();
            //src.BeginInit();
            //src.UriSource = new Uri(@"Resources/Images/App/Backgrounds/BG1.jpg", UriKind.Relative);
            //src.CacheOption = BitmapCacheOption.OnLoad;
            //src.EndInit();
            //Uri(@"C:\Images\original.jpg")));



            //pack://application:,,,/Resources/Australia.png"
            //preview.Source = src;
            //preview.Stretch = Stretch.Uniform;
        }

        private void DisplayVersion()
        {
            var version = string.Format(" {0}", ((System.Reflection.AssemblyFileVersionAttribute)
            (System.Reflection.Assembly.GetExecutingAssembly().
            GetCustomAttributes(typeof(System.Reflection.AssemblyFileVersionAttribute),
            false)[0])).Version);

          // var a =  Application.ResourceAssembly.GetCustomAttributes;

            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            string About = string.Format(CultureInfo.InvariantCulture, @"YourApp Version {0}.{1}.{2} (r{3})", v.Major, v.Minor, v.Build, v.Revision);

    //        var ver = Windows.ApplicationModel.Package.Current.Id.Version;

    //        var attribute = (AssemblyVersionAttribute)Assembly
    //.GetExecutingAssembly()
    //.GetCustomAttributes(typeof(AssemblyVersionAttribute), true)
    //.Single();
    //        var a =  attribute.Version;


            //System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            //string version2 = fvi.FileVersion;

            //System.Deployment.Application.ApplicationDeployment ad =
            //     System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
            //version = string.Format("gggc: {0}", ad.CurrentVersion.ToString());

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                System.Deployment.Application.ApplicationDeployment ad =
                System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                version = string.Format(" {0}", ad.CurrentVersion.ToString());
            }
           // Title = "BiG - Grupo Guadiana GC ";         

            string strWeek = DateTime.Now.DayOfWeek.ToString();

            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            int days = ciCurr.Calendar.GetDayOfYear(DateTime.Now);
            int leftDays = 0;
            if (ciCurr.Calendar.IsLeapDay(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
            {
                leftDays =366-days;
            }
            else
            {
                 leftDays =365-days;
            }
            //return weekNum;

            this.sbiUser.Content = "Usuario: " + GlobalModule.ActualUserName;
            this.sbiUserName.Content = GlobalModule.ActualName;
            this.sbiDate.Content = "" + DateTime.Now.ToLongDateString();
            this.sbiDays.Content = "Semana: " + weekNum.ToString() + "| Dias Transcurridos: " + days.ToString() + " | Quedan: " + leftDays.ToString();
            this.sbiVersion.Content = "Versión: " + version;
            
            this.sbiIP.Content = "" + GlobalModule.GetLocalIP();

          //  lblVersion.Content = "versión: " + version;
        }


        private void updateStatusBar(string message)
        {
            //sbGG.ba. = message;
        }

        private void MenuModulos(RadOutlookBarItem r)
        {
            RadTreeView radTreeView = new RadTreeView();
            RadTreeViewItem category = new RadTreeViewItem();
            category.Header = "Modulos";
            // category.Foreground = new SolidColorBrush(Colors.Green);
            radTreeView.Items.Add(category);
            // Adding child items
            RadTreeViewItem product = new RadTreeViewItem();
            product.Header = "1. Cargar Requisicion";
            product.Tag = "50";
            category.Items.Add(product);
            //product = new RadTreeViewItem();
            //product.Header = "Product1.1";
            //category.Items.Add(product);
            //category = new RadTreeViewItem();
            //category.Header = "CLT - Centro Llantero Tornel";
            ////category.Foreground = new SolidColorBrush(Colors.Purple);
            //radTreeView.Items.Add(category);
            //// Adding child items
            //product = new RadTreeViewItem();
            //product.Header = "Product2.1";
            //category.Items.Add(product);
            //product = new RadTreeViewItem();
            //product.Header = "Product2.2";
            //category.Items.Add(product);

            r.Content = radTreeView;
            //  radTreeView.SelectionChanged += new EventHandler(radTreeView_SelectionChanged(radTreeView, e = new System.Windows.Controls.SelectionChangedEventArgs())); 
        }

        private void CrearArbol()
        {

            Department dp1 = new Department();
            dp1.Name = "Inventarios";
            dp1.Subjects.Add(new Subject() { ID = "M101", Name = "Productos" });
            dp1.Subjects.Add(new Subject() { ID = "M210", Name = "Servicios" });
            dp1.Subjects.Add(new Subject() { ID = "M211", Name = "Refacciones" });
            dp1.Subjects.Add(new Subject() { ID = "M212", Name = "Rines" });
            dp1.Subjects.Add(new Subject() { ID = "M213", Name = "Art. Promocionales" });
            dp1.Subjects.Add(new Subject() { ID = "M900", Name = "Grupos" });
            dp1.Subjects.Add(new Subject() { ID = "M850", Name = "Marcas" });

            //Department dp1 = new Department();
            //dp1.Name = "Math";
            //dp1.Subjects.Add(new Subject() { ID = "M101", Name = "PreCalculus" });
            //dp1.Subjects.Add(new Subject() { ID = "M210", Name = "Calculus 1" });
            //dp1.Subjects.Add(new Subject() { ID = "M211", Name = "Calculus 2" });
            //dp1.Subjects.Add(new Subject() { ID = "M212", Name = "Calculus 3" });
            //dp1.Subjects.Add(new Subject() { ID = "M213", Name = "Differential Equations" });
            //dp1.Subjects.Add(new Subject() { ID = "M218", Name = "Linear Algebra" });

            Department dp2 = new Department();
            dp2.Name = "Catálogo de Proveedores";
            dp2.Subjects.Add(new Subject() { ID = "CS101", Name = "Introduction to Comuter" });
            dp2.Subjects.Add(new Subject() { ID = "CS201", Name = "Comuter Science 1" });
            dp2.Subjects.Add(new Subject() { ID = "CS202", Name = "Comuter Science 2" });
            dp2.Subjects.Add(new Subject() { ID = "CS210", Name = "Data Structure" });
            dp2.Subjects.Add(new Subject() { ID = "CS211", Name = "Database Management" });

            Department dp3 = new Department();
            dp3.Name = "Catálogo de Clientes";
            dp3.Subjects.Add(new Subject() { ID = "ACT101", Name = "Accounting 1" });
            dp3.Subjects.Add(new Subject() { ID = "ACT102", Name = "Accounting 2" });
            dp3.Subjects.Add(new Subject() { ID = "ACT201", Name = "Accounting 3" });
            dp3.Subjects.Add(new Subject() { ID = "ACT202", Name = "Accounting 4" });
            dp3.Subjects.Add(new Subject() { ID = "ACT203", Name = "Cost Accounting" });
            dp3.Subjects.Add(new Subject() { ID = "ACT214", Name = "Auditing" });

            uni.Departments.Add(dp1);
            uni.Departments.Add(dp2);
            uni.Departments.Add(dp3);

            Type type = this.GetType();
            Assembly assembly = type.Assembly;
            //UserControl mathUC = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.MathUserControl", type.Namespace));
            //UserControl csUC = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.CSUserControl", type.Namespace));
            //UserControl ticketsUC = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Patrimony.TicketsView", type.Namespace));

            //UserControl actUC = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.AccountingUserControl", type.Namespace));
            //UserControl cs2UC = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.cs2", type.Namespace));
            //UserControl testUC = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.ucTest", type.Namespace));

            UserControl mathUC = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.MathUserControl", type.Namespace));
            UserControl csUC = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.ucServicios", type.Namespace));
            UserControl csUC2 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.ucRef", type.Namespace));
            UserControl csUC3 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.ucRines", type.Namespace));
            UserControl csUC4 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.ucArt", type.Namespace));
            UserControl csUC5 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.ucGrupos", type.Namespace));
            UserControl csUC6 = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.ucMarcas", type.Namespace));

            // UserControl ticketsUC = (UserControl)assembly.CreateInstance(string.Format("{0}.Modules.Patrimony.TicketsView", type.Namespace));

            UserControl actUC = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.AccountingUserControl", type.Namespace));
            UserControl cs2UC = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.cs2", type.Namespace));
            UserControl testUC = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.ucTest", type.Namespace));

            //userControls.Add(dp1.Name, mathUC);
            userControls.Add("M101", mathUC);
            //userControls.Add("CS101", csUC);
            userControls.Add("M210", csUC);
            userControls.Add("M211", csUC2);
            userControls.Add("M212", csUC3);
            userControls.Add("M213", csUC4);
            userControls.Add("M900", csUC5);
            userControls.Add("M850", csUC6);
            //userControls.Add(dp1.Name, mathUC);
            //userControls.Add("CS101", ticketsUC);
            ////userControls.Add("CS101", csUC);
            userControls.Add("CS210", testUC);
            ////userControls.Add(dp2.Name, csUC);
            //userControls.Add(dp3.Name, actUC);
            //    DataContext = uni;
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            //UserControl u = new GGGC.Admin.Views.ucTest();
            // cuad.Children.Add(GGGC.Admin.Views.ucTest);
            // UserControl uc = new UserControl();
            UserControl u1 = new GGGC.Admin.Views.ucTest();
            //ucTest 
            //u1.Background = ConsoleColor.Cyan;
            //u1.Height = 100;
            //u1.Width = 100;
            //u1.Content = "Hello";
            // cuad.Children.Add(u1);
            //this.cuad.conten
            //this.Children.Add(u1);
            //        this.g
        }

        private void Button_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {
            UserControl u1 = new GGGC.Admin.Views.ucTest();
            //  u1.Background = ConsoleColor.Cyan;
            //u1.Height = 100;
            //u1.Width = 100;
            //  u1.Content = "Hello";
            //ccMain.Content = u1;
            // this.cuad.conten
            //this.Children.Add(u1);
            var baseUri = System.Windows.Navigation.BaseUriHelper.GetBaseUri(this);
        }

        private void CrearMenus()
        {
            RadOutlookBarItem rbi1 = new RadOutlookBarItem();
            rbi1.Header = "Grupo Guadiana";
            rbi1.Height = 30;
            rbi1.Icon = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/GGGC.Admin;component/Resources/Images/Menus/GrupoGuadiana32.png", UriKind.Absolute));
            rbi1.SmallIcon = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/GGGC.Admin;component/Resources/Images/Menus/GrupoGuadiana16.png", UriKind.Absolute));
            //this.robMain.Items.Add(rbi1);
            AddTreeViewMenu1(rbi1);

            RadOutlookBarItem rbi2 = new RadOutlookBarItem();
            rbi2.Header = "Memo";
            rbi2.Height = 35;
            rbi2.Icon = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/GGGC.Admin;component/Resources/Images/Menus/ark.png", UriKind.Absolute));
            // rbi2.SmallIcon = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/GGGC.Admin;component/Resources/Images/Menus/GrupoGuadiana16.png", UriKind.Absolute));
           // this.robMain.Items.Add(rbi2);
            // AddTreeViewMenu1(rbiMemo);
        }



        private void AddTreeViewMenu1(RadOutlookBarItem r)
        {
            RadTreeView radTreeView = new RadTreeView();
            RadTreeViewItem category = new RadTreeViewItem();
            category.Header = "Category1";
            // category.Foreground = new SolidColorBrush(Colors.Green);
            radTreeView.Items.Add(category);
            // Adding child items
            RadTreeViewItem product = new RadTreeViewItem();
            product.Header = "Product1.1";
            category.Items.Add(product);
            product = new RadTreeViewItem();
            product.Header = "Product1.1";
            category.Items.Add(product);
            category = new RadTreeViewItem();
            category.Header = "Category2";
            //category.Foreground = new SolidColorBrush(Colors.Purple);
            radTreeView.Items.Add(category);
            // Adding child items
            product = new RadTreeViewItem();
            product.Header = "Product2.1";
            category.Items.Add(product);
            product = new RadTreeViewItem();
            product.Header = "Product2.2";
            category.Items.Add(product);

            r.Content = radTreeView;
        }

        private void TreeViewGrupoGuadiana(RadOutlookBarItem r)
        {
            RadTreeView radTreeView = new RadTreeView();
            RadTreeViewItem category = new RadTreeViewItem();
            category.Header = "LRG - Llantas y Rines del Guadiana";
            // category.Foreground = new SolidColorBrush(Colors.Green);
            radTreeView.Items.Add(category);
            // Adding child items
            RadTreeViewItem product = new RadTreeViewItem();
            product.Header = "Product1.1";
            category.Items.Add(product);
            product = new RadTreeViewItem();
            product.Header = "Product1.1";
            category.Items.Add(product);
            category = new RadTreeViewItem();
            category.Header = "CLT - Centro Llantero Tornel";
            //category.Foreground = new SolidColorBrush(Colors.Purple);
            radTreeView.Items.Add(category);
            // Adding child items
            product = new RadTreeViewItem();
            product.Header = "Product2.1";
            category.Items.Add(product);
            product = new RadTreeViewItem();
            product.Header = "Product2.2";
            category.Items.Add(product);

            r.Content = radTreeView;
        }

        private void TreeViewSistemas(RadOutlookBarItem r)
        {
            RadTreeView radTreeView = new RadTreeView();
            RadTreeViewItem category = new RadTreeViewItem();
            category.Header = "Sucursales";
            //category.is
            // category.Foreground = new SolidColorBrush(Colors.Green);
            radTreeView.Items.Add(category);
            // Adding child items
            RadTreeViewItem product = new RadTreeViewItem();
            product.Header = "Product1.1";
            category.Items.Add(product);
            product = new RadTreeViewItem();
            product.Header = "Product1.1";
            category.Items.Add(product);
            category = new RadTreeViewItem();
            category.Header = "Access Point";
            //category.Foreground = new SolidColorBrush(Colors.Purple);
            radTreeView.Items.Add(category);
            // Adding child items
            product = new RadTreeViewItem();
            product.Header = "Product2.1";
            category.Items.Add(product);
            product = new RadTreeViewItem();
            product.Header = "Product2.2";
            category.Items.Add(product);
            r.Content = radTreeView;
        }

        private void MetroWindow_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Closing -= MetroWindow_Closing_1;
            //e.Cancel = true;
            //var anim = new System.Windows.Media.Animation.DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(2));
            //anim.Completed += (s, _) => this.Close();
            //this.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        //private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    Subject subject = e.NewValue as Subject;

        //    if (subject != null)
        //    {
        //        if (subject.ID.ElementAt(0) == 'M')
        //        {
        //            //container.Content = userControls["Math"];
        //           // container.Content = userControls[subject.ID];
        //        }
        //        else if (subject.ID.ElementAt(0) == 'A')
        //        {
        //           // container.Content = userControls["Accounting"];
        //        }
        //        else if (subject.ID.ElementAt(0) == 'C')
        //        {
        //           // container.Content = userControls[subject.ID];
        //            //container.Content = userControls["Computer Science"];
        //        }
        //        else
        //        {
        //          //  container.Content = null;
        //        }
        //    }
        //}

        private void MetroWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            DisplayLoginScreen();
            // System.Windows.Forms.MessageBox.Show("load");
            // prcOcultarControles();
        }

        public static string GetCurrentDirectory()
        {
            string path = null;

            path = AppDomain.CurrentDomain.BaseDirectory;
            if (path.IndexOf(@"\bin") > 0)
            {
                path = path.Substring(0, path.LastIndexOf(@"\bin"));
            }

            return path;
        }

        private void DisplayLoginScreen()
        {
            //frmLogin frm = new frmLogin();
            //frm.ShowDialog();
            //  frm.Owner = this;

            //if (frm.DialogResult.HasValue && frm.DialogResult.Value)
            //    MessageBox.Show("User Logged In");
            //else
            //    this.Close();

            // Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // string strPath = GetCurrentDirectory();
            // // MessageBox.Show(strPath);

            // string strUser = "";
            // // Authenticate the current user and set the default principal

            //// GGGC.Admin.Login.Views.MainWindow auth = new MainWindow(strPath, ref strUser);

            // GGGC.Admin.Login.Views.MainWindow auth = new MainWindow(strPath, ref strUser);

            // ////  GGGC.Admin.Login.Views.Test auth = new Test();
            // //  auth.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // bool? dialogResult = auth.ShowDialog();


            // // deal with the results
            // if (dialogResult.HasValue && dialogResult.Value)
            // {
            //     // MessageBox.Show("User Logged In");
            //     //var DoubleAnimation animFadeIn = new DoubleAnimation();
            //     //animFadeIn.From = 0;
            //     //animFadeIn.To = 1;
            //     //animFadeIn.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            //     //window.Show();
            //     //window.BeginAnimation(Window.OpacityProperty, animFadeIn);
            //     //lsWindows[lsWindows.Count - 2].Hide();
            //     //Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            //     this.Close();

            //    // base.OnStartup(e);
            //     //Bootstrapper bs = new Bootstrapper();
            //     //bs.Run();

            //     // clsUser Usuario = new clsUser(auth.ViewModel.UserName);
            //     // GlobalVar.User = auth.ViewModel.UserName;

            // //    ObjectBase.Container = MEFLoader.Init(new List<ComposablePartCatalog>() 
            // //{
            // //    new AssemblyCatalog(Assembly.GetExecutingAssembly())
            // //});

            // }
            // else
            // {
            //     MessageBox.Show("User Clicked Cancel");
            //     //this.Shutdown(-1);
            //    // this.Close();
            //     //Application.Current.Shutdown(0);
            //     //App.Current.Exit();
            // }
        }


        #region Event handler

        private void btnLock_Click(object sender, RoutedEventArgs e)
        {
            //this.SmartLoginOverlayControl.Lock();
            //this.robMain.Width = 0;

            //     SmartLoginOverlayControl.

            //SmartLoginOverlayControl.UNi
        }

        #endregion




        //private void myTreeView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //{
        //    Telerik.Windows.Controls.RadTreeView treeView = sender as Telerik.Windows.Controls.RadTreeView;

        //    ObservableCollection<Object> selectedItems = treeView.SelectedItems;
        //    RadTreeViewItem item = selectedItems[0] as RadTreeViewItem;


        //    RadTreeViewItem selectedItem = e.OriginalSource as RadTreeViewItem;
        //    RadTreeViewItem selectedItem2 = selectedItems[0] as RadTreeViewItem;
        //}

        private void tvModulos_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            Telerik.Windows.Controls.RadTreeView treeView = sender as Telerik.Windows.Controls.RadTreeView;

            ObservableCollection<Object> selectedItems = treeView.SelectedItems;
            RadTreeViewItem item = selectedItems[0] as RadTreeViewItem;


            RadTreeViewItem selectedItem = e.OriginalSource as RadTreeViewItem;
            RadTreeViewItem selectedItem2 = selectedItems[0] as RadTreeViewItem;


            // Get the previous item and the previous sibling item
            //RadTreeViewItem previousItem = item.PreviousItem;
            //RadTreeViewItem previousSiblingItem = item.PreviousSiblingItem;

            // Get the currently selected items
            //ObservableCollection<Object> selectedItems = treeView.SelectedItems;
            //RadTreeViewItem item = selectedItems[0] as RadTreeViewItem;

            string strelemento = ((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Name;



            if (strelemento == "1. Cargar Requisicion")
            {
                Type type = this.GetType();
                Assembly assembly = type.Assembly;

                UserControl uc = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.ucRines", type.Namespace));
                userControls.Add("256", uc);
               // container.Content = userControls["256"];

            }

            //  container.Content = userControls[subject.ID];



            //var newArgs = new System.Windows.Controls.SelectionChangedEventArgs(e);
            // Get a reference to the treeview
            //System.Windows.RoutedEventArgs

            // treeView.SelectedContainer
            // Telerik.Windows.Controls.RadTreeView treeView = sender as Telerik.Windows.Controls.RadTreeView;
            //// Get the currently selected items
            //ObservableCollection<Object> selectedItems = treeView.SelectedItems;
            //RadTreeViewItem item = selectedItems[0] as RadTreeViewItem;


            //BusinessItem item = this.sampleVM.GetItemByName(this.textBox.Text);
            //if (item != null)
            //{
            //    item.IsSelected = true;
            //    string path = item.GetPath();
            //    myTreeView.BringPathIntoView(path);
            //}


            //object item = treeView.SelectedItem;
            //if (item != null)
            //{
            //    RadTreeViewItem tvi = treeView.ContainerFromItemRecursive(treeView.SelectedItem);
            //    if (tvi != null)
            //    {
            //        // here we can deal with the tree-view item 
            //        // ... 
            //        tvi.IsSelected = true;
            //        //string path = item.GetPath();
            //        //myTreeView.BringPathIntoView(path);
            //    }
            //} 
        }

        private void tvSuc_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Telerik.Windows.Controls.RadTreeView treeView = sender as Telerik.Windows.Controls.RadTreeView;

            ObservableCollection<Object> selectedItems = treeView.SelectedItems;
            RadTreeViewItem item = selectedItems[0] as RadTreeViewItem;

            RadTreeViewItem selectedItem = e.OriginalSource as RadTreeViewItem;
            RadTreeViewItem selectedItem2 = selectedItems[0] as RadTreeViewItem;

            string strelemento = ((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Name;

            if (strelemento == "1. Cargar Requisicion")
            {
                Type type = this.GetType();
                Assembly assembly = type.Assembly;

                UserControl uc = (UserControl)assembly.CreateInstance(string.Format("{0}.Views.IT.ucRines", type.Namespace));
                userControls.Add("256", uc);
               // container.Content = userControls["256"];
            }
        }




        void prcOcultarControles()
        {
            this.dpBottom.Height = 20;
            //btnBloquear.Visibility = Visibility.Visible;
            this.lblFecha.Visibility = Visibility.Hidden;
            // this.lblTitulo.Visibility = Visibility.Hidden;
            this.lblVersion.Visibility = Visibility.Hidden;
            this.lblDerechos.Visibility = Visibility.Hidden;
           // this.imgTittle.Visibility = Visibility.Hidden;

            this.lblDayLong.Content = DateTime.Now.ToString("MMMM").ToUpper();
            this.lblDayLong.FontSize = 28;
            this.lblDayNumber.Content = DateTime.Now.Day.ToString().ToUpper();
            this.lblMonth.Content = DateTime.Now.ToString("dddd").ToUpper();
        }
        private void SmartLoginOverlayControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (GlobalModule.blnEntrar == true)
            {
                GlobalModule.blnEntrar = false;

                prcOcultarControles();

               // prcPerfil(GlobalModule.ActualUserID);

                prcPerfil(GlobalModule.ActualPerfilID);
                DisplayVersion();



                // this.dpBottom.Height = 15;
                // //btnBloquear.Visibility = Visibility.Visible;
                // this.lblFecha.Visibility = Visibility.Hidden;
                //// this.lblTitulo.Visibility = Visibility.Hidden;
                // this.lblVersion.Visibility = Visibility.Hidden;
                // this.lblDerechos.Visibility = Visibility.Hidden;
                // this.imgTittle.Visibility= Visibility.Hidden;

                // this.lblDayLong.Content = DateTime.Now.ToString("dddd").ToUpper();
                // this.lblDayNumber.Content = DateTime.Now.Day.ToString().ToUpper();
                // this.lblMonth.Content = DateTime.Now.ToString("MMMM").ToUpper();



                //this.tlDay.Count = DateTime.Now.Day.ToString();

                //            var storageAccountInfo = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["sacc"]);
                //            var tableStorage = storageAccountInfo.CreateCloudTableClient();

                //           // var cloudTable = storageAccountInfo.
                //          //  cloudTable

                //            CloudTableClient tableClient = storageAccountInfo.CreateCloudTableClient();
                //CloudTable table = tableClient.GetTableReference("info");
                //table.CreateIfNotExists();

                //            var log = new LoggerConfiguration()
                //           .MinimumLevel.Debug()
                //                .WriteTo.AzureTableStorage(storageAccountInfo)
                //                .CreateLogger();

                //           // Log.Information("No one listens to me!");

                //            log.Information("esto es el log de un error no one guillermo uribe aguilar");

                // log.Write("helo");
                //log.
                //ILogger logger = new LoggerConfiguration()
                //      .WriteTo.("logfile.txt", outputTemplate: customTemplate, fileSizeLimitBytes: null)
                //      .CreateLogger();

                //Log.Logger = logger;


                //var storage = CloudStorageAccount.FromConfigurationSetting("MyStorage");

                //var log = new LoggerConfiguration()
                //    .WriteTo.AzureTableStorage()
                //    .CreateLogger();

            }

            //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
            //{

            //    MessageBox.Show("visible");
            //    //if (MessageBox.Show("Geen resultaat gevonden, " )
            //    //{
            //    //   // RB_handmatig.IsChecked = true;
            //    //    MessageBox.Show("visible");
            //    //}
            //    //else
            //    //{
            //    //    //
            //    //}
            //}));
        }

        private void btnBloquear_Click_1(object sender, RoutedEventArgs e)
        {
            // SmartLoginOverlayControl.Lock();

            //this.SmartLoginOverlayControl.Lock();
            //this.robMain.Width = 0;
        }

        private void fConfig_IsOpenChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            // MessageBox.Show("isopen");


        }

        private void containerOK_Unloaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("close");
        }

        private void containerOK_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.Source is TabControlExt) //if this event fired from TabControl then enter
            {

                if (lastSelectedItem != null)
                {
                    string tabItem = ((sender as TabControl).SelectedItem as TabItem).Header as string;

                    MessageBox.Show("Last Selected item: " + lastSelectedItem.Header.ToString());

                    //TabItem ti = Tabs1.SelectedItem as TabItem;
                    //MessageBox.Show("This is " + ti.Header + " tab");

                    if (sender != null)
                    {
                       

                        switch (tabItem)
                        {
                            case "Item1":
                                break;

                            case "Item2":
                                break;

                            case "Item3":
                                break;

                            default:
                                // MessageBox.Show("close");
                                return;
                        }
                    }
                }
                lastSelectedItem = this.containerOK.SelectedItem as TabItemExt;

               
            }


           
            
        }

        private void containerOK_OnCloseButtonClick(object sender, CloseTabEventArgs e)
        {

          //  int i = containerOK.SelectedIndex;

            int index = (int)containerOK.SelectedIndex;

            if (index >= 0)
            {

                containerOK.Items.RemoveAt(index);

                string strTag = ((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Tag;
                // (this.DataContext as ViewModel.TabcontrolViewModel).TabCollection.RemoveAt(index);

                if (strTag!="00") // sino es la raiz, seleccione el menu padre del treeview
                {
                    treeView.SelectedItem = ((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Parent;
                }

               
            }

            if (lastSelectedItem != null)
            {
                MessageBox.Show("Last Selected item: " + lastSelectedItem.Header.ToString());
            }

            //RoutedCommand command = TabControlCommands.CloseCurrentTabItem;
            //command.Execute(containerOK.SelectedItem, containerOK);

        }

        //private void radTreeViewCatalogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        //private void fConfig_Loaded(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    MessageBox.Show("Loaded");
        //}

        //private void robMain_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //  //  MessageBox.Show("cahngue");
        //}

        //private void robMain_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    MessageBox.Show("cambiado");
        //}
    }

    public class Animal
    {
        public Animal(string name, Category category)
        {
            this.Name = name;
            this.Category = category;
        }
        public string Name
        {
            get;
            set;
        }
        public Category Category
        {
            get;
            set;
        }

        public IEnumerable<Animal> AnimalList
        {
            get
            {
                List<Animal> animalList = new List<Animal>();
                animalList.Add(new Animal("California Newt", Category.Amphibians));
                animalList.Add(new Animal("Giant Panda", Category.Bears));
                animalList.Add(new Animal("Coyote", Category.Canines));
                animalList.Add(new Animal("Golden Silk Spader", Category.Spiders));
                animalList.Add(new Animal("Mandrill", Category.Primates));
                animalList.Add(new Animal("Black Bear", Category.Bears));
                animalList.Add(new Animal("Jaguar", Category.BigCats));
                animalList.Add(new Animal("Bornean Gibbon", Category.Primates));
                animalList.Add(new Animal("African Wildcat", Category.BigCats));
                animalList.Add(new Animal("Artic Fox", Category.Canines));
                animalList.Add(new Animal("Tomato Frog", Category.Amphibians));
                animalList.Add(new Animal("Grizzly Bear", Category.Bears));
                animalList.Add(new Animal("Dingo", Category.Canines));
                animalList.Add(new Animal("Gorilla", Category.Primates));
                animalList.Add(new Animal("Green Tree Frog", Category.Amphibians));
                animalList.Add(new Animal("Bald Vakari", Category.Primates));
                animalList.Add(new Animal("Polar Bear", Category.Bears));
                animalList.Add(new Animal("Black Widow Spider", Category.Spiders));
                animalList.Add(new Animal("Bat-Eared Fox", Category.Canines));
                animalList.Add(new Animal("Cheetah", Category.BigCats));
                return animalList.AsEnumerable();
            }
        }



    }


    public class MenuItem
    {
        public string ID
        { get; set; }

        public string Name
        { get; set; }
    }

    public class Group
    {
        public Group()
        {
            this.Subjects = new ObservableCollection<MenuItem>();

        }

        public string Name
        { get; set; }

        public ObservableCollection<MenuItem> Subjects
        { get; set; }
    }


    public class Subject
    {
        public string ID
        { get; set; }

        public string Name
        { get; set; }
    }

    public class Department
    {
        public Department()
        {
            this.Subjects = new ObservableCollection<Subject>();

        }
        public string Name
        { get; set; }

        public ObservableCollection<Subject> Subjects
        { get; set; }
    }

    public class University
    {
        public University()
        {
            this.Departments = new ObservableCollection<Department>();
        }

        public ObservableCollection<Department> Departments
        { get; set; }

    }

    public static class GlobalModule
    {
        //private GlobalModule() { } // Private ctor for class with all static methods.
        public static bool blnCRefaccion = false;
        public static bool blnCAcumulador = false;
        public static bool blnCAmortiguador = false;
        public static bool blnCMisc = false;
        public static bool blnEntrar = false;
        public static int intMESCOMOVAMOS = 8;
        public static int ActualUserID = 0;
        public static string ActualUserPassword = "";
        public static string ActualUserName = "";
        public static string ActualName = "";
        public static string ActualEmail = "";
        public static string ActualStoreID = "";
        public static string ActualPerfilID = "";

        public static  string strRUTA_DE_EXPORTACION = @"C:\BIG\";
        public static  string strEMPRESA_LRG = @"LRG";
        public static  string strEMPRESA_CLT = @"CLT";
        public static  string strEMPRESA_GGGC = @"GGGC";
        public static  string strARCHIVO_EXCEL = @"Excel";

        public static string exeFolder = "";

        public static byte bytSUCURSAL = 0;
        public static byte bytEMPRESA = 0;
        //public static int MyFunction()
        //{
        //    return 22;
        //}

        public static string getPublicIP()
        {
            try
            {
                string externalIP = "";

                string url = "http://checkip.amazonaws.com/";
                System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                externalIP = sr.ReadToEnd().Trim();
                //string[] a = response.Split(':');
                //string a2 = a[1].Substring(1);
                //string[] a3 = a2.Split('<');
                //string a4 = a3[0];
                //return a4;
                //externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                //externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                //             .Matches(externalIP)[0].ToString();
                //externalIP = response;
                return externalIP;
            }
            catch { return "0.0.0.0"; }
        }

        public static string GetLocalIP()
        {
            try
            {
                string LocalIP = "1.1.1.1";


                string strHostName = System.Net.Dns.GetHostName();

                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

                foreach (IPAddress ipAddress in ipEntry.AddressList)
                {
                    if (ipAddress.AddressFamily.ToString() == "InterNetwork")
                    {
                        LocalIP = ipAddress.ToString();

                    }

                }
                return LocalIP;
            }

            catch { return "1.1.1.1"; }
        }

        public static string GetSystemInfo()
        {
            try
            {
                string strOS = System.Environment.Is64BitOperatingSystem ? "x64" : "x86";
                string strLogicalDrives = "";
                string strOSVersion = "";

                OperatingSystem os = Environment.OSVersion;
                // Get the version informationVersion vs = os.Version;
                strOSVersion = " Major:" + os.Version.Major + " Minor:" + os.Version.Minor + " Revision:" + os.Version.Revision + " Build: " + os.Version.Major ;             
                // identifying logical drive letters
                foreach (String s3 in Environment.GetLogicalDrives())
                    strLogicalDrives += " " + s3 + "- ";

                string strInfo = System.Environment.OSVersion + "-" + strOSVersion + "|" + strOS + "|CPU:" + System.Environment.ProcessorCount + "|CLR:" + System.Environment.Version + "|DD:" + strLogicalDrives;
                return strInfo;
            }

            catch { return "SystemInfo"; }
        }

        public static string GetUserInfo()
        {
            try
            {                        
                string strInfo = "D:" + System.Environment.UserDomainName.ToString() + "|M:" + System.Environment.MachineName.ToString() + "|U:" + System.Environment.UserName.ToString();
                return strInfo;
            }

            catch { return "UserInfo"; }
        }



        public static void WriteActualPath()
        {
            exeFolder= System.Reflection.Assembly.GetExecutingAssembly().Location;
        }

    }


}

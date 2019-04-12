using GGGC.Admin.Menus;
using GGGC.Admin.UI.Docking;
using GGGC.Admin.UI.Docking.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;
// The following using statements were added for this sample.
using System.Globalization;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
//using System.Net.Http;
//using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using GGGC.Admin.Sign.ViewModels;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Windows.Media;

namespace GGGC.Admin
{
    /// <summary>
    /// Interaction logic for ShellDockDesign.xaml
    /// </summary>
    public partial class ShellDockDesign : MetroWindow
    {
        private ItemsViewModel iVM;
        Telerik.Windows.Controls.RadTreeView treeView;
        public static Dictionary<string, UserControl> userControls = new Dictionary<string, UserControl>();
        private Syncfusion.Windows.Tools.Controls.TabItemExt lastSelectedItem;
        //private List<TabItemExt> _tabItems;
        //private TabItemExt _tabAdd;
        public LoginViewModel ViewModel;
        private bool blnInside = false;



        #region Config Values

        //
        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        // The Tenant is the name of the Azure AD tenant in which this application is registered.
        // The AAD Instance is the instance of Azure, for example public Azure or Azure China.
        // The Redirect URI is the URI where Azure AD will return OAuth responses.
        // The Authority is the sign-in URL of the tenant.
        //
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        Uri redirectUri = new Uri(ConfigurationManager.AppSettings["ida:RedirectUri"]);
        private static string authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

        private static string graphResourceId = ConfigurationManager.AppSettings["ida:GraphResourceId"];
        private static string graphApiVersion = ConfigurationManager.AppSettings["ida:GraphApiVersion"];
        private static string graphApiEndpoint = ConfigurationManager.AppSettings["ida:GraphEndpoint"];

        #endregion

        //private HttpClient httpClient = new HttpClient();
        //private AuthenticationContext authContext = null;

        public ShellDockDesign()
        {

            //this.OnLoaded += MetroWindow_Loaded_1(sender,e);

            InitializeComponent();

            //  authContext = new AuthenticationContext(authority, new FileCache());

            //   CheckForCachedToken();

            // this.Loaded += OnLoaded;

            //this.win = WindowState.Maximized;

            //  Application.Current.Exit += OnApplicationExit;

            //this.ViewModel = new LoginViewModel(ref blnInside);
            //this. = this.ViewModel;

            // this.DataContext = this.ViewModel;


            iVM = new ItemsViewModel(ItemsViewModel.Menu.GG);
            tvGG.ItemsSource = iVM;
            tvGG.ExpandAll();

            prcOcultarControles();
        }

        //public async void CheckForCachedToken()
        //{
        //    // As the application starts, try to get an access token without prompting the user.  If one exists, show the user as signed in.
        //    AuthenticationResult result = null;
        //    try
        //    {
        //        result = await authContext.AcquireTokenAsync(graphResourceId, clientId, redirectUri, new PlatformParameters(PromptBehavior.Never));
        //    }
        //    catch (AdalException ex)
        //    {
        //        if (ex.ErrorCode != "user_interaction_required")
        //        {
        //            // An unexpected error occurred.
        //            MessageBox.Show(ex.Message);
        //        }

        //        // If user interaction is required, proceed to main page without singing the user in.
        //        return;
        //    }

        //    // A valid token is in the cache
        //   // SignOutButton.Visibility = Visibility.Visible;
        //    UserNameLabel.Text = result.UserInfo.DisplayableId;
        //}

     

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
           // var viewModel = this.DataContext as ShellDockViewModel;
            //if (viewModel != null)
            //{
            //    viewModel.Save(this.radDocking);
            //}
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //var viewModel = this.DataContext as ShellDockViewModel;
            //if (viewModel != null)
            //{
            //    viewModel.Load(this.radDocking);
            //}
        }

       

        private void OnClose(object sender, StateChangeEventArgs e)
        {
            //var documents = e.Panes.Select(p => p.DataContext).OfType<PaneViewModel>().Where(vm => vm.IsDocument).ToList();
            //foreach (var document in documents)
            //{
            //    ((ShellDockViewModel)this.DataContext).Panes.Remove(document);
            //}
        }

        private void FilterActiveViewsSource(object sender, System.Windows.Data.FilterEventArgs e)
        {
            var vm = e.Item as PaneViewModel;
            e.Accepted = vm.IsDocument;
        }

        private void FilterToolboxesSource(object sender, System.Windows.Data.FilterEventArgs e)
        {
            var vm = e.Item as PaneViewModel;
            e.Accepted = !vm.IsDocument;
        }


        private void MetroWindow_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Closing -= MetroWindow_Closing_1;
            //e.Cancel = true;
            //var anim = new System.Windows.Media.Animation.DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(2));
            //anim.Completed += (s, _) => this.Close();
            //this.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        private void fConfig_IsOpenChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            // MessageBox.Show("isopen");


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


        private void MetroWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            //DisplayLoginScreen();
            // System.Windows.Forms.MessageBox.Show("load");
            // prcOcultarControles();

            //    var viewModel = this.DataContext as ShellDockViewModel;
            //    // this.da
            //    if (viewModel != null)
            //    {
            //        viewModel.Load(this.radDocking);
            //    }
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

           // strTag = "7004";
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

                case "206":
                    UserControl uc_206 = null;
                    if (userControls.ContainsKey("206"))
                    {
                        userControls.Remove("206");
                        uc_206 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.AR.Directorio.Tile", type.Namespace));
                        userControls.Add("206", uc_206);
                        //AddDetail(uc_8011, "EDI");
                    }
                    else
                    {
                        uc_206 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.AR.Directorio.Tile", type.Namespace));
                        userControls.Add("206", uc_206);
                        AddDetail(uc_206, "DH - Lista de Empleados");
                    }
                    break;

                case "8012":
                    UserControl uc_8011 = null;
                    if (userControls.ContainsKey("8012"))
                    {
                        userControls.Remove("8012");
                        uc_8011 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.AR.Catalogs.Edi.Views.MainView", type.Namespace));
                        userControls.Add("8012", uc_8011);
                        //AddDetail(uc_8011, "EDI");
                    }
                    else
                    {
                        uc_8011 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.AR.Catalogs.Edi.Views.MainView", type.Namespace));
                        userControls.Add("8012", uc_8011);
                        AddDetail(uc_8011, "EDI - Electronic Data Interchange");
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
                        AddDetail(uc7002, "Historiales");
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

                case "0102":
                    UserControl uc0102 = null;
                    if (userControls.ContainsKey("0102"))
                    {
                        userControls.Remove("0102");
                        uc0102 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Ncr.Views.NcrView", type.Namespace));
                        userControls.Add("Notas de Crédito", uc0102);
                    }
                    else
                    {
                        uc0102 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Ncr.Views.NcrView", type.Namespace));
                        userControls.Add("0102", uc0102);
                        AddDetail(uc0102, "Notas de Crédito");                     
                    }
                    break;

                case "0103":
                    UserControl uc0103 = null;
                    if (userControls.ContainsKey("0103"))
                    {
                        userControls.Remove("0103");
                        uc0103 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Compr.Views.Compraview", type.Namespace));
                        userControls.Add("Compras", uc0103);
                    }
                    else
                    {
                        uc0103 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Compr.Views.Compraview", type.Namespace));
                        userControls.Add("0103", uc0103);
                        AddDetail(uc0103, "Compras");
                    }
                    break;

                case "0117":
                    UserControl uc0117 = null;
                    if (userControls.ContainsKey("0117"))
                    {
                        userControls.Remove("0117");
                        uc0117 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Ordenes.Views.OrdenView", type.Namespace));
                        userControls.Add("Ordenes", uc0117);
                    }
                    else
                    {
                        uc0117 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Ordenes.Views.OrdenView", type.Namespace));
                        userControls.Add("0117", uc0117);
                        AddDetail(uc0117, "Ordenes");
                    }
                    break;
                case "01117":
                    UserControl uc01117 = null;
                    if (userControls.ContainsKey("01117"))
                    {
                        userControls.Remove("01117");
                        uc01117 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Precios.PreciosView", type.Namespace));
                        userControls.Add("Precios", uc01117);
                    }
                    else
                    {
                        uc01117 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Precios.PreciosView", type.Namespace));
                        userControls.Add("01117", uc01117);
                        AddDetail(uc01117, "Precios");
                    }
                    break;
                case "01118":
                    UserControl uc01118 = null;
                    if (userControls.ContainsKey("01118"))
                    {
                        userControls.Remove("01118");
                        uc01118 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.OrderMaterial.OrdenView", type.Namespace));
                        userControls.Add("Precios", uc01118);
                    }
                    else
                    {
                        uc01118 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.OrderMaterial.OrdenView", type.Namespace));
                        userControls.Add("01118", uc01118);
                        AddDetail(uc01118, "Precios");
                    }
                    break;
                case "01336":
                    UserControl uc01336 = null;
                    if (userControls.ContainsKey("01336"))
                    {
                        userControls.Remove("01336");
                        uc01336 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Plexi.Plexi", type.Namespace));
                        userControls.Add("PL-Existencias", uc01336);
                    }
                    else
                    {
                        uc01336 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Plexi.Plexi", type.Namespace));
                        userControls.Add("01336", uc01336);
                        AddDetail(uc01336, "PL-Existencias");
                    }
                    break;
                case "04444":
                    UserControl uc04444 = null;
                    if (userControls.ContainsKey("04444"))
                    {
                        userControls.Remove("04444");
                        uc04444 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.PagareArellantas.PagareAView", type.Namespace));
                        userControls.Add("Pagares Arellantas", uc04444);
                    }
                    else
                    {
                        uc04444 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.PagareArellantas.PagareAView", type.Namespace));
                        userControls.Add("04444", uc04444);
                        AddDetail(uc04444, "Pagares Arellantas");
                    }
                    break;
                case "04447":
                    UserControl uc04447 = null;
                    if (userControls.ContainsKey("04447"))
                    {
                        userControls.Remove("04447");
                        uc04447 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Inventarios.Inventarioview", type.Namespace));
                        userControls.Add("Inventarios Rpt", uc04447);
                    }
                    else
                    {
                        uc04447 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Inventarios.Inventarioview", type.Namespace));
                        userControls.Add("04447", uc04447);
                        AddDetail(uc04447, "Inventarios Rpt");
                    }
                    break;

                case "04422":
                    UserControl uc04422 = null;
                    if (userControls.ContainsKey("04422"))
                    {
                        userControls.Remove("04422");
                        uc04422 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Ajustes.AjustesView", type.Namespace));
                        userControls.Add("Ajustes Almacen", uc04422);
                    }
                    else
                    {
                        uc04422 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Ajustes.AjustesView", type.Namespace));
                        userControls.Add("04422", uc04422);
                        AddDetail(uc04422, "Ajustes Almacen");
                    }
                    break;
                case "04423":
                    UserControl uc04423 = null;
                    if (userControls.ContainsKey("04423"))
                    {
                        userControls.Remove("04423");
                        uc04423 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Transferencias.TransferenciasView", type.Namespace));
                        userControls.Add("Transferencias", uc04423);
                    }
                    else
                    {
                        uc04423 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Transferencias.TransferenciasView", type.Namespace));
                        userControls.Add("04423", uc04423);
                        AddDetail(uc04423, "Transferencias");
                    }
                    break;
                case "011788":
                    UserControl uc011788 = null;
                    if (userControls.ContainsKey("011788"))
                    {
                        userControls.Remove("011788");
                        uc011788 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Compras.ComprasView", type.Namespace));
                        userControls.Add("Compras", uc011788);
                    }
                    else
                    {
                        uc011788 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Compras.ComprasView", type.Namespace));
                        userControls.Add("011788", uc011788);
                        AddDetail(uc011788, "Compras");
                    }
                    break;

                case "0665":
                    UserControl uc0665 = null;
                    if (userControls.ContainsKey("0665"))
                    {
                        userControls.Remove("0665");
                        uc0665 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Operarios.OperariosView", type.Namespace));
                        userControls.Add("Operarios", uc0665);
                    }
                    else
                    {
                        uc0665 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Operarios.OperariosView", type.Namespace));
                        userControls.Add("0665", uc0665);
                        AddDetail(uc0665, "Operarios");
                    }
                    break;
                case "9991":
                    UserControl uc9991 = null;
                    if (userControls.ContainsKey("9991"))
                    {
                        userControls.Remove("9991");
                        uc9991 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Servicios.Serviciosview", type.Namespace));
                        userControls.Add("Servicios", uc9991);
                    }
                    else
                    {
                        uc9991 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Servicios.Serviciosview", type.Namespace));
                        userControls.Add("09991", uc9991);
                        AddDetail(uc9991, "Servicios");
                    }
                    break;
                case "07117":
                    UserControl uc07117 = null;
                    if (userControls.ContainsKey("07117"))
                    {
                        userControls.Remove("07117");
                        uc07117 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.VentasGeneral.VentasGral", type.Namespace));
                        userControls.Add("Ventas General", uc07117);
                    }
                    else
                    {
                        uc07117 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.VentasGeneral.VentasGral", type.Namespace));
                        userControls.Add("07117", uc07117);
                        AddDetail(uc07117, "Ventas General");
                    }
                    break;
                case "071173":
                    UserControl uc071173 = null;
                    if (userControls.ContainsKey("071173"))
                    {
                        userControls.Remove("071173");
                        uc071173 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.PagareCentro.PagareCView", type.Namespace));
                        userControls.Add("Pagares centro", uc071173);
                    }
                    else
                    {
                        uc071173 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.PagareCentro.PagareCView", type.Namespace));
                        userControls.Add("071173", uc071173);
                        AddDetail(uc071173, "Pagares centro");
                    }
                    break;
                case "0711739":
                    UserControl uc0711739 = null;
                    if (userControls.ContainsKey("0711739"))
                    {
                        userControls.Remove("0711739");
                        uc0711739 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Remisiones.RemisionView", type.Namespace));
                        userControls.Add("Remisiones", uc0711739);
                    }
                    else
                    {
                        uc0711739 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Remisiones.RemisionView", type.Namespace));
                        userControls.Add("0711739", uc0711739);
                        AddDetail(uc0711739, "Remisiones");
                    }
                    break;


                case "08117":
                    UserControl uc08117 = null;
                    if (userControls.ContainsKey("08117"))
                    {
                        userControls.Remove("08117");
                        uc08117 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Clientes_agregados.Agregadosview", type.Namespace));
                        userControls.Add("Clientes Agregados", uc08117);
                    }
                    else
                    {
                        uc08117 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Clientes_agregados.Agregadosview", type.Namespace));
                        userControls.Add("08117", uc08117);
                        AddDetail(uc08117, "Clientes Agregados");
                    }
                    break;
                case "0119":
                    UserControl uc0119 = null;
                    if (userControls.ContainsKey("0119"))
                    {
                        userControls.Remove("0119");
                        uc0119 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Remisiones.Views.RemisionView", type.Namespace));
                        userControls.Add("Remisiones", uc0119);
                    }
                    else
                    {
                        uc0119 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Remisiones.Views.RemisionView", type.Namespace));
                        userControls.Add("0119", uc0119);
                        AddDetail(uc0119, "Remisiones");
                    }
                    break;


                case "0888":
                    UserControl uc0888 = null;
                    if (userControls.ContainsKey("0888"))
                    {
                        userControls.Remove("0888");
                        uc0888 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Remisiones.Views.IngresosView", type.Namespace));
                        userControls.Add("Otros Ingresos", uc0888);
                    }
                    else
                    {
                        uc0888 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.OtrosIngresos.Views.IngresosView", type.Namespace));
                        userControls.Add("0888", uc0888);
                        AddDetail(uc0888, "Otros Ingresos");
                    }
                    break;

                case "08888":
                    UserControl uc08888 = null;
                    if (userControls.ContainsKey("08888"))
                    {
                        userControls.Remove("08888");
                        uc08888 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Pagares.PagareView", type.Namespace));
                        userControls.Add("Pagares", uc08888);
                    }
                    else
                    {
                        uc08888 = (UserControl)assembly.CreateInstance(string.Format("{0}.AZ.Pagares.PagareView", type.Namespace));
                        userControls.Add("08888", uc08888);
                        AddDetail(uc08888, "Pagares");
                    }
                    break;



                case "951":
                    UserControl uc_951 = null;
                    if (userControls.ContainsKey("951"))
                    {
                        userControls.Remove("951");
                        uc_951 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.MTE.Garage.Views.MainView", type.Namespace));
                        userControls.Add("951", uc_951);
                        AddDetail(uc_951, "Llantas");
                    }
                    else
                    {
                        uc_951 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.MTE.Garage.Views.MainView", type.Namespace));
                        userControls.Add("951", uc_951);
                        AddDetail(uc_951, "Llantas");
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

            //_tabItems.Add(_tabAdd);

            var pane = new RadPane();
           // pane.Background = Brushes.Pink;
            pane.Content = detail;
            pane.Header = name;
           // leftGroup.Background = Brushes.Pink;

            //pane.Background = 


            //    create and populate the new tab and add it to the tab control
            //  TabItemExt newTab = new TabItemExt();
            //  newTab.Content = detail;
            //newTab.Header = name;
            containerOK.Items.Add(pane);
            //_tabItems.Add(pane);

            //    display the new tab to the user; if this line is missing
            //    you get a blank tab
            //   containerOK.SelectedItem = newTab;

            // containerOK.Visibility = Visibility.Visible;
            //containerOK.fil


            //containerOK.Items.Refresh
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var frm = new ModalWindow();
            //frm.Owner = this;
          // Login();
            // Login3();

            // Authorize().Wait();
            // Authorize().RunSynchronously();
            // frm.ShowDialog();

            prcOcultarControles();

            // prcPerfil(GlobalModule.ActualUserID);

           // prcPerfil(GlobalModule.ActualPerfilID);
            DisplayVersion();




        }

        //private Task Authorize()
        //{
        //    return Task.Run(async () => {
        //        AuthenticationResult result = null;
        //        try
        //        {
                 
        //            // UserNameLabel.Content = result.UserInfo.DisplayableId;
        //            //SignOutButton.Visibility = Visibility.Visible;
        //        }
        //        catch (AdalException ex)
        //        {
        //            // An unexpected error occurred, or user canceled the sign in.
        //            if (ex.ErrorCode != "access_denied")
        //                MessageBox.Show(ex.Message);

        //            return;
        //        }
        //    });
        //}



        void prcOcultarControles()
        {
            this.dpBottom.Height = 20;
            //btnBloquear.Visibility = Visibility.Visible;
            this.lblFecha.Visibility = Visibility.Hidden;
            // this.lblTitulo.Visibility = Visibility.Hidden;
            this.lblVersion.Visibility = Visibility.Hidden;
            this.lblDerechos.Visibility = Visibility.Hidden;
            // this.imgTittle.Visibility = Visibility.Hidden;


            radDocking.Visibility = Visibility.Visible;
            //SmartLoginOverlayControl.Visibility = Visibility.Hidden;

            //this.lblDayLong.Content = DateTime.Now.ToString("MMMM").ToUpper();
            //this.lblDayLong.FontSize = 28;
            //this.lblDayNumber.Content = DateTime.Now.Day.ToString().ToUpper();
            //this.lblMonth.Content = DateTime.Now.ToString("dddd").ToUpper();
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
                leftDays = 366 - days;
            }
            else
            {
                leftDays = 365 - days;
            }
            //return weekNum;

            this.sbiUser.Content = "Usuario: " + GlobalModule.ActualUserName;
            this.sbiUserName.Content = GlobalModule.ActualName;
            this.sbiDate.Content = "" + DateTime.Now.ToLongDateString();
            // this.sbiDays.Content = "Semana: " + weekNum.ToString() + "| Dias Transcurridos: " + days.ToString() + " | Quedan: " + leftDays.ToString();
            this.sbiVersion.Content = "Versión: " + version;

            this.sbiIP.Content = "" + GlobalModule.GetLocalIP();

            lblVersion.Content = "versión: " + version;
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

          //  this.dpMain.Visibility = Visibility.Visible;
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


     


     

        //public static Task<bool> ShowAsync()
        //{
        //    var tcs = new TaskCompletionSource<bool>();
        //    var dialog = new DialogWindow(tcs.SetResult);
        //    dialog.Show();
        //    return tcs.Task;
        //}

       
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

        public static string strRUTA_DE_EXPORTACION = @"C:\BIG\";
        public static string strEMPRESA_LRG = @"LRG";
        public static string strEMPRESA_CLT = @"CLT";
        public static string strEMPRESA_GGGC = @"GGGC";
        public static string strARCHIVO_EXCEL = @"Excel";

        public static string exeFolder = "";

        public static byte bytSUCURSAL = 0;
        public static byte bytEMPRESA = 0;

        // public int chkgato = 0;
        //public static int MyFunction()
        //{
        //    return 22;
        //}


        public static string GetSetting(string appName, string section, string key, string sDefault)
        {
            // Los datos de VB se guardan en:
            // HKEY_CURRENT_USER\Software\VB and VBA Program Settings
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"Software\VB and VBA Program Settings\" +
                                                              appName + "\\" + section);
            string s = sDefault;
            if (rk != null)
            {
                s = (string)rk.GetValue(key);
            }
            return s;
        }

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
                strOSVersion = " Major:" + os.Version.Major + " Minor:" + os.Version.Minor + " Revision:" + os.Version.Revision + " Build: " + os.Version.Major;
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
            exeFolder = System.Reflection.Assembly.GetExecutingAssembly().Location;
        }

    }
}

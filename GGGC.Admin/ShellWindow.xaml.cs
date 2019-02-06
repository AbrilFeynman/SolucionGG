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
using GGGC.Admin.ViewModels;

namespace GGGC.Admin
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow
    {
        public static Dictionary<string, UserControl> userControls = new Dictionary<string, UserControl>();

        private TabItemExt lastSelectedItem;
        //Type type1 = null;
        //Assembly assembly1 = null;
        //UserControl uc1501 = (UserControl)assembly1.CreateInstance(string.Format("{0}.ERP.Modules.LRG.Databooks.Views.DatabookView", type1.Namespace));
        private bool blnCrearUserControl = false;
        private bool bln153 = true;
        private readonly IDialogService dialogService;
        private ItemsViewModel iVM;

        private List<TabItemExt> _tabItems;
        private TabItemExt _tabAdd;

        public ShellWindow()
        {
            _tabItems = new List<TabItemExt>();
            this.DataContext = new ShellWindowViewModel();
           
            InitializeComponent();
            Inicio();


            iVM = new ItemsViewModel(ItemsViewModel.Menu.GG);
            tvGG.ItemsSource = iVM;
            tvGG.ExpandAll();


            //var theme = ThemeManager.DetectAppStyle(this);
            //ThemeManager.ChangeAppStyle(this, theme.Item2, ThemeManager.GetAppTheme("BaseDark"));

        }

        private void Inicio()
        {


            //GreenPalette.Palette.ReadOnlyBackgroundColor = GreenPalette.Palette.PrimaryColor;
            //GreenPalette.Palette.ReadOnlyOpacity = 0.6;
           
            
            //BrushConverter converter = new BrushConverter();
            //this.Background = (Brush)converter.ConvertFromString(b.Name);
            //Color color;
            //   ColorConverter converter = new ColorConverter();
            //   var color = (Color)ColorConverter.ConvertFromString("#FF023374");



          //  GreenPalette.LoadPreset(GreenPalette.ColorVariation.Light);
            //GreenPalette.Palette.AccentHighColor = (Color)ColorConverter.ConvertFromString("#FF023374");
            //Gr¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿eenPalette.Palette.AccentLowColor = (Color)ColorConverter.ConvertFromString("#FF0E7FB9");
            //GreenPalette.Palette.AlternativeColor = (Color)ColorConverter.ConvertFromString("#FF1D1E21");
            //GreenPalette.Palette.BasicColor = (Color)ColorConverter.ConvertFromString("#FF474747");
            //GreenPalette.Palette.ComplementaryColor = (Color)ColorConverter.ConvertFromString("#FF444646");
            //GreenPalette.Palette.HighColor = (Color)ColorConverter.ConvertFromString("#FF131313");
            //GreenPalette.Palette.LowColor = (Color)ColorConverter.ConvertFromString("#FF343434");
            //GreenPalette.Palette.MainColor = (Color)ColorConverter.ConvertFromString("#FF1B1B1F");
            //GreenPalette.Palette.MarkerColor = (Color)ColorConverter.ConvertFromString("#FFF1F1F1");
            //GreenPalette.Palette.MouseOverColor = (Color)ColorConverter.ConvertFromString("#FF338F99");
            //GreenPalette.Palette.PrimaryColor = (Color)ColorConverter.ConvertFromString("#FF2B2C2E");
            //GreenPalette.Palette.SelectedColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            //GreenPalette.Palette.SemiAccentLowColor = (Color)ColorConverter.ConvertFromString("#590E8EB9");
            //GreenPalette.Palette.StrongColor = (Color)ColorConverter.ConvertFromString("#FF646464");
            //GreenPalette.Palette.ValidationColor = (Color)ColorConverter.ConvertFromString("#FF00E6BA");



            VisualStudio2013Palette.Palette.AccentColor = (Color)ColorConverter.ConvertFromString("#FF007ACC");
            VisualStudio2013Palette.Palette.AccentDarkColor = (Color)ColorConverter.ConvertFromString("#FF007ACC");
            VisualStudio2013Palette.Palette.AccentMainColor = (Color)ColorConverter.ConvertFromString("#FF3399FF");
            VisualStudio2013Palette.Palette.AlternativeColor = (Color)ColorConverter.ConvertFromString("#FFF5F5F5");
            VisualStudio2013Palette.Palette.BasicColor = (Color)ColorConverter.ConvertFromString("#FFCCCEDB");
            VisualStudio2013Palette.Palette.ComplementaryColor = (Color)ColorConverter.ConvertFromString("#FFDBDDE6");
            VisualStudio2013Palette.Palette.DefaultForegroundColor = (Color)ColorConverter.ConvertFromString("#FF1E1E1E");
            VisualStudio2013Palette.Palette.HeaderColor = (Color)ColorConverter.ConvertFromString("#FF007ACC");
            VisualStudio2013Palette.Palette.MainColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            VisualStudio2013Palette.Palette.MarkerColor = (Color)ColorConverter.ConvertFromString("#FF1E1E1E");
            VisualStudio2013Palette.Palette.MouseOverColor = (Color)ColorConverter.ConvertFromString("#FFC9DEF5");
            VisualStudio2013Palette.Palette.PrimaryColor = (Color)ColorConverter.ConvertFromString("#FFEEEEF2");
            VisualStudio2013Palette.Palette.SelectedColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            VisualStudio2013Palette.Palette.SemiBasicColor = (Color)ColorConverter.ConvertFromString("#66CCCEDB");
            VisualStudio2013Palette.Palette.StrongColor = (Color)ColorConverter.ConvertFromString("#FF717171");
            VisualStudio2013Palette.Palette.ValidationColor = (Color)ColorConverter.ConvertFromString("#FFFF3333");




            //GreenPalette.Palette.highb = (Color)ColorConverter.ConvertFromString("#FF00E6BA");

            //GreenPalette.LoadPreset(GreenPalette.ColorVariation.Light);
            //GreenPalette.Palette.AccentHighColor = (Color)ColorConverter.ConvertFromString("#FF023374");
            //GreenPalette.Palette.AccentLowColor = (Color)ColorConverter.ConvertFromString("#FF0E7FB9");
            //GreenPalette.Palette.AlternativeColor = (Color)ColorConverter.ConvertFromString("#FF1D1E21");
            //GreenPalette.Palette.BasicColor = (Color)ColorConverter.ConvertFromString("#FF474747");
            //GreenPalette.Palette.ComplementaryColor = (Color)ColorConverter.ConvertFromString("#FF444646");
            //GreenPalette.Palette.HighColor = (Color)ColorConverter.ConvertFromString("#FF131313");
            //GreenPalette.Palette.LowColor = (Color)ColorConverter.ConvertFromString("#FF343434");
            //GreenPalette.Palette.MainColor = (Color)ColorConverter.ConvertFromString("#FF1B1B1F");
            //GreenPalette.Palette.MarkerColor = (Color)ColorConverter.ConvertFromString("#FFF1F1F1");
            //GreenPalette.Palette.MouseOverColor = (Color)ColorConverter.ConvertFromString("#FF338F99");
            //GreenPalette.Palette.PrimaryColor = (Color)ColorConverter.ConvertFromString("#FF2B2C2E");
            //GreenPalette.Palette.SelectedColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            //GreenPalette.Palette.SemiAccentLowColor = (Color)ColorConverter.ConvertFromString("#590E8EB9");
            //GreenPalette.Palette.StrongColor = (Color)ColorConverter.ConvertFromString("#FF646464");
            //GreenPalette.Palette.ValidationColor = (Color)ColorConverter.ConvertFromString("#FF00E6BA");



            //GreenPalette.LoadPreset(GreenPalette.ColorVariation.Light);
            //GreenPalette.Palette.AccentHighColor = (Color)ColorConverter.ConvertFromString("#FF023374");
            //GreenPalette.Palette.AccentLowColor = (Color)ColorConverter.ConvertFromString("#FF0E7FB9");
            //GreenPalette.Palette.AlternativeColor = (Color)ColorConverter.ConvertFromString("#FF1D1E21");
            //GreenPalette.Palette.BasicColor = (Color)ColorConverter.ConvertFromString("#FF474747");
            //GreenPalette.Palette.ComplementaryColor = (Color)ColorConverter.ConvertFromString("#FF444646");
            //GreenPalette.Palette.HighColor = (Color)ColorConverter.ConvertFromString("#FF131313");
            //GreenPalette.Palette.LowColor = (Color)ColorConverter.ConvertFromString("#FF343434");
            //GreenPalette.Palette.MainColor = (Color)ColorConverter.ConvertFromString("#FF1B1B1F");
            //GreenPalette.Palette.MarkerColor = (Color)ColorConverter.ConvertFromString("#FFF1F1F1");
            //GreenPalette.Palette.MouseOverColor = (Color)ColorConverter.ConvertFromString("#FF338F99");
            //GreenPalette.Palette.PrimaryColor = (Color)ColorConverter.ConvertFromString("#FF2B2C2E");
            //GreenPalette.Palette.SelectedColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            //GreenPalette.Palette.SemiAccentLowColor = (Color)ColorConverter.ConvertFromString("#590E8EB9");
            //GreenPalette.Palette.StrongColor = (Color)ColorConverter.ConvertFromString("#FF646464");
            //GreenPalette.Palette.ValidationColor = (Color)ColorConverter.ConvertFromString("#FF00E6BA");
            //GreenPalette.Palette.highb = (Color)ColorConverter.ConvertFromString("#FF00E6BA");

            //#FFECECEC
            // GreenPalette.Palette.



            // GreenPalette.Palette.AccentHighColor = (Color)ColorConverter.ConvertFromString("#FF2A579A");
            // GreenPalette.Palette.AlternativeColor = (Color)ColorConverter.ConvertFromString("#FF88C3FF");
            // GreenPalette.Palette.MouseOverColor = (Color)ColorConverter.ConvertFromString("#FF3E6DB6");
            // GreenPalette.Palette.HighColor = (Color)ColorConverter.ConvertFromString("#FF19478A");
            // GreenPalette.Palette.AlternativeColor = (Color)ColorConverter.ConvertFromString("#FFF1F1F1");
            // GreenPalette.Palette.BasicColor = (Color)ColorConverter.ConvertFromString("#FFABABAB");
            // GreenPalette.Palette.ComplementaryColor = (Color)ColorConverter.ConvertFromString("#FFE1E1E1");
            //// GreenPalette.Palette.IconColor = (Color)ColorConverter.ConvertFromString("#FF444444");
            // GreenPalette.Palette.MainColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            // GreenPalette.Palette.MarkerColor = (Color)ColorConverter.ConvertFromString("#FF444444");
            //// GreenPalette.Palette.MarkerInvertedColor = (Color)ColorConverter.ConvertFromString("#FFF9F9F9");
            // GreenPalette.Palette.MouseOverColor = (Color)ColorConverter.ConvertFromString("#FFC5C5C5");
            //// GreenPalette.Palette.PressedColor = (Color)ColorConverter.ConvertFromString("#FFAEAEAE");
            // GreenPalette.Palette.PrimaryColor = (Color)ColorConverter.ConvertFromString("#FFE6E6E6");
            // GreenPalette.Palette.SelectedColor = (Color)ColorConverter.ConvertFromString("#FFEBEBEB");
            // GreenPalette.Palette.ValidationColor = (Color)ColorConverter.ConvertFromString("#FFE81123");


            //if (this.skipInitialThemeVariationTracking)
            //{
            //    this.skipInitialThemeVariationTracking = false;
            //    return;
            //}


            // GreenPalette.LoadPreset(GreenPalette.ColorVariation.Light);
            //EqatecMonitor.Instance.TrackFeature(string.Concat(EqatecConstants.Theme, ".", themeVariation));

            List<TabItem> _tabItems;
            TabItem _tabAdd;

            Type type = this.GetType();
            Assembly assembly = type.Assembly;


            ctrlBusy.IsBusy = true;
            UserControl uc805 = null;
            uc805 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Guadiana.Tickets.Views.MainView", type.Namespace));
            userControls.Add("801", uc805);
            //((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Parent.IsSelected = true;

            
          //  AddDetail(uc805, "Tickets");

            ctrlBusy.IsBusy = false;
        }

        private void MetroWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
           // DisplayLoginScreen();
            // System.Windows.Forms.MessageBox.Show("load");
            // prcOcultarControles();
        }

        private void MetroWindow_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Closing -= MetroWindow_Closing_1;
            //e.Cancel = true;
            //var anim = new System.Windows.Media.Animation.DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(2));
            //anim.Completed += (s, _) => this.Close();
            //this.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        private void OnTreeViewPreviewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var item = e.AddedItems[0] as NavigationNode;
                if (item != null && item.ChildNodes.Count != 0)
                {
                    e.Handled = true;
                }
            }
        }

        private void radTreeViewCatalogs_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            // Get a reference to the treeview
            Telerik.Windows.Controls.RadTreeView treeView = sender as Telerik.Windows.Controls.RadTreeView;
            ObservableCollection<Object> selectedItems = treeView.SelectedItems;
            // selectedItems[0].
            RadTreeViewItem item = selectedItems[0] as RadTreeViewItem;
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

                default:

                    uc805 = (UserControl)assembly.CreateInstance(string.Format("{0}.ERP.Modules.Guadiana.Invoice.Views.MainView", type.Namespace));
                    userControls.Add("805", uc805);
                    //((GGGC.Admin.Menus.BusinessItem)(treeView.SelectedItem)).Parent.IsSelected = true;


                    AddDetail(uc805, "uc 805");

                    break;











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


        private void containerOK_OnCloseButtonClick(object sender, CloseTabEventArgs e)
        {
            if (lastSelectedItem != null)
            {
                MessageBox.Show("Last Selected item: " + lastSelectedItem.Header.ToString());
            }

        }

        public void AddDetail(UserControl detail, string name)
        {

            //    make sure the passed in arguments are good
            Debug.Assert(detail != null, "UserControl detail is null");
            Debug.Assert(name != null, "string name is null");

            //    locate the TabControl that the tab will be added to
            containerOK = (TabControlExt)this.FindName("containerOK");
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
            //containerOK.fil


            //containerOK.Items.Refresh
        }


    }
}

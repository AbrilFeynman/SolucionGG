using System.Linq;
using System.Windows;
using Infragistics.Windows.Ribbon;
using GGGC.UI.Infrastructure;
using Infragistics.Windows.OutlookBar;
using System.Collections;
using MahApps.Metro;
using MahApps.Metro.Controls;
//using ThemeManager = Infragistics.Windows.Themes.ThemeManager;

namespace GGGC.UI
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : MetroWindow
    {
        public Shell(ShellViewModel viewModel)
        {
            InitializeComponent();
           // XamOutlookBar.Theme = "Office2k7Silver";
            //HydrateAllThemeResources("IGTheme");
            //XamOutlookBar.Theme = "IGTheme";


            DataContext = viewModel;
            string lastUsedAccent;
            lastUsedAccent = "Blue";
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(x => x.Name == lastUsedAccent), Theme.Light);
            Infragistics.Windows.OutlookBar.Resources.Customizer.SetCustomizedString("NavigationPaneMinimizedText", "Text when minimized");
         //  this.sbGG.
        }

        public static void HydrateAllThemeResources(string theme)
        {
           // ResourceDictionary rd = Infragistics.Windows.Themes.ThemeManager.GetResourceSet(theme, ThemeManager.AllGroupingsLiteral);
            //AccessAllThemeResources(rd);
        }

        private static void AccessAllThemeResources(ResourceDictionary resources)
        {
            foreach (System.Collections.DictionaryEntry o in resources)
            {
                object x = o.Value;
            }

            foreach (ResourceDictionary rd in resources.MergedDictionaries)
                AccessAllThemeResources(rd);
        }


        private void XamOutlookBar_SelectedGroupChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            var group = ((XamOutlookBar)sender).SelectedGroup as IOutlookBarGroup;
            if (group != null)
            {
                Commands.NavigateCommand.Execute(group.DefaultNavigationPath);
            }
        }

        private void MetroWindow_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
           // this.Close();
        }
    }
}

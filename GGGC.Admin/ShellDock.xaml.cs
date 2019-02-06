using GGGC.Admin.UI.Docking;
using GGGC.Admin.UI.Docking.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;
using MahApps.Metro.Controls;


namespace GGGC.Admin
{
    /// <summary>
    /// Interaction logic for ShellDock.xaml
    /// </summary>
    public partial class ShellDock : MetroWindow
    {
        public ShellDock()
        {
            InitializeComponent();
            InitializeComponent();
            //this.Loaded += OnLoaded;
            Application.Current.Exit += OnApplicationExit;
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
            //var flyout = this.Flyouts.Items[index] as Flyout;
            //if (flyout == null)
            //{
            //    return;
            //}

            //flyout.IsOpen = !flyout.IsOpen;
        }

        private void ShowSettingsLeft(object sender, RoutedEventArgs e)
        {
            //var flyout = (Flyout)this.Flyouts.Items[6];
            //flyout.Position = Position.Left;
        }

        private void ShowSettingsRight(object sender, RoutedEventArgs e)
        {
            //var flyout = (Flyout)this.Flyouts.Items[6];
            //flyout.Position = Position.Right;
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

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
            //var viewModel = this.DataContext as ShellDockViewModel;
            //if (viewModel != null)
            //{
            //    viewModel.Save(this.radDocking);
            //}
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
           // var viewModel = this.DataContext as ShellDockViewModel;
           //// this.da
           // if (viewModel != null)
           // {
           //     viewModel.Load(this.radDocking);
           // }
        }

        private void OnPreviewShowCompass(object sender, PreviewShowCompassEventArgs e)
        {
            bool isRootCompass = e.Compass is RootCompass;
            var splitContainer = e.DraggedElement as RadSplitContainer;
            if (splitContainer != null)
            {
                bool isDraggingDocument = splitContainer.EnumeratePanes().Any(p => p is RadDocumentPane);
                var isTargetDocument = e.TargetGroup == null ? true : e.TargetGroup.EnumeratePanes().Any(p => p is RadDocumentPane);
                if (isDraggingDocument)
                {
                    e.Canceled = isRootCompass || !isTargetDocument;
                }
                else
                {
                    e.Canceled = !isRootCompass && isTargetDocument;
                }
            }
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
    }
}

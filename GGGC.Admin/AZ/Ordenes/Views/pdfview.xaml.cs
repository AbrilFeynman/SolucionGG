using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Telerik.Windows.Documents.Fixed;

namespace GGGC.Admin.AZ.Ordenes.Views
{
    /// <summary>
    /// Interaction logic for pdfview.xaml
    /// </summary>
    public partial class pdfview : Window
    {
        public pdfview(string folio)
        {
            InitializeComponent();
            
            LoadPdfFromUri(folio);
        }


        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // ((ContentControl)this.Parent).Content = null;

            //((ContentControl)this.Parent).Content.
            // var metroWindow = (MetroWindow)Application.Current.MainWindow;

            //Shell.GetCurrentDirectory();

            // Shell.userControls["1501"]
            //Shell.userControls.Remove("1501");
            //Shell.userControls["1501"] = null;

            //var metroWindow = (MetroWindow)Application.Current.MainWindow;
            //var settings = new MetroDialogSettings()
            //{
            //    AffirmativeButtonText = "Aceptar",
            //    AnimateHide = true
            //};
            //var result = await metroWindow.ShowMessageAsync("Grupo Guadiana GC", strMessage, MessageDialogStyle.Affirmative, settings);

            var before = GC.GetTotalMemory(false);
            MessageBox.Show(before.ToString("#,###"));

            //System.Windows.Shell.userControls.Remove("1501");
            //System.Windows.Shell.userControls["1501"] = null;

            ((ContentControl)this.Parent).Content = null;

            //  MessageBox.Show(before.ToString("antes de gc" + "#,###"));

            GC.Collect();

            var after = GC.GetTotalMemory(false);

            //  MessageBox.Show(after.ToString("#,###"));
            // GC.c



        }


        void pdfViewer_DocumentChanged(object sender, EventArgs e)
        {
            // pdfViewer.Commands.FixedDocumentViewer.ScaleFactor = this.ViewModel.InitialScaleFactor;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadFromStream2(sender, e);
        }

        private void LoadFromStream2(object sender, System.Windows.RoutedEventArgs e)
        {
            //Stream str = App.GetResourceStream(new System.Uri("/GGGC.Admin;component/Resources/Files/DataBooks/MICH/Michelin.pdf", System.UriKind.Relative)).Stream;
            //this.pdfViewer.DocumentSource = new PdfDocumentSource(str);


            Stream str = App.GetResourceStream(new System.Uri(@"pack://application:,,,/Resources/Files/DataBooks/MICH/Michelin.pdf")).Stream;
            //  this.pdfViewer.DocumentSource = new PdfDocumentSource(str);

            //LayoutRoot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/App/Backgrounds/suc1.png")));

            //D:\GGGC_sln\GGGC_sln\GGGC.Admin\Resources\Files\DataBooks\MICH
        }

        private void tbCurrentPage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
            }
        }



        private void LoadFromStream(object sender, System.Windows.RoutedEventArgs e)
        {
            Stream str = App.GetResourceStream(new System.Uri(@"pack://application:,,,/Resources/Files/DataBooks/MICH/Michelin.pdf")).Stream;
            //Stream str = App.GetResourceStream(new System.Uri("GGGC.Admin;component/SampleData/Sample.pdf", System.UriKind.Relative)).Stream;
            this.pdfViewer.DocumentSource = new PdfDocumentSource(str);

            this.pdfViewer.ScaleFactor = (this.pdfViewer.ActualWidth + this.pdfViewer.VerticalScrollBar.Width) / this.pdfViewer.CurrentPage.ActualWidth;

        }

        private void LoadPdfFromStream()
        {
            Stream str = App.GetResourceStream(new System.Uri(@"pack://application:,,,/Resources/Files/DataBooks/MICH/Michelin.pdf")).Stream;
            //Stream str = App.GetResourceStream(new System.Uri("GGGC.Admin;component/SampleData/Sample.pdf", System.UriKind.Relative)).Stream;
            this.pdfViewer.DocumentSource = new PdfDocumentSource(str);

            this.pdfViewer.ScaleFactor = .5;//(this.pdfViewer.ActualWidth + this.pdfViewer.VerticalScrollBar.Width) / this.pdfViewer.CurrentPage.ActualWidth;

        }

        private void LoadPdfFromUri(string folio)
        {
            //Stream str = App.GetResourceStream(new System.Uri(@"pack://application:,,,/Resources/Files/DataBooks/MICH/Michelin.pdf")).Stream;
            //Stream str = App.GetResourceStream(new System.Uri("GGGC.Admin;component/SampleData/Sample.pdf", System.UriKind.Relative)).Stream;
            //this.pdfViewer.DocumentSource = new PdfDocumentSource(str);
            //pdfViewer.Commands.
            //try {
            this.pdfViewer.DocumentSource = new PdfDocumentSource(new Uri(@"C:\Ektelesis.Net\CFDI\DATOS\PDF\" + folio + ".pdf"));
            //}
            //catch (Exception x){this.pdfViewer.childre  }

            //  this.pdfViewer.ScaleFactor = 0.5;// (this.pdfViewer.ActualWidth + this.pdfViewer.VerticalScrollBar.Width) / this.pdfViewer.CurrentPage.ActualWidth;

        }

        private void LoadFromUri(object sender, System.Windows.RoutedEventArgs e)
        {
            //this.pdfViewer.DocumentSource = new PdfDocumentSource(new System.Uri("GGGC.Admin;component/SampleData/Sample.pdf", System.UriKind.Relative));

            //this.pdfViewer.DocumentSource = new PdfDocumentSource(new System.Uri(@"pack://application:,,,/Resources/Files/DataBooks/MICH/Michelin.pdf"));

            this.pdfViewer.DocumentSource = new PdfDocumentSource(new Uri("D:\\GGGC_sln\\GGGC_sln\\GGGC.Admin\\Resources\\Files\\DataBooks\\MICH\\Michelin.pdf"));
            //this.pdfViewer.FitToPage();
            //this.pdfViewer.FitToWidth();
            fitToWidthButton.IsChecked = true;

            // fitToWidthButton.Command = FitToWidthFixedDocumentPagesPresenterCommandDescriptor.Command;
            //this.pdfViewer.zo
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Unloaded");
        }




        private void button_Click_1(string folio)
        {

            //Userpdf user = new Userpdf(folio);
            //grids.Children.Clear();
            //grids.Children.Add(user);
            UserControl1 user = new UserControl1(folio);

        }







    }
}

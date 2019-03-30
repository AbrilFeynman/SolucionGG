using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
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

namespace GGGC.Admin.AZ.Compr.Views
{
    /// <summary>
    /// Interaction logic for AddCompra2.xaml
    /// </summary>
    public partial class AddCompra2 : UserControl
    {


        #region Fields
        const int m_columnCount = 9;
        private double m_totalDue;
        private double m_totalCant;
        private IList<InvoiceItem> m_items;
        int m_currentRowIndex = 0;
        int m_selectedIndex = -1;
        int m_prevSelectedIndex = -1;
        const int m_rowHeight = 30;
        Border m_border;
        ProductoDialog m_fieldsPopup;
        AddressDialog m_addressPopup;
        BillingInformation m_billInfo;
        ProductList m_productList;
        ProductoDialog m_producto;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public double TotalDue
        {
            get
            {
                return m_totalDue;
            }
        }

        public double TotalCant
        {
            get
            {
                return m_totalCant;
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public AddCompra2()
        {
            this.InitializeComponent();
            //Initialize
            Initialize();
        }
        /// <summary>
        /// Initialize
        /// </summary>
        private void Initialize()
        {

            m_productList = new ProductList();

            m_items = new List<InvoiceItem>();

            m_border = new Border();

            m_billInfo = new BillingInformation();

            pickerFactura.SelectedDate = System.DateTime.Now;
            pickermercancia.SelectedDate = System.DateTime.Now;
            ////Load price list from XML document
            //m_productList.LoadFromXml();

            //Set Billing information
            this.Name.Text = m_billInfo.Name = "Fran Wilson";
            //  this.BillingAddress.Text = m_billInfo.Address = "89, Chiaroscuro Road, Portland, 97219.";
            this.DueDate.Text = (m_billInfo.Date = DateTime.Now.Date).ToString("d");
            this.InvoiceNumber.Text = m_billInfo.InvoiceNumber = new Random().Next(100, 10000).ToString();
            this.DueDate.Text = (m_billInfo.DueDate = DateTime.Now.Date).ToString("d");

            //InvoiceItem defaultItem = new InvoiceItem() { Descripcion = m_productList[0].Name, Quantity = 1, Rate = m_productList[0].Rate };
            //defaultItem.Iva = m_productList[0].Rate * 0.07;

            //Add an item by default
            //AddItem(defaultItem, false);
        }
        #endregion

        ///// <summary>
        ///// Invoked when this page is about to be displayed in a Frame.
        ///// </summary>
        ///// <param name="e">Event data that describes how this page was reached.  The Parameter
        ///// property is typically used to configure the page.</param>
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    //base.OnNavigatedTo(e);
        //}
        //private void OnGoBack(object parameter)
        //{
        //    //Frame.Navigate(typeof(SampleBrowser.Home));
        //}
        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add(object sender, RoutedEventArgs e)
        {
            m_fieldsPopup = new ProductoDialog();
            //m_fieldsPopup = new CompraDialog(m_productList);
            m_fieldsPopup.CloseRequested += myDialog_CloseRequested;
            m_fieldsPopup.UpdateRequested += myDialog_UpdateRequested;
            m_fieldsPopup.Activated += fieldsPopup_Opened;
            m_fieldsPopup.Background = new SolidColorBrush(Colors.Red);
            RootGrid.Opacity = 0.1;
            m_fieldsPopup.Opacity = 1;
            m_fieldsPopup.ShowDialog();
            RootGrid.Opacity = 1;
        }
    

        private void M_producto_CloseRequested(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fieldsPopup_Opened(object sender, object e)
        {
            (sender as ProductoDialog).InitializeFocus();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void myDialog_UpdateRequested(object sender, EventArgs e)
        {
            InvoiceItem item = (e as FieldsUpdateEventArgs).UpdatedFields;
            AddItem(item, false);
            UpdateTotal();
            
            m_fieldsPopup.Close();
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void myDialog_CloseRequested(object sender, EventArgs e)
        {
            m_fieldsPopup.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoBack(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(SampleBrowser.Home));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete(object sender, RoutedEventArgs e)
        {
            if (m_selectedIndex == -1)
                return;

            RemoveItem(m_selectedIndex);
            this.DeleteButton.IsEnabled = false;
        }

         private void Abrir(object sender, RoutedEventArgs e)
        {
            RootGrid.Opacity = 0.1;
            ProductoDialog win2 = new ProductoDialog();
            win2.Show();
            //window2 win2 = new window2();
            //la progrmacion me ha hecho aprend.
            //win2.Show();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditBillingDetails(object sender, RoutedEventArgs e)
        {
            #region Popup
            m_addressPopup = new AddressDialog(m_billInfo);
            m_addressPopup.CloseRequested += addressDialog_CloseRequested;
            m_addressPopup.UpdateRequested += addressDialog_UpdateRequested;
            //addressDialog.Width = this.ActualWidth;
            //double verticalOff = this.ActualHeight - addressDialog.Height;
            //m_addressPopup.VerticalOffset = verticalOff / 2;
            //m_addressPopup.WindowState = WindowState.Maximized;
            RootGrid.Opacity = 0.1;
            m_addressPopup.Opacity = 1;
            m_addressPopup.ShowDialog();
            RootGrid.Opacity = 1;
            #endregion
        }

        private void EditBillingDetailso(object sender, RoutedEventArgs e)
        {
            #region Popup
            m_producto = new ProductoDialog();
            m_producto.CloseRequested += arddressDialog_CloseRequested;
            m_producto.UpdateRequested += myDialog_UpdateRequested;
            m_producto.Activated += fieldsPopup_Opened;
            RootGrid.Opacity = 0.1;
            m_producto.Opacity = 1;
            m_producto.ShowDialog();
            RootGrid.Opacity = 1;
           
            
            #endregion
        }

      

        private void M_producto_CloseRequested1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void addressDialog_UpdateRequested(object sender, EventArgs e)
        {
            this.Name.Text = m_billInfo.Name;
            //this.BillingAddress.Text = m_billInfo.Address;
            this.DueDate.Text = m_billInfo.Date.ToString("d");
            this.InvoiceNumber.Text = m_billInfo.InvoiceNumber;
            this.DueDate.Text = m_billInfo.DueDate.ToString("d");
            m_addressPopup.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void addressDialog_CloseRequested(object sender, EventArgs e)
         {
            m_addressPopup.Close();
        }

        void arddressDialog_CloseRequested(object sender, EventArgs e)
        {
            m_producto.Close();
           // m_addressPopup.Close();
        }        /// <summary>
                 /// 
                 /// </summary>
                 /// <param name="sender"></param>
                 /// <param name="e"></param>
        private void InvoiceGrid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                InvoiceGrid_DoubleTapped(sender, e);
            }
            else
            {
                InvoiceGrid_Tapped(sender, e);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceGrid_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            FrameworkElement element = null;
            this.DeleteButton.IsEnabled = true;
            if ((element = e.OriginalSource as FrameworkElement) != null)
            {
                m_selectedIndex = Grid.GetRow(element);
            }
            if (m_border != null && m_selectedIndex == m_prevSelectedIndex)
            {
                InvoiceGrid.Children.Remove(m_border);
                m_prevSelectedIndex = -1;
                // m_Border = new Border();
                return;
            }
            m_border.Height = m_rowHeight;
          
            m_border.Background = new SolidColorBrush(Color.FromRgb(102, 153, 255)) { Opacity = 0.4 };
           // m_border.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 08, 71, 96));
            m_border.BorderThickness = new Thickness(1);
            Grid.SetColumnSpan(m_border, m_columnCount);
            Grid.SetRow(m_border, m_selectedIndex);

            if (m_border.Parent == null)
                InvoiceGrid.Children.Add(m_border);
            m_prevSelectedIndex = m_selectedIndex;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceGrid_DoubleTapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FrameworkElement element = null;
            if ((element = e.OriginalSource as FrameworkElement) != null)
            {
                m_selectedIndex = Grid.GetRow(element);
            }
            InvoiceItem invoiceItem = m_items[m_selectedIndex];
            //esta no estaba comentada int selectedProductIndex = m_productList.IndexOf(m_productList[invoiceItem.Descripcion]);
            m_fieldsPopup = new ProductoDialog(invoiceItem, "Edit the Fields");
            m_fieldsPopup.UpdateRequested += EditDialog_UpdateRequested;
            m_fieldsPopup.CloseRequested += EditDialog_CloseRequested;
            m_fieldsPopup.lblTitle.Content = "Editar Articulos";
            RootGrid.Opacity = 0.2;
            m_fieldsPopup.Opacity = 1;
            m_fieldsPopup.ShowDialog();
            RootGrid.Opacity = 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EditDialog_CloseRequested(object sender, EventArgs e)
        {
            m_fieldsPopup.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EditDialog_UpdateRequested(object sender, EventArgs e)
        {
            UpdateGrid();
            m_fieldsPopup.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PDFExport_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.IsHitTestVisible = false;
            MainGrid.Opacity = 0.5;
            //progress.Visibility = Windows.UI.Xaml.Visibility.Visible;

            //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, (() =>
            //    {
            //        PdfExport export = new PdfExport();
            //        export.CreatePDF(m_items, m_billInfo, TotalDue);
            //    }));

            //if (m_items.Count > 0)
            //{
            //    PdfExport export = new PdfExport();
            //    export.CreatePDF(m_items, m_billInfo, TotalDue);
            //}
            //else
            //{
            //    MessageBox.Show("No invoice items found to generate the report!", "Export Cancelled");
            //}

            MainGrid.IsHitTestVisible = true;
            MainGrid.Opacity = 1;
            //progress.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WordExport_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.IsHitTestVisible = false;
            MainGrid.Opacity = 0.5;
            //progress.Visibility = Windows.UI.Xaml.Visibility.Visible;

            //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, (() =>
            //{

            //if (m_items.Count > 0)
            //{
            //    ExportWord exportWord = new ExportWord();
            //    exportWord.CreateWord(m_items, m_billInfo, TotalDue);
            //}
            //else
            //{
            //    MessageBox.Show("No invoice items found to generate the report!", "Export Cancelled");
            //}
            //}));

            MainGrid.IsHitTestVisible = true;
            MainGrid.Opacity = 1;
            //progress.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
      
        private void ExcelExport_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.IsHitTestVisible = false;
            MainGrid.Opacity = 0.5;
            //progress.Visibility = Windows.UI.Xaml.Visibility.Visible;

            //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, (() =>
            //{

            //if (m_items.Count > 0)
            //{
            //    ExportToExcel exportToExcel = new ExportToExcel();
            //    exportToExcel.GenerateReport(m_items, m_billInfo);
            //}
            //else
            //{
            //    MessageBox.Show("No invoice items found to generate the report!", "Export Cancelled");
            //}
            //}));

            MainGrid.IsHitTestVisible = true;
            MainGrid.Opacity = 1;
            //progress.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export(object sender, RoutedEventArgs e)
        {
            //this.BottomAppBar1.IsOpen = true;
        }
       
        private void About(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region HelperMethods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        private RowDefinition CreateRowDefinition(double height)
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(height);
            return rowDefinition;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        private ColumnDefinition CreateColumnDefinition(double width)
        {
            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(width);
            
            return columnDefinition;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="addToGridAlone"></param>
        public void AddItem(InvoiceItem item, bool addToGridAlone)
        {

            
            int rowIndex = m_currentRowIndex;

            if (!addToGridAlone)
                m_items.Add(item);

            if (!(InvoiceGrid.RowDefinitions.Count > m_currentRowIndex))
            {
                InvoiceGrid.RowDefinitions.Add(CreateRowDefinition(m_rowHeight));
                

            }
            DrawBorder(rowIndex);
           
            
            SetCell(rowIndex, 0, item.Codigo.ToString());
            SetCell(rowIndex, 1, item.Descripcion.ToString());
            SetCell(rowIndex, 2, item.Cantidad.ToString()) ;
            SetCell(rowIndex, 3, item.Cantidad.ToString());
            SetCell(rowIndex, 4, "$" + item.Total.ToString("#,###.00", CultureInfo.InvariantCulture));
            SetCell(rowIndex, 5, "$" + item.Rate.ToString("#,###.00", CultureInfo.InvariantCulture));
            SetCell(rowIndex, 6, "$" + item.Iva.ToString("#,###.00", CultureInfo.InvariantCulture));
            SetCell(rowIndex, 7, "$" + item.Total.ToString("#,###.00", CultureInfo.InvariantCulture));
            SetCell(rowIndex, 8, "$" + item.Iva.ToString("#,###.00", CultureInfo.InvariantCulture));

            //<Rectangle Grid.Row="0" Height="1" StrokeThickness="0.75" VerticalAlignment="Bottom" Grid.ColumnSpan="5" 
            //StrokeDashArray="4,4" Stroke="#FFCECECE"></Rectangle>

            m_totalDue += Convert.ToDouble(item.Total);
            m_totalCant += Convert.ToDouble(item.Cantidad);
            m_currentRowIndex++;
            m_selectedIndex = m_items.Count;
            UpdateTotal();
            UpdateCantidad();

        }
        private void DrawBorder(int rowIndex)
        {
            Rectangle rect = new Rectangle();
            rect.Height = 1;
            rect.StrokeThickness = 0.75;
            rect.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            rect.StrokeDashArray.Add(4);
            rect.StrokeDashArray.Add(4);
            rect.Stroke = new SolidColorBrush(Color.FromArgb(255, 206, 206, 206));
            Grid.SetColumn(rect, 0);
           
            Grid.SetColumnSpan(rect, 9);
            Grid.SetRow(rect, rowIndex);
            InvoiceGrid.Children.Add(rect);
        }
 
        private void SetCell(int rowIndex, int columnIndex, string value)
        {
            if ( columnIndex == 4 || columnIndex == 5 || columnIndex == 6 || columnIndex == 7 || columnIndex == 8)
            {
                Grid amountGrid = new Grid();
                Grid.SetColumn(amountGrid, columnIndex);
                Grid.SetRow(amountGrid, rowIndex);
                InvoiceGrid.Children.Add(amountGrid);
                amountGrid.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                if (columnIndex == 2)
                    amountGrid.ColumnDefinitions.Add(new ColumnDefinition());
                amountGrid.ColumnDefinitions.Add(new ColumnDefinition());
                amountGrid.ColumnDefinitions.Add(new ColumnDefinition());
                //amountGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength( new double(),GridUnitType.Auto)});

                //Dollar
                TextBlock textBlockDollor = null;
                textBlockDollor = new TextBlock();
                textBlockDollor.Text = "$";
                textBlockDollor.FontSize = 13;
                textBlockDollor.FontFamily = new FontFamily("Segoe UI");
                textBlockDollor.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
                //textBlockDollor.Foreground = new SolidColorBrush(Colors.Black);
                textBlockDollor.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                textBlockDollor.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                textBlockDollor.TextAlignment = TextAlignment.Right;
                if (columnIndex == 2)
                    Grid.SetColumn(textBlockDollor, 1);
                else
                    Grid.SetColumn(textBlockDollor, 0);
                Grid.SetRow(textBlockDollor, 0);
                amountGrid.Children.Add(textBlockDollor);
                //Data
                TextBlock textBlock = null;
                textBlock = new TextBlock();
                textBlock.Text = value.Trim('$');
                textBlock.FontSize = 13;
                textBlock.FontFamily = new FontFamily("Segoe UI");
                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
                //HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
                if (columnIndex == 0 )
                    textBlock.Padding = new Thickness(0, 0, 0, 0);
               
                else
                    textBlock.Padding = new Thickness(0, 0, 0, 0);
                //textBlock.Foreground = new SolidColorBrush(Colors.Black);
                textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                if (columnIndex > 0)
                    textBlock.TextAlignment = TextAlignment.Right;
                if (columnIndex == 2)
                    Grid.SetColumn(textBlock, 2);
                else
                    Grid.SetColumn(textBlock, 1);
                Grid.SetRow(textBlock, 0);
                amountGrid.Children.Add(textBlock);
            }
            else
            {
                //Data
                TextBlock textBlock = null;
                textBlock = new TextBlock();
                textBlock.Text = value.Trim('$');
                textBlock.FontSize = 13;
                textBlock.FontFamily = new FontFamily("Segoe UI");
                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
                //HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
                if (columnIndex == 0)
                    textBlock.Padding = new Thickness(0, 0, 0, 0);
                else
                    textBlock.Padding = new Thickness(0, 0, 0, 0);
                //textBlock.Foreground = new SolidColorBrush(Colors.Black);
                textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                if (columnIndex > 0)
                    textBlock.TextAlignment = TextAlignment.Right;

                SetCell(rowIndex, columnIndex, textBlock);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="textBlock"></param>
        private void SetCell(int rowIndex, int columnIndex, TextBlock textBlock)
        {
            Grid.SetColumn(textBlock, columnIndex);
            Grid.SetRow(textBlock, rowIndex);
            InvoiceGrid.Children.Add(textBlock);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void RemoveItem(int index)
        {
            if (index < 0)
                return;
            if (InvoiceGrid.Children.Count <= 0)
                return;
            if (index < m_items.Count)
            {
                m_items.RemoveAt(index);
                UpdateGrid();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void UpdateGrid()
        {
            m_totalDue = 0;
            m_totalCant = 0;
            m_currentRowIndex = 0;
            InvoiceGrid.Children.Clear();
            //InvoiceGrid.RowDefinitions.RemoveAt(InvoiceGrid.RowDefinitions.Count - 1);
            for (int i = 0; i < InvoiceGrid.RowDefinitions.Count; i++)
                DrawBorder(i);
            foreach (InvoiceItem item in m_items)
            {
                AddItem(item, true);
            }
            UpdateTotal();
            UpdateCantidad();
        }
        /// <summary>
        /// 
        /// </summary>
        void UpdateTotal()
        {
            this.TotalAmount.Text = "$" + TotalDue.ToString("#,###.00", CultureInfo.InvariantCulture);
        }


        void UpdateCantidad()
        {
            this.TotalCantidad.Text =  TotalCant.ToString();
        }


      
        #endregion

    }
}

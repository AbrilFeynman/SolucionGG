using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;
using Telerik.Windows.Controls;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.ObjectModel;

namespace GGGC.Admin.AZ.Ordenes.Views
{
    /// <summary>
    /// Interaction logic for OrdenView.xaml
    /// </summary>
    public partial class OrdenView : UserControl
    {
        bool onInit;
        OrdenItem m_invoiceItem;
        double rate = 0;
        #region Fields
        const int m_columnCount = 9;
        private double m_totalDue;
        private double m_totalCant;
        int osomaloso = 0;
        private IList<OrdenItem> m_items;
        int m_currentRowIndex = 0;
        int m_selectedIndex = -1;
        int m_prevSelectedIndex = -1;
        const int m_rowHeight = 30;
        Border m_border;
        OrdenDialog m_fieldsPopup;
        OrderCliente m_fieldsCliente;
        Cliente m_cliente;
        int estrella = 1;
        string item_cliente = "";
        string item_telefono = "";
        string item_direccion = "";
        string m_numerodefolio = "";
        bool paq = false;
        string paq_code = "";
        string nivelpaq = "";
        OrdenDialog m_producto;
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

        //private void OnKeyDownHandler(object sender, KeyEventArgs e)
        //{
        //   
        //}
        #endregion
        public OrdenView()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
            // Sets the UI culture to French (France)
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-MX");



            InitializeComponent();
            Initialize();
            llenarcombos();
            llenarcombazo();
        }

        private void llenarcombos()
        {
            DateRecepcionH.SelectedTime = DateTime.Now;
            DateEntregaH.SelectedTime = DateTime.Now;
            CapturaH.SelectedTime = DateTime.Now;
          

        }

            DataTable tabla = new DataTable();

        private void Initialize()
        {
           

            Random r = new Random();
            var x = r.Next(1000000, 9000000);
            string Od = x.ToString();
            osomaloso = x;

            GlobalId o = new GlobalId();
            GlobalId.Identificador = Od;
            //m_productList = new ProductList();
            //  GGGC.Admin.App.
            // tabPendientes.
            //empezar los datapickers
            DateRecepcion.SelectedDate = DateTime.Now;
            DateEntrega.SelectedDate = DateTime.Now;
            DateCaptura.SelectedDate = DateTime.Now;
            m_items = new List<OrdenItem>();

            m_border = new Border();
            tabla.Columns.Add("Id", typeof(string));
            tabla.Columns.Add("Renglon", typeof(int));
            tabla.Columns.Add("Codigo", typeof(string));
            tabla.Columns.Add("Descripcion", typeof(string));
            tabla.Columns.Add("Cantidad", typeof(string));
            tabla.Columns.Add("Preciolista", typeof(string));
            tabla.Columns.Add("Nivel", typeof(string));
            tabla.Columns.Add("Precioventa", typeof(string));
            tabla.Columns.Add("PrecioExtendido", typeof(string));
            tabla.Columns.Add("Empleado",typeof(int));


            llenarConsulta();

        }




        private void llenarConsulta()
        {
            try
            {

                string conect = "SERVER = gggctserver.database.windows.net; DATABASE =devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099";
                SqlConnection con = new SqlConnection(conect);

                string cmd = "Select OrderHeader.OrderID, OrderHeader.CustomerID, OrderHeader.RFC, OrderHeader.OrderDate," +
                                                      " Clientes_Frecuentes.Nombre + ' ' + " +
                                                       " Clientes_Frecuentes.Apellido_Paterno + ' ' + Clientes_Frecuentes.Apellido_Materno as Nombre, " +
                                                       " Clientes_Frecuentes.Direccion + ' ' + Clientes_Frecuentes.Colonia + ' ' + Clientes_Frecuentes.Ciudad as Direccion " +
                                                       " From OrderHeader Inner join  Clientes_Frecuentes" +
                                                       "  ON OrderHeader.CustomerID = Clientes_Frecuentes.Id_Radial order by OrderHeader.OrderDate desc";
                                                       


                con.Open();

                DataTable dtbl = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);

                DataSet dsPubs = new DataSet("Pubs");

                sda.Fill(dsPubs, "OrderHeader");

                dtbl = dsPubs.Tables["OrderHeader"];

                InvoiceOrder.ItemsSource = dsPubs.Tables["OrderHeader"].DefaultView;
                //dataGrid2.Columns[0].IsVisible = false;

                con.Close();
            }

            catch (SqlException ex)
            {

                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Revise su conexón a internet.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }

        }

        private int fncalllevel(string paquete, string nivel)
        {
            int noart = 0;
            string conect = "SERVER = 192.168.200.10; DATABASE = Punto_De_venta; USER ID = sa; PASSWORD = dgo2007";
            SqlConnection con = new SqlConnection(conect);

            string cmd = " SELECT Paquetes_Contenido.Codigo_De_Articulo, Precios.Precio FROM" +
           "  Paquetes_Contenido LEFT OUTER JOIN  Precios ON Paquetes_Contenido.Codigo_De_Articulo =" +
           " Precios.Codigo_De_Articulo WHERE(Paquetes_Contenido.Codigo_De_Paquete = '"+paquete+"') AND (Precios.Nivel_De_Precios = '"+nivel+"')";

            try { con.Open(); }
            catch (SqlException ex) { MessageBox.Show("Revise su conexión a internet"); }

            SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);

            DataSet dsPubs = new DataSet("Pubs");

            sda.Fill(dsPubs, "LLantas");
            DataTable DtblPaq = new DataTable();
            DtblPaq = dsPubs.Tables["LLantas"];
            con.Close();
            noart = DtblPaq.Rows.Count;

            return noart;

        }

        private void Add(object sender, RoutedEventArgs e)
        {
            
           
            //VALIDACIONES ANTES DE GUARDAR
            if (combo.SelectedIndex > -1 && radComboNivel.SelectedIndex >-1)
            {
                var objPrecio = radComboNivel.SelectedItem;
                nivelpaq = Convert.ToString(((System.Data.DataRowView)objPrecio).Row.ItemArray[1]);
                if (paq == true)
                {
                    int noart = 0;
                    int nopaqt = 0;
                    noart = fncalllevel(paq_code, nivelpaq);
                    //seleccionar toda la tabla 
                    

                    string conect = "SERVER = 192.168.200.10; DATABASE = Punto_De_venta; USER ID = sa; PASSWORD = dgo2007";
                    SqlConnection con = new SqlConnection(conect);

                    string cmd = "SELECT     Paquetes_Contenido.*, Articulos.Descripcion AS Descripcion " +
                        "FROM Paquetes_Contenido INNER JOIN Articulos ON Paquetes_Contenido.Codigo_De_Articulo =" +
                        " Articulos.Codigo_De_Articulo WHERE(Paquetes_Contenido.Codigo_De_Paquete = '" + paq_code + "')";
                    try { con.Open(); }
                    catch (SqlException ex) { MessageBox.Show("Revise su conexión a internet"); }

                    SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);

                    DataSet dsPubs = new DataSet("Pubs");

                    sda.Fill(dsPubs, "LLantas");
                    DataTable DtblPaq = new DataTable();
                    DtblPaq = dsPubs.Tables["LLantas"];
                    con.Close();
                    nopaqt = DtblPaq.Rows.Count;
                    //por cada elemento agregarlo a los renglones 

                    if (noart<nopaqt)
                    { MessageBox.Show("No todos los articulos contienen el nivel seleccionado"); }
                    else
                    {
                        foreach (DataRow Registro in DtblPaq.Rows)
                        {
                            m_invoiceItem.Nivel = nivelpaq;
                            m_invoiceItem.Cantidad = Convert.ToInt32(Registro[2]);
                            m_invoiceItem.Codigo = Registro[1].ToString();
                            m_invoiceItem.Descripcion = Registro[4].ToString();
                            DataTable tblprecios = new DataTable();

                            tblprecios = PaqPrecios(m_invoiceItem.Codigo);
                            // string oso = tblprecios.Select("Nivel_De_Precios = PL").ToString();

                            DataRow[] resultss = tblprecios.AsEnumerable().Where(row => row.Field<string>("Nivel_De_Precios").Contains("PL")).Select(row => row).ToArray<DataRow>();
                            var ratex = Convert.ToInt32(resultss[0][2]);
                            DataRow[] resultsx = tblprecios.AsEnumerable().Where(row => row.Field<string>("Nivel_De_Precios").Contains(nivelpaq)).Select(row => row).ToArray<DataRow>();
                            var ratexc = Convert.ToInt32(resultsx[0][2]);

                            m_invoiceItem.Preciolista = ratex;
                            m_invoiceItem.Rate = ratexc;

                            m_invoiceItem.Total = m_invoiceItem.Cantidad * m_invoiceItem.Rate;
                            OrdenItem item = m_invoiceItem;
                            AddItem(item, false);
                            UpdateTotal();
                            agreItem(item);
                            clnart();

                        }

                        Cantidad.IsReadOnly = false;

                    }

                   

                }
                else
                {
                    OrdenItem item = m_invoiceItem;
                    AddItem(item, false);
                    UpdateTotal();
                    agreItem(item);
                    clnart();
                }
                
              


            }
            else
            {

                MessageBox.Show("Llene todos los campos de articulo."); 

            }

            // m_fieldsPopup.Close();

            //limpiar clase y controles







        }
        private void clnart()
        {
            combo.SelectedIndex = -1;
            combo.SelectedValue = null;
            descript.Text = "";
            radComboNivel.SelectedIndex = -1;
            precioCodigo.Text = "";
            Cantidad.Value = 1;
            Total.Content = "Total";

            rate = 0.00;
        }
        private void RadButton_Click(object sender, RoutedEventArgs e)

        {
            var objtitem = InvoiceOrder.SelectedItem;
            string folio = ((System.Data.DataRowView)objtitem).Row.ItemArray[0].ToString();

           
            DataTable tablaOrden = GetHeader(folio);
            DataTable tablaDeatil = GetDetail(folio);
            Ordenprint Orden = new Ordenprint(tablaOrden, tablaDeatil);



         
            System.Drawing.Printing.PrinterSettings printerSettings
              = new System.Drawing.Printing.PrinterSettings();


            System.Drawing.Printing.PrintController standardPrintController =
                new System.Drawing.Printing.StandardPrintController();


            Telerik.Reporting.Processing.ReportProcessor reportProcessor
                = new Telerik.Reporting.Processing.ReportProcessor();

            reportProcessor.PrintController = standardPrintController;

            Telerik.Reporting.TypeReportSource typeReportSource =
                new Telerik.Reporting.TypeReportSource();



            reportProcessor.PrintReport(Orden, printerSettings);




        }


        private void descargar(string folio)
        {

            try
            {

                var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=gggcbig;AccountKey=CQBHPdnT3EFKPxLLKcm7sWSyx6/l90Xj1FJ9q8ia69pHLRRFnahEdfLXCOGDfc+7PzE+Ck/rniwJ+OHQh+i00Q==;EndpointSuffix=core.windows.net");
                //var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));
                var myClient = account.CreateCloudBlobClient();
                var container = myClient.GetContainerReference("erp");
                container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

                //lines modified
                var blockBlob = container.GetBlockBlobReference(folio);
                string documento = @"C:\Ektelesis.Net\CFDI\DATOS\PDF\" + folio + ".pdf";
                using (var fileStream = System.IO.File.OpenWrite(documento))
                {
                    blockBlob.DownloadToStream(fileStream);
                }





            }
            catch (Exception E)
            { //MessageBox.Show("Revise su conexión"); 

            }



        }





        private void M_producto_CloseRequested(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void fieldsPopup_Opened(object sender, object e)
        {
            (sender as OrdenDialog).InitializeFocus();
        }

        void fieldsPopup_Openedc(object sender, object e)
        {
            (sender as OrderCliente).InitializeFocus();
        }

        void myDialog_UpdateRequested(object sender, EventArgs e)
        {


            OrdenItem item = (e as FieldsUpdateEventArgs).UpdatedFields;
            AddItem(item, false);
            UpdateTotal();
            agreItem(item);
            m_fieldsPopup.Close();
        }

        void myDialog_UpdateRequestedc(object sender, EventArgs e)
        {


            ClienteItem itemc = (e as FieldsUpdateEventArgss).UpdatedFields;
            AddItemc(itemc, false);


            m_fieldsCliente.Close();
        }



        public void AddItemc(ClienteItem item, bool addToGridAlone)
        {

            txtradial.Text = item.Numero_De_Cliente.ToString();
            item_telefono = item.Telefono.ToString();
            item_cliente = item.Nombre_De_Cliente.ToString();
            txtrfc.Text = item.RFC.ToString();
            item_direccion = item.Direccion.ToString();

               

        }

            void myDialog_CloseRequested(object sender, EventArgs e)
        {
            m_fieldsPopup.Close();
        }
        void myDialog_CloseRequestedC(object sender, EventArgs e)
        {
            m_fieldsCliente.Close();
        }



        private void Delete(object sender, RoutedEventArgs e)
        {
            if (tabla.Rows.Count == 0)
            {

            }
            else
            {
                tabla.Rows[m_selectedIndex].Delete();
                if (m_selectedIndex == -1)
                    return;

                RemoveItem(m_selectedIndex);
                //this.DeleteButton.IsEnabled = false;
            }
            
           
        }

        private void Abrir(object sender, RoutedEventArgs e)
        {
            RootGrid.Opacity = 0.1;
            OrdenDialog win2 = new OrdenDialog();
            win2.Show();
            //window2 win2 = new window2();
            //la progrmacion me ha hecho aprend.
            //win2.Show();

        }




        private void M_producto_CloseRequested1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


       


        private void InvoiceGrid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (e.ClickCount == 2)
            //{
            //    InvoiceGrid_DoubleTapped(sender, e);
            //}
            //else
            //{
            //    InvoiceGrid_Tapped(sender, e);
            //}

        }


        private void InvoiceGrid_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            FrameworkElement element = null;
           // this.DeleteButton.IsEnabled = true;
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

        private void InvoiceGrid_DoubleTapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FrameworkElement element = null;
            if ((element = e.OriginalSource as FrameworkElement) != null)
            {
                m_selectedIndex = Grid.GetRow(element);
            }
            OrdenItem invoiceItem = m_items[m_selectedIndex];
            //esta no estaba comentada int selectedProductIndex = m_productList.IndexOf(m_productList[invoiceItem.Descripcion]);
            m_fieldsPopup = new OrdenDialog(invoiceItem, "Edit the Fields");
            m_fieldsPopup.UpdateRequested += EditDialog_UpdateRequested;
            m_fieldsPopup.CloseRequested += EditDialog_CloseRequested;
            m_fieldsPopup.lblTitle.Content = "Editar Articulos";
            RootGrid.Opacity = 0.2;
            m_fieldsPopup.Opacity = 1;
            m_fieldsPopup.ShowDialog();
            RootGrid.Opacity = 1;
        }


        void EditDialog_CloseRequested(object sender, EventArgs e)
        {
            m_fieldsPopup.Close();
        }

        void EditDialog_UpdateRequested(object sender, EventArgs e)
        {
            UpdateGrid();
            m_fieldsPopup.Close();
        }

        private RowDefinition CreateRowDefinition(double height)
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(height);
            return rowDefinition;
        }

        private ColumnDefinition CreateColumnDefinition(double width)
        {
            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(width);

            return columnDefinition;
        }

       

        public void AddItem(OrdenItem item, bool addToGridAlone)
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
            SetCell(rowIndex, 2, item.Cantidad.ToString());
            SetCell(rowIndex, 3, item.Preciolista.ToString("#,###.00", CultureInfo.InvariantCulture));
            SetCell(rowIndex, 4, item.Nivel.ToString());
            SetCell(rowIndex, 5, "$" + item.Rate.ToString("#,###.00", CultureInfo.InvariantCulture));
            SetCell(rowIndex, 6, "$" + item.Total.ToString("#,###.00", CultureInfo.InvariantCulture));
            SetCell(rowIndex, 7, item.Codigo.ToString());
            //SetCell(rowIndex, 7, "$" + item.Total.ToString("#,###.00", CultureInfo.InvariantCulture));
            //SetCell(rowIndex, 8, "$" + item.Iva.ToString("#,###.00", CultureInfo.InvariantCulture));

            //<Rectangle Grid.Row="0" Height="1" StrokeThickness="0.75" VerticalAlignment="Bottom" Grid.ColumnSpan="5" 
            //StrokeDashArray="4,4" Stroke="#FFCECECE"></Rectangle>

            m_totalDue += Convert.ToDouble(item.Total);
            m_totalCant += Convert.ToDouble(item.Cantidad);
            m_currentRowIndex++;
            m_selectedIndex = m_items.Count;
            UpdateTotal();
            UpdateCantidad();

           
             }

        private void agreItem(OrdenItem item)
        {
            m_cliente = new Cliente();
            item.Marca = m_cliente.Marci;

            //item.Numero_De_Documento = GlobalId.Identificador;
            item.Numero_De_Documento = osomaloso.ToString();
            item.Renglon = estrella;
           // item.Renglon = tabla.Rows.Count;
            tabla.Rows.Add(item.Numero_De_Documento,item.Renglon,item.Codigo, item.Descripcion, item.Cantidad, item.Preciolista, item.Nivel, item.Rate, item.Total,item.Empleado);
            estrella = estrella+1;
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
            if (columnIndex == 3 || columnIndex == 5 || columnIndex == 6)
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
                textBlockDollor.FontSize = 12;
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
                textBlock.FontSize = 12;
                textBlock.FontFamily = new FontFamily("Segoe UI");
                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
                //HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
                if (columnIndex == 0)
                    textBlock.Padding = new Thickness(10, 0, 0, 0);

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
            else if (columnIndex == 2 || columnIndex == 4)
            {
                TextBlock textBlock = null;
                textBlock = new TextBlock();
                textBlock.Text = value.Trim('$');
                textBlock.FontSize = 12;
                textBlock.FontFamily = new FontFamily("Segoe UI");
                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
                //HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
                if (columnIndex == 0)
                    textBlock.Padding = new Thickness(10, 0, 0, 0);
                else
                    textBlock.Padding = new Thickness(0, 0, 15, 0);
                //textBlock.Foreground = new SolidColorBrush(Colors.Black);
                textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                if (columnIndex > 0)
                    textBlock.TextAlignment = TextAlignment.Center;

                SetCell(rowIndex, columnIndex, textBlock);


            }

            else if (columnIndex == 7)
            {
                Button button1 = null;
                button1 = new Button();
                button1.Content = "X";
                button1.Click += borrar;
                SetCell1(rowIndex, columnIndex, button1);

            }
            else
            {
                //Data
                TextBlock textBlock = null;
                textBlock = new TextBlock();
                textBlock.Text = value.Trim('$');
                textBlock.FontSize = 12;
                textBlock.FontFamily = new FontFamily("Segoe UI");
                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
                //HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
                if (columnIndex == 0)
                    textBlock.Padding = new Thickness(10, 0, 0, 0);
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

        public void borrar(object sender, RoutedEventArgs e)
        {
            if (tabla.Rows.Count == 0)
            {

            }
            else
            {
                FrameworkElement element = null;
                //this.DeleteButton.IsEnabled = true;
                if ((element = e.OriginalSource as FrameworkElement) != null)
                {
                    m_selectedIndex = Grid.GetRow(element);
                }
                







                tabla.Rows[m_selectedIndex].Delete();
                if (m_selectedIndex == -1)
                    return;

                RemoveItem(m_selectedIndex);
                //this.DeleteButton.IsEnabled = false;
            }

        }
        private void SetCell(int rowIndex, int columnIndex, TextBlock textBlock)
        {
            Grid.SetColumn(textBlock, columnIndex);
            Grid.SetRow(textBlock, rowIndex);
            InvoiceGrid.Children.Add(textBlock);
        }

        private void SetCell1(int rowIndex, int columnIndex, Button textBlock)
        {
            Grid.SetColumn(textBlock, columnIndex);
            Grid.SetRow(textBlock, rowIndex);
            InvoiceGrid.Children.Add(textBlock);
        }


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

        private void UpdateGrid()
        {
            m_totalDue = 0;
            m_totalCant = 0;
            m_currentRowIndex = 0;
            InvoiceGrid.Children.Clear();
            //InvoiceGrid.RowDefinitions.RemoveAt(InvoiceGrid.RowDefinitions.Count - 1);
            for (int i = 0; i < InvoiceGrid.RowDefinitions.Count; i++)
                DrawBorder(i);
            foreach (OrdenItem item in m_items)
            {
                AddItem(item, true);
            }
            UpdateTotal();
            UpdateCantidad();
        }

        void UpdateTotal()
        {
            this.TotalAmount.Text = "$" + TotalDue.ToString("#,###.00", CultureInfo.InvariantCulture);
            double totalisimo = TotalDue;
            double ttoal = totalisimo * 1.16;
            double ivas = totalisimo * 0.16;
            TotalIva.Text = "$" + ttoal.ToString("#,###.00", CultureInfo.InvariantCulture);
            TotalIva_Copy.Text = ttoal.ToString();
            Iva.Text = "$" + ivas.ToString("#,###.00", CultureInfo.InvariantCulture);
            Iva_Copy.Text = ivas.ToString();
        }

        void UpdateCantidad()
        {
          // this.TotalCantidad.Text = TotalCant.ToString();
        }


        public int fncObtenAccesorios1()
        {
            int bytValor;
            bytValor = 0;

            if (chkmaneral.IsChecked == true)
                bytValor = bytValor + 4096;
            if (chkllave.IsChecked == true)
                bytValor = bytValor + 2048;
            if (chkgato.IsChecked == true)
                bytValor = bytValor + 1024;
            if (chktaponradiador.IsChecked == true)
                bytValor = bytValor + 512;
            if (chktaponaceite.IsChecked == true)
                bytValor = bytValor + 256;
            if (chkvarilla.IsChecked == true)
                bytValor = bytValor + 128;
            if (chkestuche.IsChecked == true)
                bytValor = bytValor + 64;
            if (chktriangulo.IsChecked == true)
                bytValor =bytValor + 32;
            if (chkllrefa.IsChecked == true)
                bytValor = bytValor + 16;
            if (chkfiltroaire.IsChecked == true)
                bytValor = bytValor + 8;
            if (chkllbateri.IsChecked == true)
                bytValor = bytValor + 4;
            if (chkclax.IsChecked == true)
                bytValor = bytValor + 2;
            return bytValor;
        }
      


        public int fncObtenExteriores1()
        {
            int bytValor;
            bytValor = 0;

            //QUITARELCONVERT
            if (chkemblema.IsChecked == true)
                bytValor = bytValor + 4096;
            if (chkcuarto.IsChecked == true)
                bytValor = bytValor + 2048;
            if (chkparabrisa.IsChecked == true)
                bytValor =bytValor + 1024;
            if (chkcarroseria.IsChecked == true)
                bytValor = bytValor + 512;
            if (chktapon.IsChecked == true)
                bytValor = bytValor + 256;
            if (chkbocina.IsChecked == true)
                bytValor = bytValor + 128;
            if (chkmoldeduras.IsChecked == true)
                bytValor = bytValor + 64;
            if (chktapas.IsChecked == true)
                bytValor = bytValor + 32;
            if (chkcristales.IsChecked == true)
                bytValor = bytValor + 16;
            if (chkespejo.IsChecked == true)
                bytValor = bytValor + 8;
            if (chkantena.IsChecked == true)
                bytValor = bytValor + 4;
            if (chkluces.IsChecked == true)
                bytValor = bytValor + 2;
            return bytValor;
        }


        public int fncObtenPendientes()
        {
            int bytValor;
            bytValor = 0;

            if (pcambioaceite.IsChecked == true)
                bytValor = bytValor + 128;
            if (pfrenos.IsChecked == true)
                bytValor = bytValor + 64;
            if (psuspension.IsChecked == true)
                bytValor = bytValor + 32;
            if (pamortiguadores.IsChecked == true)
                bytValor = bytValor + 16;
            if (pbalanceo.IsChecked == true)
                bytValor = bytValor + 8;
            if (palineacion.IsChecked == true)
                bytValor = bytValor + 4;
            if (pllantas.IsChecked == true)
                bytValor = bytValor + 2;
            return bytValor;
        }



        public int fncObtenInteriores1()
        {
            int bytValor;
            bytValor = 0;

            if (chkbtninte.IsChecked == true)
                bytValor = bytValor + 4096;
            if (chkcenicero.IsChecked == true)
                bytValor = bytValor + 2048;
            if (chkcale.IsChecked == true)
                bytValor = bytValor + 1024;
            if (chkvestidura.IsChecked == true)
                bytValor = bytValor + 512;
            if (chktapetes.IsChecked == true)
                bytValor = bytValor + 256;
            if (chkmanijas.IsChecked == true)
                bytValor = bytValor + 128;
            if (chkcinturon.IsChecked == true)
                bytValor = bytValor + 64;
            if (chkespejoretro.IsChecked == true)
                bytValor = bytValor + 32;
            if (chkencendedor.IsChecked == true)
                bytValor = bytValor + 16;
            if (chkbocinas.IsChecked == true)
                bytValor = bytValor + 8;
            if (chkradio.IsChecked == true)
                bytValor =bytValor + 4;
            if (chktablero.IsChecked == true)
                bytValor =bytValor + 2;
            return bytValor;
        }
       

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string trecepcion = DateRecepcionH.SelectedTime.Value.ToString("HH:mm:ss");
            string tEntrega = DateEntregaH.SelectedTime.Value.ToString("HH:mm:ss");
            int rampa = 0;
            if (string.IsNullOrWhiteSpace(Rampa.Text))
            {  }
            else
            {
             
                rampa = Convert.ToInt32(Rampa.Text);
            }
            
            int bytExte1 = fncObtenExteriores1();
            if (tabla.Rows.Count > 0 && txtradial.Text.Length > 4 )
            {
                if (trecepcion != tEntrega)
                {
                    if (bytExte1 >1)
                    {
                        if (rampa > 0 & rampa < 5)
                        {
                            SaveHeader();

                            SaveDetail();
                            //Limpiar botones
                            cleanscreen();
                            DateRecepcionH.SelectedTime = DateTime.Now;
                            CapturaH.SelectedTime = DateTime.Now;
                            DateEntregaH.SelectedTime = DateTime.Now;
                            string oso = m_numerodefolio;
                            llenarConsulta();
                            Window1 form = new Window1(oso);
                            form.ShowDialog();
                        }
                        else
                        {

                            MessageBox.Show("Número de rampa no disponible");

                        }


                       
                    }
                    else
                    {
                        MessageBox.Show("Registre los exteriores del vehiculo.");
                    }


                }

                else
                {
                    MessageBox.Show("La hora de entrega debe ser diferente a la hora de recepción");

                }

               


            }
            else
            {
                MessageBox.Show("La orden de patio no debe de estar vacia.");

            }
            

        }


        private void AnioValidationTextBoxx(object sender, TextCompositionEventArgs e)
        {

            //ONLY NUMBERS

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);




        }

        private void GasoValidationTextBoxx(object sender, TextCompositionEventArgs e)
        {

            //ONLY NUMBERS

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);




        }


        private void RampaValidationTextBoxx(object sender, TextCompositionEventArgs e)
        {

            //ONLY NUMBERS

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);




        }


        private void KmValidationTextBoxx(object sender, TextCompositionEventArgs e)
        {

            //ONLY NUMBERS

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);




        }

        private void cleanscreen()
        {
            InvoiceGrid.Children.Clear();
            m_items = new List<OrdenItem>();
           
            UpdateGrido();
            osomaloso++;
            m_fieldsCliente = new OrderCliente();
            txtradial.Text = "";
            txtrfc.Text = "";
            DateRecepcion.SelectedDate = DateTime.Now;
            DateEntrega.SelectedDate = DateTime.Now; 
            DateCaptura.SelectedDate = DateTime.Now;
            DateRecepcionH.SelectedTime = DateTime.Now;
            DateEntregaH.SelectedTime = DateTime.Now;
            CapturaH.SelectedTime = DateTime.Now;
            Rampa.Text = "";
            DateRecepcionH.SelectedTime = null;
            DateEntregaH.SelectedTime = null;
            CapturaH.SelectedTime = null;
            ObservacionInterna.Text = "";
            ObservacionCliente.Text = "";
            Marca.Text = "";
            Modelo.Text = "";
            Ano.Text = "";
            Placas.Text = "";
            Kilometraje.Text = "";
            estrella = 1;

            Cantidadd.Value = 0;


            pllantas.IsChecked = false;
            palineacion.IsChecked = false;
            pbalanceo.IsChecked = false;
            pamortiguadores.IsChecked = false;
            psuspension.IsChecked = false;
            pfrenos.IsChecked = false;
            pcambioaceite.IsChecked = false;


            chkgato.IsChecked = false;
            chkmaneral.IsChecked = false;
            chkcenicero.IsChecked = false;
            chkllave.IsChecked = false;
            chktaponaceite.IsChecked = false;
            chktaponradiador.IsChecked = false;
            chkvarilla.IsChecked = false;
            chkestuche.IsChecked = false;
            chktriangulo.IsChecked = false;
            chkllrefa.IsChecked = false;
            chkfiltroaire.IsChecked = false;
            chkllbateri.IsChecked = false;
            chkclax.IsChecked = false;
            chkcuarto.IsChecked = false;
            chkluces.IsChecked = false;
            chkantena.IsChecked = false;
            chkespejo.IsChecked = false;
            chkcristales.IsChecked = false;
            chkemblema.IsChecked = false;
            chkmoldeduras.IsChecked = false;
            chkbocina.IsChecked = false;
            chktapon.IsChecked = false;
            chkcarroseria.IsChecked = false;
            chkparabrisa.IsChecked = false;
            chktapas.IsChecked = false;
            chktablero.IsChecked = false;
            chkradio.IsChecked = false;
            chkbocinas.IsChecked = false;
            chkencendedor.IsChecked = false;
            chkespejoretro.IsChecked = false;
            chkcale.IsChecked = false;
            chkcinturon.IsChecked = false;
            chkbtninte.IsChecked = false;
            chkmanijas.IsChecked = false;
            chktapetes.IsChecked = false;
            chkvestidura.IsChecked = false;
            tabla.Rows.Clear();

        }

        private void UpdateGrido()
        {
            m_totalDue = 0;
            m_totalCant = 0;
            m_currentRowIndex = 0;
            InvoiceGrid.Children.Clear();
            //InvoiceGrid.RowDefinitions.RemoveAt(InvoiceGrid.RowDefinitions.Count - 1);
            for (int i = 0; i < InvoiceGrid.RowDefinitions.Count; i++)
                DrawBorder(i);
            foreach (OrdenItem item in m_items)
            {
                AddItem(item, true);
            }
            UpdateTotal();
            UpdateCantidad();
        }


        private void SaveHeader()
        {

            //IFormPass formPass = this.Owner as IFormPass;
            //if (formPass != null)
            int year = 2000;
            // tabPendientes.
            DateTime xxx = new DateTime();
            xxx = Convert.ToDateTime(DateRecepcion.SelectedDate);
            var V = xxx.ToString("MM/dd/yyyy");
            //DateTime recep = Convert.ToDateTime(DateRecepcion.DateTimeText);
            //var V = recep.ToString("MM/dd/yyyy");
            m_numerodefolio = osomaloso.ToString();

           // string gaso = Cantidadd.Value.ToString();
            int bytAccesorios1 = fncObtenAccesorios1();
           
            int bytExte1 = fncObtenExteriores1();
          
            int bytInte1 = fncObtenInteriores1();

            int pendientes = fncObtenPendientes();

            string factura = Folioo.Text;

            string marca = Marca.Text;
            string modelo = Modelo.Text;
            if (Ano.Text.Length > 0)
            { year = Convert.ToInt32(Ano.Text); }
            else

            { year = 2000; }

            int km = 1;
            string placas = Placas.Text;
            if (Kilometraje.Text.Length > 0)
            { km = Convert.ToInt32(Kilometraje.Text); }
            else

            { km = 1; }

            string rampa;
            if (Rampa.Text.Length > 0)
            { rampa = Rampa.Text; }
            else

            { rampa = "0"; }



            decimal gasolina =Convert.ToDecimal(Cantidadd.Value);

            decimal subtotal = Convert.ToDecimal(TotalDue);
            decimal iva = Convert.ToDecimal(Iva_Copy.Text);
            decimal supertotal = Convert.ToDecimal(TotalIva_Copy.Text);
            string observacion = ObservacionInterna.Text;
            string rfc = txtrfc.Text;
            string cliente = txtradial.Text;
            int OrderId = Convert.ToInt32( GlobalId.Identificador);
            string fechtimenormal = DateTime.Now.ToString();
            string frecepcion = DateRecepcion.SelectedDate.Value.ToString("yyyy-MM-dd");
            string trecepcion = DateRecepcionH.SelectedTime.Value.ToString("HH:mm:ss");
            string fecharecepcion = frecepcion+ "T" + trecepcion;
            string fEntrega = DateEntrega.SelectedDate.Value.ToString("yyyy-MM-dd");
            string tEntrega = DateEntregaH.SelectedTime.Value.ToString("HH:mm:ss");
            string fechaEntrega = fEntrega + "T" + tEntrega;
            string fCaptura = DateCaptura.SelectedDate.Value.ToString("yyyy-MM-dd");
            string tCaptura = CapturaH.SelectedTime.Value.ToString("HH:mm:ss");
            string fechaCaptura = fCaptura + "T" + tCaptura;



            string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
            SqlConnection sqlcon = new SqlConnection(connectionStringer);
            sqlcon.Open();


            SqlCommand agregar = new SqlCommand("INSERT INTO [dbo].[OrderHeader] ([OrderID],[CustomerID] ,[RFC],[InvoiceNumber],[Prefix],[Sufix],[DeliveryMethodID],[OrderDate],[ReceptionDate],[DueDate],[OrderQty],[Subtotal],[Tax],[Total],[Marca],[Modelo],[Year],[Placas],[Km],[Rampa],[Gasolina],[ExteriorValues],[InteriorValues],[AccesoriesValues],[PendientesValues],[CompanyID],[StoreID],[Comments],[UserID],[LocalIP],[PublicIP],[SystemInfo],[UserInfo],[InsertDate],[ModifiedDate],[LastUpdate],[StatusID],[DeletedFlag])" +
                "values (" + osomaloso + ",'"+cliente+"','"+rfc+"','','','',0,convert(datetime,'"+fechaCaptura+ "'),convert(datetime,'" + fecharecepcion + "'),convert(datetime,'" + fechaEntrega + "'),0, " + subtotal + " ,0 , " + supertotal+ " ,'"+marca+"','"+modelo+"',"+year+",'"+placas+"',"+km+","+rampa+","+gasolina+"," + bytExte1+ "," + bytInte1 + "," + bytAccesorios1 + ","+pendientes+",0,0,'" + observacion + "',0,'192.168','','','',getdate(),getdate(),getdate(),0,0)", sqlcon);


            //SqlCommand agregar = new SqlCommand("Insert Into Ordenes2 values ('" + factura + "'," + bytAccesorios1 + "," + bytAccesorios2 + ")", sqlcon);

            try
            {
                //  Guid.NewGuid*()
                agregar.ExecuteNonQuery();
                sqlcon.Close();
                //MessageBox.Show("se guardo Correctamente " );

            }
            catch (Exception ex)
            {
                MessageBox.Show("error al cargar " + ex.ToString());
            }

        }

        private void SaveDetail()
        {

            string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
            SqlConnection sqlcon = new SqlConnection(connectionStringer);
            sqlcon.Open();

            foreach (DataRow Registro in tabla.Rows)
            {
                
                string codigo = Registro[2].ToString();
                int id = Convert.ToInt32(Registro[0]);
                int renglon = Convert.ToInt32(Registro[1]);
                string descripcion = Registro[3].ToString();
                int cantidad = Convert.ToInt32(Registro[4]);
                string level = Registro[06].ToString();
                decimal precioventa = Convert.ToDecimal(Registro[07]);
                decimal preciototal = Convert.ToDecimal(Registro[08]);
                //int rampa = Convert.ToInt32(Registro[09])
                try
                {
                    string consulta = "INSERT INTO [dbo].[OrderDetail] ([OrderID],[OrderDetailID],[ProductID],[CodeID],[ProductDescription],[Level],[UnitID],[Qty] ,[UnitPrice], [ModifiedDate],[LastUpdate]) VALUES(" + id + "," + renglon + ",100,'" + codigo + "','" + descripcion + "','"+level+"',2," + cantidad + "," + precioventa + ",GETDATE(),GETDATE())";
                    SqlCommand agregar = new SqlCommand(consulta, sqlcon);
                    agregar.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("error al conectar " + ex.ToString());
                }


            }

            sqlcon.Close();
            //MessageBox.Show("se guardo Correctamente ");


        }

        private void AddButton_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Return)
            //{
            //    MessageBox.Show("No se pudo llenar los campos");
            //}
        }

        private void contentgrid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void RootGrid_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.F5)
            //{
            //    MessageBox.Show("delete pressed");
            //    e.Handled = true;
            //}

        
            if  (e.Key == Key.F6)
            {
                m_fieldsPopup = new OrdenDialog();
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


            if (e.Key == Key.F3)
            {
                m_fieldsCliente = new OrderCliente();
               
             
                m_fieldsCliente.CloseRequestedc += myDialog_CloseRequestedC;
                m_fieldsCliente.UpdateRequestedc += myDialog_UpdateRequestedc;
                m_fieldsCliente.Activated += fieldsPopup_Openedc;
                m_fieldsCliente.Background = new SolidColorBrush(Colors.Red);
                RootGrid.Opacity = 0.1;
                m_fieldsCliente.Opacity = 1;
                m_fieldsCliente.ShowDialog();
                RootGrid.Opacity = 1;
            }


            if (e.Key == Key.F7)
            {
                tabla.Rows[m_selectedIndex].Delete();
                if (m_selectedIndex == -1)
                    return;

                RemoveItem(m_selectedIndex);
               // this.DeleteButton.IsEnabled = false;
            }

        }

        private void Folioo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        DataTable dtbl = new DataTable();

        private void llenarcombazo()
        {
            DataTable dtbl1 = new DataTable();

            try
            {

                string conect = "SERVER = 192.168.200.10; DATABASE = Punto_De_venta; USER ID = sa; PASSWORD = dgo2007";
                SqlConnection con = new SqlConnection(conect);

                string cmd = "Select top 3 dbo.Articulos.Codigo_De_Articulo AS Codigo, dbo.Articulos.Descripcion, dbo.Articulos.Ancho,ISNULL(dbo.Articulos.Serie, 0 )As Serie," +
                    "ISNULL(dbo.Articulos.Rin, 0) as Rin , dbo.Articulos.Descripcion AS NombreCompleto,dbo.Articulos.Descripcion + ' ' + dbo.Articulos.Ancho + ' ' + CONVERT(varchar(50)," +
                    "ISNULL(dbo.Articulos.Serie, 0)) + ' ' + CONVERT(varchar(50), ISNULL(dbo.Articulos.Rin, 0)) AS Llanta from Articulos where " +
                    "(Articulos.Marca in('BFG', 'HANK', 'MICH', 'TORN', 'UNIR', 'YOKO', 'TIG', 'GT', 'LAUF')) and Articulos.Baja_Logica = 0 ";
                try { con.Open(); }
                catch (SqlException ex) { MessageBox.Show("Revise su conexión a internet"); }

                SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);

                DataSet dsPubs = new DataSet("Pubs");

                sda.Fill(dsPubs, "LLantas");

                dtbl1 = dsPubs.Tables["LLantas"];







               // cmbName.ItemsSource = dsPubs.Tables["LLantas"].DefaultView;


                con.Close();

                DataTable tblpaquetes = Paquetes();


                var query2 = from table1 in tblpaquetes.AsEnumerable()
                             

                    select new
                             {
                                Codigo = table1.Field<string>("Codigo_De_Paquete"),
                                Descripcion = table1.Field<string>("Descripcion"),
                                Ancho = "0",
                                Serie = "0",
                                Rin = "0",
                                NombreCompleto = "0",
                                Llanta = "0",
                    };

                DataTable nueva = new DataTable();
                nueva.Columns.Add("Codigo", typeof(string));
                nueva.Columns.Add("Descripcion", typeof(string));
                nueva.Columns.Add("Ancho", typeof(string));
                nueva.Columns.Add("Serie", typeof(Single));
                nueva.Columns.Add("Rin", typeof(Single));
                nueva.Columns.Add("NombreCompleto", typeof(string));
                nueva.Columns.Add("Llanta", typeof(string));


                foreach (var item in query2)
                {

                    nueva.Rows.Add(item.Codigo, item.Descripcion,item.Ancho,item.Serie,item.Rin,item.NombreCompleto,item.Llanta );
                   
                }

                var test = new ObservableCollection<Test>();
                foreach (DataRow row in dtbl.Rows)
                {
                    var obj = new Test()
                    {
                        id_test = (string)row.ItemArray[0],
                        name = (string)row.ItemArray[1]

                    };
                    test.Add(obj);




                }

                dtbl1.Merge(nueva);
                cmbName.ItemsSource = dtbl1.DefaultView;

            }

            catch (InvalidCastException e)
            {
                MessageBox.Show("No se pudo llenar los campos" + e.ToString());
            }

        }

        static DataTable Paquetes()
        {



            string conect = "SERVER = 192.168.200.10; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007 ";

            SqlConnection sqlconn = new SqlConnection(conect);
            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR DE CONEXION" + ex.Message);
            }

            DataTable tbl2 = new DataTable();



            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Paquetes.Codigo_De_Paquete, Articulos.Descripcion " +
                "FROM         Articulos INNER JOIN Paquetes ON Articulos.Codigo_De_Articulo = Paquetes.Codigo_De_Paquete " +
                "WHERE(Articulos.Baja_Logica = 0)", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "Vista");
            
            tbl2 = dsPubs.Tables["Vista"];
            sqlconn.Close();

            return tbl2;

        }


        public ObservableCollection<Test> test;

        public class Test
        {
            public string id_test { get; set; }
            public string name { get; set; }
        }



        private void Exportar_Click(object sender, RoutedEventArgs e)
        {
            string strfolio = Folioo.Text;
            export15(strfolio);


        }


        private void export15(string folio)
        {


            DataTable tablaOrden = GetHeader(folio);
            DataTable tablaDeatil = GetDetail(folio);

            RptOrden fac = new RptOrden(tablaOrden, tablaDeatil);

        }

        static DataTable GetHeader(string folio)
        {
            string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";

            SqlConnection sqlconn = new SqlConnection(conect);
            try {
                sqlconn.Open();
            }
            catch (Exception ex) {

                MessageBox.Show("ERROR DE CONEXION"+ex.Message);
            }




            //SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM OrderHeader WHERE OrderID = " + folio + " ", sqlconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select OrderHeader.OrderID, OrderHeader.CustomerID, OrderHeader.RFC, OrderHeader.OrderDate,"+
                                                       " OrderHeader.ReceptionDate, OrderHeader.DueDate, OrderHeader.OrderQty, OrderHeader.Subtotal, OrderHeader.Total, "+
                                                       " OrderHeader.ExteriorValues, OrderHeader.InteriorValues, OrderHeader.AccesoriesValues, Clientes_Frecuentes.Nombre + ' ' + "+
                                                       " Clientes_Frecuentes.Apellido_Paterno + ' ' + Clientes_Frecuentes.Apellido_Materno as Nombre, "+
                                                       " Clientes_Frecuentes.Telefono_Personal as Telefono,  OrderHeader.Marca, OrderHeader.Modelo, OrderHeader.Year, OrderHeader.Placas,OrderHeader.Km, " +
                                                       " Clientes_Frecuentes.Direccion + ' ' + Clientes_Frecuentes.Colonia + ' ' + Clientes_Frecuentes.Ciudad as Direccion,  OrderHeader.Comments as Observaciones, OrderHeader.Gasolina,OrderHeader.PendientesValues From OrderHeader Inner join  Clientes_Frecuentes" +
                                                       "  ON OrderHeader.CustomerID = Clientes_Frecuentes.Id_Radial"+
                                                        " where OrderHeader.OrderID =  " + folio + "", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "OrderHeader");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["OrderHeader"];
            sqlconn.Close();

            return dtbl;
        }

        static DataTable PaqPrecios(string articulo)
        {
            string conect = "SERVER = 192.168.200.10; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007 ";

            SqlConnection sqlconn = new SqlConnection(conect);
            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR DE CONEXION" + ex.Message);
            }




            //SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM OrderHeader WHERE OrderID = " + folio + " ", sqlconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Precios where Codigo_De_Articulo =  '" + articulo + "' ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "OrderHeader");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["OrderHeader"];
            sqlconn.Close();

            return dtbl;
        }
        static DataTable GetDetail(string folio)
        {
            string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";


            SqlConnection sqlconn = new SqlConnection(conect);
           
            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR DE CONEXION" + ex.Message);
            }
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT OrderDetail.EntityKey "+
     ", OrderDetail.OrderID, OrderDetail.OrderDetailID, OrderDetail.ProductID, OrderDetail.CodeID, OrderDetail.ProductDescription, OrderDetail.Level, OrderDetail.UnitID,"+
    " OrderDetail.Qty, OrderDetail.UnitPrice, OrderDetail.LineTotal, OrderDetail.ModifiedDate,"+
     "OrderDetail.LastUpdate, OrderDetail.RowID, OrderHeader.Rampa From OrderHeader Inner join OrderDetail"+
     " ON OrderHeader.OrderID = OrderDetail.OrderID WHERE OrderDetail.OrderID = " + folio + " ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "OrderDetail");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["OrderDetail"];
            sqlconn.Close();

            return dtbl;
        }

        private void Txtrfc_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void quantity_ValueChangedx(object sender, DependencyPropertyChangedEventArgs e)
        {


            if (onInit)
                return;
            if (rate != 0)
            {

                double value = rate;
                {
                    int quantityValue = GetQuantityAsInt();
                    CalculateTax();
                    UpdateTotalAmount();

                    //rate.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 64, 81));
                    //  CalculateTax();

                }
            }
        }
        private void CalculateTax()
        {
            m_invoiceItem.Cantidad = GetQuantityAsInt();
            double currentRate = (double)Cantidad.Value;

            int currentQuantity = GetQuantityAsInt();
            double precio = m_invoiceItem.Rate;
            double totall = currentQuantity * precio;
            m_invoiceItem.Total = totall;


            Total.Content = "$" + totall.ToString("#,###.00", CultureInfo.InvariantCulture);



        }
        private void UpdateTotalAmount()
        {

            int quantityValue = GetQuantityAsInt();
            double rateValue = rate;
            
            double calculatedTax = m_invoiceItem.Iva;
            double taxedAmount = (quantityValue * rateValue) + calculatedTax;
            //m_invoiceItem.Rate = taxedAmount;
            this.Total.Content = "$" + taxedAmount.ToString("#,###.00", CultureInfo.InvariantCulture);
        }
        public int GetQuantityAsInt()
        {
            return (Cantidad.Value is double) ? (int)Cantidad.Value : (int)(double)Cantidad.Value;
        }
        private void quantity_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Cantidadd.Value > 0)
            {
                osopointter.Value = Convert.ToDouble(Cantidadd.Value);
            }
            else
            {
                osopointter.Value = 0;
            }


        }

        private void Litros_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Cantidadd.Value > 0)
            {
                osopointter.Value = Convert.ToDouble(Cantidadd.Value);
            }
            else
            {
                osopointter.Value = 0;
            }
           

        }
        private void nivel_SelectioChanged(object sender, SelectionChangedEventArgs e)
        {
            var objPrecio = radComboNivel.SelectedItem;
            //((System.Data.DataRowView)objPrecio).Row.ItemArray[2].ToString();
            if (objPrecio != null)
            {
               double practual=Convert.ToDouble( ((System.Data.DataRowView)objPrecio).Row.ItemArray[2]);



                string precioactual = ((System.Data.DataRowView)objPrecio).Row.ItemArray[2].ToString();
                precioCodigo.Text = "$" + practual.ToString("#,###.00", CultureInfo.InvariantCulture);
                m_invoiceItem.Rate = Convert.ToDouble(((System.Data.DataRowView)objPrecio).Row.ItemArray[2].ToString());
                m_invoiceItem.Nivel = ((System.Data.DataRowView)objPrecio).Row.ItemArray[1].ToString();
                // m_invoiceItem.Preciolista =
                m_invoiceItem.Cantidad = Convert.ToInt32(Cantidad.Value);
                rate = Convert.ToDouble(precioactual);
                double preciodelnivel = Convert.ToDouble(precioactual);
                double quantity = Convert.ToDouble(Cantidad.Value);
                double meactualizo = preciodelnivel * quantity;
                Total.Content ="$" + meactualizo.ToString("#,###.00", CultureInfo.InvariantCulture);
                Cantidad.Focus();
            }
            else
            {
                precioCodigo.Text = "";
            }
            //m_invoiceItem.Nivel = row_selected["Codigo"].ToString();
        }
        private void RadMultiColumnComboBox_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            //read in sql 


            m_invoiceItem = new OrdenItem();
            var cour = combo.SelectedItem;
           
            if (cour!=null)
            {
                paq_code = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();
                paq = fncPaqueteEncontrado(paq_code);
                if (paq == true)
                {
                    Cantidad.IsReadOnly = true;
                    paq_code = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();
                  

                   
                    descript.Text = ((System.Data.DataRowView)cour).Row.ItemArray[1].ToString();
                    string strigCodigo = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();
                    m_invoiceItem.Codigo = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();
                    m_invoiceItem.Descripcion = ((System.Data.DataRowView)cour).Row.ItemArray[5].ToString();
                    llenarcombobox(strigCodigo);
                    radComboNivel.Focus();
                }
                else
                {
                    descript.Text = ((System.Data.DataRowView)cour).Row.ItemArray[1].ToString();
                    string strigCodigo = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();
                    m_invoiceItem.Codigo = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();
                    m_invoiceItem.Descripcion = ((System.Data.DataRowView)cour).Row.ItemArray[5].ToString();
                    llenarcombobox(strigCodigo);
                    radComboNivel.Focus();
                }
               

            }
            else
            {
                descript.Text = "";
            }

            //item_Copy.Text = ((System.Data.DataRowView)cour).Row.ItemArray[6].ToString();
            //m_invoiceItem.Codigo = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();
            //m_invoiceItem.Descripcion = ((System.Data.DataRowView)cour).Row.ItemArray[6].ToString();

            //string strigCodigo = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();



        }


        private bool fncPaqueteEncontrado(string strCodigoDePaquete)
        {
            SqlConnection cn;
            SqlCommand cmd;
            SqlDataReader dr;

            cn = new SqlConnection(GetOrigen());
            cn.Open();

            cmd= new SqlCommand("SELECT * FROM Paquetes WHERE Codigo_De_Paquete = '" + strCodigoDePaquete + "'", cn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
               
                return true;
            }
            else
            {
               
                return false;
            }
        }
        static string GetOrigen()
        {
            string conexion;

            //string vpn = "B20";
            //string server = getvpn(vpn);
            conexion = "SERVER = 192.168.200.20; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
            return conexion;

        }
        private void llenarcombobox(string codigo)
        {

            // xaxaxa var hat = codigo;

            try
            {

                string conect = "SERVER = 192.168.200.10; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                SqlConnection con = new SqlConnection(conect);
                try
                {
                    con.Open();

                    string cmd = " SELECT Codigo_De_Articulo,Nivel_De_Precios,Precio FROM Precios where Codigo_De_Articulo = '" + codigo + "' ";



                    SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);
                    DataSet dsPubs = new DataSet("Pubs");
                    sda.Fill(dsPubs, "Precios");
                    DataTable dtblg = new DataTable();
                    dtblg = dsPubs.Tables["Precios"];
                    radComboNivel.ItemsSource = dsPubs.Tables["Precios"].DefaultView;
                    var oso = dsPubs.Tables["Precios"].DefaultView;


                    try
                    {

                        DataRow[] resultss = dtblg.AsEnumerable().Where(row => row.Field<string>("Nivel_De_Precios").Contains("PL")).Select(row => row).ToArray<DataRow>();
                        var ratex = Convert.ToInt32(resultss[0][2]);
                       
                        m_invoiceItem.Preciolista = ratex;



                        //string apollo = dtblg.Rows[0][2].ToString();
                        //m_invoiceItem.Preciolista = Convert.ToDouble(apollo);
                    }
                    catch
                    {
                        Total.Content = "";
                        //MessageBox.Show("Este codigo no tiene niveles registrados", "Articulo sin precio");
                    }
                    //dataGrid1.ItemsSource = dsPubs.Tables["Precios"].DefaultView;
                    //dataGrid1.Columns[0].Visibility = false;
                    //dataGrid1.Tables["LLantas"].Columns[0].ColumnMapping = MappingType.Hidden;
                    con.Close();

                }

                catch (SqlException ex)
                {
                    MessageBox.Show("Revise su conexión a internet");
                }



            }
            catch (InvalidCastException e)
            {
                MessageBox.Show("No se pudo llenar los campos" + e.ToString());
            }
        }

        private void Combo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {


                radComboNivel.Focus();

            }
        }
    }



}

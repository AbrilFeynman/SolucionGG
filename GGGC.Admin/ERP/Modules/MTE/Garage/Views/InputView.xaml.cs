using Core.Common.UI.Core;
using GGGC.Admin.ERP.Modules.MTE.Garage.Models;
using GGGC.Modules.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Views
{
    /// <summary>
    /// Interaction logic for InputView.xaml
    /// </summary>
    public partial class InputView : UserControlViewBase
    {
        private AccesoDatos sCen;
        private AccesoDatos sLRG;
        private AccesoDatos sCLT;

        #region Fields
        const int m_columnCount = 5;
        private double m_totalDue;
        private IList<InvoiceItem> m_items;
        int m_currentRowIndex = 0;
        int m_selectedIndex = -1;
        int m_prevSelectedIndex = -1;
        const int m_rowHeight = 30;
        Border m_border;
        InputDetailView m_fieldsPopup;
        SupplierDialog m_addressPopup;
        SupplierInformation supplierInfo;
        ProductList m_productList;

        string strFolioOrden;
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

        #endregion
        public InputView()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            m_productList = new ProductList();
            m_items = new List<InvoiceItem>();
            m_border = new Border();
            supplierInfo = new SupplierInformation();

            //Load price list from XML document
            m_productList.LoadFromXml();

            strFolioOrden = strFolio();
            txtFolio.Text = "Folio:" + strFolioOrden;

            //Set Billing information
            this.Name.Text = supplierInfo.Name;// = "Fran Wilson";
            this.BillingAddress.Text = supplierInfo.Address;// = "89, Chiaroscuro Road, Portland, 97219.";                      
            
            this.DATE.Text = (supplierInfo.Date = DateTime.Now.Date).ToString("dd/MMM/yy");
            this.InvoiceNumber.Text = supplierInfo.InvoiceNumber;// = new Random().Next(100, 10000).ToString();
            this.DueDate.Text = (supplierInfo.DueDate = DateTime.Now.Date).ToString("dd/MMM/yy");

            InvoiceItem defaultItem = new InvoiceItem() { CodeID = m_productList[0].CodeID, ItemName = m_productList[0].Name, Quantity = 1, UnitPrice = m_productList[0].Price };
           // defaultItem.Taxes = m_productList[0].Rate * 0.07;
            //Add an item by default
           // AddItem(defaultItem, false);
            ////m_productList = new ProductList();
            //m_items = new List<InvoiceItem>();
            //m_border = new Border();
            //supplierInfo = new SupplierInformation();
            ////Load price list from XML document
            //m_productList.LoadFromXml();

            ////Set Billing information
            //this.Name.Text = supplierInfo.Name = "Fran Wilson";
            //this.BillingAddress.Text = supplierInfo.Address = "89, Chiaroscuro Road, Portland, 97219.";
            //this.DATE.Text = (supplierInfo.Date = DateTime.Now.Date).ToString("d");
            //this.InvoiceNumber.Text = supplierInfo.InvoiceNumber = new Random().Next(100, 10000).ToString();
            //this.DueDate.Text = (supplierInfo.DueDate = DateTime.Now.Date).ToString("d");

            //InvoiceItem defaultItem = new InvoiceItem() { CodeID = "10570", Description = "MICHELIN LTX", Quantity = 1, Rate = 5 };
            //defaultItem.Taxes = 5 * 0.07;

            //Add an item by default
            //AddItem(defaultItem, false);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    //base.OnNavigatedTo(e);
        //}
        private void OnGoBack(object parameter)
        {
            //Frame.Navigate(typeof(SampleBrowser.Home));
        }

        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add(object sender, RoutedEventArgs e)
        {
            m_fieldsPopup = new InputDetailView(m_productList);
            m_fieldsPopup.CloseRequested += myDialog_CloseRequested;
            m_fieldsPopup.UpdateRequested += myDialog_UpdateRequested;
            m_fieldsPopup.Activated += fieldsPopup_Opened;
            m_fieldsPopup.Background = new SolidColorBrush(Colors.Black);
            RootGrid.Opacity = 0.1;
            m_fieldsPopup.Opacity = 1;
            m_fieldsPopup.ShowDialog();
            RootGrid.Opacity = 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fieldsPopup_Opened(object sender, object e)
        {
            (sender as InputDetailView).InitializeFocus();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void myDialog_UpdateRequested(object sender, EventArgs e)
        {
            InvoiceItem item = (e as FieldsUpdateEventArgsInput).UpdatedFields;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditSupplierDetails(object sender, RoutedEventArgs e)
        {
            #region Popup
            m_addressPopup = new SupplierDialog(supplierInfo);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void addressDialog_UpdateRequested(object sender, EventArgs e)
        {
            this.Name.Text = supplierInfo.Name;
            this.BillingAddress.Text = supplierInfo.Address;
            this.DATE.Text = supplierInfo.Date.ToString("d");
            this.InvoiceNumber.Text = supplierInfo.InvoiceNumber;
            this.DueDate.Text = supplierInfo.DueDate.ToString("d");
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
        /// <summary>
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
            m_border.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 08, 71, 96));
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
            int selectedProductIndex = m_productList.IndexOf(m_productList[invoiceItem.ItemName]);
            m_fieldsPopup = new InputDetailView(invoiceItem, "Edit the Fields", m_productList, selectedProductIndex);
            m_fieldsPopup.UpdateRequested += EditDialog_UpdateRequested;
            m_fieldsPopup.CloseRequested += EditDialog_CloseRequested;
            m_fieldsPopup.lblTitle.Content = "Edit the Fields";
            RootGrid.Opacity = 0.2;
            m_fieldsPopup.Opacity = 1;
            m_fieldsPopup.ShowDialog();
            RootGrid.Opacity = 1;

            //FrameworkElement element = null;
            //if ((element = e.OriginalSource as FrameworkElement) != null)
            //{
            //    m_selectedIndex = Grid.GetRow(element);
            //}
            //InvoiceItem invoiceItem = m_items[m_selectedIndex];
            //int selectedProductIndex = m_productList.IndexOf(m_productList[invoiceItem.Description]);
            
            ////m_fieldsPopup = new InputDetailView(invoiceItem, "Edit the Fields", m_productList, selectedProductIndex);
           
            
            //m_fieldsPopup.UpdateRequested += EditDialog_UpdateRequested;
            //m_fieldsPopup.CloseRequested += EditDialog_CloseRequested;
            //m_fieldsPopup.lblTitle.Content = "Edit the Fields";
            //RootGrid.Opacity = 0.2;
            //m_fieldsPopup.Opacity = 1;
            //m_fieldsPopup.ShowDialog();
            //RootGrid.Opacity = 1;
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
            //        export.CreatePDF(m_items, supplierInfo, TotalDue);
            //    }));

            if (m_items.Count > 0)
            {
                //PdfExport export = new PdfExport();
                //export.CreatePDF(m_items, supplierInfo, TotalDue);
            }
            else
            {
               // MessageBox.Show("No invoice items found to generate the report!", "Export Cancelled");

                MostrarMensajeError("Debe Ingresar al menos 1 producto para guardar la compra");
            }

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

            if (m_items.Count > 0)
            {
                //ExportWord exportWord = new ExportWord();
                //exportWord.CreateWord(m_items, supplierInfo, TotalDue);
            }
            else
            {
                MessageBox.Show("No invoice items found to generate the report!", "Export Cancelled");
            }
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
        private void ExcelExport_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.IsHitTestVisible = false;
            MainGrid.Opacity = 0.5;
            //progress.Visibility = Windows.UI.Xaml.Visibility.Visible;

            //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, (() =>
            //{

            if (m_items.Count > 0)
            {
                //ExportToExcel exportToExcel = new ExportToExcel();
                //exportToExcel.GenerateReport(m_items, supplierInfo);
                if (supplierInfo.InvoiceNumber == null)
                {

                    MostrarMensajeError("Debe Seleccionar un Proveedor Existente");
                }

                else
                {
                    if (fncGuardar() == true)
                    {                   
                                              MostrarMensajeError("Compra Almacenada Exitosamente");
                                              Clear();
                    }
                    else
                    {
                        MostrarMensajeError("Ocurrió un Error al Almacenar la compra, Intente de Nuevo");
                    }

                }

               
            }
            else
            {
               // MessageBox.Show("Debe ingresar al menos 1 producto para guardar la compra", "Registro Cancelado");
                MostrarMensajeError("Debe Ingresar al menos 1 producto para guardar la compra");
            }
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About(object sender, RoutedEventArgs e)
        {

        }
        #endregion


        private bool fncGuardar()
        {
            string str1 = "1";
            string str2 = "1";
            string sSQL = "";
            string sSQL2 = "";
            string sSQL3 = "";
            string sSQL4 = "";
            string sSQL5 = "";
            string sSQL6 = "";

            //if (fncValidar() == false)
            //    return false;

            List<SqlParameter> pars = new List<SqlParameter>();
            List<SqlParameter> pars2 = new List<SqlParameter>();
            List<SqlParameter> pars3 = new List<SqlParameter>();
            //RadWindow.Alert(new DialogParameters()
            //{
            //    Content = "Producto Guardado \n\n o Exitosamente del sistema",
            //    Header = "GGGC"
            //    // IconContent = "";
            //});
            //            RadMessageBox.Show(this, "My Message", "Window title",
            //MessageBoxButton.OK, MessageBoxImage.Information);
            try
            {

                string strOrderID = strFolioOrden;
                int strSupplierID = supplierInfo.SupplierID;
                
                string strPrefix = "P";
                string strSufix = "S";
                long lngTicks = DateTime.Now.Ticks;
                string strRFC = supplierInfo.Name;
                string strInvoiceNumber = InvoiceNumber.Text.Trim();
                string strPaymentConditionID = "0";
                string strDeliveryMetodID = "0";
                DateTime dteInvoiceDate = supplierInfo.Date;
                DateTime dteReceptionDate = supplierInfo.DueDate;
                DateTime dteDueDate = DateTime.Now;
                DateTime dteAccountingDate = DateTime.Now;
                decimal decDiscount = 0;
                int intOrderQty = 0;
                decimal decSubtotal = 0;
                decimal decTax = 0;
                decimal decTotal = 0;
                decimal decRaiting = 0;
                int intQuarterID = 0;
                int intCompanyID = 0;
                byte bytStoreID = Convert.ToByte(GlobalModule.ActualStoreID);
                byte bytRevisioNumber = 0;
                string strComments = "";
                int intUserID = GlobalModule.ActualUserID;
                string strLocalIP = GlobalModule.GetLocalIP();
                string strPublicIP = GlobalModule.getPublicIP();
                string strSystemInfo = GlobalModule.GetSystemInfo();
                string strUserInfo = GlobalModule.GetUserInfo();
                DateTime dteInsertDate = DateTime.Now;
                DateTime dteModifiedDate = DateTime.Now;
                DateTime dteLastUpDate = DateTime.Now;
                byte bytStatusID = 0;
                byte bytDeleteFlag = 0;
                Guid guidRowID = Guid.NewGuid();

                // Detail

              //  m_items, supplierInfo


                //string strCodigo = this.txtCodigo.Text.Trim();
                //string strID = this.txtID.Text.Trim();
                //string strDesc = this.txtDescripcion.Text.Trim();
                //string strGUID = Guid.NewGuid().ToString();
                //string strObs = "obs";
                //string strMarca = "MMRF";
                //int productID = Convert.ToInt32(this.txtID.Text);
                //double decRin = 0.0;
                //byte bytCero = 0;
                //bool blnCentralesOK = false;
                //bool blnSucursalesOK = false;
                //bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false, b6 = false, b7 = false, b8 = false, b9 = false, b10 = false, b11 = false, b12 = false, b13 = false, b14 = false, b15 = false;
                //string strLinea = cboLine.SelectedValue.ToString();
                //switch (strLinea)
                //{
                //    case "1":
                //        strLinea = "02001";
                //        break;
                //    default:
                //        strLinea = "02001";
                //        break;
                //}
                if (true)
                {
                    //_idEmpresa = 2;//datos.BaseDatos.SelectMaxID("idEmpresa", "Empresas");
                    //_idSucursal = 6;                                   
                    sSQL = "INSERT INTO mto_PurchaseOrderHeader ([OrderID],[SupplierID],[Prefix],[Sufix],[Ticks],[InvoiceNumber], [RFC], [PaymentConditionID],[DeliveryMethodID],[InvoiceDate],[ReceptionDate],[DueDate],[AccountingDate],[Discount],[OrderQty],[Subtotal],[Tax],[Total],[Raiting],[QuarterID],[CompanyID],[StoreID],[RevisionNumber],[Comments],[UserID],[LocalIP], [PublicIP],[SystemInfo], [UserInfo],[InsertDate],[ModifiedDate], [LastUpdate], [StatusID],[DeletedFlag],[RowID]) "
                        + "VALUES (@OrderID, @SupplierID, @Prefix, @Sufix, @Ticks, @InvoiceNumber, @Rfc, @PaymentConditionID, @DeliveryMethodID, @InvoiceDate, @ReceptionDate, @DueDate, @AccountingDate, @Discount, @OrderQty, @Subtotal, @Tax, @Total, @Raiting, @QuarterID, @CompanyID, @StoreID, @RevisionNumber,@Comments, @UserID, @LocalIP, @PublicIP, @SystemInfo, @UserInfo, @InsertDate, @ModifiedDate, @LastUpdate, @StatusID, @DeletedFlag, @RowID  )";

                    sSQL2 = "INSERT INTO Articulos (Codigo_De_Articulo, Descripcion,Unidad,Impuesto,Codigo_De_Linea,Marca, Ancho, Rin,Serie,Gama,Rango_De_Velocidad,Codigo_Tipo_De_Aplicacion,Treadware,Bloque,Renglon,Codigo_Editable,Prefijo,Inventariado,Movimiento,Baja_Logica, En_Liquidacion, Fecha_Y_Hora_De_Ultima_Actualizacion) "
                        + "VALUES (@Codigo_De_Articulo, @Descripcion, @Unidad, @Impuesto, @Codigo_De_Linea, @Marca, @Ancho, @Rin, @Serie, @Gama, @Rango_De_Velocidad, @Codigo_Tipo_De_Aplicacion, @Treadware, @Bloque, @Renglon, @Codigo_Editable, @Prefijo, @Inventariado, @Movimiento, @Baja_Logica, @En_Liquidacion, @LastUpdate)";
                    //sSQL3 = "DELETE FROM Stocks_Ideales WHERE Codigo_De_Articulo = @Codigo_De_Articulo";
                    //sSQL4 = "DELETE FROM Stocks_Ideales WHERE Codigo_De_Articulo = @Codigo_De_Articulo";
                    //strInstruccion = "INSERT INTO Stocks_Ideales SELECT Numero_Corto_De_Sucursal, " & strCodigoDeArticulo & " AS Codigo_De_Articulo, 0 AS Stock_Ideal,1 AS Modificacion_Directa, GETDATE() AS Fecha_Y_Hora_De_Ultima_Actualizacion FROM Sucursales WHERE (Numero_Corto_De_Sucursal NOT IN (0, 90)) AND (Estatus_De_Sucursal = 1) "
                    //sSQL = "INSERT INTO Requisiciones ([RequisicionID],[Requisicion],[Fecha]) "
                    //   + "VALUES ( @RequisicionID, @RequisicionID, @Fecha)";
                }
                else
                {
                    //sSQL = "UPDATE Empresas SET Nombre = @Nombre, RFC = @RFC, Calle = @Calle, NumExt = @NumExt, NumInt = @NumInt, Colonia = @Colonia, " +
                    //    "Localidad = @Localidad, Municipio = @Municipio, Estado = @Estado, Pais = @Pais, CP = @CP, Referencia = @Referencia, ModoPrueba = @ModoPrueba, RutaTrabajo = @RutaTrabajo WHERE (idEmpresa = " + _idEmpresa + ")";
                }
                //, , , ,, , , , , , , , , , , @RowID  )";
                
                pars.Clear();
                pars.Add(new SqlParameter("@OrderID", strOrderID));
                pars.Add(new SqlParameter("@SupplierID", strSupplierID));
                pars.Add(new SqlParameter("@Prefix", strPrefix));
                pars.Add(new SqlParameter("@Sufix", strSufix));
                pars.Add(new SqlParameter("@Ticks", lngTicks));
                pars.Add(new SqlParameter("@InvoiceNumber", strInvoiceNumber));
                pars.Add(new SqlParameter("@Rfc", strRFC));
                pars.Add(new SqlParameter("@PaymentConditionID", strPaymentConditionID));
                pars.Add(new SqlParameter("@DeliveryMethodID", strDeliveryMetodID));
                pars.Add(new SqlParameter("@InvoiceDate", dteInvoiceDate));
                pars.Add(new SqlParameter("@ReceptionDate", dteReceptionDate));
                pars.Add(new SqlParameter("@DueDate", dteDueDate));
                pars.Add(new SqlParameter("@AccountingDate", dteAccountingDate));

                pars.Add(new SqlParameter("@Discount", decDiscount));
                pars.Add(new SqlParameter("@OrderQty", intOrderQty));
                pars.Add(new SqlParameter("@Subtotal", decSubtotal));
                pars.Add(new SqlParameter("@Tax", decTax));
                pars.Add(new SqlParameter("@Total", decTotal));
                pars.Add(new SqlParameter("@Raiting", decRaiting));
                pars.Add(new SqlParameter("@QuarterID", intQuarterID));
                pars.Add(new SqlParameter("@CompanyID", intCompanyID));
                pars.Add(new SqlParameter("@StoreID", bytStoreID));

                pars.Add(new SqlParameter("@RevisionNumber", bytRevisioNumber));
                pars.Add(new SqlParameter("@Comments", strComments));
                pars.Add(new SqlParameter("@UserID", intUserID));
                pars.Add(new SqlParameter("@LocalIP", strLocalIP));
                pars.Add(new SqlParameter("@PublicIP", strPublicIP));
                pars.Add(new SqlParameter("@SystemInfo", strSystemInfo));
                pars.Add(new SqlParameter("@UserInfo", strUserInfo));
                pars.Add(new SqlParameter("@InsertDate", dteInsertDate));
                pars.Add(new SqlParameter("@ModifiedDate", dteModifiedDate));
                pars.Add(new SqlParameter("@LastUpdate", dteLastUpDate));
                pars.Add(new SqlParameter("@DeletedFlag", bytDeleteFlag));
                pars.Add(new SqlParameter("@StatusID", bytStatusID));
                pars.Add(new SqlParameter("@RowID", guidRowID));
                            
               //pars2.Clear();

                AccesoDatos sCen = new AccesoDatos(6);
                sCen.BaseDatos.Ejecuta(sSQL, pars.ToArray());
                pars.Clear();

                //Agregar registros del detalle
                decimal cantidad = 0, valorUnitario = 0, importe = 0;
                string descripcion, unidad, codigo;
                int productID = 0;
                int intZero = 0;

                int intFilas = m_items.Count;
                for (int i = 0; i < intFilas; i++)
                {

                    productID = m_items[i].ProductID;
                    codigo = m_items[i].CodeID;
                    descripcion = m_items[i].ItemName;
                    cantidad = m_items[i].Quantity;
                    importe = m_items[i].UnitPrice;                 
              
                    sSQL2 = "INSERT INTO mto_PurchaseOrderDetail ([OrderID],[OrderDetailID],[ProductID], [CodeID], [ProductDescription], [UnitID], [OrderQty], [RejectedQty],[Qty] ,[Discount],[UnitPrice],[ModifiedDate],[LastUpdate],[RowID]) "
                         + "VALUES (@OrderID, @OrderDetailID, @ProductID, @CodeID, @ProductDescription, @UnitID, @OrderQty, @RejectedQty, @Qty, @Discount, @UnitPrice, @ModifiedDate, @LastUpdate, @RowID  )";

                    pars.Clear();
                    pars.Add(new SqlParameter("@OrderID", strOrderID));
                    pars.Add(new SqlParameter("@OrderDetailID", i+1));
                    pars.Add(new SqlParameter("@ProductID", productID));
                    pars.Add(new SqlParameter("@CodeID", codigo));
                    pars.Add(new SqlParameter("@ProductDescription", descripcion));
                    pars.Add(new SqlParameter("@UnitID", intZero));
                    pars.Add(new SqlParameter("@OrderQty", cantidad));
                    pars.Add(new SqlParameter("@RejectedQty", intZero));
                    pars.Add(new SqlParameter("@Qty", cantidad));
                    pars.Add(new SqlParameter("@Discount", intZero));
                    pars.Add(new SqlParameter("@UnitPrice", importe));
                    pars.Add(new SqlParameter("@ModifiedDate", dteModifiedDate));
                    pars.Add(new SqlParameter("@LastUpdate", DateTime.Now));
                    pars.Add(new SqlParameter("@RowID", Guid.NewGuid()));

                    sCen.BaseDatos.Ejecuta(sSQL2, pars.ToArray());
                    pars.Clear();
               
                   
                  
                    //datos.BaseDatos.Ejecuta(sSQL, pars.ToArray());
                }


                //sCen.BaseDatos.Ejecuta(sSQL2, pars.ToArray());
                //pars.Clear();

                sCen.BaseDatos.Ejecuta("UPDATE mtto_Config SET OrderID = OrderID + 1", pars.ToArray());
                //Application.Current.Dispatcher.Invoke(new Action(() => updateLabel("Guardando en Servidor LRG...")));

                //this.Dispatcher.Invoke(DispatcherPriority.Normal, 
                //      (System.Windows.Forms.MethodInvoker)delegate()
                //           {
                //           }); 
                //  string strMail = "<p>Alta de Artículo</p> <p>Codigo_De_Articulo:<b> " + productID + "</b></p>" + "<p>Descripción: " + strDesc + "</p> <font color=\"#08088A\"> Fecha:" + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToLongTimeString() + "</font> <p>...</p>";
                AccesoDatos sPV = null;
                return true;
            }

            catch (Exception ex)
            {
               // throw new Exception("Error al Guardar: " + ex.Message);


                //RadWindow.Alert(new DialogParameters()
                //{
                //    Content = "La requisicion puede estar duplicada \n\n o esta pendiente habilitar mas funciones del sistema",
                //    Header = "GGGC"
                //    // IconContent = "";
                //});

                 MessageBox.Show("Mensaje error: " + ex.Message);
            }
            return false;
        }


        private string strFolio()
        {
            string folio = "";
            try
            {
                //Series y Folios
                object Obj;
                AccesoDatos sCen = new AccesoDatos(6);
                string sSQL = "SELECT OrderID FROM mtto_Config;";
                Obj = sCen.BaseDatos.ConsultaDato(sSQL); ;//
                if (Obj != null)
                {
                    if (!Convert.IsDBNull(Obj))
                    {
                      folio = Obj.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error Al Obtener el Folio: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //this.Close();
            }
            return folio;
        }

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

            SetCell(rowIndex, 0, item.CodeID.ToString());
            SetCell(rowIndex, 1, item.ItemName.ToString());
            //SetCell(rowIndex, 2, "PZA");
            SetCell(rowIndex, 2, item.Quantity.ToString());
            SetCell(rowIndex, 3, "" + item.UnitPrice.ToString("#,##0.00", CultureInfo.InvariantCulture));
            //SetCell(rowIndex, 3, "$" + item.Taxes.ToString("#,###.00", CultureInfo.InvariantCulture));
            SetCell(rowIndex, 4, "" + item.TotalAmount.ToString("#,##0.00", CultureInfo.InvariantCulture));

            //<Rectangle Grid.Row="0" Height="1" StrokeThickness="0.75" VerticalAlignment="Bottom" Grid.ColumnSpan="5" 
            //StrokeDashArray="4,4" Stroke="#FFCECECE"></Rectangle>

            m_totalDue += Convert.ToDouble(item.TotalAmount);
            m_currentRowIndex++;
            m_selectedIndex = m_items.Count;
            UpdateTotal();

            //int rowIndex = m_currentRowIndex;

            //if (!addToGridAlone)
            //    m_items.Add(item);

            //if (!(InvoiceGrid.RowDefinitions.Count > m_currentRowIndex))
            //{
            //    InvoiceGrid.RowDefinitions.Add(CreateRowDefinition(m_rowHeight));

            //}
            //DrawBorder(rowIndex);
            
            //SetCell(rowIndex, 0, item.CodeID.ToString());

            //SetCell(rowIndex, 1, item.Description.ToString());
            //SetCell(rowIndex, 2, "pza");

            //SetCell(rowIndex, 3, item.Quantity.ToString());
            
            ////SetCell(rowIndex, 2, "$" + item.Rate.ToString("#,###.00", CultureInfo.InvariantCulture));
            ////SetCell(rowIndex, 3, "$" + item.Taxes.ToString("#,###.00", CultureInfo.InvariantCulture));
            //SetCell(rowIndex, 4, "$" + item.TotalAmount.ToString("#,###.00", CultureInfo.InvariantCulture));

            ////<Rectangle Grid.Row="0" Height="1" StrokeThickness="0.75" VerticalAlignment="Bottom" Grid.ColumnSpan="5" 
            ////StrokeDashArray="4,4" Stroke="#FFCECECE"></Rectangle>

            //m_totalDue += Convert.ToDouble(item.TotalAmount);
            //m_currentRowIndex++;
            //m_selectedIndex = m_items.Count;
            ////UpdateTotal();

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
            Grid.SetColumnSpan(rect, 5);
            Grid.SetRow(rect, rowIndex);
            InvoiceGrid.Children.Add(rect);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="value"></param>
        private void SetCell(int rowIndex, int columnIndex, string value)
        {
            if ( columnIndex == 3 || columnIndex == 4)
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
                textBlockDollor.FontSize = 18;
                textBlockDollor.FontFamily = new FontFamily("Segoe UI");
                textBlockDollor.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
                //textBlockDollor.Foreground = new SolidColorBrush(Colors.Black);
                textBlockDollor.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                textBlockDollor.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                textBlockDollor.TextAlignment = TextAlignment.Left;
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
                textBlock.FontSize = 18;
                textBlock.FontFamily = new FontFamily("Segoe UI");
                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
                //HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
                if (columnIndex == 0)
                    textBlock.Padding = new Thickness(10, 0, 0, 0);
                else
                    //textBlock.Padding = new Thickness(0, 0, 15, 0);
                textBlock.Padding = new Thickness(10, 0, 0, 0);
                //textBlock.Foreground = new SolidColorBrush(Colors.Black);
                textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                if (columnIndex > 0)
                    textBlock.TextAlignment = TextAlignment.Left;
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
                textBlock.FontSize = 18;
                textBlock.FontFamily = new FontFamily("Segoe UI");
                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
                //HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
                if (columnIndex == 0)
                    textBlock.Padding = new Thickness(10, 0, 0, 0);
                else
                    //textBlock.Padding = new Thickness(0, 0, 15, 0);
                textBlock.Padding = new Thickness(10, 0, 0, 0);
                //textBlock.Foreground = new SolidColorBrush(Colors.Black);
                textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                if (columnIndex > 0)
                    textBlock.TextAlignment = TextAlignment.Right;

                if (columnIndex == 1) //Desc
                {
                    textBlock.Padding = new Thickness(0, 0, 0, 0);
                    textBlock.TextAlignment = TextAlignment.Left;
                }
                   

                if (columnIndex == 2)
                {
                    textBlock.Padding = new Thickness(0, 0, 35, 0);
                    textBlock.TextAlignment = TextAlignment.Right;
                }
                  

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
        }
        /// <summary>
        /// 
        /// </summary>
        void UpdateTotal()
        {
            this.TotalAmount.Text = "$" + TotalDue.ToString("#,##0.00", CultureInfo.InvariantCulture);
            this.TotalIva.Text = "$" + (TotalDue * 1.16).ToString("#,##0.00", CultureInfo.InvariantCulture);
        }

        private void Clear()
        {
            m_items.Clear();
            UpdateGrid();
            Initialize();
        }


        //public async void LaunchedFromToast(String arguments)
        //{
        //    StorageFolder folder = KnownFolders.DocumentsLibrary;
        //    StorageFile file = await folder.GetFileAsync(arguments);
        //    bool success = await Windows.System.Launcher.LaunchFileAsync(file);



        //    if (!success)
        //    {
        //        MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");
        //        UICommand yesCmd = new UICommand("Yes");
        //        msgDialog.Commands.Add(yesCmd);
        //        UICommand noCmd = new UICommand("No");
        //        msgDialog.Commands.Add(noCmd);
        //        IUICommand cmd = await msgDialog.ShowAsync();
        //        if (cmd == yesCmd)
        //        {
        //            // Launch the retrieved file
        //            success = await Windows.System.Launcher.LaunchFileAsync(file);
        //        }
        //    }
        //}

        private async void MostrarMensajeError(string strMessage)
        {
            var metroWindow = (MetroWindow)Application.Current.MainWindow;
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                AnimateHide = true
            };
            var result = await metroWindow.ShowMessageAsync("Grupo Guadiana GC", strMessage, MessageDialogStyle.Affirmative, settings);
            //metroWindow.sh
            //metroWindow.sh

            //LoginDialogData result = await this.ShowLoginAsync("Authentication", "Enter your credentials", new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, InitialUsername = "MahApps", EnablePasswordPreview = true });
            //if (result == null)
            //{
            //    //User pressed cancel
            //}
            //else
            //{
            //    MessageDialogResult messageResult = await this.ShowMessageAsync("Authentication Information", String.Format("Username: {0}\nPassword: {1}", result.Username, result.Password));
            //}

            //  blnMostrar = true;


            //return;
        }
        #endregion


    }
}

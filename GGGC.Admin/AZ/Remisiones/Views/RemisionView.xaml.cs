using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
using Telerik.Windows.Controls;

namespace GGGC.Admin.AZ.Remisiones.Views
{

    public partial class RemisionView : UserControl
    {

        #region Fields
        const int m_columnCount = 9;
        private double m_totalDue;
        private string m_nombre;
        private int m_numeroRemision;
        private string m_rfc;
        private string m_direccion;
        private string m_colonia;
        private string m_ciudad;
        private string m_estado;
        private string m_cp;
        private string m_nocliente;
        private double m_totalCant;
        private int m_numeroFolio;
        private IList<RemiItem> m_items;
        int m_currentRowIndex = 0;
        RemiCliente m_fieldsCliente;
        int m_selectedIndex = -1;
        int m_prevSelectedIndex = -1;
        const int m_rowHeight = 30;
        Border m_border;
        ProductoDialog m_fieldsPopup;
        AddressDialog m_addressPopup;
        BillingInformation m_billInfo;
        ProductList m_productList;
        ProductoDialog m_producto;
        int estrella = 1;
        #endregion


        #region Vars
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



        public int NumeroFolio
        {
            get
            {
                return m_numeroFolio;
            }
        }
        public int NumeroRemision
        {
            get
            {
                return m_numeroRemision;
            }
        }

        public string Nombre
        {
            get
            {
                return m_nombre;
            }
        }

        public string Nocliente
        {
            get
            {
                return m_nocliente;
            }
        }
        public string Rfc
        {
            get
            {
                return m_rfc;
            }
        }
        public string Direccion
        {
            get
            {
                return m_direccion;
            }
        }

        public string Colonia
        {
            get
            {
                return m_colonia;
            }
        }


        public string Ciudad
        {
            get
            {
                return m_ciudad;
            }
        }

        public string Estado
        {
            get
            {
                return m_estado;
            }
        }
        public string Cp
        {
            get
            {
                return m_cp;
            }
        }
        #endregion


        public RemisionView()
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
            // Sets the UI culture to French (France)
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-MX");
            //
            InitializeComponent();
            StyleManager.SetTheme(InvoiceRemision, new Windows8Theme());

            Initialize();

        }


        private void Initialize()
        {
            m_productList = new ProductList();

            m_items = new List<RemiItem>();

            m_border = new Border();

            m_billInfo = new BillingInformation();


            getFolio();

            tabla.Columns.Add("Id", typeof(string));
            tabla.Columns.Add("Renglon", typeof(int));
            tabla.Columns.Add("Codigo", typeof(string));
            tabla.Columns.Add("Descripcion", typeof(string));
            tabla.Columns.Add("Cantidad", typeof(string));
            tabla.Columns.Add("Preciolista", typeof(string));
            tabla.Columns.Add("Nivel", typeof(string));
            tabla.Columns.Add("Precioventa", typeof(string));
            tabla.Columns.Add("PrecioExtendido", typeof(string));
            tabla.Columns.Add("Medida", typeof(string));

            llenarConsulta();



        }

        private void getFolio()
        {
            try {
                string conect = "SERVER = gggctserver.database.windows.net; DATABASE =devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099";
                SqlConnection con = new SqlConnection(conect);

                string cmd = "SELECT TOP (1) [Prefijo],NumeroRemision  FROM [dbo].[DatosRemision] ";
                con.Open();


                SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);

                DataSet dsPubs = new DataSet("Pubs");

                sda.Fill(dsPubs, "DatosRemision");

                DataTable dtbl = new DataTable();

                dtbl = dsPubs.Tables["DatosRemision"];

                txtFolio.Text = dtbl.Rows[0][0].ToString() + dtbl.Rows[0][1].ToString();
                m_numeroFolio = Convert.ToInt32(dtbl.Rows[0][1]);
                con.Close();
            }
            catch (SqlException ex) {




                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "No se pudo conecta a la base de  datos.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }


        }

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

        void fieldsPopup_Opened(object sender, object e)
        {
            (sender as ProductoDialog).InitializeFocus();
        }

        void myDialog_UpdateRequested(object sender, EventArgs e)
        {
            RemiItem item = (e as FieldsUpdateEventArgs).UpdatedFields;
            AddItem(item, false);
            UpdateTotal();
            agreItem(item);
            m_fieldsPopup.Close();
        }

        void myDialog_CloseRequested(object sender, EventArgs e)
        {
            m_fieldsPopup.Close();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (tabla.Rows.Count > 0)

            { tabla.Rows[m_selectedIndex].Delete(); }
            else {

                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "No hay articulos para eliminar.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });



            }

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

            //win2.Show();

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.Key == Key.F6)
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


            if (e.Key == Key.F3)
            {
                m_fieldsCliente = new RemiCliente();


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
                this.DeleteButton.IsEnabled = false;
            }


        }
        void fieldsPopup_Openedc(object sender, object e)
        {
            (sender as RemiCliente).InitializeFocus();
        }

        void myDialog_UpdateRequestedc(object sender, EventArgs e)
        {

            ClienteItem itemc = (e as FieldsUpdateEventArgss).UpdatedFields;
            AddItemc(itemc, false);


            m_fieldsCliente.Close();
        }
        public void AddItemc(ClienteItem item, bool addToGridAlone)
        {



            m_nombre = item.Nombre.ToString();
            m_nocliente = item.Numero_De_Cliente.ToString();
            m_rfc = item.RFC.ToString();
            m_ciudad = item.Ciudad.ToString();
            m_colonia = item.Colonia.ToString();
            m_cp = item.Cp.ToString();
            m_direccion = item.Direccion.ToString();
            m_estado = item.Estado.ToString();
            nocliente.Text = item.Numero_De_Cliente.ToString();
            Provs.Text = item.RFC.ToString();
            nombre.Text = item.Nombre.ToString();

            DueDireccion.Text = item.Direccion.ToString();
            duecolonia.Text = item.Colonia.ToString();
            dueciudad.Text = item.Ciudad.ToString();
            duecp.Text = item.Cp.ToString();

            // agreItemH(item);

        }

        public void Hola(string value)
        {
            CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }


        private void agreItemH(ClienteItem item)
        {
        }

        DataTable tablaH = new DataTable();
        DataTable tabla = new DataTable();
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


        void myDialog_CloseRequestedC(object sender, EventArgs e)
        {
            m_fieldsCliente.Close();
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


        private void Exportar_Click(object sender, RoutedEventArgs e)
        {



            // export15(strfolio);

        }

        private void export15(string folio)
        {


            DataTable tablaOrden = GetHeader(folio);
            DataTable tablaDeatil = GetDetail(folio);

            RptRemision fac = new RptRemision(tablaOrden, tablaDeatil);

        }


        static DataTable GetHeader(string folio)
        {
            string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";


            SqlConnection sqlconn = new SqlConnection(conect);

            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {


                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Revise su conexión a internet.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM RemisionHeader WHERE Folio_Remision = '" + folio + "' ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "RemisionHeader");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["RemisionHeader"];
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


                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Revise su conexón a internet.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM RemisionDetail WHERE FolioRemision = '" + folio + "' ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "RemisionDetail");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["RemisionDetail"];
            sqlconn.Close();

            return dtbl;
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(nocliente.Text) || tabla.Rows.Count == 0)
            {

                //if (string.IsNullOrWhiteSpace(nocliente.Text))
                { System.Windows.MessageBox.Show("Debe de seleccionar al menos un cliente y un articulo ", "Error al guardar"); }


                //if (tabla.Rows.Count == 0)
                //{ System.Windows.MessageBox.Show("Debe de seleccionar un articulo", "Error al actualizar"); }


            }
            else
            {
                SaveHeader();

                SaveDetail();
                string oso = m_numeroFolio.ToString();
                m_numeroFolio = m_numeroFolio + 1;
                updateFolio(m_numeroFolio);

                getFolio();
                limpgrid();
                string strfolio = "II" + oso;
                Window1 form = new Window1(strfolio);
                form.ShowDialog();

                //InvoiceGrid.Children.Clear();
                //Cleaner();
                //m_items = new List<RemiItem>();
            }











        }

        private void limpgrid()
        {   //borra todo dentro del grid
            InvoiceGrid.Children.Clear();
            //Iniciar de nuevo la la clase RemiItem(lo articulos a llevar)
            m_items = new List<RemiItem>();
            // vuelve a dibujar el grid
            UpdateGrid();
            //limpia al cliente XD
            m_fieldsCliente = new RemiCliente();




            nombre.Text = "";
            Provs.Text = "";
            nocliente.Text = "";
            DueDireccion.Text = "";
            duecolonia.Text = "";
            dueciudad.Text = "";
            duecp.Text = "";
            TotalAmount.Text = "$ 00.00";
            Iva.Text = "$ 00.00";
            TotalIva.Text = "$ 00.00";
            tabla.Rows.Clear();
            estrella = 1;
            ObservacionCliente.Text = "";

            //MessageBox.Show("se guardo Correctamente ");



        }
        private void SaveHeader()
        {

            try {
                string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
                SqlConnection sqlcon = new SqlConnection(connectionStringer);
                sqlcon.Open();

                string obsv = "";
                obsv = ObservacionCliente.Text;
                string folio = txtFolio.Text;
                decimal subtotal = Convert.ToDecimal(TotalAmount_Copy.Text);
                decimal total = Convert.ToDecimal(TotalIva_Copy.Text);
                string numeroremi = m_numeroFolio.ToString() + "007";
                m_numeroRemision = Convert.ToInt32(numeroremi);

                string consulta = "INSERT INTO [dbo].[RemisionHeader] ([RemisionID],[Numero_De_Cliente],[RFC],[Nombre],[Direccion],[Colonia],[CP] ,[Ciudad], [Estado],[Pais],[Correo_Electronico],[Subtotal],[Total],[Folio_Remision],[Observaciones],[Tipo_De_Cliente],[Codigo_De_Condiciones_De_Pago],[Codigo_De_Ejecutivo],[Fecha_Y_Hora_De_Ultima_Actualizacion]) VALUES(" + numeroremi + ", '" + m_nocliente + "' ,'" + m_rfc + "','" + m_nombre + "','" + m_direccion + "','" + m_colonia + "','" + m_cp + "','" + m_ciudad + "','" + m_estado + "','',''," + subtotal + ", " + total + ", '" + folio + "','" + obsv + "','','', '', getdate())";
                SqlCommand agregar = new SqlCommand(consulta, sqlcon);
                agregar.ExecuteNonQuery();


                sqlcon.Close();
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


        private void updateFolio(int folio)
        {


            try
            {
                string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
                SqlConnection sqlcon = new SqlConnection(connectionStringer);
                sqlcon.Open();
                string consulta = " UPDATE [dbo].[DatosRemision] SET [Prefijo] = 'II' ,[NumeroRemision] = " + folio + " WHERE Prefijo = 'II'";
                SqlCommand agregar = new SqlCommand(consulta, sqlcon);
                agregar.ExecuteNonQuery();
                sqlcon.Close();
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


        private void SaveDetail()
        {
            try
            {
                string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
                SqlConnection sqlcon = new SqlConnection(connectionStringer);
                sqlcon.Open();

                foreach (DataRow Registro in tabla.Rows)
                {
                    int numeroremi = m_numeroRemision;
                    string folio = txtFolio.Text;
                    string medida = "NA";
                    string codigo = Registro[2].ToString();
                    string id = Convert.ToString(Registro[0]);
                    int renglon = Convert.ToInt32(Registro[1]);
                    string descripcion = Registro[3].ToString();
                    int cantidad = Convert.ToInt32(Registro[4]);
                    decimal precioventa = Convert.ToDecimal(Registro[07]);
                    decimal preciototal = Convert.ToDecimal(Registro[08]);

                    medida = Convert.ToString(Registro[09]);
                    decimal preciolista = Convert.ToDecimal(Registro[05]);
                    int nivel = Convert.ToInt16(Registro[06]);

                    string consulta = "INSERT INTO [dbo].[RemisionDetail] ([RemisionID],[RemisionDetailID],[ProductID],[CodeID],[ProductDescription],[UnitID],[Qty] ,[UnitPrice],[ListPrice],[FolioRemision],[Nivel], [ModifiedDate],[LastUpdate]) VALUES(" + numeroremi + "," + renglon + ",100,'" + codigo + "','" + descripcion + "','" + medida + "'," + cantidad + "," + precioventa + "," + preciolista + ",'" + folio + "','" + nivel + "',GETDATE(),GETDATE())";
                    SqlCommand agregar = new SqlCommand(consulta, sqlcon);
                    agregar.ExecuteNonQuery();





                }

                sqlcon.Close();
            }
            catch (SqlException ex) {

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


        private void M_producto_CloseRequested1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void addressDialog_UpdateRequested(object sender, EventArgs e)
        {
            // this.Name.Text = m_billInfo.Name;
            //this.BillingAddress.Text = m_billInfo.Address;
            //this.DueDate.Text = m_billInfo.Date.ToString("d");
            //this.InvoiceNumber.Text = m_billInfo.InvoiceNumber;
            //this.DueDate.Text = m_billInfo.DueDate.ToString("d");
            m_addressPopup.Close();
        }
        void addressDialog_CloseRequested(object sender, EventArgs e)
        {
            m_addressPopup.Close();
        }
        void arddressDialog_CloseRequested(object sender, EventArgs e)
        {
            m_producto.Close();
            // m_addressPopup.Close();
        }

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
            RemiItem invoiceItem = m_items[m_selectedIndex];
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

        /// <param name="e"></param>
        void EditDialog_CloseRequested(object sender, EventArgs e)
        {
            m_fieldsPopup.Close();
        }
        void EditDialog_UpdateRequested(object sender, EventArgs e)
        {
            UpdateGrid();
            m_fieldsPopup.Close();
        }
        private void Export(object sender, RoutedEventArgs e)
        {
            //this.BottomAppBar1.IsOpen = true;
        }

        private void About(object sender, RoutedEventArgs e)
        {

        }

        private RowDefinition CreateRowDefinition(double height)
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(height);
            return rowDefinition;
        }
        //
        private ColumnDefinition CreateColumnDefinition(double width)
        {
            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(width);

            return columnDefinition;
        }



        public void AddItem(RemiItem item, bool addToGridAlone)
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
            //SetCell(rowIndex, 7, "$" + item.Total.ToString("#,###.00", CultureInfo.InvariantCulture));
            //SetCell(rowIndex, 8, "$" + item.Iva.ToString("#,###.00", CultureInfo.InvariantCulture));

            //<Rectangle Grid.Row="0" Height="1" StrokeThickness="0.75" VerticalAlignment="Bottom" Grid.ColumnSpan="5" 
            //StrokeDashArray="4,4" Stroke="#FFCECECE"> </Rectangle>

            m_totalDue += Convert.ToDouble(item.Total);
            m_totalCant += Convert.ToDouble(item.Cantidad);
            m_currentRowIndex++;
            m_selectedIndex = m_items.Count;
            UpdateTotal();
            UpdateCantidad();


        }

        private void agreItem(RemiItem item)
        {
            //m_cliente = new Cliente();
            //item.Marca = m_cliente.Marci;

            item.Numero_De_Documento = txtFolio.Text;

            item.Renglon = estrella;
            // item.Renglon = tabla.Rows.Count;
            tabla.Rows.Add(item.Numero_De_Documento, item.Renglon, item.Codigo, item.Descripcion, item.Cantidad, item.Preciolista, item.Nivel, item.Rate, item.Total, item.Medida);
            estrella = estrella + 1;
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


        //private void DrawBorderx(int rowIndex)
        //{
        //    Rectangle rect = new Rectangle();
        //    rect.Height = 1;
        //    rect.StrokeThickness = 0.75;
        //    rect.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
        //    rect.StrokeDashArray.Add(4);
        //    rect.StrokeDashArray.Add(4);
        //    rect.Stroke = new SolidColorBrush(Color.FromArgb(255, 206, 206, 206));
        //    Grid.SetColumn(rect, 0);

        //    Grid.SetColumnSpan(rect, 9);
        //    Grid.SetRow(rect, rowIndex);
        //    InvoiceGridx.Children.Add(rect);
        //}

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
                textBlockDollor.FontSize = 13;
                //textBlockDollor.FontFamily = new FontFamily("Segoe UI");
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
                //textBlock.FontFamily = new FontFamily("Segoe UI");
                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
                //HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
                if (columnIndex == 0)
                    textBlock.Padding = new Thickness(10, 0, 0, 0);

                else
                    textBlock.Padding = new Thickness(0, 0, 15, 0);
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
                textBlock.FontSize = 13;
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
            else
            {
                //Data
                TextBlock textBlock = null;
                textBlock = new TextBlock();
                textBlock.Text = value.Trim('$');
                textBlock.FontSize = 13;
                //textBlock.FontFamily = new FontFamily("Segoe UI");
                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
                //HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
                if (columnIndex == 0)
                    textBlock.Padding = new Thickness(10, 0, 0, 0);
                else
                    textBlock.Padding = new Thickness(0, 0, 15, 0);
                //textBlock.Foreground = new SolidColorBrush(Colors.Black);
                textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                if (columnIndex > 0)
                    textBlock.TextAlignment = TextAlignment.Right;

                SetCell(rowIndex, columnIndex, textBlock);
            }
        }

        //private void SetCellx(int rowIndex, int columnIndex, string value)
        //{
        //    if (columnIndex == 3 || columnIndex == 5 || columnIndex == 6)
        //    {
        //        Grid amountGrid = new Grid();
        //        Grid.SetColumn(amountGrid, columnIndex);
        //        Grid.SetRow(amountGrid, rowIndex);
        //        InvoiceGridx.Children.Add(amountGrid);
        //        amountGrid.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
        //        if (columnIndex == 2)
        //            amountGrid.ColumnDefinitions.Add(new ColumnDefinition());
        //        amountGrid.ColumnDefinitions.Add(new ColumnDefinition());
        //        amountGrid.ColumnDefinitions.Add(new ColumnDefinition());
        //        //amountGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength( new double(),GridUnitType.Auto)});

        //        //Dollar
        //        TextBlock textBlockDollor = null;
        //        textBlockDollor = new TextBlock();
        //        textBlockDollor.Text = "$";
        //        textBlockDollor.FontSize = 14;
        //        textBlockDollor.FontFamily = new FontFamily("Segoe UI");
        //        textBlockDollor.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
        //        //textBlockDollor.Foreground = new SolidColorBrush(Colors.Black);
        //        textBlockDollor.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
        //        textBlockDollor.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        //        textBlockDollor.TextAlignment = TextAlignment.Right;
        //        if (columnIndex == 2)
        //            Grid.SetColumn(textBlockDollor, 1);
        //        else
        //            Grid.SetColumn(textBlockDollor, 0);
        //        Grid.SetRow(textBlockDollor, 0);
        //        amountGrid.Children.Add(textBlockDollor);
        //        //Data
        //        TextBlock textBlock = null;
        //        textBlock = new TextBlock();
        //        textBlock.Text = value.Trim('$');
        //        textBlock.FontSize = 14;
        //        textBlock.FontFamily = new FontFamily("Segoe UI");
        //        textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
        //        //HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
        //        if (columnIndex == 0)
        //            textBlock.Padding = new Thickness(10, 0, 0, 0);

        //        else
        //            textBlock.Padding = new Thickness(0, 0, 15, 0);
        //        //textBlock.Foreground = new SolidColorBrush(Colors.Black);
        //        textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
        //        textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

        //        if (columnIndex > 0)
        //            textBlock.TextAlignment = TextAlignment.Right;
        //        if (columnIndex == 2)
        //            Grid.SetColumn(textBlock, 2);
        //        else
        //            Grid.SetColumn(textBlock, 1);
        //        Grid.SetRow(textBlock, 0);
        //        amountGrid.Children.Add(textBlock);
        //    }
        //    else if (columnIndex == 2 || columnIndex == 4)
        //    {
        //        TextBlock textBlock = null;
        //        textBlock = new TextBlock();
        //        textBlock.Text = value.Trim('$');
        //        textBlock.FontSize = 14;
        //        textBlock.FontFamily = new FontFamily("Segoe UI");
        //        textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
        //        //HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
        //        if (columnIndex == 0)
        //            textBlock.Padding = new Thickness(10, 0, 0, 0);
        //        else
        //            textBlock.Padding = new Thickness(0, 0, 15, 0);
        //        //textBlock.Foreground = new SolidColorBrush(Colors.Black);
        //        textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
        //        textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

        //        if (columnIndex > 0)
        //            textBlock.TextAlignment = TextAlignment.Center;

        //        SetCellx(rowIndex, columnIndex, textBlock);


        //    }
        //    else
        //    {
        //        //Data
        //        TextBlock textBlock = null;
        //        textBlock = new TextBlock();
        //        textBlock.Text = value.Trim('$');
        //        textBlock.FontSize = 14;
        //        textBlock.FontFamily = new FontFamily("Segoe UI");
        //        textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
        //        //HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
        //        if (columnIndex == 0)
        //            textBlock.Padding = new Thickness(10, 0, 0, 0);
        //        else
        //            textBlock.Padding = new Thickness(0, 0, 15, 0);
        //        //textBlock.Foreground = new SolidColorBrush(Colors.Black);
        //        textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
        //        textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

        //        if (columnIndex > 0)
        //            textBlock.TextAlignment = TextAlignment.Right;

        //        SetCellx(rowIndex, columnIndex, textBlock);
        //    }
        //}



        private void SetCell(int rowIndex, int columnIndex, TextBlock textBlock)
        {
            Grid.SetColumn(textBlock, columnIndex);
            Grid.SetRow(textBlock, rowIndex);
            InvoiceGrid.Children.Add(textBlock);
        }

        //private void SetCellx(int rowIndex, int columnIndex, TextBlock textBlock)
        //{
        //    Grid.SetColumn(textBlock, columnIndex);
        //    Grid.SetRow(textBlock, rowIndex);
        //    InvoiceGridx.Children.Add(textBlock);
        //}


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
            foreach (RemiItem item in m_items)
            {
                AddItem(item, true);
            }
            UpdateTotal();
            UpdateCantidad();
        }



        private void UpdateGrid1()
        {
            m_totalDue = 0;
            m_totalCant = 0;
            m_currentRowIndex = 0;
            InvoiceGrid.Children.Clear();
            //InvoiceGrid.RowDefinitions.RemoveAt(InvoiceGrid.RowDefinitions.Count - 1);
            for (int i = 0; i < InvoiceGrid.RowDefinitions.Count; i++)
                DrawBorder(i);
            //foreach (RemiItem item in m_items)
            //{
            //    AddItem(item, true);
            //}
            UpdateTotal();
            UpdateCantidad();
        }
        /// <summary>
        /// 
        /// </summary>
        void UpdateTotal()
        {
            this.TotalAmount.Text = "$" + TotalDue.ToString("#,###.00", CultureInfo.InvariantCulture);
            articulos.Text = TotalDue.ToString();
            TotalAmount_Copy.Text = m_totalDue.ToString();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            m_fieldsCliente = new RemiCliente();


            m_fieldsCliente.CloseRequestedc += myDialog_CloseRequestedC;
            m_fieldsCliente.UpdateRequestedc += myDialog_UpdateRequestedc;
            m_fieldsCliente.Activated += fieldsPopup_Openedc;
            m_fieldsCliente.Background = new SolidColorBrush(Colors.Red);
            RootGrid.Opacity = 0.1;
            m_fieldsCliente.Opacity = 1;
            m_fieldsCliente.ShowDialog();
            RootGrid.Opacity = 1;

        }

        //private void txtFoliox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    InvoiceGridx.Children.Clear();
        //    UpdateGridx();
        //    Searchvalue(txtFoliox.Text);
        //    fillgrig(txtFoliox.Text);

        //}

        //private void fillgrig(string value)
        //{

        //    string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";


        //    SqlConnection sqlconn = new SqlConnection(conect);

        //    try
        //    {
        //        sqlconn.Open();
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show("ERROR DE CONEXION" + ex.Message);
        //    }
        //    SqlDataAdapter adapter = new SqlDataAdapter("SELECT CodeID, ProductDescription, Qty, UnitPrice,LineTotal FROM RemisionDetail WHERE FolioRemision = '" + value + "' ", sqlconn);
        //    DataSet dsPubs = new DataSet("Pubs");
        //    adapter.Fill(dsPubs, "RemisionDetail");
        //    DataTable dtbl = new DataTable();

        //    dtbl = dsPubs.Tables["RemisionDetail"];
        //    sqlconn.Close();

        //    if (dtbl.Rows.Count > 0 )
        //    {
        //        // InvoiceGridx.ItemsSource = dtbl.DefaultView;

        //        //InvoiceGridx.ItemsSource = dsPubs.Tables["RemisionDetail"].DefaultView;
        //        fillgrigx(dtbl, false);



        //    }



        //}

        //private void fillgrigx(System.Data.DataTable tbl, bool addToGridAlone)
        //{
        //     int rowIndexx = 0;

        //     foreach (DataRow Registro in tbl.Rows)
        //     {


        //         //int rowIndex = m_currentRowIndex;

        //         //if (!addToGridAlone)
        //         //    m_items.Add(item);

        //         string codigo = Convert.ToString(Registro[0]);
        //         string descr = Convert.ToString(Registro[1]);
        //         string qty = Convert.ToString(Registro[2]);
        //        double rate = Convert.ToDouble(Registro[3]);
        //         double total = Convert.ToDouble(Registro[4]);
        //         if (!(InvoiceGridx.RowDefinitions.Count > m_currentRowIndex))
        //         {
        //             InvoiceGridx.RowDefinitions.Add(CreateRowDefinition(m_rowHeight));


        //         }
        //         DrawBorderx(rowIndexx);


        //         SetCellx(rowIndexx, 0, codigo);
        //         SetCellx(rowIndexx, 1, descr);
        //         SetCellx(rowIndexx, 2,qty);
        //         SetCellx(rowIndexx, 5, "$" + rate.ToString("#,###.00", CultureInfo.InvariantCulture));
        //         SetCellx(rowIndexx, 6,"$" + total.ToString("#,###.00", CultureInfo.InvariantCulture));
        //         rowIndexx = rowIndexx + 1;
        //         //SetCell(rowIndex, 1, item.Descripcion.ToString());
        //         //SetCell(rowIndex, 2, item.Cantidad.ToString());
        //         //SetCell(rowIndex, 3, item.Preciolista.ToString("#,###.00", CultureInfo.InvariantCulture));
        //         //SetCell(rowIndex, 4, item.Nivel.ToString());
        //         //SetCell(rowIndex, 5, "$" + item.Rate.ToString("#,###.00", CultureInfo.InvariantCulture));
        //         //SetCell(rowIndex, 6, "$" + item.Total.ToString("#,###.00", CultureInfo.InvariantCulture));

        //     }

        // }



        //private void Searchvalue(string value)
        //{

        //    string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";


        //    SqlConnection sqlconn = new SqlConnection(conect);

        //    try
        //    {
        //        sqlconn.Open();
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show("ERROR DE CONEXION" + ex.Message);
        //    }
        //    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM RemisionHeader WHERE Folio_Remision = '" + value + "' ", sqlconn);
        //    DataSet dsPubs = new DataSet("Pubs");
        //    adapter.Fill(dsPubs, "RemisionHeader");
        //    DataTable dtblx = new DataTable();

        //    dtblx = dsPubs.Tables["RemisionHeader"];
        //    sqlconn.Close();
        //    if (dtblx.Rows.Count > 0) {
        //        DueDireccionx.Text = dtblx.Rows[0][4].ToString();
        //        nombrex.Text = dtblx.Rows[0][3].ToString();
        //        Provsx.Text = dtblx.Rows[0][2].ToString();
        //        duecoloniax.Text = dtblx.Rows[0][5].ToString();
        //        duecpx.Text = dtblx.Rows[0][6].ToString();
        //        noclientex.Text = dtblx.Rows[0][1].ToString();
        //        dueciudadx.Text = dtblx.Rows[0][7].ToString();
        //        ObservacionClientex.Text = dtblx.Rows[0][14].ToString();
        //        double sb = 00.0;
        //        sb = Convert.ToDouble(dtblx.Rows[0][11]);
        //        cantidades(sb);
        //        //evento que mante la cantidad
        //    }



        //}

        //private void cantidades(double valor)
        //{


        //    TotalAmountx.Text = "$" + valor.ToString("#,###.00", CultureInfo.InvariantCulture);
        //    double iva = valor * 0.16;
        //    Ivax.Text = "$" + iva.ToString("#,###.00", CultureInfo.InvariantCulture);
        //    double total = valor * 1.16;
        //    TotalIvax.Text = "$" + total.ToString("#,###.00", CultureInfo.InvariantCulture);


        //}

        //private void Savex_Click(object sender, RoutedEventArgs e)
        //{
        //    string strfolio = txtFoliox.Text;
        //    Window1 formi = new Window1(strfolio);
        //    formi.ShowDialog();
        //}

        private void llenarConsulta()
        {
            try
            {

                string conect = "SERVER = gggctserver.database.windows.net; DATABASE =devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099";
                SqlConnection con = new SqlConnection(conect);

                string cmd = "SELECT top 30 [Folio_Remision],[Numero_De_Cliente],[Nombre],[RFC],[Direccion] ,[Colonia],[Ciudad]  ,[Total] FROM [dbo].[RemisionHeader]   order by  [Fecha_Y_Hora_De_Ultima_Actualizacion] desc ";
                con.Open();

                DataTable dtbl = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);

                DataSet dsPubs = new DataSet("Pubs");

                sda.Fill(dsPubs, "RemisionHeader");

                dtbl = dsPubs.Tables["RemisionHeader"];

                InvoiceRemision.ItemsSource = dsPubs.Tables["RemisionHeader"].DefaultView;
                //dataGrid2.Columns[0].IsVisible = false;

                con.Close();
            }

            catch (SqlException ex) {

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

        private void RadButton_Click(object sender, RoutedEventArgs e)

        {


            var objtitem = InvoiceRemision.SelectedItem;
            string oso = ((System.Data.DataRowView)objtitem).Row.ItemArray[0].ToString();
            //string folio = InvoiceRemision.row
            descargar(oso);
            //Window1 formi = new Window1(oso);
            //formi.ShowDialog();
            pdfview formu = new pdfview(oso);
            formu.ShowDialog();

        }


        private void descargar(string folio)
        {

            try {

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

        private void Exportar_Click_1(object sender, RoutedEventArgs e)
        {
            Exportar.IsEnabled = false;
            if (dtinicial.SelectedDate != null && dtfinal.SelectedDate != null)
            {
                if (dtinicial.SelectedDate <= dtfinal.SelectedDate)
                {
                    generareporte();
                }
                else
                {
                    RadWindow radWindow = new RadWindow();
                    RadWindow.Alert(new DialogParameters()
                    {
                        Content = "La fecha inicial no debe de ser mayor que la fecha final.",
                        Header = "BIG",

                        DialogStartupLocation = WindowStartupLocation.CenterOwner
                        // IconContent = "";
                    });


                }


            }
            else
            {
                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Los parámetros son inválidos.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
                Exportar.IsEnabled = true;

            }
           
        }


        private void generareporte()
        {
           
            try
            {

                string strFechaIniciale = Convert.ToDateTime(dtinicial.SelectedDate).ToString("yyyy-MM-dd");
                string strFechaFinale = Convert.ToDateTime(dtfinal.SelectedDate).ToString("yyyy-MM-dd");

                string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
                SqlConnection sqlcon = new SqlConnection(connectionStringer);
                sqlcon.Open();
                string consulta = "SELECT [Folio_Remision] as Folio,[Numero_De_Cliente] as NoCliente,[Nombre] ,CodeID as Codigo,ProductDescription as Descripcion,Qty as Cantidad ,convert(varchar, Fecha_Y_Hora_De_Ultima_Actualizacion, 103) as Fecha,cast(LineTotal as decimal(10,2)) as Total " +
                                   "FROM[dbo].[RemisionHeader] INNER JOIN RemisionDetail ON RemisionHeader.Folio_Remision = RemisionDetail.FolioRemision " +
                                   "where Fecha_Y_Hora_De_Ultima_Actualizacion BETWEEN '" + strFechaIniciale + "' AND '" + strFechaFinale + "' order by Fecha_Y_Hora_De_Ultima_Actualizacion";

                SqlDataAdapter sda = new SqlDataAdapter(consulta, sqlcon);

                DataSet dsPubs = new DataSet("Pubs");

                sda.Fill(dsPubs, "tblremi");

                DataTable dtbla = new DataTable();

                dtbla = dsPubs.Tables["tblremi"];

                string vpn = dtbla.Rows[0][0].ToString();

                sqlcon.Close();

                Formatear(dtbla);

            }
            catch (Exception ex)
            {
                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "No existen registros para exportar.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });

            }
        }

        private void  Formatear(DataTable vista)
        {
            string strFechaInicial = Convert.ToDateTime(dtinicial.SelectedDate).ToString("dd-MMM-yyyy").ToUpper();
            string strFechaFinal = Convert.ToDateTime(dtfinal.SelectedDate).ToString("dd-MMM-yyyy").ToUpper();



            DateTime date = DateTime.Now;
            string datewithformat = date.ToString();
            string dateday = date.ToString("dd MMM yyyy HH mm ");

            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;
                //IStyle headerStyle = wo.Styles.Add("HeaderStyle");
                IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];
                worksheet.EnableSheetCalculations();
                DataTable tabla = vista;
                int osos = tabla.Rows.Count;
                worksheet.Name = "LRG";
                worksheet.ImportDataTable(tabla, true, 2, 1);
                worksheet.AutoFilters.FilterRange = worksheet.Range["A2:H2"];
                string namesuc = "Mayoreo";
                worksheet.Range["A1"].Text = "Llantas y Rines del Guadiana S.A. de C.V. - Remisiones LRG Del " + strFechaInicial + " Al " + strFechaFinal;
                // worksheet.Range["A1"].Text = "Llantas y Rines del Guadiana S.A. de C.V. - Existencias LRG Al "+dateday+"- B4 Francisco Villa";
                worksheet.Rows[1].FreezePanes();
                worksheet.Rows[2].FreezePanes();

                IStyle headerStyle = workbook.Styles.Add("HeaderStyle");
                headerStyle.BeginUpdate();

                workbook.SetPaletteColor(8, System.Drawing.Color.FromArgb(46, 204, 113));

                headerStyle.Color = System.Drawing.Color.FromArgb(46, 204, 113);

                headerStyle.Font.Bold = true;

                headerStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;

                headerStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                headerStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;

                headerStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                headerStyle.EndUpdate();

                worksheet.Rows[1].CellStyle = headerStyle;

                IStyle pStyle = workbook.Styles.Add("pStyle");
                pStyle.BeginUpdate();

                workbook.SetPaletteColor(9, System.Drawing.Color.FromArgb(89, 171, 227));

                pStyle.Color = System.Drawing.Color.FromArgb(89, 171, 227);
                
                pStyle.Font.Bold = true;

                pStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;

                pStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                pStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;

                pStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                pStyle.EndUpdate();

                worksheet.Rows[0].CellStyle = pStyle;
                worksheet.SetColumnWidth(1, 10);
                worksheet.SetColumnWidth(2, 10);
                worksheet.SetColumnWidth(3, 25);
                worksheet.SetColumnWidth(4, 10);
                worksheet.SetColumnWidth(5, 61);
                worksheet.SetColumnWidth(6, 7);
                worksheet.SetColumnWidth(7, 10);
                worksheet.SetColumnWidth(8, 10);

                IStyle pStyles= workbook.Styles.Add("pStyles");
                pStyles.BeginUpdate();
                worksheet.Columns[3].HorizontalAlignment = ExcelHAlign.HAlignLeft;

                pStyles.EndUpdate();
               
                // Create Table with data in the given range
                int soviet = osos;
                int rojos = soviet + 3;
                int rus = soviet + 4;
                string rusia = rus.ToString();
                string cossacks = rojos.ToString();
                string gulag = "A2:H" + cossacks + "";
                //IListObject table = worksheet.ListObjects.Create("Table1", worksheet[gulag]);
                string registros = soviet.ToString();
                //IRange range = worksheet.Range[gulag];
                //table.ShowTotals = true;
                //table.Columns[0].TotalsRowLabel = "Total";

                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                string chorchill = "H2,H" + cossacks + "";
                string russevel = "H" + rusia + "";
                string totalr = "A" + rusia + "";
                worksheet.Range[totalr].Text = registros + " Registros";
                worksheet.Range[totalr].CellStyle = pStyle;
                string nrusia = "=SUM(H2:H" + cossacks + ")";
                worksheet.Range[russevel].Formula = nrusia;
                worksheet.Range[russevel].CellStyle = pStyle;
                worksheet.Range["H2:"+russevel].NumberFormat = "#,##0.00";
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                //table.Columns[5].TotalsCalculation = ExcelTotalsCalculation.Sum;
                //hacer el subtotal pero conformula ** el otro marca error con total calculation 
                //range.SubTotal(0, ConsolidationFunction.Sum, new int[] {1,rojos});
                string namer = dateday;
                string fileName = "Remisiones LRG Del " + strFechaInicial + " Al " + strFechaFinal + "( " + dateday + " )" + ".xlsx";
                // string fileName = "LRG-Existencias al " + namer + "B4 Francisco Villa.xlsx";
                workbook.SaveAs(@"C:\Ektelesis.Net\"+ fileName);
              
                //workbook.Close();
                //excelEngine.Dispose();

            }



        }
    }
}

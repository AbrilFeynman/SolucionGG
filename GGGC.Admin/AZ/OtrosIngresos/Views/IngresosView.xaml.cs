using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace GGGC.Admin.AZ.OtrosIngresos.Views
{
    /// <summary>
    /// Interaction logic for IngresosView.xaml
    /// </summary>
    public partial class IngresosView : UserControl
    {
        #region Fields
        const int m_columnCount = 9;
        private double m_totalDue;
        private string m_nombre;
        private int m_numeroRemision;
        private string m_rfc;
        private string m_direccion;
        private string m_numeroDocumento;
        private string m_colonia;
        private string m_ciudad;
        private string m_estado;
        private string m_cp;
        private string m_nocliente;
        private double m_totalCant;
        private int m_numeroFolio;
        
        int m_currentRowIndex = 0;
        OtrosCliente m_fieldsCliente;
        int m_selectedIndex = -1;
        int m_prevSelectedIndex = -1;
        const int m_rowHeight = 30;
        Border m_border;
       
       
        
        
       
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

        public string NumeroDocumento
        {
            get
            {
                return m_numeroDocumento;
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


        private System.Collections.ObjectModel.ObservableCollection<City> cities = new ObservableCollection<City>();

        public IngresosView()
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
            // Sets the UI culture to French (France)
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-MX");
            //
            InitializeComponent();
            StyleManager.SetTheme(InvoiceRemision, new Windows8Theme());
            llenarcomboo();
            Initialize();

        }

        private void Initialize()
        {
           

          

            m_border = new Border();

          

            getFolio();
            double tres = 28.77;
            string ese = tres.ToString();
            cities.Add(new City() { Codigo = "1", Forma = "Contado" });
            cities.Add(new City() { Codigo = "2", Forma = "Crédito" });
            
            DataContext = cities;

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


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void DecimalValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void getFolio()
        {
            try
            {
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
            catch (SqlException ex)
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


        }


       

        private void M_producto_CloseRequested(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

       
       

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (tabla.Rows.Count > 0)
            { tabla.Rows[m_selectedIndex].Delete(); }
            else
            {

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

        private void RemoveItem(int index)
        {
            if (index < 0)
                return;
            if (InvoiceGrid.Children.Count <= 0)
                return;
            //if (index < m_items.Count)
            //{
            //    m_items.RemoveAt(index);
            //    UpdateGrid();
            //}
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {




            if (e.Key == Key.F3)
            {
                m_fieldsCliente = new OtrosCliente();


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
            (sender as OtrosCliente).InitializeFocus();
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


        private void llenarcomboo()
        {
            try
            {

                string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
                SqlConnection con = new SqlConnection(conect);
                try
                {
                    con.Open();

                    // string cmd = " SELECT Codigo_De_Articulo,Nivel_De_Precios,Precio FROM Precios where Codigo_De_Articulo = '" + codigo + "' ";

                    string cmd = "SELECT  Codigo,Descripcion FROM TipoIngreso ";

                    SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);
                    DataSet dsPubs = new DataSet("Pubs");
                    sda.Fill(dsPubs, "TipoIngreso");
                    DataTable dtblg = new DataTable();
                    dtblg = dsPubs.Tables["TipoIngreso"];
                    combo.ItemsSource = dsPubs.Tables["TipoIngreso"].DefaultView;
                    //cboRegion.ItemsSource = dtblg.DefaultView;
                    var oso = dsPubs.Tables["TipoIngreso"].DefaultView;


                    try
                    {
                        //    string apollo = dtblg.Rows[0][2].ToString();
                        //    string med = dtblg.Rows[0][3].ToString();
                        //    m_invoiceItem.Medida = med;
                        //    m_invoiceItem.Preciolista = Convert.ToDouble(apollo);
                    }
                    catch
                    {


                    }
                    //dataGrid1.ItemsSource = dsPubs.Tables["Precios"].DefaultView;
                    //dataGrid1.Columns[0].Visibility = false;
                    //dataGrid1.Tables["LLantas"].Columns[0].ColumnMapping = MappingType.Hidden;
                    con.Close();

                }

                catch (SqlException ex)
                {


                }



            }
            catch (InvalidCastException e)
            {

            }
            //quemedidatengo(codigo);
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
        


        void myDialog_CloseRequestedC(object sender, EventArgs e)
        {
            m_fieldsCliente.Close();
        }

       


        private void Exportar_Click(object sender, RoutedEventArgs e)
        {



            // export15(strfolio);

        }

        private void export15(string folio)
        {


            DataTable tablaOrden = GetHeader(folio);
            DataTable tablaDeatil = GetDetail(folio);

            RptIngresos fac = new RptIngresos(tablaOrden, tablaDeatil);

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

            if (string.IsNullOrWhiteSpace(nocliente.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                

                //if (string.IsNullOrWhiteSpace(nocliente.Text))
                { System.Windows.MessageBox.Show("Debe de seleccionar al menos un cliente y un articulo ", "Error al guardar"); }


                //if (tabla.Rows.Count == 0)
                //{ System.Windows.MessageBox.Show("Debe de seleccionar un articulo", "Error al actualizar"); }


            }
            else
            {

                if (string.IsNullOrEmpty(combo.Text) || string.IsNullOrEmpty(combo2.Text))
                {
                    RadWindow radWindow = new RadWindow();
                    RadWindow.Alert(new DialogParameters()
                    {
                        Content = "Seleccione el tipode ingreso y forma de pago.",
                        Header = "BIG",

                        DialogStartupLocation = WindowStartupLocation.CenterOwner
                        // IconContent = "";
                    });

                }
                else {
                    SaveHeader();

                    SaveDetail();
                    string oso = m_numeroFolio.ToString();
                    m_numeroFolio = m_numeroFolio + 1;
                    updateFolio(m_numeroFolio);

                    getFolio();
                    limpgrid();
                    string strfolio = m_numeroRemision.ToString();
                    Window1 form = new Window1(strfolio);
                    form.ShowDialog();

                }
                

                //InvoiceGrid.Children.Clear();
                //Cleaner();
                //m_items = new List<RemiItem>();
            }











        }


        private void limpgrid()
        {  

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

            txtDescripcion.Text = "";
            txtCantidad.Text = "0";
            txtPrecioVenta.Text = "00.00";
            txtPrecioExtend.Text = "00.00";


        }
        private void SaveHeader()
        {

            try
            {
                string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
                SqlConnection sqlcon = new SqlConnection(connectionStringer);
                sqlcon.Open();

                string obsv = "";
                obsv = ObservacionCliente.Text;
                string folio = txtFolio.Text;
               string subtotal =TotalAmount_Copy.Text;
                string total = TotalIva_Copy.Text;
                string numeroremi = m_numeroFolio.ToString() + "007";
                m_numeroDocumento = numeroremi;
                m_numeroRemision = Convert.ToInt32(numeroremi);
                //toma elvalor de un combbo fill by binding 
                City selectedCar = (City)combo2.SelectedItem;
                int selecteVal = Convert.ToInt32(selectedCar.Codigo);

               
                

                string consulta = "INSERT INTO [dbo].[IngresosHeader] ([IngresosID],[Numero_De_Cliente],[RFC],[Nombre],[Direccion],[Colonia],[CP] ,[Ciudad], [Estado],[Pais],[Correo_Electronico],[Subtotal],[Total],[FormaPagoID],[Folio],[Observaciones],[Tipo_De_Cliente],[Codigo_De_Condiciones_De_Pago],[Codigo_De_Ejecutivo],[Fecha_Y_Hora_De_Ultima_Actualizacion]) VALUES(" + numeroremi + ", '" + m_nocliente + "' ,'" + m_rfc + "','" + m_nombre + "','" + m_direccion + "','" + m_colonia + "','" + m_cp + "','" + m_ciudad + "','" + m_estado + "','',''," + subtotal + ", " + total + "," + selecteVal + ", '" + folio + "','" + obsv + "','','', '', getdate())";
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


                string numeroremi = m_numeroFolio.ToString() + "007";
             
                    string folio = txtFolio.Text;
                    string medida = "NA";
                    
                   // string id = Convert.ToString(Registro[0]);
                    int renglon = 1;
                    string descripcion = txtDescripcion.Text;
                    string cantidad = txtCantidad.Text;
                    string precioventa = txtPrecioVenta.Text;
                    decimal preciototal = Convert.ToDecimal(TotalIva_Copy.Text);

                    medida = "NA";
                    decimal preciolista =Convert.ToDecimal(txtPrecioVenta.Text);
                    int nivel = 0;
                    //tipo ingreso seria el codigo del artitculo
                    var objPago = combo.SelectedItem;
                    string codigo = ((System.Data.DataRowView)objPago).Row.ItemArray[0].ToString();

                    string consulta = "INSERT INTO [dbo].[IngresosDetail] ([IngresosID],[IngresosDetailID],[ProductID],[CodeID],[ProductDescription],[UnitID],[Qty] ,[UnitPrice],[ListPrice],[Nivel], [ModifiedDate],[LastUpdate]) VALUES(" + numeroremi + "," + renglon + ",100,'" + codigo + "','" + descripcion + "','" + medida + "'," + cantidad + "," + precioventa + "," + preciolista + ",'" + nivel + "',GETDATE(),GETDATE())";
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


        private void M_producto_CloseRequested1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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


        /// <param name="e"></param>
        
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



      

        //private void agreItem(RemiItem item)
        //{
        //    //m_cliente = new Cliente();
        //    //item.Marca = m_cliente.Marci;

        //    item.Numero_De_Documento = txtFolio.Text;

        //    item.Renglon = estrella;
        //    // item.Renglon = tabla.Rows.Count;
        //    tabla.Rows.Add(item.Numero_De_Documento, item.Renglon, item.Codigo, item.Descripcion, item.Cantidad, item.Preciolista, item.Nivel, item.Rate, item.Total, item.Medida);
        //    estrella = estrella + 1;
        //}



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

        

     




        private void SetCell(int rowIndex, int columnIndex, TextBlock textBlock)
        {
            Grid.SetColumn(textBlock, columnIndex);
            Grid.SetRow(textBlock, rowIndex);
            InvoiceGrid.Children.Add(textBlock);
        }

       

       
        /// <summary>
        /// 
        /// </summary>
   

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
           
        }


        void UpdateCantidad()
        {
            // this.TotalCantidad.Text = TotalCant.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            m_fieldsCliente = new OtrosCliente();


            m_fieldsCliente.CloseRequestedc += myDialog_CloseRequestedC;
            m_fieldsCliente.UpdateRequestedc += myDialog_UpdateRequestedc;
            m_fieldsCliente.Activated += fieldsPopup_Openedc;
            m_fieldsCliente.Background = new SolidColorBrush(Colors.Red);
            RootGrid.Opacity = 0.1;
            m_fieldsCliente.Opacity = 1;
            m_fieldsCliente.ShowDialog();
            RootGrid.Opacity = 1;

        }


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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            int cant = 0;
            double preciovta = 00.00;
            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {

                txtPrecioExtend.Text = " 00.00";

            }
            else if(string.IsNullOrWhiteSpace(txtPrecioVenta.Text))
            {
                txtPrecioExtend.Text = " 00.00";
            }
            else
            {
                preciovta = Convert.ToDouble(txtPrecioVenta.Text);
                cant = Convert.ToInt32(txtCantidad.Text);
                double total = cant * preciovta;
                txtPrecioExtend.Text =  total.ToString();

            }


        }

        private void txtPrecioVenta_TextChanged(object sender, TextChangedEventArgs e)
        {
            int cant = 0;
            double preciovta = 00.00;
            if (string.IsNullOrWhiteSpace(txtPrecioVenta.Text))
            {


                txtPrecioExtend.Text = " 00.00";
            }
            else if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {

                txtPrecioExtend.Text = " 00.00";

            }
            else if (txtPrecioVenta.Text==".")
            {
                txtPrecioExtend.Text = " 00.00";
            }
            else if (txtPrecioVenta.Text == "-" || txtPrecioVenta.Text == "_")
            {
                txtPrecioExtend.Text = " 00.00";
            }
            else
            {
                preciovta = Convert.ToDouble(txtPrecioVenta.Text);
                cant = Convert.ToInt32(txtCantidad.Text);
                double total = cant * preciovta;
                txtPrecioExtend.Text = total.ToString("N2");
            }
           
        }

        private void txtPrecioExtend_TextChanged(object sender, TextChangedEventArgs e)
        {
            double subtotal = Convert.ToDouble(txtPrecioExtend.Text);
            double iva = subtotal * 0.16;
            double total = subtotal * 1.16;
            TotalAmount_Copy.Text = subtotal.ToString();
            Iva_Copy.Text = iva.ToString();
            TotalIva_Copy.Text = total.ToString();
            TotalAmount.Text = "$"+ subtotal.ToString("N2");
            Iva.Text = "$" + iva.ToString("N2");
            TotalIva.Text = "$" + total.ToString("N2");


        }
    }


    class City
    {
        public string Codigo { get; set; }
        public string Forma { get; set; }
       
    }
}

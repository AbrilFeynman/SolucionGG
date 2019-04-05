using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using Telerik.Windows.Controls;

namespace GGGC.Admin.AZ.Transferencias
{
    /// <summary>
    /// Interaction logic for TransferenciasView.xaml
    /// </summary>
    public partial class TransferenciasView : UserControl
    {

        #region Fields
        const int m_columnCount = 9;

        private double m_totalCant;
        int osomaloso = 0;
        private IList<TransferenciaItem> m_items;
        int m_currentRowIndex = 0;
        int m_selectedIndex = -1;
        int m_prevSelectedIndex = -1;
        const int m_rowHeight = 30;
        Border m_border;
        TransferenciaDialog m_fieldsPopup;

        int estrella = 1;
        string item_cliente = "";
        string descuento = "0";
        string item_nombre = "";
        string item_direccion = "";

        TransferenciaDialog m_producto;
        #endregion
        #region Properties



        public double TotalCant
        {
            get
            {
                return m_totalCant;
            }
        }


        #endregion

        public TransferenciasView()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
            // Sets the UI culture to French (France)
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-MX");



            InitializeComponent();
            Initialize();
        }


        DataTable tabla = new DataTable();

        private void Initialize()
        {


            Random r = new Random();
            var x = r.Next(1000000, 7000000);
            string Od = x.ToString();
            osomaloso = x;
            DateFactura.SelectedDate = DateTime.Now;

            m_items = new List<TransferenciaItem>();

            m_border = new Border();
            tabla.Columns.Add("Numero_De_Documento", typeof(string));
            tabla.Columns.Add("Renglon", typeof(int));
            tabla.Columns.Add("Codigo", typeof(string));
            tabla.Columns.Add("Descripcion", typeof(string));
            tabla.Columns.Add("Unidad", typeof(string));
            tabla.Columns.Add("Cantidad", typeof(string));
            CargarSucursal();


        }

        private void Add(object sender, RoutedEventArgs e)
        {
            m_fieldsPopup = new TransferenciaDialog();
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
            (sender as TransferenciaDialog).InitializeFocus();
        }



        void myDialog_UpdateRequested(object sender, EventArgs e)
        {


            TransferenciaItem item = (e as FieldsUpdateEventArgs).UpdatedFields;
            AddItem(item, false);

            agreItem(item);
            m_fieldsPopup.Close();
        }








        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {

            //ONLY DECIMAL
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));


        }

        private void CNumberValidation(object sender, TextCompositionEventArgs e)
        {

            //ONLY DECIMAL
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));


        }


        private void NumberValidationTextBoxx(object sender, TextCompositionEventArgs e)
        {

            //ONLY NUMBERS

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);




        }



        void myDialog_CloseRequested(object sender, EventArgs e)
        {
            m_fieldsPopup.Close();
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
                this.DeleteButton.IsEnabled = false;
            }


        }

        private void Abrir(object sender, RoutedEventArgs e)
        {
            RootGrid.Opacity = 0.1;
            TransferenciaDialog win2 = new TransferenciaDialog();
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
            TransferenciaItem invoiceItem = m_items[m_selectedIndex];
            //esta no estaba comentada int selectedProductIndex = m_productList.IndexOf(m_productList[invoiceItem.Descripcion]);
            m_fieldsPopup = new TransferenciaDialog(invoiceItem, "Edit the Fields");
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



        public void AddItem(TransferenciaItem item, bool addToGridAlone)
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
            SetCell(rowIndex, 2, item.Unidad.ToString());
            SetCell(rowIndex, 3, item.Cantidad.ToString());



            m_totalCant += Convert.ToDouble(item.Cantidad);

            m_currentRowIndex++;
            m_selectedIndex = m_items.Count;

            UpdateCantidad();


        }

        private void agreItem(TransferenciaItem item)
        {
            //m_cliente = new Cliente();
            //item.Marca = m_cliente.Marci;

            //item.Numero_De_Documento = GlobalId.Identificador;
            item.Numero_De_Documento = osomaloso.ToString();
            item.Renglon = estrella;

            // item.Renglon = tabla.Rows.Count;
            tabla.Rows.Add(item.Numero_De_Documento, item.Renglon, item.Codigo, item.Descripcion, item.Unidad, item.Cantidad);
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

        private void SetCell(int rowIndex, int columnIndex, string value)
        {
            if (columnIndex == 2 || columnIndex == 3)
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
                if (columnIndex == 2)
                {
                    textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                }
                else
                {
                    textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                }

                textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                if (columnIndex > 0)
                    textBlock.TextAlignment = TextAlignment.Right;

                SetCell(rowIndex, columnIndex, textBlock);


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


        private void SetCell(int rowIndex, int columnIndex, TextBlock textBlock)
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

            m_totalCant = 0;

            m_currentRowIndex = 0;
            InvoiceGrid.Children.Clear();
            //InvoiceGrid.RowDefinitions.RemoveAt(InvoiceGrid.RowDefinitions.Count - 1);
            for (int i = 0; i < InvoiceGrid.RowDefinitions.Count; i++)
                DrawBorder(i);
            foreach (TransferenciaItem item in m_items)
            {
                AddItem(item, true);
            }

            UpdateCantidad();

        }



        void UpdateCantidad()
        {
            this.TotalAmount.Text = TotalCant.ToString();
            this.TotalAmount_Copy.Text = TotalCant.ToString();
        }









        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (tabla.Rows.Count > 0 && Folioo.Text.Length > 0 && comboDestino.SelectedIndex > -1)
            {
                SaveHeader();

                SaveDetail();
                //Limpiar botones
                cleanscreen();

                MessageBox.Show("Se guardo Correctamente ");
            }
            else
            {
                MessageBox.Show("La compra no debe de estar vacia.");

            }


        }

        private void cleanscreen()
        {
            InvoiceGrid.Children.Clear();
            m_items = new List<TransferenciaItem>();

            UpdateGrido();
            osomaloso++;

            DateFactura.SelectedDate = DateTime.Now;

            Folioo.Text = "";
            comboDestino.SelectedItem = null;
            txtradial.Text = "";
            txtNombre.Text = "";
            txtChofer.Text = "";
            empleadoTransf.Text = "";
            txtEmpleadoNombre.Text = "";
            txtChoferNombre.Text = "";

             estrella = 1;

            tabla.Rows.Clear();

        }

        private void UpdateGrido()
        {

            m_totalCant = 0;

            m_currentRowIndex = 0;
            InvoiceGrid.Children.Clear();
            //InvoiceGrid.RowDefinitions.RemoveAt(InvoiceGrid.RowDefinitions.Count - 1);
            for (int i = 0; i < InvoiceGrid.RowDefinitions.Count; i++)
                DrawBorder(i);
            foreach (TransferenciaItem item in m_items)
            {
                AddItem(item, true);
            }

            UpdateCantidad();

        }


        private void SaveHeader()
        {


            string canti = TotalAmount_Copy.Text;

           
            int OrderId = osomaloso;
            DateTime fecha = Convert.ToDateTime(DateFactura.SelectedDate);
            string fechax = fecha.ToString("MM/dd/yyyy");
            string cantarti = TotalAmount.Text;
            string folio = Folioo.Text;
            var objDias = comboDestino.SelectedItem;
            string destino = ((System.Data.DataRowView)objDias).Row.ItemArray[1].ToString();
            string numerounidad = "";
            if (String.IsNullOrEmpty(txtradial.Text))
            {
               
                numerounidad = "";
            }
            else
            {
                numerounidad = txtradial.Text;
            }

            string placas = "";
            if (String.IsNullOrEmpty(txtNombre.Text))
            {
             
                placas = "";
            }
            else
            {
                placas = txtNombre.Text;
            }

            string chofer = "";
            if (String.IsNullOrEmpty(txtChofer.Text))
            {
                chofer = "";
            }
            else
            {
               
                chofer = txtChofer.Text;
            }

            string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
            SqlConnection sqlcon = new SqlConnection(connectionStringer);
            sqlcon.Open();


            SqlCommand agregar = new SqlCommand("INSERT INTO [dbo].[Transferencias]([Numero_De_Documento],[Folio_De_Transferencia] ,[Fecha]," +
                "[Codigo_De_Sucursal_Origen],[Codigo_De_Sucursal_Destino],[Cantidad_Total],[Numero_De_Unidad]," +
                "[Placas],[Chofer],[Estatus_Del_Documento],[Numero_Corto_De_Sucursal],[Estatus_De_Replicacion],[Fecha_Y_Hora_De_Ultima_Actualizacion])" +
                " VALUES " +
                " (" + osomaloso + ",'" + folio + "','" + fechax + "','B4','"+destino+"',"+cantarti+",'"+numerounidad+"'," +
                "  '"+placas+"','"+chofer+"',1,4,1,GETDATE())", sqlcon);



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

        private void CargarSucursal()
        {
            try
            {

                string conect = "SERVER = gggctserver.database.windows.net; DATABASE = b4Migration; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099";
                SqlConnection con = new SqlConnection(conect);
                try
                {
                    con.Open();

                    string cmd = "SELECT [Codigo_De_Sucursal]+' ' +'-'+' '+[Descripcion] as Nombre ,[Codigo_De_Sucursal] FROM [dbo].[Sucursales] order by  Nombre desc";
                    //" SELECT Codigo_De_Articulo,Nivel_De_Precios,Precio FROM Precios where Codigo_De_Articulo = '" + codigo + "' ";



                    SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);
                    DataSet dsPubs = new DataSet("Pubs");
                    sda.Fill(dsPubs, "Sucursales");
                    DataTable dtblg = new DataTable();
                    dtblg = dsPubs.Tables["Sucursales"];
                    comboDestino.ItemsSource = dsPubs.Tables["Sucursales"].DefaultView;
                    var oso = dsPubs.Tables["Sucursales"].DefaultView;



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



        private void SaveDetail()
        {

            string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
            SqlConnection sqlcon = new SqlConnection(connectionStringer);
            sqlcon.Open();

            foreach (DataRow Registro in tabla.Rows)
            {


                int id = Convert.ToInt32(Registro[0]);
                int renglon = Convert.ToInt32(Registro[1]);
                string codigo = Registro[2].ToString();
                string descripcion = Registro[3].ToString();
                string unidad = Registro[4].ToString();
                int cantidad = Convert.ToInt32(Registro[5]);


                try
                {
                    string consulta = "INSERT INTO [dbo].[Transferencias_Detalle] ([Numero_De_Documento],[Renglon],[Codigo_De_Articulo],[Descripcion],[Unidad] ,[Cantidad]) VALUES " +
                        "(" + id + "," + renglon + ",'" + codigo + "','" + descripcion + "','" + unidad + "'," + cantidad + ")";
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

            if (e.Key == Key.F6)
            {
                m_fieldsPopup = new TransferenciaDialog();
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





            if (e.Key == Key.F7)
            {
                tabla.Rows[m_selectedIndex].Delete();
                if (m_selectedIndex == -1)
                    return;

                RemoveItem(m_selectedIndex);
                this.DeleteButton.IsEnabled = false;
            }

        }

        private void Folioo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }








        private void Txtrfc_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {

            //ONLY NUMBERS

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);




        }


        private void Ordenserv_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(empleadoTransf.Text))
            {
                empleadoTransf.Text = "";
            }
            else
            {
                cargarEmpleado();

            }
        }

        public void cargarEmpleado()

        {

            try
            {


                string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = b4Migration; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
          

                SqlConnection cn = new SqlConnection(connectionStringer); ;
                SqlCommand cmd;
                SqlDataReader dr;


               
                cn.Open();
                string id = this.empleadoTransf.Text;

                cmd = new SqlCommand("SELECT [Codigo_De_Empleado],[Nombre] + ' ' +[Apellido_Paterno]+' '+[Apellido_Materno] as Nombre FROM [dbo].[Empleados] where Codigo_De_Empleado= "+id+" and Baja_Logica = 0", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())

                {
                    txtEmpleadoNombre.Text = dr["Nombre"].ToString();

                }
                else
                {
                    txtEmpleadoNombre.Text = " ";
                }
            }

            catch (Exception l)
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

        private void TxtChofer_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtChofer.Text))
            {
                txtChofer.Text = "";
            }
            else
            {
                cargarChofer();

            }
        }
        public void cargarChofer()

        {

            try
            {


                string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = b4Migration; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";


                SqlConnection cn = new SqlConnection(connectionStringer); ;
                SqlCommand cmd;
                SqlDataReader dr;



                cn.Open();
                string id = this.txtChofer.Text;

                cmd = new SqlCommand("SELECT [Codigo_De_Empleado],[Nombre] + ' ' +[Apellido_Paterno]+' '+[Apellido_Materno] as Nombre FROM [dbo].[Empleados] where Codigo_De_Empleado= " + id + " and Baja_Logica = 0", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())

                {
                    txtChoferNombre.Text = dr["Nombre"].ToString();

                }
                else
                {
                    txtChoferNombre.Text = " ";
                }
            }

            catch (Exception l)
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





    }

}



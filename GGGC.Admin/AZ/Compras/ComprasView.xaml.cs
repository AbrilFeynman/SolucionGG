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
using System.Text.RegularExpressions;

namespace GGGC.Admin.AZ.Compras
{
   
    public partial class ComprasView : UserControl
    {


        #region Fields
        const int m_columnCount = 9;
        private double m_totalDue;
        private double m_totalsind;
        private double m_totalCant;
        int osomaloso = 0;
        private IList<CompraItem> m_items;
        int m_currentRowIndex = 0;
        int m_selectedIndex = -1;
        int m_prevSelectedIndex = -1;
        const int m_rowHeight = 30;
        Border m_border;
        CompraDialog m_fieldsPopup;
        CompraProveedor m_fieldsCliente;
        
        int estrella = 1;
        string item_cliente = "";
        string descuento = "0";
        string item_nombre = "";
        string item_direccion = "";
       
        CompraDialog m_producto;
        #endregion
        #region Properties
     
        public double TotalDue
        {
            get
            {
                return m_totalDue;
            }
        }

        public double Totalsind
        {
            get
            {
                return m_totalsind;
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
        public ComprasView()
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
            GlobalId o = new GlobalId();
            GlobalId.Identificador = Od;
            //m_productList = new ProductList();
            //  GGGC.Admin.App.
            // tabPendientes.
            //empezar los datapickers
            DateFactura.SelectedDate = DateTime.Now;
            DateRecepcion.SelectedDate = DateTime.Now;

            m_items = new List<CompraItem>();

            m_border = new Border();
            tabla.Columns.Add("Numero_De_Documento", typeof(string));
            tabla.Columns.Add("Renglon", typeof(int));
            tabla.Columns.Add("Codigo", typeof(string));
            tabla.Columns.Add("Descripcion", typeof(string));
            tabla.Columns.Add("Unidad", typeof(string));
            tabla.Columns.Add("Cantidad", typeof(string));
            tabla.Columns.Add("Precio_Unitario_Sin_Descuento", typeof(string));
            tabla.Columns.Add("Descuento ", typeof(string));
            tabla.Columns.Add("Precio_Unitario_Con_Descuento", typeof(string));
            tabla.Columns.Add("Precio_Extendido_Con_Descuento", typeof(string));

        }

        private void Add(object sender, RoutedEventArgs e)
        {
            m_fieldsPopup = new CompraDialog();
            //m_fieldsPopup = new CompraDialog(m_productList);
            m_fieldsPopup.CloseRequested += myDialog_CloseRequested;
            m_fieldsPopup.UpdateRequested += myDialog_UpdateRequested;
            m_fieldsPopup.Activated += fieldsPopup_Opened;
            m_fieldsPopup.Background = new SolidColorBrush(Colors.Red);
            RootGrid.Opacity = 0.1;
            m_fieldsPopup.Opacity = 1;
            m_fieldsPopup.ShowDialog();
            RootGrid.Opacity = 1;
            // lblGas.
            //chk
        }

        private void M_producto_CloseRequested(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void fieldsPopup_Opened(object sender, object e)
        {
            (sender as CompraDialog).InitializeFocus();
        }

        void fieldsPopup_Openedc(object sender, object e)
        {
            (sender as CompraProveedor).InitializeFocus();
        }

        void myDialog_UpdateRequested(object sender, EventArgs e)
        {


            CompraItem item = (e as FieldsUpdateEventArgs).UpdatedFields;
            AddItem(item, false);
            UpdateTotal();
            agreItem(item);
            m_fieldsPopup.Close();
        }

        void myDialog_UpdateRequestedc(object sender, EventArgs e)
        {


            ProveedorItem itemc = (e as FieldsUpdateEventArgss).UpdatedFields;
            AddItemc(itemc, false);


            m_fieldsCliente.Close();
        }



        public void AddItemc(ProveedorItem item, bool addToGridAlone)
        {

            txtradial.Text = item.Numero_Proveedor.ToString();

            item_cliente = item.Numero_Proveedor.ToString();
            item_nombre = item.Nombre.ToString();
            txtNombre.Text = item.Nombre.ToString();
            
            string numero = item.Numero_Proveedor.ToString();
            cargarcondicion(numero);



        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
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


        private void cargarcondicion (string noprov)
            
        {
            try
            {

                string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099";
                SqlConnection con = new SqlConnection(conect);
                try
                {
                    con.Open();

                    string cmd = "SELECT Codigo_De_Proveedor, Condicion_De_Compra FROM Condiciones_De_Compra where Codigo_De_Proveedor = '"+noprov+"'";
                        //" SELECT Codigo_De_Articulo,Nivel_De_Precios,Precio FROM Precios where Codigo_De_Articulo = '" + codigo + "' ";



                    SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);
                    DataSet dsPubs = new DataSet("Pubs");
                    sda.Fill(dsPubs, "Condiciones");
                    DataTable dtblg = new DataTable();
                    dtblg = dsPubs.Tables["Condiciones"];
                    combodias.ItemsSource = dsPubs.Tables["Condiciones"].DefaultView;
                    var oso = dsPubs.Tables["Condiciones"].DefaultView;


                    try
                    {
                        string apollo = dtblg.Rows[0][1].ToString();
                        //m_invoiceItem.Preciolista = Convert.ToDouble(apollo);
                    }
                    catch
                    {
                        MessageBox.Show("Este proveedor no tiene condiciones registradas", "Proveedor sin condicion");
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
                this.DeleteButton.IsEnabled = false;
            }


        }

        private void Abrir(object sender, RoutedEventArgs e)
        {
            RootGrid.Opacity = 0.1;
            CompraDialog win2 = new CompraDialog();
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
            CompraItem invoiceItem = m_items[m_selectedIndex];
            //esta no estaba comentada int selectedProductIndex = m_productList.IndexOf(m_productList[invoiceItem.Descripcion]);
            m_fieldsPopup = new CompraDialog(invoiceItem, "Edit the Fields");
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



        public void AddItem(CompraItem item, bool addToGridAlone)
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
            SetCell(rowIndex, 4, item.Preciouni.ToString("#,###.00", CultureInfo.InvariantCulture));
            SetCell(rowIndex, 5,   item.Descuento.ToString());
            SetCell(rowIndex, 6,  item.Descuento.ToString());
            SetCell(rowIndex, 7,  item.Preciocdesc.ToString("#,###.00", CultureInfo.InvariantCulture));
            SetCell(rowIndex, 8, item.Precioextend.ToString("#,###.00", CultureInfo.InvariantCulture));
            //SetCell(rowIndex, 7, "$" + item.Total.ToString("#,###.00", CultureInfo.InvariantCulture));
            //SetCell(rowIndex, 8, "$" + item.Iva.ToString("#,###.00", CultureInfo.InvariantCulture));

            //<Rectangle Grid.Row="0" Height="1" StrokeThickness="0.75" VerticalAlignment="Bottom" Grid.ColumnSpan="5" 
            //StrokeDashArray="4,4" Stroke="#FFCECECE"></Rectangle>

            m_totalDue += Convert.ToDouble(item.Precioextend);
            m_totalsind += Convert.ToDouble(item.Precioextendsindesc);
            m_totalCant += Convert.ToDouble(item.Cantidad);

            m_currentRowIndex++;
            m_selectedIndex = m_items.Count;
            UpdateTotal();
            UpdateTotalsind();
            UpdateCantidad();


        }

        private void agreItem(CompraItem item)
        {
            //m_cliente = new Cliente();
            //item.Marca = m_cliente.Marci;

            //item.Numero_De_Documento = GlobalId.Identificador;
            item.Numero_De_Documento = osomaloso.ToString();
            item.Renglon = estrella;
            descuento = item.Descuento;
            // item.Renglon = tabla.Rows.Count;
            tabla.Rows.Add(item.Numero_De_Documento, item.Renglon, item.Codigo, item.Descripcion, item.Unidad, item.Cantidad, item.Preciouni, item.Descuento, item.Preciocdesc, item.Precioextend);
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
            if (columnIndex == 4 || columnIndex == 5 || columnIndex == 6 || columnIndex == 7 || columnIndex == 8)
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
                TextBlock textBlock = null;
                textBlock = new TextBlock();
                textBlock.Text = "$" + value;

                textBlock.FontSize = 13;
                textBlock.FontFamily = new FontFamily("Segoe UI");
                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
                //HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
                if (columnIndex == 0)
                    textBlock.Padding = new Thickness(0, 0, 0, 0);
                else
                    textBlock.Padding = new Thickness(0, 0, 0, 0);
                //textBlock.Foreground = new SolidColorBrush(Colors.Black);
                textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;


                textBlock.TextAlignment = TextAlignment.Right;

                SetCell(rowIndex, columnIndex, textBlock);


                //TextBlock textBlockDollor = null;
                //textBlockDollor = new TextBlock();
                //textBlockDollor.Text = "$";
                //textBlockDollor.FontSize = 13;
                //textBlockDollor.FontFamily = new FontFamily("Segoe UI");
                //textBlockDollor.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
                ////textBlockDollor.Foreground = new SolidColorBrush(Colors.Black);
                //textBlockDollor.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                //textBlockDollor.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                //textBlockDollor.TextAlignment = TextAlignment.Right;
                //if (columnIndex == 2)
                //    Grid.SetColumn(textBlockDollor, 1);
                //else
                //    Grid.SetColumn(textBlockDollor, 0);
                //Grid.SetRow(textBlockDollor, 0);
                //amountGrid.Children.Add(textBlockDollor);
                ////Data
                //TextBlock textBlock = null;
                //textBlock = new TextBlock();
                //
                //textBlock.FontSize = 13;
                //textBlock.FontFamily = new FontFamily("Segoe UI");
                //textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
                ////HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
                if (columnIndex == 5 || columnIndex == 6)
                {
                    textBlock.Text = value.Trim('$');

                }
                    

                //else
                //    textBlock.Padding = new Thickness(0, 0, 0, 0);
                ////textBlock.Foreground = new SolidColorBrush(Colors.Black);
                //textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                //textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                //if (columnIndex > 0)
                //    textBlock.TextAlignment = TextAlignment.Right;
                //if (columnIndex == 2)
                //    Grid.SetColumn(textBlock, 2);
                //else
                //    Grid.SetColumn(textBlock, 1);
                //Grid.SetRow(textBlock, 0);
                //amountGrid.Children.Add(textBlock);
            }
            //else if (columnIndex == 4)
            //{
            //    TextBlock textBlock = null;
            //    textBlock = new TextBlock();
            //    textBlock.Text = "$" +value;

            //    textBlock.FontSize = 13;
            //    textBlock.FontFamily = new FontFamily("Segoe UI");
            //    textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
            //    //HorizontalAlignment="Center" FontWeight="Normal" FontFamily="Segoe UI" Foreground="#3F3F3F"
            //    if (columnIndex == 0)
            //        textBlock.Padding = new Thickness(0, 0, 0, 0);
            //    else
            //        textBlock.Padding = new Thickness(0, 0, 0, 0);
            //    //textBlock.Foreground = new SolidColorBrush(Colors.Black);
            //    textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            //    textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;


            //   textBlock.TextAlignment = TextAlignment.Right;

            //    SetCell(rowIndex, columnIndex, textBlock);
            //}
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

        //private void SetCell(int rowIndex, int columnIndex, string value)
        //{
        //    if (columnIndex == 4 || columnIndex == 5 || columnIndex == 6 || columnIndex == 7 || columnIndex == 8)
        //    {
        //        Grid amountGrid = new Grid();
        //        Grid.SetColumn(amountGrid, columnIndex);
        //        Grid.SetRow(amountGrid, rowIndex);
        //        InvoiceGrid.Children.Add(amountGrid);
        //        amountGrid.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
        //        if (columnIndex == 2)
        //            amountGrid.ColumnDefinitions.Add(new ColumnDefinition());
        //        amountGrid.ColumnDefinitions.Add(new ColumnDefinition());
        //        amountGrid.ColumnDefinitions.Add(new ColumnDefinition());
        //        amountGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(new double(), GridUnitType.Auto) });

        //        Dollar
        //        TextBox textBlockDollor = null;
        //        textBlockDollor = new TextBox();
        //        textBlockDollor.Text = "$";
        //        textBlockDollor.FontSize = 13;
        //        textBlockDollor.FontFamily = new FontFamily("Segoe UI");
        //        textBlockDollor.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
        //        textBlockDollor.Foreground = new SolidColorBrush(Colors.Black);
        //        textBlockDollor.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
        //        textBlockDollor.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        //        textBlockDollor.TextAlignment = TextAlignment.Right;
        //        if (columnIndex == 2)
        //            Grid.SetColumn(textBlockDollor, 1);
        //        else
        //            Grid.SetColumn(textBlockDollor, 0);
        //        Grid.SetRow(textBlockDollor, 0);
        //        amountGrid.Children.Add(textBlockDollor);
        //        Data
        //        TextBlock textBlock = null;
        //        textBlock = new TextBlock();
        //        textBlock.Text = value.Trim('$');
        //        textBlock.FontSize = 13;
        //        textBlock.FontFamily = new FontFamily("Segoe UI");
        //        textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
        //        HorizontalAlignment = "Center" FontWeight = "Normal" FontFamily = "Segoe UI" Foreground = "#3F3F3F"
        //        if (columnIndex == 0)
        //            textBlock.Padding = new Thickness(0, 0, 0, 0);

        //        else
        //            textBlock.Padding = new Thickness(0, 0, 0, 0);
        //        textBlock.Foreground = new SolidColorBrush(Colors.Black);
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
        //    else
        //    {
        //        Data
        //        TextBox textBlock = null;

        //        textBlock = new TextBox();
        //        textBlock.Text = value.Trim('$');
        //        textBlock.Style = null;
        //        textBlock.BorderBrush = null;
        //        textBlock.IsReadOnly = true;

        //        textBlock.BorderThickness = new Thickness(0, 0, 0, 0);
        //        textBlock.FontSize = 13;
        //        textBlock.FontFamily = new FontFamily("Segoe UI");
        //        textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 63, 63));
        //        HorizontalAlignment = "Center" FontWeight = "Normal" FontFamily = "Segoe UI" Foreground = "#3F3F3F"
        //        if (columnIndex == 0)
        //            textBlock.Padding = new Thickness(0, 0, 0, 0);
        //        else
        //            textBlock.Padding = new Thickness(0, 0, 0, 0);
        //        textBlock.Foreground = new SolidColorBrush(Colors.Black);
        //        textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
        //        textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

        //        if (columnIndex > 0)
        //            textBlock.TextAlignment = TextAlignment.Right;

        //        SetCell(rowIndex, columnIndex, textBlock);
        //    }
        //}

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
            m_totalDue = 0;
            m_totalCant = 0;
            m_totalsind = 0;
            m_currentRowIndex = 0;
            InvoiceGrid.Children.Clear();
            //InvoiceGrid.RowDefinitions.RemoveAt(InvoiceGrid.RowDefinitions.Count - 1);
            for (int i = 0; i < InvoiceGrid.RowDefinitions.Count; i++)
                DrawBorder(i);
            foreach (CompraItem item in m_items)
            {
                AddItem(item, true);
            }
            UpdateTotal();
            UpdateCantidad();
            UpdateTotalsind();
        }

        void UpdateTotal()
        {
            this.TotalIva.Text = "$" + TotalDue.ToString("#,###.00", CultureInfo.InvariantCulture);
            this.TotalIva_Copy1.Text =  TotalDue.ToString();


        }
        void UpdateTotalsind()
        {
            this.Iva.Text = "$" + Totalsind.ToString("#,###.00", CultureInfo.InvariantCulture);
            this.Iva_Copy1.Text = Totalsind.ToString();



        }

        void UpdateCantidad()
        {
          this.TotalAmount.Text = TotalCant.ToString();
          this.TotalAmount_Copy.Text = TotalCant.ToString();
        }


        


        



        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (tabla.Rows.Count > 0 && txtradial.Text.Length > 4 && Folioo.Text.Length >2)
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
            m_items = new List<CompraItem>();

            UpdateGrido();
            osomaloso++;
            m_fieldsCliente = new CompraProveedor();
            txtradial.Text = "";
            txtNombre.Text = "";
            DateFactura.SelectedDate = DateTime.Now;
            DateRecepcion.SelectedDate = DateTime.Now;
            Folioo.Text = "";
            CantidadA.Text = "";
            Ordenserv.Text = "";
            txtradial.Text = "";
            txtNombre.Text = "";
            combodias.SelectedItem = null;
            Ivaa.SelectedItem = null;

            estrella = 1;

            tabla.Rows.Clear();

        }

        private void UpdateGrido()
        {
            m_totalDue = 0;
            m_totalCant = 0;
            m_totalsind = 0;
            m_currentRowIndex = 0;
            InvoiceGrid.Children.Clear();
            //InvoiceGrid.RowDefinitions.RemoveAt(InvoiceGrid.RowDefinitions.Count - 1);
            for (int i = 0; i < InvoiceGrid.RowDefinitions.Count; i++)
                DrawBorder(i);
            foreach (CompraItem item in m_items)
            {
                AddItem(item, true);
            }
            UpdateTotal();
            UpdateCantidad();
            UpdateTotalsind();
        }


        private void SaveHeader()
        {

            DateTime xxx = new DateTime();
            xxx = Convert.ToDateTime(DateFactura.SelectedDate);
            var V = xxx.ToString("MM/dd/yyyy");
            string canti = TotalAmount_Copy.Text;
            string factura = Folioo.Text;
            string proveedor = "";
            proveedor = txtradial.Text;
            double costototal = 0.00;
            costototal = Convert.ToDouble(TotalIva_Copy1.Text);
            string cliente = txtradial.Text;
            int Iva = 0;
            if (combodias.SelectedItem ==null)
            {
                Iva = 0;
            }
            else
            {
                Iva = Convert.ToInt32(Ivaa.SelectionBoxItem);
            }
            
            string Orden = "";
            Orden = Ordenserv.Text;
            int cond = 0;
            var objDias = combodias.SelectedItem;
            if (combodias.SelectedItem == null)
            {
                cond = 0;
            }
            else
            {
             
                cond = Convert.ToInt16(((System.Data.DataRowView)objDias).Row.ItemArray[1].ToString());
            }

            DateTime Vencimiento =Convert.ToDateTime(DateFactura.SelectedDate);
            Vencimiento = Vencimiento.AddDays(cond);
            string strVencimiento = fncFechaDeVencimiento(Vencimiento).ToString("MM/dd/yyyy");



            int OrderId = osomaloso;
            string fechtimenormal = DateTime.Now.ToString();
            string frecepcion = DateFactura.SelectedDate.Value.ToString("yyyy-MM-dd");

            string fEntrega = DateRecepcion.SelectedDate.Value.ToString("yyyy-MM-dd");

            string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
            SqlConnection sqlcon = new SqlConnection(connectionStringer);
            sqlcon.Open();


            SqlCommand agregar = new SqlCommand("INSERT INTO [dbo].[Compras_Y_Devoluciones] ([Numero_De_Documento],[Folio_De_Factura],[Fecha_De_Factura],[Cantidad_Total_De_Articulos], " +
                "[Fecha_De_Recepcion],[Descuento_Aplicado_A_La_Factura],[Codigo_De_Proveedor],[Costo_Total],[Iva_Aplicado_Al_Documento], " +
                "[Estatus_De_Compra],[Estatus_De_Documento],[Orden_De_Servicio],[Numero_Corto_De_Sucursal],  [Condiciones_De_Pago], " +
                "[Fecha_De_Vencimiento],[Estatus_De_Replicacion],[Fecha_Y_Hora_De_Ultima_Actualizacion]) VALUES " +
                " (" + OrderId + ",'" + factura + "','"+V+"'," + canti + ",'" + V + "','" + descuento + "','" + proveedor + "'," +
                "  "+costototal+","+Iva+",1,1,'"+Orden+"',4,"+cond+",'"+ strVencimiento+ "',1,GETDATE())", sqlcon);


           
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



        private DateTime fncFechaDeVencimiento(DateTime dteFechaDeVencimiento)
        {
            DateTime dteFechaDeVencimientoss  ;
            if (this.Folioo.Text != "")
            {


                if (dteFechaDeVencimiento.DayOfWeek == DayOfWeek.Saturday)
                    dteFechaDeVencimiento = dteFechaDeVencimiento.AddDays(2);

                if (dteFechaDeVencimiento.DayOfWeek == DayOfWeek.Sunday)
                    dteFechaDeVencimiento = dteFechaDeVencimiento.AddDays(1);

                dteFechaDeVencimientoss = dteFechaDeVencimiento;
            }
            return dteFechaDeVencimiento;
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
                decimal precunisind = Convert.ToDecimal(Registro[06]);
                string descuento = Registro[07].ToString();
                decimal precunicond = Convert.ToDecimal(Registro[08]);
                decimal precextcond = Convert.ToDecimal(Registro[09]);

                try
                {
                    string consulta = "INSERT INTO [dbo].[Compras_Y_Devoluciones_Detalle] ([Numero_De_Documento],[Renglon],[Codigo_De_Articulo],[Descripcion],[Unidad] ,[Cantidad],[Precio_Unitario_Sin_Descuento] ,[Descuento],[Precio_Unitario_Con_Descuento],[Precio_Extendido_Con_Descuento]) VALUES " +
                        "(" + id + "," + renglon + ",'"+codigo+"','" + descripcion+ "','" + unidad + "'," + cantidad + "," + precunisind + ",'"+descuento+"',"+ precunicond + ","+ precextcond + ")";
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


            if (e.Key == Key.F6)
            {
                m_fieldsPopup = new CompraDialog();
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
                m_fieldsCliente = new CompraProveedor();


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

        private void Folioo_TextChanged(object sender, TextChangedEventArgs e)
        {

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

            //RptOrden fac = new RptOrden(tablaOrden, tablaDeatil);

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

                MessageBox.Show("ERROR DE CONEXION" + ex.Message);
            }




            //SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM OrderHeader WHERE OrderID = " + folio + " ", sqlconn);
            SqlDataAdapter adapter = new SqlDataAdapter("Select OrderHeader.OrderID, OrderHeader.CustomerID, OrderHeader.RFC, OrderHeader.OrderDate," +
                                                       " OrderHeader.ReceptionDate, OrderHeader.DueDate, OrderHeader.OrderQty, OrderHeader.Subtotal, OrderHeader.Total, " +
                                                       " OrderHeader.ExteriorValues, OrderHeader.InteriorValues, OrderHeader.AccesoriesValues, Clientes_Frecuentes.Nombre + ' ' + " +
                                                       " Clientes_Frecuentes.Apellido_Paterno + ' ' + Clientes_Frecuentes.Apellido_Materno as Nombre, " +
                                                       " Clientes_Frecuentes.Telefono_Personal as Telefono, " +
                                                       " Clientes_Frecuentes.Direccion + ' ' + Clientes_Frecuentes.Colonia + ' ' + Clientes_Frecuentes.Ciudad as Direccion,  OrderHeader.Comments as Observaciones From OrderHeader Inner join  Clientes_Frecuentes" +
                                                       "  ON OrderHeader.CustomerID = Clientes_Frecuentes.Id_Radial" +
                                                        " where OrderHeader.OrderID =  " + folio + "", sqlconn);
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
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM OrderDetail WHERE OrderID = " + folio + " ", sqlconn);
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


    }
}

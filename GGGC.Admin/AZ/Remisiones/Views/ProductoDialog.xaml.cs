using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Telerik.Windows.Controls;

namespace GGGC.Admin.AZ.Remisiones.Views
{
    /// <summary>
    /// Interaction logic for ProductoDialog.xaml
    /// </summary>
    public sealed partial class ProductoDialog : Window
    {

        public event EventHandler CloseRequested;
        //public event EventHandler CloseLlamada;
        bool onInit;
        public event EventHandler UpdateRequested;
        RemiItem m_invoiceItem;
        RemisionView compra;

        public ProductoDialog()
            : this(null, "Add Fields")
        {
        }

        public ProductoDialog(RemiItem newItem, string title)
        {
            InitializeComponent();
            dataGrid1.ShowGroupPanel = false;
            llenargrid();

            if (newItem == null)
            {
                //Cantidad.Value = GetQuantityAsInt();
                rate.Value = 0.00;
                //prueba usando datacontext
                //InvoiceItem newItem;
                newItem = new RemiItem();
            }

            item.Text = newItem.Codigo;
            item_Copy.Text = newItem.Descripcion;
            //aqui debe de ir a buscar el precio del codigo que trae de regreso
            // newItem.Cantidad = GetQuantityAsInt();
            //newItem.Cantidad = 2;
            
            rate.Value = 00.00;
            m_invoiceItem = newItem;
            Cantidad.Value = newItem.Cantidad;

            //Cantidad.Value = 2;
            // rate.Value = 80.00;


            if (newItem.Cantidad == 0)
            {
                newItem.Cantidad = GetQuantityAsInt();
                this.Cantidad.Value = 1;
                CalculateTax();
                UpdateTotalAmount();
            }
            //newItem.Codigo = item.Text;
            //
            // newItem.Descripcion = item_Copy.Text;
            // newItem.Cantidad = (double)Cantidad.Value;
            // newItem.Rate = (double)rate.Value;
            // m_invoiceItem = newItem;
            this.DataContext = m_invoiceItem;
            //// llenargrid();

           
        }
        DataTable dtbl = new DataTable();

        private void llenargrid()
        {
            try
            {
                //aqui puede ir una validacion si la coneccion regresa true haz esto si no salte 

                string conect = "SERVER = 192.168.200.20; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                SqlConnection con = new SqlConnection(conect);

                string cmd = "Select Top 100 percent dbo.Articulos.Codigo_De_Articulo AS Codigo, dbo.Articulos.Descripcion, dbo.Articulos.Ancho, dbo.Articulos.Serie, dbo.Articulos.Rin, dbo.Articulos.Descripcion +' '+dbo.Articulos.Ancho+' '+dbo.Articulos.Codigo_De_Articulo AS NombreCompleto, dbo.Articulos.Descripcion +' '+dbo.Articulos.Ancho +' '+CONVERT (varchar(50), dbo.Articulos.Serie)+' '+CONVERT (varchar(50), dbo.Articulos.Rin) AS Llanta from Articulos ";
                
                 con.Open(); 
               

                SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);

                DataSet dsPubs = new DataSet("Pubs");

                sda.Fill(dsPubs, "LLantas");

                dtbl = dsPubs.Tables["LLantas"];

                dataGrid1.ItemsSource = dsPubs.Tables["LLantas"].DefaultView;


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


        private void Cerrar(object sender, RoutedEventArgs e)
        {
            if (CloseRequested != null)

                CloseRequested(this, EventArgs.Empty);

            // este esta bien this.Close();

        }

        private void updtButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(item_Copy.Text) || string.IsNullOrWhiteSpace(radComboNivel.Text))
            {

                if (string.IsNullOrWhiteSpace(item_Copy.Text))
                { System.Windows.MessageBox.Show("Debe de seleccionar un articulo, tap twice ", "Error al actualizar"); }


                if (string.IsNullOrWhiteSpace(radComboNivel.Text))
                { System.Windows.MessageBox.Show("Debe de seleccionar un nivel del articulo", "Error al actualizar"); }


            }
            else
            {
                FieldsUpdateEventArgs args = new FieldsUpdateEventArgs();
                args.UpdatedFields = m_invoiceItem;

                if (UpdateRequested != null)
                {
                    UpdateRequested(this, args);
                }
            }

        }

        private void UpdateTotalAmount()
        {

            int quantityValue = GetQuantityAsInt();
            double rateValue = (double)rate.Value;
            double calculatedTax = m_invoiceItem.Iva;
            double taxedAmount = (quantityValue * rateValue) + calculatedTax;
            //m_invoiceItem.Rate = taxedAmount;
            this.Total.Content = "$" + taxedAmount.ToString();
        }


        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //cuando cambie la seleccion del renglon 
            DataGrid gd = (DataGrid)sender;
            //InvoiceItem newItem;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                // newItem = new InvoiceItem(); version funsiona con nativo grid 
                //newItem.Codigo= row_selected["Codigo"].ToString();
                m_invoiceItem.Codigo = row_selected["Codigo"].ToString();
                m_invoiceItem.Descripcion = row_selected["Llanta"].ToString();
                //m_invoiceItem.Cantidad =(double)Cantidad.Value;
                // m_invoiceItem.Cantidad = (double)Cantidad.Value;

                item.Text = row_selected["Codigo"].ToString();
                item_Copy.Text = row_selected["Llanta"].ToString();
            }

           
            //rate_Iva.Text = "15";
        }

        private void tbAssembly_TextChanged(object sender, RoutedEventArgs e)
        {


        }

        private void textFactura_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView DV = new DataView(dtbl);
            DV.RowFilter = string.Format("NombreCompleto LIKE '%{0}%'", item.Text);
            dataGrid1.ItemsSource = DV;

            if (string.IsNullOrWhiteSpace(item.Text))
            {
                item_Copy.Text = "";
                radComboNivel.ItemsSource = null;
                precioCodigo.Text = "";
            }

        }

        private void quantity_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {


            if (onInit)
                return;
            if (rate != null)
            {

                double value = (double)rate.Value;
                {
                    int quantityValue = GetQuantityAsInt();
                    CalculateTax();
                    UpdateTotalAmount();
                    //m_invoiceItem.Cantidad =(double)Cantidad.Value;
                    //newItem.Cantidad = (double)Cantidad.Value;
                    //rate.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 64, 81));
                    //  CalculateTax();

                }
            }
        }

        public int GetQuantityAsInt()
        {
            return (Cantidad.Value is double) ? (int)Cantidad.Value : (int)(double)Cantidad.Value;
        }
        private void CalculateTax()
        {
            m_invoiceItem.Cantidad = GetQuantityAsInt();
            double currentRate = (double)Cantidad.Value;

            int currentQuantity = GetQuantityAsInt();
            double precio = m_invoiceItem.Rate;
            double totall = currentQuantity * precio;
            m_invoiceItem.Total = totall;


            Total.Content = "$" + totall.ToString();
            //m_invoiceItem.Cantidad = (double)Cantidad.Val


        }

        private void rdvMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            var cour = dataGrid1.SelectedItem;

            item.Text = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();
            item_Copy.Text = ((System.Data.DataRowView)cour).Row.ItemArray[6].ToString();
            m_invoiceItem.Codigo = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();
            m_invoiceItem.Descripcion = ((System.Data.DataRowView)cour).Row.ItemArray[6].ToString();
            // m_invoiceItem.Cantidad = (double)Cantidad.Value;
            //rate.Value = 3080;
            string strigCodigo = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();

            llenarcombobox(strigCodigo);
           // quemedidatengo(strigCodigo);
        }
        private void quemedidatengo(string codigo)
        {
            try
            {

                string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099";
                SqlConnection con = new SqlConnection(conect);
                try
                {
                    con.Open();

                    string cmd = " SELECT Codigo_De_Articulo, Unidad FROM Articulos where Codigo_De_Articulo = '" + codigo + "' ";



                    SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);
                    DataSet dsPubs = new DataSet("Pubs");
                    sda.Fill(dsPubs, "Articulos");
                    DataTable dtblg = new DataTable();
                    dtblg = dsPubs.Tables["Articulos"];
                    radComboNivel.ItemsSource = dsPubs.Tables["Articulos"].DefaultView;
                    


                    try
                    {
                        string apollo = dtblg.Rows[0][1].ToString();
                        //Medida.Text = apollo;
                        m_invoiceItem.Medida = apollo;
                    }
                    catch
                    {

                        RadWindow radWindow = new RadWindow();
                        RadWindow.Alert(new DialogParameters()
                        {
                            Content = "El código no tiene niveles de precio",
                            Header = "BIG",

                            DialogStartupLocation = WindowStartupLocation.CenterOwner
                            // IconContent = "";
                        });
                    }
                    //dataGrid1.ItemsSource = dsPubs.Tables["Precios"].DefaultView;
                    //dataGrid1.Columns[0].Visibility = false;
                    //dataGrid1.Tables["LLantas"].Columns[0].ColumnMapping = MappingType.Hidden;
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
            catch (InvalidCastException e)
            {

                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "No se pueden llenar los campos.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }

        }

        private void llenarcombobox(string codigo)
        {

            try
            {

                string conect = "SERVER = 192.168.200.20; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                SqlConnection con = new SqlConnection(conect);
                try
                {
                    con.Open();

                    // string cmd = " SELECT Codigo_De_Articulo,Nivel_De_Precios,Precio FROM Precios where Codigo_De_Articulo = '" + codigo + "' ";

                    string cmd = "SELECT  Precios.Codigo_De_Articulo, Precios.Nivel_De_Precios, Precios.Precio, Articulos.Unidad FROM Articulos INNER JOIN Precios ON Articulos.Codigo_De_Articulo = Precios.Codigo_De_Articulo WHERE(Articulos.Codigo_De_Articulo = '" + codigo + "')";

                    SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);
                    DataSet dsPubs = new DataSet("Pubs");
                    sda.Fill(dsPubs, "Precios");
                    DataTable dtblg = new DataTable();
                    dtblg = dsPubs.Tables["Precios"];
                    radComboNivel.ItemsSource = dsPubs.Tables["Precios"].DefaultView;
                    var oso = dsPubs.Tables["Precios"].DefaultView;


                    try
                    {
                        string apollo = dtblg.Rows[0][2].ToString();
                        string med = dtblg.Rows[0][3].ToString();
                        m_invoiceItem.Medida = med;
                        m_invoiceItem.Preciolista = Convert.ToDouble(apollo);
                    }
                    catch
                    {

                        RadWindow radWindow = new RadWindow();
                        RadWindow.Alert(new DialogParameters()
                        {
                            Content = "Este código no tiene niveles registrados",
                            Header = "BIG",

                            DialogStartupLocation = WindowStartupLocation.CenterOwner
                            // IconContent = "";
                        });
                    }
                    //dataGrid1.ItemsSource = dsPubs.Tables["Precios"].DefaultView;
                    //dataGrid1.Columns[0].Visibility = false;
                    //dataGrid1.Tables["LLantas"].Columns[0].ColumnMapping = MappingType.Hidden;
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
            catch (InvalidCastException e)
            {

                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "No se pueden llenar los campos.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }
            //quemedidatengo(codigo);

        }

        private void nivel_SelectioChanged(object sender, SelectionChangedEventArgs e)
        {

            var objPrecio = radComboNivel.SelectedItem;
            //((System.Data.DataRowView)objPrecio).Row.ItemArray[2].ToString();
            if (objPrecio != null)
            {
                precioCodigo.Text = ((System.Data.DataRowView)objPrecio).Row.ItemArray[2].ToString();
                string precioactual = ((System.Data.DataRowView)objPrecio).Row.ItemArray[2].ToString();
                m_invoiceItem.Rate = Convert.ToDouble(((System.Data.DataRowView)objPrecio).Row.ItemArray[2].ToString());
                m_invoiceItem.Nivel = ((System.Data.DataRowView)objPrecio).Row.ItemArray[1].ToString();
                // m_invoiceItem.Preciolista = 
                rate.Value = Convert.ToDouble(precioactual);
                Total.Content = precioactual;
            }
            else
            {
                precioCodigo.Text = "";
            }
            //m_invoiceItem.Nivel = row_selected["Codigo"].ToString();

        }


        private void texboxx_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("ALL TERRAIN 55 255 14");
            data.Add("ALL TERRAIN 55 255 15");
            data.Add("MICHELLIN 55 255 16");
            data.Add("ALL TERRAIN 55 255 14");
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }

        private void texboxx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedcomboitem = sender as ComboBox;
            string name = selectedcomboitem.SelectedItem as string;

        }

        public void InitializeFocus()
        {
            this.item.Focus();
        }

        private void comboBox_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid1_DataLoaded(object sender, EventArgs e)
        {

            dataGrid1.Columns[5].IsVisible = false;
            dataGrid1.Columns[6].IsVisible = false;

        }
    }


    public class FieldsUpdateEventArgs : EventArgs
    {
        private RemiItem m_invoiceItem;
        public RemiItem UpdatedFields
        {
            get
            {
                return m_invoiceItem;
            }
            set
            {
                m_invoiceItem = value;
            }
        }
    }
}

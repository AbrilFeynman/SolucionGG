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

namespace GGGC.Admin.AZ.Ordenes.Views
{
    /// <summary>
    /// Interaction logic for OrderCliente.xaml
    /// </summary>
    public sealed partial class OrderCliente : Window
    {

        public event EventHandler CloseRequestedc;
        //public event EventHandler CloseLlamada;
        bool onInit;
        public event EventHandler UpdateRequestedc;
        ClienteItem m_invoiceItemC;

        public OrderCliente()
           : this(null, "Add Fields")
        {

        }
        public OrderCliente(ClienteItem newItem, string title)
        {
            InitializeComponent();


            dataGrid2.ShowGroupPanel = false;
            llenargrid();

            if (newItem == null)
            {
                
                rate.Value = 0.00;
                
                newItem = new ClienteItem();
            }

            //item.Text = newItem.Codigo;

            //item_Copy.Text = newItem.Descripcion;

            //rate.Value = 00.00;
            //m_invoiceItem = newItem;
            //Cantidad.Value = newItem.Cantidad;

            m_invoiceItemC = newItem;


            this.DataContext = m_invoiceItemC;
       


        }

        DataTable dtbl = new DataTable();

        private void llenargrid()
        {
            try
            {

                string conect = "SERVER = gggctserver.database.windows.net; DATABASE =devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099";
                SqlConnection con = new SqlConnection(conect);

                string cmd = "SELECT Top 100 percent  Id_Radial as Radial ,Tipo_De_Persona as Tipo,Nombre  +' '+Apellido_Paterno+' '+Apellido_Materno as Nombre, RFC as RFC, Telefono_Personal as Telefono, Direccion  +' '+Colonia+' '+ Ciudad as Direccion FROM Clientes_Frecuentes ";
                try { con.Open(); }
                catch (SqlException ex) { MessageBox.Show("Revise su conexión a internet"); }

                SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);

                DataSet dsPubs = new DataSet("Pubs");

                sda.Fill(dsPubs, "Clientes_Frecuentes");

                dtbl = dsPubs.Tables["Clientes_Frecuentes"];





              

                dataGrid2.ItemsSource = dsPubs.Tables["Clientes_Frecuentes"].DefaultView;
               

                con.Close();

            }
            catch (InvalidCastException e)
            {
                MessageBox.Show("No se pudo llenar los campos" + e.ToString());
            }

        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            if (CloseRequestedc != null)

                CloseRequestedc(this, EventArgs.Empty);

            // este esta bien this.Close();

        }


        private void updtButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(item_Copy.Text) )
            {

               
                 System.Windows.MessageBox.Show("Debe de seleccionar un articulo, tap twice ", "Error al actualizar"); 


                

                
            }
            else
            {

                FieldsUpdateEventArgss args = new FieldsUpdateEventArgss();
                args.UpdatedFields = m_invoiceItemC;

                if (UpdateRequestedc != null)
                {
                    UpdateRequestedc(this, args);
                }
            }

        }


        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //cuando cambie la seleccion del renglon 
            DataGrid gd = (DataGrid)sender;
            //InvoiceItem newItem;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
             
                m_invoiceItemC.Numero_De_Cliente =Convert.ToInt32( row_selected["Radial"]);
                m_invoiceItemC.RFC = row_selected["RFC"].ToString();
                m_invoiceItemC.Nombre_De_Cliente = row_selected["Nombre"].ToString();
                m_invoiceItemC.Telefono = row_selected["Telefono"].ToString();
                m_invoiceItemC.Telefono = row_selected["Direccion"].ToString();



            }

        
        }

        private void textFactura_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView DV = new DataView(dtbl);
            DV.RowFilter = string.Format("Nombre LIKE '%{0}%'", item.Text);
            dataGrid2.ItemsSource = DV;
            // llenarcombobox(item.Text);
            if (string.IsNullOrWhiteSpace(item.Text))
            {
                item_Copy.Text = "";
                item_Copy1.Text = "";
               
            }

            // llenarcombobox(item.Text);
        }

        //private void rdvMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{

        //    var cour = dataGrid1.SelectedItem;

        //    item.Text = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();
        //    item_Copy.Text = ((System.Data.DataRowView)cour).Row.ItemArray[2].ToString();
        //    item_Copy1.Text = ((System.Data.DataRowView)cour).Row.ItemArray[3].ToString();
        //    m_invoiceItemC.Numero_De_Cliente = Convert.ToInt32( ((System.Data.DataRowView)cour).Row.ItemArray[0]);
        //    m_invoiceItemC.RFC = ((System.Data.DataRowView)cour).Row.ItemArray[3].ToString();
           
        //    string strigCodigo = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();

        //}

        public void InitializeFocus()
        {
            this.item.Focus();
        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var objtitem = dataGrid2.SelectedItem;
            item.Text = ((System.Data.DataRowView)objtitem).Row.ItemArray[0].ToString();
            item_Copy.Text = ((System.Data.DataRowView)objtitem).Row.ItemArray[2].ToString();
            item_Copy1.Text = ((System.Data.DataRowView)objtitem).Row.ItemArray[3].ToString();
            m_invoiceItemC.Numero_De_Cliente = Convert.ToInt32(((System.Data.DataRowView)objtitem).Row.ItemArray[0]);
            m_invoiceItemC.Nombre_De_Cliente = ((System.Data.DataRowView)objtitem).Row.ItemArray[2].ToString();
            m_invoiceItemC.RFC = ((System.Data.DataRowView)objtitem).Row.ItemArray[3].ToString();
            m_invoiceItemC.Telefono = ((System.Data.DataRowView)objtitem).Row.ItemArray[4].ToString();
            m_invoiceItemC.Direccion = ((System.Data.DataRowView)objtitem).Row.ItemArray[5].ToString();
            //string strigCodigo = ((System.Data.DataRowView)objtitem).Row.ItemArray[0].ToString();

        }

        private void dataGrid2_DataLoaded(object sender, EventArgs e)
        {
           //Hide las columnas que no quieras mostrar
            dataGrid2.Columns[4].IsVisible = false;
            dataGrid2.Columns[5].IsVisible = false;

                   

        }
    }


    public class FieldsUpdateEventArgss : EventArgs
    {
        private ClienteItem m_invoiceItemC;
        public ClienteItem UpdatedFields
        {
            get
            {
                return m_invoiceItemC;
            }
            set
            {
                m_invoiceItemC = value;
            }
        }
    }
}

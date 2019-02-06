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

namespace GGGC.Admin.AZ.OtrosIngresos.Views
{
    /// <summary>
    /// Interaction logic for OtrosCliente.xaml
    /// </summary>
    public partial class OtrosCliente : Window
    {
        public event EventHandler CloseRequestedc;
        //public event EventHandler CloseLlamada;
        bool onInit;
        public event EventHandler UpdateRequestedc;
        ClienteItem m_invoiceItemC;

        public OtrosCliente()
           : this(null, "Add Fields")
        {

        }
        public OtrosCliente(ClienteItem newItem, string title)
        {
            InitializeComponent();


            dataGrid2.ShowGroupPanel = false;
            llenargrid();

            if (newItem == null)
            {

                rate.Value = 0.00;

                newItem = new ClienteItem();
            }

            // item.Text = newItem.Numero_De_Cliente.ToString();

            //item_Copy.Text = newItem.Nombre;

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

                string cmd = "SELECT Top 100 percent  Numero_De_Cliente as NoCliente ,Nombre  as Nombre, RFC as RFC, Direccion as Direccion, Colonia as Colonia, Ciudad as Ciudad, Estado as Estado, CP as CP FROM Clientes  ";
                con.Open();


                SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);

                DataSet dsPubs = new DataSet("Pubs");

                sda.Fill(dsPubs, "Clientes");

                dtbl = dsPubs.Tables["Clientes"];

                dataGrid2.ItemsSource = dsPubs.Tables["Clientes"].DefaultView;
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

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            if (CloseRequestedc != null)

                CloseRequestedc(this, EventArgs.Empty);

            // este esta bien this.Close();

        }


        private void updtButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(item_Copy.Text))
            {


                System.Windows.MessageBox.Show("Debe de seleccionar un cliente ", "Error al actualizar");





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

                m_invoiceItemC.Numero_De_Cliente = Convert.ToInt32(row_selected["NoCliente"]);
                m_invoiceItemC.RFC = row_selected["RFC"].ToString();



            }


        }

        private void textFactura_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView DV = new DataView(dtbl);
            DV.RowFilter = string.Format("Nombre LIKE '%{0}%'", item.Text);
            dataGrid2.ItemsSource = DV;

            if (string.IsNullOrWhiteSpace(item.Text))
            {
                item_Copy.Text = "";
                item_Copy1.Text = "";

            }


        }


        public void InitializeFocus()
        {
            this.item.Focus();
        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var objtitem = dataGrid2.SelectedItem;
            item.Text = ((System.Data.DataRowView)objtitem).Row.ItemArray[1].ToString();
            item_Copy.Text = ((System.Data.DataRowView)objtitem).Row.ItemArray[0].ToString();

            item_Copy1.Text = ((System.Data.DataRowView)objtitem).Row.ItemArray[2].ToString();
            m_invoiceItemC.Numero_De_Cliente = Convert.ToInt32(((System.Data.DataRowView)objtitem).Row.ItemArray[0]);
            m_invoiceItemC.RFC = ((System.Data.DataRowView)objtitem).Row.ItemArray[2].ToString();
            m_invoiceItemC.Nombre = ((System.Data.DataRowView)objtitem).Row.ItemArray[1].ToString();
            m_invoiceItemC.Direccion = ((System.Data.DataRowView)objtitem).Row.ItemArray[3].ToString();
            m_invoiceItemC.Colonia = ((System.Data.DataRowView)objtitem).Row.ItemArray[4].ToString();
            m_invoiceItemC.Ciudad = ((System.Data.DataRowView)objtitem).Row.ItemArray[5].ToString();
            m_invoiceItemC.Estado = ((System.Data.DataRowView)objtitem).Row.ItemArray[6].ToString();
            m_invoiceItemC.Cp = ((System.Data.DataRowView)objtitem).Row.ItemArray[7].ToString();
            //string strigCodigo = ((System.Data.DataRowView)objtitem).Row.ItemArray[0].ToString();

        }

        private void dataGrid2_DataLoaded(object sender, EventArgs e)
        {
            dataGrid2.Columns[3].IsVisible = false;
            dataGrid2.Columns[4].IsVisible = false;
            dataGrid2.Columns[5].IsVisible = false;
            dataGrid2.Columns[6].IsVisible = false;
            dataGrid2.Columns[7].IsVisible = false;

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

﻿using System;
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

namespace GGGC.Admin.AZ.Compras
{
    /// <summary>
    /// Interaction logic for CompraProveedor.xaml
    /// </summary>
    public partial class CompraProveedor : Window
    {

        public event EventHandler CloseRequestedc;
        //public event EventHandler CloseLlamada;
        bool onInit;
        public event EventHandler UpdateRequestedc;
        ProveedorItem m_invoiceItemC;

        public CompraProveedor()
             : this(null, "Add Fields")
        {
           
        }


        public CompraProveedor(ProveedorItem newItem, string title)
        {
            InitializeComponent();


            dataGrid2.ShowGroupPanel = false;
            llenargrid();

            if (newItem == null)
            {

                rate.Value = 0.00;

                newItem = new ProveedorItem();
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

                string cmd = "SELECT Codigo_De_Proveedor as Codigo_De_Proveedor ,Nombre_De_Proveedor as Nombre_De_Proveedor,RFC as RFC FROM [dbo].[Proveedores]";
                try { con.Open(); }
                catch (SqlException ex) { MessageBox.Show("Revise su conexión a internet"); }

                SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);

                DataSet dsPubs = new DataSet("Pubs");

                sda.Fill(dsPubs, "Proveedores");

                dtbl = dsPubs.Tables["Proveedores"];







                dataGrid2.ItemsSource = dsPubs.Tables["Proveedores"].DefaultView;


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

            if (string.IsNullOrWhiteSpace(item_Copy.Text))
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

                m_invoiceItemC.Numero_Proveedor = Convert.ToInt32(row_selected["Codigo_De_Proveedor"]);
                m_invoiceItemC.RFC = row_selected["RFC"].ToString();
                m_invoiceItemC.Nombre = row_selected["Nombre_De_Proveedor"].ToString();
              



            }


        }

        private void textFactura_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView DV = new DataView(dtbl);
            DV.RowFilter = string.Format("Nombre_De_Proveedor LIKE '%{0}%'", item.Text);
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
            item_Copy.Text = ((System.Data.DataRowView)objtitem).Row.ItemArray[1].ToString();
            item_Copy1.Text = ((System.Data.DataRowView)objtitem).Row.ItemArray[2].ToString();
            m_invoiceItemC.Numero_Proveedor = Convert.ToInt32(((System.Data.DataRowView)objtitem).Row.ItemArray[0]);
            m_invoiceItemC.Nombre = ((System.Data.DataRowView)objtitem).Row.ItemArray[1].ToString();
            m_invoiceItemC.RFC = ((System.Data.DataRowView)objtitem).Row.ItemArray[2].ToString();
           
            //string strigCodigo = ((System.Data.DataRowView)objtitem).Row.ItemArray[0].ToString();

        }

        private void dataGrid2_DataLoaded(object sender, EventArgs e)
        {
            //Hide las columnas que no quieras mostrar
            //dataGrid2.Columns[4].IsVisible = false;
            //dataGrid2.Columns[5].IsVisible = false;



        }
    }


    public class FieldsUpdateEventArgss : EventArgs
    {
        private ProveedorItem m_invoiceItemC;
        public ProveedorItem UpdatedFields
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

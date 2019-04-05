using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GGGC.Admin.AZ.Transferencias
{
    /// <summary>
    /// Interaction logic for TransferenciaDialog.xaml
    /// </summary>
    public partial class TransferenciaDialog : Window
    {
        public event EventHandler CloseRequested;
        //public event EventHandler CloseLlamada;
        bool onInit;
        public event EventHandler UpdateRequested;
        TransferenciaItem m_invoiceItem;
        double precio_uni = 0.00;
        double desc_uni = 0.00;
        double total_uni = 0.00;
        int cant_uni = 0;
        string descuento = "0";
        double precio_uni_desc = 0.00;
        double extendidosindesc = 0.00;
        //DataTable dt = new DataTable();

        public TransferenciaDialog()
             : this(null, "Add Fields")
        {

        }

        public TransferenciaDialog(TransferenciaItem newItem, string title)
        {
            InitializeComponent();


            dataGrid1.ShowGroupPanel = false;
            llenargrid();

            if (newItem == null)
            {

                rate.Value = 0.00;

                newItem = new TransferenciaItem();
            }

            item.Text = newItem.Codigo;

            item_Copy.Text = newItem.Descripcion;

            rate.Value = 00.00;
            m_invoiceItem = newItem;
            Cantidad.Value = newItem.Cantidad;






            if (newItem.Cantidad == 0)
            {
                newItem.Cantidad = GetQuantityAsInt();
                this.Cantidad.Value = 1;
                CalculateTax();

            }

            this.DataContext = m_invoiceItem;

        }
        DataTable dtbl = new DataTable();

        private void dataGrid2_DataLoaded(object sender, EventArgs e)
        {
            //Hide las columnas que no quieras mostrar
            dataGrid1.Columns[5].IsVisible = false;
            dataGrid1.Columns[6].IsVisible = false;



        }
        private void llenargrid()
        {
            try
            {

                string conect = "SERVER = gggctserver.database.windows.net; DATABASE = rdbms_GGGC_Public_TESTING; USER ID = abril; PASSWORD = gggc.2017";
                SqlConnection con = new SqlConnection(conect);

                string cmd = "Select Top 100 percent dbo.LLantas.Codigo_De_Articulo AS Codigo, dbo.LLantas.Descripcion, dbo.LLantas.Ancho, dbo.LLantas.Serie, dbo.LLantas.Rin, dbo.LLantas.Descripcion +' '+dbo.LLantas.Ancho+' '+dbo.LLantas.Codigo_De_Articulo AS NombreCompleto, dbo.LLantas.Descripcion +' '+dbo.LLantas.Ancho +' '+CONVERT (varchar(50), dbo.LLAntas.Serie)+' '+CONVERT (varchar(50), dbo.LLantas.Rin) AS Llanta from LLantas ";
                try { con.Open(); }
                catch (SqlException ex) { MessageBox.Show("Revise su conexión a internet"); }

                SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);

                DataSet dsPubs = new DataSet("Pubs");

                sda.Fill(dsPubs, "LLantas");

                dtbl = dsPubs.Tables["LLantas"];







                dataGrid1.ItemsSource = dsPubs.Tables["LLantas"].DefaultView;


                con.Close();

            }
            catch (InvalidCastException e)
            {
                MessageBox.Show("No se pudo llenar los campos" + e.ToString());
            }

        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            if (CloseRequested != null)

                CloseRequested(this, EventArgs.Empty);

            // este esta bien this.Close();

        }

        private void PrecioValidation(object sender, TextCompositionEventArgs e)
        {

            //ONLY DECIMAL
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));


        }

        private void DescuentoValidation(object sender, TextCompositionEventArgs e)
        {

            //ONLY DECIMAL
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));


        }

        double precdes()
        {
            double masha = 0.00;

            if (total_uni > 0)
            {

                double porcentage = 100 - Convert.ToDouble(descuento);
                double safari = porcentage / 100;
                masha = precio_uni * safari;
                masha = Math.Round(masha, 2);


            }
            else
            {
                masha = 0.00;
            }


            return masha;
        }

        private void updtButton_Click(object sender, RoutedEventArgs e)
        {



            if (string.IsNullOrWhiteSpace(item_Copy.Text) || Cantidad.Value == 0)
            {

                if (string.IsNullOrWhiteSpace(item_Copy.Text))
                { System.Windows.MessageBox.Show("Debe de seleccionar un articulo, tap twice ", "Error al actualizar"); }

                if (Cantidad.Value == 0)
                { System.Windows.MessageBox.Show("La cantidad a ajustar debe ser diferente a cero, tap twice ", "Error al actualizar"); }




            }
            else
            {



                m_invoiceItem.Cantidad = Convert.ToInt32(Cantidad.Value);
                m_invoiceItem.Unidad = "PZA";
                m_invoiceItem.Codigo = item.Text;
                m_invoiceItem.Descripcion = item_Copy.Text;


                FieldsUpdateEventArgs args = new FieldsUpdateEventArgs();
                args.UpdatedFields = m_invoiceItem;

                if (UpdateRequested != null)
                {
                    UpdateRequested(this, args);
                }
            }

        }




        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //cuando cambie la seleccion del renglon 
            DataGrid gd = (DataGrid)sender;

            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {

                m_invoiceItem.Codigo = row_selected["Codigo"].ToString();
                m_invoiceItem.Descripcion = row_selected["Llanta"].ToString();


                item.Text = row_selected["Codigo"].ToString();
                item_Copy.Text = row_selected["Llanta"].ToString();
            }


        }

        private void tbAssembly_TextChanged(object sender, RoutedEventArgs e)
        {


        }

        private void textFactura_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView DV = new DataView(dtbl);
            DV.RowFilter = string.Format("NombreCompleto LIKE '%{0}%'", item.Text);
            dataGrid1.ItemsSource = DV;
            // llenarcombobox(item.Text);
            if (string.IsNullOrWhiteSpace(item.Text))
            {
                item_Copy.Text = "";

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



        }

        private void rdvMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            var cour = dataGrid1.SelectedItem;

            item.Text = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();
            item_Copy.Text = ((System.Data.DataRowView)cour).Row.ItemArray[6].ToString();
            m_invoiceItem.Codigo = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();
            m_invoiceItem.Descripcion = ((System.Data.DataRowView)cour).Row.ItemArray[6].ToString();

            string strigCodigo = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();


        }

        private void llenarcombobox(string codigo)
        {



            try
            {

                string conect = "SERVER = gggctserver.database.windows.net; DATABASE = rdbms_GGGC_Public_TESTING; USER ID = abril; PASSWORD = gggc.2017";
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
                    //radComboNivel.ItemsSource = dsPubs.Tables["Precios"].DefaultView;
                    var oso = dsPubs.Tables["Precios"].DefaultView;


                    try
                    {
                        string apollo = dtblg.Rows[0][2].ToString();
                        // m_invoiceItem.Preciolista = Convert.ToDouble(apollo);
                    }
                    catch
                    {
                        MessageBox.Show("Este codigo no tiene niveles registrados", "Articulo sin precio");
                    }

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




    }

    public class FieldsUpdateEventArgs : EventArgs
    {
        private TransferenciaItem m_invoiceItem;
        public TransferenciaItem UpdatedFields
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Data;
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
    /// Interaction logic for OrdenDialog.xaml
    /// </summary>
    public sealed partial class OrdenDialog : Window
    {
        public event EventHandler CloseRequested;
        //public event EventHandler CloseLlamada;
        bool onInit;
        public event EventHandler UpdateRequested;
        OrdenItem m_invoiceItem;

        //DataTable dt = new DataTable();
        


        public OrdenDialog()
            : this(null, "Add Fields")
        {
            
        }
        public OrdenDialog(OrdenItem newItem, string title)
        {
            InitializeComponent();


            dataGrid1.ShowGroupPanel = false;
            llenargrid();

            if (newItem == null)
            {
               
                rate.Value = 0.00;
                //prueba usando datacontext
                //InvoiceItem newItem;
                newItem = new OrdenItem();
            }

            item.Text = newItem.Codigo;
            
            item_Copy.Text = newItem.Descripcion;
            // newItem.Cantidad = GetQuantityAsInt();
            //newItem.Cantidad = 2;
            // rate.Value = 80.00;
            //llenarcombobox(item.Text);
            rate.Value = 00.00;
            m_invoiceItem = newItem;
            Cantidad.Value = newItem.Cantidad;
            Empleado.Value = newItem.Empleado;

           
            

            if (newItem.Cantidad == 0)
            {
                newItem.Cantidad = GetQuantityAsInt();
                this.Cantidad.Value = 1;
                CalculateTax();
                UpdateTotalAmount();
            }


            if (newItem.Empleado == 0)
            {
                
                this.Empleado.Value = 1;
               
            }
            //newItem.Codigo = item.Text;
            //
            // newItem.Descripcion = item_Copy.Text;

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


        private void updtButton_Click(object sender, RoutedEventArgs e)
        {
            m_invoiceItem.Empleado = Convert.ToInt16(Empleado.Value);

            if (string.IsNullOrWhiteSpace(item_Copy.Text) || string.IsNullOrWhiteSpace(radComboNivel.Text) )
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
               

                item.Text = row_selected["Codigo"].ToString();
                item_Copy.Text = row_selected["Llanta"].ToString();
            }

           // rate.Value = 80;
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
           // llenarcombobox(item.Text);
            if (string.IsNullOrWhiteSpace(item.Text))
            {
                item_Copy.Text = "";
                radComboNivel.ItemsSource = null;
                precioCodigo.Text = "";
            }

           // llenarcombobox(item.Text);
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
            double precio =  m_invoiceItem.Rate;
            double totall = currentQuantity* precio;
            m_invoiceItem.Total = totall;
            
          
            Total.Content = "$" + totall.ToString();
          


        }

        private void rdvMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            var cour = dataGrid1.SelectedItem;

            item.Text = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();
            item_Copy.Text = ((System.Data.DataRowView)cour).Row.ItemArray[6].ToString();
            m_invoiceItem.Codigo = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();
            m_invoiceItem.Descripcion = ((System.Data.DataRowView)cour).Row.ItemArray[6].ToString();
        
            string strigCodigo = ((System.Data.DataRowView)cour).Row.ItemArray[0].ToString();

           llenarcombobox(strigCodigo);
        }

        private void llenarcombobox(string codigo)
        {

            // xaxaxa var hat = codigo;

              try
            {

                string conect = "SERVER = gggctserver.database.windows.net; DATABASE = rdbms_GGGC_Public_TESTING; USER ID = abril; PASSWORD = gggc.2017";
                SqlConnection con = new SqlConnection(conect);
                try { con.Open();

                    string cmd = " SELECT Codigo_De_Articulo,Nivel_De_Precios,Precio FROM Precios where Codigo_De_Articulo = '" + codigo + "' ";



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
                        m_invoiceItem.Preciolista = Convert.ToDouble(apollo);
                    }
                    catch
                    {
                        MessageBox.Show("Este codigo no tiene niveles registrados", "Articulo sin precio");
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
                rate.Value = Convert.ToDouble( precioactual);
                double preciodelnivel = Convert.ToDouble(precioactual);
                double quantity =Convert.ToDouble(Cantidad.Value);
                double meactualizo = preciodelnivel * quantity;
                Total.Content = meactualizo;
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

        //public Dialog(InvoiceItem newItem, string title, int productIndex)
        //{
        //    this.InitializeComponent();

        //    //this.Height 

        //    if (newItem == null)
        //    {
        //        newItem = new InvoiceItem();
        //        newItem.Descripcion = productList[0].Name;
        //        newItem.Rate = productList[0].Rate;
        //    }
        //    m_invoiceItem = newItem;
        //    onInit = true;
        //    this.DataContext = m_invoiceItem;
        //    this.Title = title;
        //    double currentRate = m_invoiceItem.Rate;

        //    m_productlist = productList;
        //    this.item.ItemsSource = m_productlist;
        //    this.item.DisplayMemberPath = "Name";
        //    this.item.SelectedItem = m_productlist[productIndex];
        //    if (currentRate != 0)
        //        this.rate.Value = currentRate;
        //    if (!newItem.Taxes.Equals("0"))
        //    {
        //        this.taxesTextBlock.Text = newItem.Taxes.ToString();
        //    }
        //    if (newItem.Rate != 0)
        //    {
        //        this.rate.Value = newItem.Rate;

        //        UpdateTotalAmount();
        //    }

        //    if (newItem.Quantity == 0)
        //    {
        //        newItem.Quantity = 1;
        //        this.quantity.Value = 1;
        //        CalculateTax();
        //        UpdateTotalAmount();
        //    }
        //    onInit = false;
        //}







    }

    public class FieldsUpdateEventArgs : EventArgs
    {
        private OrdenItem m_invoiceItem;
        public OrdenItem UpdatedFields
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

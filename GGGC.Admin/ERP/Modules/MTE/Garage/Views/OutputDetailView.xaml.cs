using GGGC.Admin.ERP.Modules.MTE.Garage.Support;
using GGGC.Admin.ERP.Modules.MTE.Garage.Models;
using GGGC.Modules.Data;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Views
{
    /// <summary>
    /// Interaction logic for InputDetailView.xaml
    /// </summary>
    public partial class OutputDetailView : Window
    {

        private AccesoDatos sCen;
        private AccesoDatos sLRG;
        private AccesoDatos sCLT;
         #region Fields
        public event EventHandler CloseRequested;
        public event EventHandler UpdateRequested;
        InvoiceItem m_invoiceItem;
        ProductList m_productlist;

        GGGC.Admin.ERP.Modules.MTE.Garage.Support.Product listaProductos;
        const double DefaultTaxInPercent = 0;
        bool onInit;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public OutputDetailView()
            : this(null, "Add Fields", new ProductList())
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public OutputDetailView(ProductList productList)
            : this(null, "Agregar Productos", productList)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="title"></param>
        /// <param name="productList"></param>
        public OutputDetailView(InvoiceItem newItem, string title, ProductList productList)
            : this(newItem, title, productList, 0)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="title"></param>
        /// 
        public OutputDetailView(InvoiceItem newItem, string title, ProductList productList, int productIndex)
        {
            this.InitializeComponent();

            //this.Height 

            if (newItem == null)
            {
                newItem = new InvoiceItem();
                newItem.ProductID = productList[0].ProductID;
                newItem.CodeID = productList[0].CodeID;
                newItem.ItemName = productList[0].Name;
                newItem.UnitPrice = productList[0].Price;
               // newItem.Quantity = productList[0].q
                this.item.SelectedItem = null;
            }
            m_invoiceItem = newItem;
            onInit = true;

            this.DataContext = m_invoiceItem;
            this.Title= title;
            decimal currentRate = m_invoiceItem.UnitPrice;

            m_productlist = productList;
            this.item.ItemsSource = m_productlist;
            this.item.DisplayMemberPath = "CodeID";
            this.item.SelectedItem = m_productlist[productIndex];
            if (currentRate != 0)
                this.txtPrecio.Value = Convert.ToDouble(currentRate);
            //if (!newItem.Taxes.Equals("0"))
            //{
            //    this.taxesTextBlock.Text = newItem.Taxes.ToString();
            //}
            if (newItem.UnitPrice != 0)
            {
                this.txtPrecio.Value = Convert.ToDouble(newItem.UnitPrice);

                UpdateTotalAmount();
            }

            if (newItem.Quantity == 0)
            {
                newItem.Quantity = 1;
               // this.quantity.Value = 1;
                CalculateTax();
                UpdateTotalAmount();
            }
            onInit = false;
            this.item.SelectedItem = null;
        }            


        #endregion


        #region Implementation

      



        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (CloseRequested != null)
                CloseRequested(this, EventArgs.Empty);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updtButton_Click(object sender, RoutedEventArgs e)
        {

            if (blnValido())
            {

                FieldsUpdateEventArgsOutput args = new FieldsUpdateEventArgsOutput();
                args.UpdatedFields = m_invoiceItem;

                if (UpdateRequested != null)
                {
                    UpdateRequested(this, args);
                }
            }
            

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private bool blnValido()
        {
            if (item.Text.Trim().Length == 0)
            {
                MostrarMensajeError("Debe Ingresar un Código de Producto");
                this.item.Focus();
                return false;
            }

            if (txtPrecio.Text.Trim() == "")
            {
                MostrarMensajeError("Debe Ingresar un Precio de Producto Existente");
                this.txtPrecio.Focus();
                return false;
            }

            if (item.SelectedIndex == -1)
            {
                MostrarMensajeError("Debe Ingresar un Código de Producto Existente, Sino esta dado de alta vaya al Catálogo de Productos ");
                this.item.Focus();
                return false;
            }


            return true;

        }
        private void item_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)e.OriginalSource;
            textBox.SelectAll();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (onInit)
                return;
            int value = 0;
            if (int.TryParse(quantity.Value.ToString(), out value))
            {
                //CalculateTax();
                UpdateTotalAmount();
                quantity.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 64, 81));
            }
            else
            {
                quantity.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 37, 37));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private async void MostrarMensajeError(string strMessage)

        {

            MessageBox.Show(strMessage, "Grupo Guadiana GC");
            //var metroWindow = (MetroWindow)Application.Current.MainWindow;
            //var settings = new MetroDialogSettings()
            //{
            //    AffirmativeButtonText = "Aceptar",
            //    AnimateHide = true
            //};
            //var result = await metroWindow.ShowMessageAsync("Grupo Guadiana GC", strMessage, MessageDialogStyle.Affirmative, settings);
            ////metroWindow.sh
            //metroWindow.sh

            //LoginDialogData result = await this.ShowLoginAsync("Authentication", "Enter your credentials", new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, InitialUsername = "MahApps", EnablePasswordPreview = true });
            //if (result == null)
            //{
            //    //User pressed cancel
            //}
            //else
            //{
            //    MessageDialogResult messageResult = await this.ShowMessageAsync("Authentication Information", String.Format("Username: {0}\nPassword: {1}", result.Username, result.Password));
            //}

            //  blnMostrar = true;


            //return;
        }
        private void rate_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void item_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (onInit)
        //        return;
        //    //Product product = cboCode.SelectedItem as Product;
        //    //m_invoiceItem.ItemName = product.Name;
        //    //if (!m_invoiceItem.Rate.Equals(product.Rate.ToString()))
        //    //{
        //    //    m_invoiceItem.Rate = product.Rate;
        //    //    rate.Value = m_invoiceItem.Rate;
        //    //    CalculateTax();
        //    //    UpdateTotalAmount();
        //    //}

        //    //MessageBox.Show("item_SelectionChanged");
        //}

        private void item_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (onInit)
                return;

            try
            {
                if (item.SelectedItem != null)
                {
                    GGGC.Admin.ERP.Modules.MTE.Garage.Models.Product product = item.SelectedItem as GGGC.Admin.ERP.Modules.MTE.Garage.Models.Product;
                    m_invoiceItem.ItemName = product.Name;
                    m_invoiceItem.CodeID = product.CodeID;
                    m_invoiceItem.UnitPrice = product.Price;
                    m_invoiceItem.ProductID = product.ProductID;
                }
           
            }
            catch (Exception ex)
            {
                ex = ex;

            }
         
            //if (!m_invoiceItem.Rate.Equals(product.Rate.ToString()))
            //{
            //    m_invoiceItem.Rate = product.Rate;
            //    rate.Value = m_invoiceItem.Rate;
            //    //CalculateTax();
            //    UpdateTotalAmount();
            //}
        }


        #endregion
        #region Helper Method
        /// <summary>
        /// 
        /// </summary>
        public void InitializeFocus()
        {
            //this.cboCode.Focus();

              this.item.Focus();

        }
        /// <summary>
        /// 
        /// </summary>
        public GGGC.Admin.ERP.Modules.MTE.Garage.Models.Product SelectedProduct
        {
            set
            {
                if (value != null)
                {
                   // cboCode.SelectedItem = value;
                    this.item.SelectedItem = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void UpdateTotalAmount()
        {

            int quantityValue = GetQuantityAsInt();
            decimal rateValue = (decimal)txtPrecio.Value;
            decimal calculatedTax = m_invoiceItem.Taxes;
            decimal taxedAmount = (quantityValue * rateValue) + calculatedTax;
          //  this.totalAmount.Text = taxedAmount.ToString() + "$";
        }
        public int GetQuantityAsInt()
        {
            if (quantity.Value == null)
            {
                quantity.Value = 1;
                return (quantity.Value is int) ? (int)quantity.Value : (int)(double)quantity.Value;
            }
            else
            {
                return (quantity.Value is int) ? (int)quantity.Value : (int)(double)quantity.Value;
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        private void CalculateTax()
        {
            double currentRate = 0;// (double)rate.Value;
            int currentQuantity = GetQuantityAsInt();

            {
                double calculatedTax = 0.0;
                calculatedTax = (currentRate * currentQuantity) * (DefaultTaxInPercent / 100);
               // m_invoiceItem.Taxes = calculatedTax;
                //taxesTextBlock.Text = "$" + calculatedTax.ToString();
            }
        }
        #endregion

        //private void quantity_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    if (onInit)
        //        return;

        //    CalculateTax();
        //    UpdateTotalAmount();
        //    //if (txtPrecio != null)
        //    //{
        //    //    double value = (double)txtPrecio.Value;
        //    //    {
        //    //        //rate.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 64, 81));
        //    //        CalculateTax();
        //    //        UpdateTotalAmount();
        //    //    }
        //    //}
        //}
        #endregion
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //txtCodigo.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                this.quantity.Focus();
                this.quantity.SelectAll();
                //if (ue.Tag != null && ue.Tag.ToString == "IgnoreEnterKeyTraversal")
                //{
                //    //ignore
                //}
                //else
                //{
                //    e.Handled = true;
                //    ue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                //}
            }
        }

        private void quantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //txtCodigo.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                this.btnAdd.Focus();
                //this.quantity.SelectAll();
                //if (ue.Tag != null && ue.Tag.ToString == "IgnoreEnterKeyTraversal")
                //{
                //    //ignore
                //}
                //else
                //{
                //    e.Handled = true;
                //    ue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                //}
            }
        }

        private void quantity_ValueChanged(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (onInit)
                return;

            CalculateTax();
            UpdateTotalAmount();

        }

        private void quantity_ValueChanged(object sender, Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs e)
        {

        }

     
    }

    public class FieldsUpdateEventArgsOutput : EventArgs
    {
        private InvoiceItem m_invoiceItem;
        public InvoiceItem UpdatedFields
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


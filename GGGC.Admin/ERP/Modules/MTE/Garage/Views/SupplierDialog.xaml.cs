using GGGC.Admin.ERP.Modules.MTE.Garage.Models;
using GGGC.Admin.ERP.Modules.MTE.Garage.Support;
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
    /// Interaction logic for SupplierDialog.xaml
    /// </summary>
    public partial class SupplierDialog : Window
    {
        private AccesoDatos sCen;
        private AccesoDatos sLRG;
        private AccesoDatos sCLT;

        public event EventHandler CloseRequested;
        public event EventHandler UpdateRequested;
        SupplierInformation info;
        /// <summary>
        /// 
        /// </summary>
        public SupplierDialog()
            : this(new SupplierInformation())
        {
        }

        public SupplierDialog(SupplierInformation billInfo)
        {
            InitializeComponent();
            //var bounds = Window.Current.Bounds;
            //this.Height = bounds.Height;
          //  LoadData();
            info = billInfo;
            this.DataContext = info;
            
        }
        //private void LoadData()
        //{
        //    //try
        //    //{
        //    //  MessageBox.Show("en lineas ");
        //    sCen = new AccesoDatos(6);
        //    System.Data.DataSet ds = new System.Data.DataSet();
        //    string sSQL = "SELECT SupplierID, RFC, TradeName FROM  mto_Suppliers WHERE Visibility = 1 ";
        //    //   MessageBox.Show("sql " + sSQL);
        //    //ds = sCen.BaseDatos.ConsultaDataset("spInventario");
        //    //DataTable tbl = sCen.BaseDatos.Consulta(sSQL);             
        //    //  string sSQL = "SELECT MarcaID, Marca FROM tblMarcas ORDER BY Marca ASC";
        //    //cboLine.ItemsSource = sCen.BaseDatos.Consulta(sSQL);
        //    cboSupplierName.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.Bas
        //    //   MessageBox.Show("consulta");
        //    cboSupplierName.SelectedValuePath = "SupplierID";
        //    cboSupplierName.DisplayMemberPath = "RFC";
        //    cboSupplierName.SelectedIndex = -1;

        //    //sSQL = "SELECT * FROM  Inventory_Brand WHERE BrandID IN(103,104,200) ORDER BY Brand ";
        //    //cboMarca.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.BaseDatos.Consulta(sSQL);
        //    //cboMarca.SelectedValuePath = "BrandID";
        //    //cboMarca.DisplayMemberPath = "Brand";
        //    //cboMarca.SelectedIndex = -1;
        //    //sSQL = "SELECT * FROM  Inventory_Unit ORDER BY UnitID ";
        //    //cboUnidad.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.BaseDatos.Consulta(sSQL);
        //    //cboUnidad.SelectedValuePath = "UnitID";
        //    //cboUnidad.DisplayMemberPath = "Unit";
        //    //cboUnidad.SelectedIndex = 0;
        //    //sSQL = "SELECT * FROM  Inventory_Tax ORDER BY TaxID ";
        //    //cboImpuesto.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.BaseDatos.Consulta(sSQL);
        //    //cboImpuesto.SelectedValuePath = "TaxID";
        //    //cboImpuesto.DisplayMemberPath = "Tax";
        //    //cboImpuesto.SelectedIndex = 1;
        //    //sSQL = "SELECT * FROM  Inventory_Status ORDER BY StatusID ";
        //    //cboEstatus.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.BaseDatos.Consulta(sSQL);
        //    //cboEstatus.SelectedValuePath = "StatusID";
        //    //cboEstatus.DisplayMemberPath = "Status";
        //    //cboEstatus.SelectedIndex = 0;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    // MessageBox.Show("Error en cargarDatos: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    //}
        //}
        ///// <summary>
        ///// Invoked when this page is about to be displayed in a Frame.
        ///// </summary>
        ///// <param name="e">Event data that describes how this page was reached.  The Parameter
        ///// property is typically used to configure the page.</param>
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //}
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
                BillingInfoEventArgs args = new BillingInfoEventArgs();
                args.SupplierInformation = info;
                if (UpdateRequested != null)
                    UpdateRequested(this, args);
            }

          
        }

        private async void MostrarMensajeError(string strMessage)
        {

            //MessageBox.Show("Grupo Guadiana GC", strMessage);
            MessageBox.Show(strMessage, "Grupo Guadiana GC");
            //var metroWindow = (MetroWindow)Application.Current.MainWindow;
            //var settings = new MetroDialogSettings()
            //{
            //    AffirmativeButtonText = "Aceptar",
            //    AnimateHide = true
            //};
            //var result = await metroWindow.ShowMessageAsync("Grupo Guadiana GC", strMessage, MessageDialogStyle.Affirmative, settings);
            //metroWindow.sh
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

        private bool blnValido()
        {
            if (cboSupplierName.Text.Trim().Length == 0)
            {
                MostrarMensajeError("Debe Ingresar el RFC del Proveedo");
                this.cboSupplierName.Focus();
                return false;
            }

            if (this.invoiceNo.Text.Trim() == "")
            {
                MostrarMensajeError("Debe Ingresar un Folio de Factura");
                this.invoiceNo.Focus();
                return false;
            }

            if (cboSupplierName.SelectedIndex == -1)
            {
                MostrarMensajeError("Debe Ingresar un Proveedor Existente, Sino esta dado de alta avise al Depto. de Sistemas ");
                this.cboSupplierName.Focus();
                return false;
            }

            return true;

        }
        private void invoiceNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value = 0;
            if (int.TryParse(invoiceNo.Text, out value))
            {
                invoiceNo.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 64, 81));
            }
            else
            {

                invoiceNo.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 37, 37));
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void cboSupplierName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboSupplierName.SelectedIndex >= 0)
            {
                string strRFC = cboSupplierName.SelectedValue.ToString();
              //  intLinea = Convert.ToInt32(strLinea);

                var a = cboSupplierName.SelectedValuePath[1];
                var b = cboSupplierName.SelectionBoxItem;
                Supplier prov = cboSupplierName.SelectedItem as Supplier;

                this.txtRFC.Text = strRFC;
                info.Name = strRFC;
                info.Address = prov.TradeName;
                info.SupplierID = prov.SupplierID;
                //this.txtID.
            }

        }

    }
    /// <summary>
    /// 
    /// </summary>
    public class BillingInfoEventArgs : EventArgs
    {
        SupplierInformation m_billInfo;
        public SupplierInformation SupplierInformation
        {
            get { return m_billInfo; }
            set { m_billInfo = value; }
        }
      
    }
}

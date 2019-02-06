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
using System.ComponentModel;
using Telerik.Windows.Controls;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Views
{
    /// <summary>
    /// Interaction logic for SupplierDialog.xaml
    /// </summary>
    public partial class CustomerDialog : Window
    {
        private AccesoDatos sCen;
        private AccesoDatos sLRG;
        private AccesoDatos sCLT;

        public event EventHandler CloseRequested;
        public event EventHandler UpdateRequested;
        CustomerInformation info;
        /// <summary>
        /// 
        /// </summary>
        public CustomerDialog()
            : this(new CustomerInformation())
        {
        }

        public CustomerDialog(CustomerInformation billInfo)
        {
            InitializeComponent();
            //var bounds = Window.Current.Bounds;
            //this.Height = bounds.Height;
          //  LoadData();
            info = billInfo;
            this.DataContext = info;

            List<Location> locations = new List<Location>();



          //  locations.Add(new Location() { EmpresaCode = "1", DepartamentoID = 10 });


            //((GridViewComboBoxColumn)this.radGridView.Columns[0]).ItemsSource = Locations.Continents;
            //this.radGridView.ItemsSource = locations;

            // locations.Add(new Location() { ContinentCode = "EU", CountryID = 2 });
            //((GridViewComboBoxColumn)this.radGridView.Columns[0]).ItemsSource = Locations.Departamentos;
             this.cboSupplierName.ItemsSource = Locations.Empresas;
            // this.cboSucursal.ItemsSource = Locations.Departamentos;

            this.cboSupplierName.SelectedValue = (object)info.CompanyID;

            if (info.CompanyID != null)
            {
                this.cboSupplierName.SelectedValue = null;
                this.cboSupplierName.SelectedValue = (object)info.CompanyID;
                this.cboSucursal.SelectedValue = (object)info.SucursalID;


                string EmpresaCode = (string)cboSupplierName.SelectedValue;

                if (EmpresaCode == "1")
                {
                    this.cboSucursal.ItemsSource = null;
                    this.cboSucursal.ItemsSource = Locations.DepartamentosLRG;
                    this.cboSucursal.SelectedValue = (object)info.SucursalID;
                }

                if (EmpresaCode == "2")
                {
                    this.cboSucursal.ItemsSource = null;
                    this.cboSucursal.ItemsSource = Locations.DepartamentosCLT;

                    this.cboSucursal.SelectedValue = (object)info.SucursalID;
                    this.cboUsuario.SelectedValue = (object)info.CustomerID;
                    this.cboVehicle.SelectedValue = (object)info.VehiculoID;

                }
            }


            //We need to sense when the combo selection is changed and submit the value immediately
            //otherwise the value will be submited on leaving the cell which is too late for our needs. 
            // this.AddHandler(RadComboBox, new Telerik.Windows.Controls.SelectionChangedEventArgs(comboSelectionChanged));
            this.AddHandler(RadComboBox.SelectionChangedEvent, new System.Windows.Controls.SelectionChangedEventHandler(comboSelectionChanged));


            this.cboSucursal.SelectedValue = (object)info.SucursalID;
            this.cboUsuario.SelectedValue = (object)info.CustomerID;
            this.cboVehicle.SelectedValue = (object)info.VehiculoID;
            this.cboUsuario.SelectedValue = (object)info.Name;

            //     this.AddHandler(RadComboBox.SelectionChangedEvent,
            //new Telerik.Windows.Controls.SelectionChangedEventHandler(ComboBox_SelectionChanged));


        }

        void comboSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            //(RadComboBox)args.OriginalSource.

           if ( ((System.Windows.FrameworkElement)args.OriginalSource).Name != "PART_Clock")
            {
                RadComboBox comboBox = (RadComboBox)args.OriginalSource;

                if (comboBox.SelectedValue == null
                    || comboBox.SelectedValuePath != "Code") // we take action only if the continent combo is changed
                    return;

                // Location location = null;

                string EmpresaCode = (string)comboBox.SelectedValue;

                if (EmpresaCode == "1")
                {
                    this.cboSucursal.ItemsSource = null;
                    this.cboSucursal.ItemsSource = Locations.DepartamentosLRG;
                    this.cboSucursal.SelectedValue = (object)info.SucursalID;
                }

                if (EmpresaCode == "2")
                {
                    this.cboSucursal.ItemsSource = null;
                    this.cboSucursal.ItemsSource = Locations.DepartamentosCLT;

                    this.cboSupplierName.SelectedValue = (object)info.CompanyID;
                    this.cboSucursal.SelectedValue = (object)info.SucursalID;
                    this.cboUsuario.SelectedValue = (object)info.CustomerID;
                    this.cboVehicle.SelectedValue = (object)info.VehiculoID;

                }

                if (EmpresaCode == "3")
                {
                    this.cboSucursal.ItemsSource = null;
                    this.cboSucursal.ItemsSource = Locations.DepartamentosConsejo;

                    this.cboSupplierName.SelectedValue = (object)info.CompanyID;
                    this.cboSucursal.SelectedValue = (object)info.SucursalID;
               


                }

                if (EmpresaCode == "4")
                {
                    this.cboSucursal.ItemsSource = null;
                    this.cboSucursal.ItemsSource = Locations.Departamentos;

                    this.cboSupplierName.SelectedValue = (object)info.CompanyID;
                    this.cboSucursal.SelectedValue = (object)info.SucursalID;
                   
                }


            }
           else
            {
                info.Date = Convert.ToDateTime(this.dteFecha.SelectedValue);
            }


            //Location location = comboBox.DataContext as Location;
            //location.EmpresaCode = (string)comboBox.SelectedValue;//we submit the value immediately rather than waiting the cell to lose focus.
        }


        //private void cboSupplierName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //if (cboSupplierName.SelectedIndex >= 0)
        //    //{
        //    //    string strRFC = cboSupplierName.SelectedValue.ToString();
        //    //  //  intLinea = Convert.ToInt32(strLinea);

        //    //    var a = cboSupplierName.SelectedValuePath[1];
        //    //    var b = cboSupplierName.SelectionBoxItem;
        //    //    Supplier prov = cboSupplierName.SelectedItem as Supplier;

        //    //    this.txtRFC.Text = strRFC;
        //    //    info.Name = strRFC;
        //    //   // info.Address = prov.TradeName;
        //    //    //info.SupplierID = prov.SupplierID;
        //    //    //this.txtID.
        //    //}

        //}

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
                CustomerInfoEventArgs args = new CustomerInfoEventArgs();
                args.CustomerInformation = info;
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
         //   return true;

            if (cboSupplierName.SelectedIndex == -1)
            {
                MostrarMensajeError("Debe Seleccionar una Empresa Existente ");
                this.cboSupplierName.Focus();
                return false;
            }


            if (cboSupplierName.Text.Trim().Length == 0)
            {
                MostrarMensajeError("Debe Ingresar la Empresa");
                this.cboSupplierName.Focus();
                return false;
            }

            if (this.cboSucursal.SelectedIndex == -1)
            {
                MostrarMensajeError("Debe Seleccionar una Sucursal Existente ");
                this.cboSucursal.Focus();
                return false;
            }

            if (this.cboUsuario.SelectedIndex == -1)
            {
                MostrarMensajeError("Debe Seleccionar el Usuario/Cliente al que se le realizara el Servicio ");
                this.cboUsuario.Focus();
                return false;
            }

            if (this.cboVehicle.SelectedIndex == -1)
            {
                MostrarMensajeError("Debe Seleccionar el Vehiculo al que se le realizara el Servicio, sino esta dado de alta solicitarlo a Sistemas ");
                this.cboVehicle.Focus();
                return false;
            }

            if (this.dteFecha.SelectedValue == null)
            {
                MostrarMensajeError("Debe Seleccionar la Fecha de la orden ");
                this.dteFecha.Focus();
                return false;
            }


            return true;


            //if (this.invoiceNo.Text.Trim() == "")
            //{
            //    MostrarMensajeError("Debe Ingresar un Folio de Factura");
            //    this.invoiceNo.Focus();
            //    return false;
            //}


            //return true;

        }
        private void invoiceNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            //int value = 0;
            //if (int.TryParse(invoiceNo.Text, out value))
            //{
            //    invoiceNo.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 64, 81));
            //}
            //else
            //{

            //    invoiceNo.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 37, 37));
            //}
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void cboSupplierName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboSupplierName.SelectedIndex >= 0)
            {

                if (cboSupplierName.SelectedValue != null)
                {
                    string strKey = cboSupplierName.SelectedValue.ToString();
                    string strValue = cboSupplierName.Text.ToString();
                    // string strValue2 = cboSupplierName.SelectionBoxItem.ToString();
                    // string strValue2 = cboSupplierName.sele.ToString();
                    info.CompanyID = strKey;
                    //  info.Name = strValue;
                    info.InvoiceNumber = strValue + " - ";
                }
             
                //  intLinea = Convert.ToInt32(strLinea);
                //var a = cboSupplierName.SelectedValuePath[1];
                //var b = cboSupplierName.SelectionBoxItem;
                //Supplier prov = cboSupplierName.SelectedItem as Supplier;
                //this.txtRFC.Text = strRFC;
                //info.Name = strRFC;
                //info.Address = prov.TradeName;
                //info.SupplierID = prov.SupplierID;
                //this.txtID.
            }
        }

        private void cboSucursal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboSucursal.SelectedIndex >= 0)
            {

                if (cboSucursal.SelectedValue != null)
                {
                    string strKey = cboSucursal.SelectedValue.ToString();
                    string strValue = cboSucursal.Text.ToString();


                    info.SucursalID = strKey;
                    info.InvoiceNumber += strValue;

                }
                
            }

        }

        private void cboUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cboUsuario.SelectedIndex >= 0)
            {
                if (cboUsuario.SelectedValue != null)
                {
                    Customer cust = cboUsuario.SelectedItem as Customer;
                    info.CustomerID = cust.SupplierID;
                    info.Name = cust.RFC;
                }
                   
            }

           //     string strKey = cboUsuario.SelectedValue.ToString();
           // string strValue = cboUsuario.Text.ToString();
           // // string strValue2 = cboSupplierName.SelectionBoxItem.ToString();
           // // string strValue2 = cboSupplierName.sele.ToString();
           //// info. = Convert.ToInt32(strKey);
           // info.Name = strValue;
        }

        private void cboVehicle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          



            if (cboVehicle.SelectedIndex >= 0)
            {

                if (cboVehicle.SelectedIndex >= 0)
                {
                    Vehiculo car = cboVehicle.SelectedItem as Vehiculo;

                    //this.txtRFC.Text = strRFC;
                    //info.Name = strRFC;
                    //info.Address = prov.TradeName;
                    //info.VehiculoID = prov.SupplierID;

                    info.VehiculoID = car.VehiculoID;
                    info.Placas = car.Placas;
                    info.Vehiculo = car.Descripcion;
                }
                    //string strRFC = cboVehicle.SelectedValue.ToString();
                    ////  intLinea = Convert.ToInt32(strLinea);

                    //var a = cboVehicle.SelectedValuePath[1];
                    //var b = cboVehicle.SelectionBoxItem;
                    //var c = cboVehicle.SelectedValuePath[0];
                    //var d = cboVehicle.SelectedValuePath[2];

             



                //string strKey = cboUsuario.SelectedValue.ToString();
                //string strValue = cboUsuario.Text.ToString();
                //// string strValue2 = cboSupplierName.SelectionBoxItem.ToString();
                //// string strValue2 = cboSupplierName.sele.ToString();
                //// info. = Convert.ToInt32(strKey);
                //info.Name = strValue;
                //this.txtID.
            }

        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class CustomerInfoEventArgs : EventArgs
    {
        CustomerInformation m_billInfo2;
        public CustomerInformation CustomerInformation
        {
            get { return m_billInfo2; }
            set { m_billInfo2 = value; }
        }
      
    }



    public class Location : INotifyPropertyChanged
    {

        private string empresaCode;
        public string EmpresaCode
        {
            get
            {
                return this.empresaCode;
            }
            set
            {
                if (value != this.empresaCode)
                {
                    this.empresaCode = value;
                    this.OnPropertyChanged("EmpresaCode");
                    this.DepartamentoID = null;
                }
            }
        }
        private int? departamentoID;
        public int? DepartamentoID
        {
            get
            {
                return this.departamentoID;
            }
            set
            {
                this.departamentoID = value;
                this.OnPropertyChanged("DepartamentoID");
            }
        }

        //public IEnumerable<Departamento> AvailableDepartamentos
        //{
        //    //get
        //    //{
        //    //    return from c in Locations.Departamentos
        //    //           where c.EmpresaCode == this.EmpresaCode
        //    //           select c;
        //    //}
        //}

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }


}

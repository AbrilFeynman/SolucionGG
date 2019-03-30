using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;
namespace GGGC.Admin.AZ.PagareCentro
{
    /// <summary>
    /// Interaction logic for PagareCView.xaml
    /// </summary>
    public partial class PagareCView : UserControl
    {


        public static byte intEMPRESAID = 0;
        public static byte intSUCURSALID = 0;

        public const string appName = "GrupoGuadiana";
        public const string section = "Config";

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int increment;
        private delegate void NoArgDelegate();

        public string FolioFactura;
        public string Estado;
        public string Cantidad;
        public string Fecha;
        public string CantLetra;
        public string numPagare;
        public string Nombre;
        public string Direccion;
        public string Colonia;
        public string Ciudad;
        public string divedendo;
        public string FechaActual;
        public decimal flores;
        decimal ttal;
        string fechaofprint = DateTime.Now.ToString();

        byte bytNumeroDePagares = 0;


        public PagareCView()
        {
            intEMPRESAID = Convert.ToByte(GetSetting(appName, section, "EmpresaID", String.Empty));
            intSUCURSALID = Convert.ToByte(GetSetting(appName, section, "SucursalID", String.Empty));

            InitializeComponent();
            Dateprint.SelectedDate= DateTime.Now;
            btnImpr2.IsEnabled = false;
            btnImpr1.IsEnabled = false;
            btnImpr3.IsEnabled = false;
            btnImpr4.IsEnabled = false;
            btnImpr5.IsEnabled = false;


            //switch (intEMPRESAID)
            //{

            //    case 1:
            //        // get the current app style (theme and accent) from the application
            //        // you can then use the current theme and custom accent instead set a new theme
            //        Tuple<AppTheme, Accent> appStyle = MahApps.Metro.ThemeManager.DetectAppStyle(Application.Current);

            //        // now set the Green accent and dark theme

            //        MahApps.Metro.ThemeManager.ChangeAppStyle(Application.Current,
            //                                    MahApps.Metro.ThemeManager.GetAccent("Green"),
            //                                    MahApps.Metro.ThemeManager.GetAppTheme("BaseLight")); // or appStyle.Item1



            //        break;
            //    case 2:


            //        break;
            //    default:
            //        break;
        }



        public static void Refresh(DependencyObject obj)
        {

            obj.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle,

                (NoArgDelegate)delegate { });
        }


        private void btnImpr1_Click(object sender, RoutedEventArgs e)


        {

            fechaofprint = Dateprint.SelectedDate.ToString();

            radbusy.IsBusy = true;
            //btnImpr1.IsEnabled = false;
            //Refresh(btnImpr1);
            //Refresh(radbusy);

            increment = 0;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            string specifier;
            System.Globalization.CultureInfo culture;
            specifier = "C";
            culture = CultureInfo.CreateSpecificCulture("es-MX");

            FolioFactura = folioid.Text;
            Cantidad = flores.ToString(specifier, culture);
            Fecha = lblF1.Content.ToString();
            CantLetra = divedendo;
            numPagare = "1";
            string numpagares = bytNumeroDePagares.ToString();
            Nombre = lblCliente.Text.ToString();
            Direccion = lblDireccion.Text.ToString();
            Colonia = lblColonia.Text.ToString();
            Ciudad = lblCiudad.Text.ToString();
            //FechaActual = DateTime.Now;

            //RptPagare report = new RptPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad, Estado);
            RptPagare report = new RptPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad, Estado,fechaofprint);
            System.Drawing.Printing.PrinterSettings printerSettings
              = new System.Drawing.Printing.PrinterSettings();

            //--------- The standard print controller comes with no UI----------//
            System.Drawing.Printing.PrintController standardPrintController =
                new System.Drawing.Printing.StandardPrintController();

            // Print the report using the custom print controller
            Telerik.Reporting.Processing.ReportProcessor reportProcessor
                = new Telerik.Reporting.Processing.ReportProcessor();

            reportProcessor.PrintController = standardPrintController;

            Telerik.Reporting.TypeReportSource typeReportSource =
                new Telerik.Reporting.TypeReportSource();



            reportProcessor.PrintReport(report, printerSettings);

            //radbusy.IsBusy = false;
            //usando el preview
            // VerPagare mostrar = new VerPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad,Estado);




        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            increment++;
            timerlabel.Content = increment.ToString();
            //Refresh(this.radbusy);
            if (increment == 3)
            {
                dispatcherTimer.Stop();
                radbusy.IsBusy = false;
            }
            // Updating the Label which displays the current second


            // Forcing the CommandManager to raise the RequerySuggested event
            // CommandManager.InvalidateRequerySuggested();
        }
        private void btnImpr2_Click(object sender, RoutedEventArgs e)
        {
            fechaofprint = Dateprint.SelectedDate.ToString();

            radbusy.IsBusy = true;
            increment = 0;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();


            string specifier;
            System.Globalization.CultureInfo culture;
            specifier = "C";
            culture = CultureInfo.CreateSpecificCulture("en-US");

            FolioFactura = folioid.Text;
            Cantidad = flores.ToString(specifier, culture);
            Fecha = lblF2.Content.ToString();
            CantLetra = divedendo;
            numPagare = "2";
            string numpagares = bytNumeroDePagares.ToString();
            Nombre = lblCliente.Text.ToString();
            Direccion = lblDireccion.Text.ToString();
            Colonia = lblColonia.Text.ToString();
            Ciudad = lblCiudad.Text.ToString();
            //FechaActual = DateTime.Now;
            //VerPagare mostrar = new VerPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad,Estado);

            //RptPagare report = new RptPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad, Estado);
            RptPagare report = new RptPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad, Estado, fechaofprint);
            System.Drawing.Printing.PrinterSettings printerSettings
              = new System.Drawing.Printing.PrinterSettings();

            //--------- The standard print controller comes with no UI----------//
            System.Drawing.Printing.PrintController standardPrintController =
                new System.Drawing.Printing.StandardPrintController();

            // Print the report using the custom print controller
            Telerik.Reporting.Processing.ReportProcessor reportProcessor
                = new Telerik.Reporting.Processing.ReportProcessor();

            reportProcessor.PrintController = standardPrintController;

            Telerik.Reporting.TypeReportSource typeReportSource =
                new Telerik.Reporting.TypeReportSource();



            reportProcessor.PrintReport(report, printerSettings);




        }
        private void btnImpr3_Click(object sender, RoutedEventArgs e)
        {
            fechaofprint = Dateprint.SelectedDate.ToString();


            radbusy.IsBusy = true;
            increment = 0;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();



            string specifier;
            System.Globalization.CultureInfo culture;
            specifier = "C";
            culture = CultureInfo.CreateSpecificCulture("en-US");

            FolioFactura = folioid.Text;
            Cantidad = flores.ToString(specifier, culture);
            Fecha = lblF3.Content.ToString();
            CantLetra = divedendo;
            numPagare = "3";
            string numpagares = bytNumeroDePagares.ToString();
            Nombre = lblCliente.Text.ToString();
            Direccion = lblDireccion.Text.ToString();
            Colonia = lblColonia.Text.ToString();
            Ciudad = lblCiudad.Text.ToString();
            //FechaActual = DateTime.Now;
            //VerPagare mostrar = new VerPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad,Estado);
            //RptPagare report = new RptPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad, Estado);
            RptPagare report = new RptPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad, Estado,fechaofprint);
            System.Drawing.Printing.PrinterSettings printerSettings
              = new System.Drawing.Printing.PrinterSettings();

            //--------- The standard print controller comes with no UI----------//
            System.Drawing.Printing.PrintController standardPrintController =
                new System.Drawing.Printing.StandardPrintController();

            // Print the report using the custom print controller
            Telerik.Reporting.Processing.ReportProcessor reportProcessor
                = new Telerik.Reporting.Processing.ReportProcessor();

            reportProcessor.PrintController = standardPrintController;

            Telerik.Reporting.TypeReportSource typeReportSource =
                new Telerik.Reporting.TypeReportSource();



            reportProcessor.PrintReport(report, printerSettings);


        }
        private void btnImpr4_Click(object sender, RoutedEventArgs e)
        {
            fechaofprint = Dateprint.SelectedDate.ToString();


            radbusy.IsBusy = true;
            increment = 0;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();



            string specifier;
            System.Globalization.CultureInfo culture;
            specifier = "C";
            culture = CultureInfo.CreateSpecificCulture("en-US");

            FolioFactura = folioid.Text;
            Cantidad = flores.ToString(specifier, culture);
            Fecha = lblF4.Content.ToString();
            CantLetra = divedendo;
            numPagare = "4";
            string numpagares = bytNumeroDePagares.ToString();
            Nombre = lblCliente.Text.ToString();
            Direccion = lblDireccion.Text.ToString();
            Colonia = lblColonia.Text.ToString();
            Ciudad = lblCiudad.Text.ToString();
            //FechaActual = DateTime.Now;
            // VerPagare mostrar = new VerPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad,Estado);
            //RptPagare report = new RptPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad, Estado);
            RptPagare report = new RptPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad, Estado,fechaofprint);
            System.Drawing.Printing.PrinterSettings printerSettings
              = new System.Drawing.Printing.PrinterSettings();

            //--------- The standard print controller comes with no UI----------//
            System.Drawing.Printing.PrintController standardPrintController =
                new System.Drawing.Printing.StandardPrintController();

            // Print the report using the custom print controller
            Telerik.Reporting.Processing.ReportProcessor reportProcessor
                = new Telerik.Reporting.Processing.ReportProcessor();

            reportProcessor.PrintController = standardPrintController;

            Telerik.Reporting.TypeReportSource typeReportSource =
                new Telerik.Reporting.TypeReportSource();



            reportProcessor.PrintReport(report, printerSettings);



        }
        private void btnImpr5_Click(object sender, RoutedEventArgs e)
        {
            fechaofprint = Dateprint.SelectedDate.ToString();


            radbusy.IsBusy = true;
            increment = 0;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();



            string specifier;
            System.Globalization.CultureInfo culture;
            specifier = "C";
            culture = CultureInfo.CreateSpecificCulture("en-US");

            FolioFactura = folioid.Text;
            Cantidad = flores.ToString(specifier, culture);
            Fecha = lblF5.Content.ToString();
            CantLetra = divedendo;
            numPagare = "5";
            string numpagares = bytNumeroDePagares.ToString();
            Nombre = lblCliente.Text.ToString();
            Direccion = lblDireccion.Text.ToString();
            Colonia = lblColonia.Text.ToString();
            Ciudad = lblCiudad.Text.ToString();
            //FechaActual = DateTime.Now;
            // VerPagare mostrar = new VerPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad,Estado);
            //RptPagare report = new RptPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad, Estado);
            RptPagare report = new RptPagare(FolioFactura, Cantidad, Fecha, CantLetra, numPagare, numpagares, Nombre, Direccion, Colonia, Ciudad, Estado,fechaofprint);
            System.Drawing.Printing.PrinterSettings printerSettings
              = new System.Drawing.Printing.PrinterSettings();

            //--------- The standard print controller comes with no UI----------//
            System.Drawing.Printing.PrintController standardPrintController =
                new System.Drawing.Printing.StandardPrintController();

            // Print the report using the custom print controller
            Telerik.Reporting.Processing.ReportProcessor reportProcessor
                = new Telerik.Reporting.Processing.ReportProcessor();

            reportProcessor.PrintController = standardPrintController;

            Telerik.Reporting.TypeReportSource typeReportSource =
                new Telerik.Reporting.TypeReportSource();



            reportProcessor.PrintReport(report, printerSettings);


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(folioid.Text))
            {

            }
            else
            {
                cargarFacturas();

            }

        }
        static string GetOrigen()
        {
            string conexion;

            string vpn = "B6";
            string server = getvpn(vpn);
            conexion = "SERVER = " + server + "; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
            return conexion;

        }

        static string getvpn(string b)
        {
            try
            {

                string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
                SqlConnection sqlcon = new SqlConnection(connectionStringer);
                sqlcon.Open();
                string consulta = "Select Address from CfnVentasGeneral where Base = '" + b + "' ";

                SqlDataAdapter sda = new SqlDataAdapter(consulta, sqlcon);

                DataSet dsPubs = new DataSet("Pubs");

                sda.Fill(dsPubs, "CfnVentasGeneral");

                DataTable dtbla = new DataTable();

                dtbla = dsPubs.Tables["CfnVentasGeneral"];

                string vpn = dtbla.Rows[0][0].ToString();

                sqlcon.Close();

                return vpn;
            }
            catch (Exception z)
            {

                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Error al validar ." + z.Message.ToString(),
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }

            return "";
        }


        public void cargarFacturas()

        {

            try
            {


                Conversion c = new Conversion();

                SqlConnection cn;
                SqlCommand cmd;
                SqlDataReader dr;


                cn = new SqlConnection(GetOrigen());
                cn.Open();
                string id = this.folioid.Text;

                cmd = new SqlCommand("Select dbo.Facturas_Y_Devoluciones.Folio_Del_Documento, dbo.Facturas_Y_Devoluciones.Fecha_Del_Documento, dbo.Facturas_Y_Devoluciones.Total, dbo.Facturas_Y_Devoluciones.Ciudad,dbo.Facturas_Y_Devoluciones.Estado, dbo.Facturas_Y_Devoluciones.Nombre, dbo.Facturas_Y_Devoluciones.Direccion, dbo.Facturas_Y_Devoluciones.Colonia, dbo.Condiciones_De_Pago.Codigo_De_Condicion_De_Pago As CodigoDePago, dbo.Condiciones_De_Pago.Descripcion As Descripcion, dbo.Condiciones_De_Pago.Ordenamiento As Ordenamiento From dbo.Facturas_Y_Devoluciones LEFT OUTER JOIN dbo.Condiciones_De_Pago on dbo.Facturas_Y_Devoluciones.Codigo_De_Condiciones_De_Pago = dbo.Condiciones_De_Pago.Codigo_De_Condicion_De_Pago where dbo.Facturas_Y_Devoluciones.Folio_Del_Documento ='" + id + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())

                {
                    lblCliente.Text = dr["Nombre"].ToString();
                    Estado = dr["Estado"].ToString();
                    lblDireccion.Text = dr["Direccion"].ToString();
                    lblCiudad.Text = dr["Ciudad"].ToString();
                    lblColonia.Text = dr["Colonia"].ToString();
                    //DateTime deit = Convert.ToDateTime(tblDetalle.Rows[0][14]);
                    //txtFecha.Value = deit.ToString("dd/MMMM/yyyy").ToUpper();
                    DateTime deit = Convert.ToDateTime(dr["Fecha_Del_Documento"]);
                    lblFecha.Text = deit.ToString("dd/MMMM/yyyy").ToUpper();
                    Dateprint.SelectedDate = deit;
                    string specifierr;
                    System.Globalization.CultureInfo culturer;
                    specifierr = "C";
                    culturer = CultureInfo.CreateSpecificCulture("en-US");
                    ttal = Convert.ToDecimal(dr["Total"]);
                    label20.Text = ttal.ToString(specifierr, culturer);


                    label18.Text = c.enletras(dr["Total"].ToString()).ToUpper();


                    lblcond.Text = dr["Descripcion"].ToString().ToUpper();
                    byte estrella = Convert.ToByte(dr["CodigoDePago"]);
                    string cnj = estrella.ToString();
                    string date = dr["Fecha_Del_Documento"].ToString();
                    DateTime dat = Convert.ToDateTime(date);


                    byte bytCondicionDePago = estrella;




                    bytNumeroDePagares = fncNumeroDePagares(bytCondicionDePago);

                    switch (bytNumeroDePagares)
                    {
                        case 0:



                            {
                                //btnImpr1.Visibility = Visibility.Hidden;
                                // lbl666.Content = "KRISHNA";
                                break;
                            }

                        case 1:


                            {
                                string specifier;
                                System.Globalization.CultureInfo culture;
                                specifier = "C";
                                culture = CultureInfo.CreateSpecificCulture("en-US");

                                txtP2.Visibility = Visibility.Hidden;
                                txtP2.Content = "";

                                label21.Text = "1 Pagaré";
                                label8.Text = "Pagaré 1";
                                label9.Text = "Días";
                                label11.Text = "";
                                label13.Text = "";
                                labelD4.Text = "";
                                labelD5.Text = "";
                                label10.Text = "";
                                label12.Text = "";
                                lblpa4.Text = "";
                                lblpa5.Text = "";



                                labelD4.Text = "";
                                labelD5.Text = "";


                                btnImpr1.Visibility = Visibility.Visible;
                                btnImpr1.IsEnabled = true;
                                btnImpr2.Visibility = Visibility.Hidden;
                                btnImpr3.Visibility = Visibility.Hidden;
                                btnImpr4.Visibility = Visibility.Hidden;
                                btnImpr5.Visibility = Visibility.Hidden;

                                decimal flor = Convert.ToDecimal(ttal);



                                decimal lblpa1 = flor / 1;


                                flores = flor;
                                divedendo = c.enletras(flores.ToString()).ToUpper();

                                txtP4.Visibility = Visibility.Hidden;
                                txtP5.Visibility = Visibility.Hidden;
                                labelD4.Visibility = Visibility.Hidden;
                                labelD5.Visibility = Visibility.Hidden;

                                //  label23.Visibility = Visibility.Hidden;
                                // label24.Visibility = Visibility.Hidden;
                                label10.Text = "";

                                label12.Text = "";
                                //txtP2.Visibility = Visibility.Hidden;
                                txtP3.Visibility = Visibility.Hidden;


                                lblF3.Visibility = Visibility.Hidden;


                                lblF2.Visibility = Visibility.Hidden;
                                lblF4.Visibility = Visibility.Hidden;
                                lblF5.Visibility = Visibility.Hidden;
                                // label244.Visibility = Visibility.Hidden;

                                //lblpa44.Visibility = Visibility.Hidden;
                                //lblx.Content = "sola : " + Environment.NewLine + "oso pardo";
                                // lblx.Content = "Pagaré 1: " + flores.ToString(specifier, culture);
                                lblPagare1.Text = flores.ToString(specifier, culture);
                                // lblPagareLetra.Content = flores.ToString();
                                // lblPagare2.Content = "Pagaré 2: " + flores.ToString(specifier, culture);
                                // lblPagare3.Content = "Pagaré 3: " + flores.ToString(specifier, culture);
                                // lblPagare4.Content = "Pagaré 4: " + flores.ToString(specifier, culture);

                                //lbl666.Content = "";

                                lblPagare2.Visibility = Visibility.Collapsed;
                                lblPagare3.Visibility = Visibility.Collapsed;
                                lblPagare4.Visibility = Visibility.Collapsed;
                                lblPagare5.Visibility = Visibility.Collapsed;
                                break;
                            }
                        case 2:

                            {
                                string specifier;
                                System.Globalization.CultureInfo culture;
                                specifier = "C";
                                culture = CultureInfo.CreateSpecificCulture("en-US");


                                txtP2.Visibility = Visibility.Visible;
                                lblF2.Visibility = Visibility.Visible;

                                decimal flor = Convert.ToDecimal(ttal);



                                flores = flor / 2;
                                divedendo = c.enletras(flores.ToString()).ToUpper();
                                // lblcond.Content = "Crédito 15-30 días";
                                label21.Text = "2 Pagarés:";

                                btnImpr1.Visibility = Visibility.Visible;
                                btnImpr1.IsEnabled = true;
                                btnImpr2.IsEnabled = true;
                                btnImpr2.Visibility = Visibility.Visible;
                                btnImpr3.Visibility = Visibility.Hidden;
                                btnImpr4.Visibility = Visibility.Hidden;
                                btnImpr5.Visibility = Visibility.Hidden;

                                label9.Text = "Días";
                                label11.Text = "Días";
                                label13.Text = "";
                                labelD4.Text = "";
                                labelD5.Text = "";
                                label8.Text = "Pagaré 1";
                                label10.Text = "Pagaré 2";
                                label12.Text = "";
                                lblpa4.Text = "";
                                lblpa5.Text = "";
                                txtP4.Visibility = Visibility.Hidden;
                                txtP5.Visibility = Visibility.Hidden;
                                lblF4.Visibility = Visibility.Hidden;
                                lblF5.Visibility = Visibility.Hidden;
                                labelD4.Visibility = Visibility.Hidden;
                                labelD5.Visibility = Visibility.Hidden;

                                // label24.Visibility = Visibility.Hidden;

                                label12.Text = "";
                                //lblpa44.Visibility = Visibility.Hidden;
                                txtP3.Visibility = Visibility.Hidden;

                                // label244.Visibility = Visibility.Hidden;
                                lblF3.Visibility = Visibility.Hidden;
                                //lblpa55.Visibility = Visibility.Hidden;
                                // label248.Visibility = Visibility.Hidden;
                                // lblx.Content = "Pagaré 1: " + flores.ToString(specifier, culture) + Environment.NewLine + "Pagaré 2: " + flores.ToString(specifier, culture);

                                lblPagare1.Text = flores.ToString(specifier, culture);
                                //lblPagareLetra.Content = flores.ToString();
                                lblPagare2.Text = flores.ToString(specifier, culture);
                                //lblPagare3.Content = "Pagaré 3: " + flores.ToString(specifier, culture);
                                // lblPagare4.Content = "Pagaré 4: " + flores.ToString(specifier, culture);
                                lblPagare1.Visibility = Visibility.Visible;
                                lblPagare2.Visibility = Visibility.Visible;
                                lblPagare3.Visibility = Visibility.Collapsed;
                                lblPagare4.Visibility = Visibility.Collapsed;
                                lblPagare5.Visibility = Visibility.Collapsed;

                                break;

                            }

                        case 3:

                            {

                                string specifier;
                                System.Globalization.CultureInfo culture;
                                specifier = "C";
                                culture = CultureInfo.CreateSpecificCulture("en-US");

                                txtP1.Visibility = Visibility.Visible;
                                txtP2.Visibility = Visibility.Visible;
                                txtP3.Visibility = Visibility.Visible;
                                lblF2.Visibility = Visibility.Visible;
                                lblF3.Visibility = Visibility.Visible;
                                // txtP1.Text = dr["Pagare1D"].ToString();
                                // txtP2.Text = dr["Pagare2D"].ToString();
                                //  txtP3.Text = dr["Pagare3D"].ToString();
                                txtP4.Visibility = Visibility.Hidden;
                                txtP5.Visibility = Visibility.Hidden;
                                decimal flor = Convert.ToDecimal(ttal);



                                btnImpr1.Visibility = Visibility.Visible;
                                btnImpr1.IsEnabled = true;
                                btnImpr2.IsEnabled = true;
                                btnImpr3.IsEnabled = true;
                                btnImpr2.Visibility = Visibility.Visible;
                                btnImpr3.Visibility = Visibility.Visible;
                                btnImpr4.Visibility = Visibility.Hidden;
                                btnImpr5.Visibility = Visibility.Hidden;
                                //lblpa1.Content = flor / 3;
                                //lblpa3.Content = flor / 3;
                                flores = flor / 3;
                                divedendo = c.enletras(flores.ToString()).ToUpper();
                                label8.Text = "Pagaré 1";
                                label10.Text = "Pagaré 2";
                                label12.Text = "Pagaré 3";
                                lblpa4.Text = "";
                                lblpa5.Text = "";
                                // lblcond.Content = "Crédito 15-30-45 días";
                                label21.Text = "3 Pagarés:";

                                // lblpa2.Content = flor / 3;
                                //label23.Content = "Pagaré 2:";
                                //label24.Content = "Pagaré 3";
                                label9.Text = "Días";
                                label11.Text = "Días";
                                label13.Text = "Días";
                                labelD4.Text = "";
                                labelD5.Text = "";



                                lblF4.Visibility = Visibility.Hidden;
                                lblF5.Visibility = Visibility.Hidden;
                                labelD4.Visibility = Visibility.Hidden;
                                labelD5.Visibility = Visibility.Hidden;
                                // label244.Visibility = Visibility.Hidden;
                                //lblpa55.Visibility = Visibility.Hidden;
                                ////label248.Visibility = Visibility.Hidden;
                                //lblpa44.Visibility = Visibility.Hidden;
                                //lblx.Content = "Pagaré 1: " + flores.ToString(specifier, culture) + Environment.NewLine + "Pagaré 2: " + flores.ToString(specifier, culture) + Environment.NewLine + "Pagaré 3: " + flores.ToString(specifier, culture);

                                lblPagare1.Text = flores.ToString(specifier, culture);
                                //lblPagareLetra.Content = flores.ToString();
                                lblPagare2.Text = flores.ToString(specifier, culture);
                                lblPagare3.Text = flores.ToString(specifier, culture);
                                // lblPagare4.Content = "Pagaré 4: " + flores.ToString(specifier, culture);

                                lblPagare1.Visibility = Visibility.Visible;
                                lblPagare2.Visibility = Visibility.Visible;
                                lblPagare3.Visibility = Visibility.Visible;
                                lblPagare4.Visibility = Visibility.Collapsed;
                                lblPagare5.Visibility = Visibility.Collapsed;

                                break;
                            }
                        case 4:

                            {
                                string specifier;
                                System.Globalization.CultureInfo culture;
                                specifier = "C";
                                culture = CultureInfo.CreateSpecificCulture("en-US");
                                // rbt3.IsChecked = true;
                                // lblcond.Content = "Crédito 30 días";
                                // rdb3.IsChecked = true;
                                txtP1.Visibility = Visibility.Visible;
                                txtP2.Visibility = Visibility.Visible;
                                txtP3.Visibility = Visibility.Visible;
                                txtP4.Visibility = Visibility.Visible;
                                lblF2.Visibility = Visibility.Visible;
                                lblF3.Visibility = Visibility.Visible;
                                lblF4.Visibility = Visibility.Visible;

                                //txtP1.Text = dr["Pagare1D"].ToString();
                                //txtP2.Text = dr["Pagare2D"].ToString();
                                //txtP3.Text = dr["Pagare3D"].ToString();
                                //txtP4.Text = dr["Pagare3D"].ToString();
                                // label20.Text = dr["Total"].ToString();

                                btnImpr1.Visibility = Visibility.Visible;
                                btnImpr1.IsEnabled = true;
                                btnImpr2.IsEnabled = true;
                                btnImpr3.IsEnabled = true;
                                btnImpr4.IsEnabled = true;
                                btnImpr2.Visibility = Visibility.Visible;
                                btnImpr3.Visibility = Visibility.Visible;
                                btnImpr4.Visibility = Visibility.Visible;
                                btnImpr5.Visibility = Visibility.Hidden;
                                label9.Text = "Días";
                                label11.Text = "Días";
                                label13.Text = "Días";
                                labelD4.Text = "Días";
                                labelD5.Text = "";
                                decimal flor = Convert.ToDecimal(ttal);

                                label8.Text = "Pagaré 1";
                                label10.Text = "Pagaré 2";
                                label12.Text = "Pagaré 3";
                                lblpa4.Text = "Pagaré 4";
                                lblpa5.Text = "";
                                // lblpa1.Content = flor / 4;
                                flores = flor / 4;
                                divedendo = c.enletras(flores.ToString()).ToUpper();
                                // lblcond.Content = "Crédito 15-30 días";
                                label21.Text = "4 Pagarés:";

                                //lblpa2.Content = flor / 4;
                                //lblpa3.Content = flor / 4;
                                //lblpa44.Content = flor / 4;
                                lblpa5.Visibility = Visibility.Hidden;

                                txtP5.Visibility = Visibility.Hidden;

                                lblF5.Visibility = Visibility.Hidden;

                                labelD5.Visibility = Visibility.Hidden;
                                //label244.Visibility = Visibility.Hidden;

                                //lblpa55.Visibility = Visibility.Hidden;

                                lblPagare1.Text = flores.ToString(specifier, culture);
                                //lblPagareLetra.Content = flores.ToString();
                                lblPagare2.Text = flores.ToString(specifier, culture);
                                lblPagare3.Text = flores.ToString(specifier, culture);
                                lblPagare4.Text = flores.ToString(specifier, culture);

                                lblPagare1.Visibility = Visibility.Visible;
                                lblPagare2.Visibility = Visibility.Visible;
                                lblPagare3.Visibility = Visibility.Visible;
                                lblPagare4.Visibility = Visibility.Visible;
                                lblPagare5.Visibility = Visibility.Collapsed;

                                break;
                            }

                        case 5:
                            {
                                string specifier;
                                System.Globalization.CultureInfo culture;
                                specifier = "C";
                                culture = CultureInfo.CreateSpecificCulture("en-US");


                                txtP1.Visibility = Visibility.Visible;
                                txtP2.Visibility = Visibility.Visible;
                                txtP3.Visibility = Visibility.Visible;
                                txtP4.Visibility = Visibility.Visible;
                                txtP5.Visibility = Visibility.Visible;
                                label13.Visibility = Visibility.Visible;
                                lblF2.Visibility = Visibility.Visible;
                                lblF3.Visibility = Visibility.Visible;
                                lblF4.Visibility = Visibility.Visible;
                                lblF5.Visibility = Visibility.Visible;
                                label9.Text = "Días";
                                label11.Text = "Días";
                                label13.Text = "Días";
                                labelD4.Text = "Días";
                                labelD5.Text = "Días";
                                //txtP1.Text = dr["Pagare1D"].ToString();
                                //txtP2.Text = dr["Pagare2D"].ToString();
                                //txtP3.Text = dr["Pagare3D"].ToString();
                                //txtP4.Text = dr["Pagare3D"].ToString();
                                //txtP5.Text = dr["Pagare3D"].ToString();
                                label8.Text = "Pagaré 1";
                                label10.Text = "Pagaré 2";
                                label12.Text = "Pagaré 3";
                                lblpa4.Text = "Pagaré 4";
                                lblpa5.Text = "Pagaré 5";
                                decimal flor = Convert.ToDecimal(ttal);

                                flores = flor / 5;
                                divedendo = c.enletras(flores.ToString()).ToUpper();

                                // lblcond.Content = "Crédito 15-30-45 días";
                                label21.Text = "5 Pagarés:";
                                //rbt4.IsChecked = true;
                                btnImpr1.Visibility = Visibility.Visible;
                                btnImpr1.IsEnabled = true;
                                btnImpr2.IsEnabled = true;
                                btnImpr3.IsEnabled = true;
                                btnImpr4.IsEnabled = true;
                                btnImpr5.IsEnabled = true;
                                btnImpr2.Visibility = Visibility.Visible;
                                btnImpr3.Visibility = Visibility.Visible;
                                btnImpr4.Visibility = Visibility.Visible;
                                btnImpr5.Visibility = Visibility.Visible;

                                //lblx.Content = "Pagaré 1: " + flores.ToString("C") + Environment.NewLine + "Pagaré 2: " + flores.ToString(specifier, culture) + Environment.NewLine + "Pagaré 3: " + flores.ToString(specifier, culture) + Environment.NewLine + "Pagaré 4: " + flores.ToString(specifier, culture) + Environment.NewLine + "Pagaré 5: " + flores.ToString(specifier, culture);
                                lblPagare1.Text = flores.ToString(specifier, culture);
                                //lblPagareLetra.Content = flores.ToString();
                                lblPagare2.Text = flores.ToString(specifier, culture);
                                lblPagare3.Text = flores.ToString(specifier, culture);
                                lblPagare4.Text = flores.ToString(specifier, culture);
                                lblPagare5.Text = flores.ToString(specifier, culture);

                                lblPagare1.Visibility = Visibility.Visible;
                                lblPagare2.Visibility = Visibility.Visible;
                                lblPagare3.Visibility = Visibility.Visible;
                                lblPagare4.Visibility = Visibility.Visible;
                                lblPagare5.Visibility = Visibility.Visible;
                                lblpa5.Visibility = Visibility.Visible;
                                labelD5.Visibility = Visibility.Visible;


                                break;
                            }

                            // lblPagare1.Content = flores2.ToString();
                            // flores2 = Convert.ToDecimal(lblPagare1.Content);




                    }

                    //this.lblMostrarLetras.Content = c.enletras(lblPagareLetra.Content.ToString()).ToUpper();
                    if (cnj == "0")
                    {
                        // string date = dr["FechaFactura"].ToString();
                        //  DateTime dat = Convert.ToDateTime(date);
                        dat = dat.AddDays(1);
                        txtP1.Content = "1";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                        btnImpr1.IsEnabled = false;
                    }
                    if (cnj == "1")
                    {
                        // string date = dr["FechaFactura"].ToString();
                        //  DateTime dat = Convert.ToDateTime(date);
                        dat = dat.AddDays(1);
                        txtP1.Content = "1";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "3")
                    {
                        // string date = dr["FechaFactura"].ToString();
                        // DateTime dat = Convert.ToDateTime(date);
                        dat = dat.AddDays(15);
                        txtP1.Content = "15";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "2")
                    {
                        // string date = dr["FechaFactura"].ToString();
                        // DateTime dat = Convert.ToDateTime(date);
                        dat = dat.AddDays(10);
                        txtP1.Content = "10";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "4")
                    {
                        // string date = dr["FechaFactura"].ToString();
                        // DateTime dat = Convert.ToDateTime(date);
                        dat = dat.AddDays(20);
                        txtP1.Content = "20";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "5")
                    {

                        dat = dat.AddDays(25);
                        txtP1.Content = "25";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "6")
                    {

                        dat = dat.AddDays(30);
                        txtP1.Content = "30";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "7")
                    {

                        dat = dat.AddDays(45);
                        txtP1.Content = "45";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "8")
                    {

                        dat = dat.AddDays(60);
                        txtP1.Content = "60";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "9")
                    {

                        dat = dat.AddDays(3);
                        txtP1.Content = "3";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }

                    else if (cnj == "12")
                    {

                        dat = dat.AddDays(8);
                        txtP1.Content = "8";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "13")
                    {

                        dat = dat.AddDays(0);
                        txtP1.Content = "0";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "14")
                    {

                        dat = dat.AddDays(30);
                        txtP1.Content = "30";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "22")
                    {

                        dat = dat.AddDays(1);
                        txtP1.Content = "1";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "24")
                    {

                        dat = dat.AddDays(180);
                        txtP1.Content = "180";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "26")
                    {

                        dat = dat.AddDays(40);
                        txtP1.Content = "40";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "31")
                    {

                        dat = dat.AddDays(90);
                        txtP1.Content = "90";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }


                    else if (cnj == "32")
                    {

                        dat = dat.AddDays(150);
                        txtP1.Content = "150";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "33")
                    {

                        dat = dat.AddDays(730);
                        txtP1.Content = "730";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "34")
                    {

                        dat = dat.AddDays(210);
                        txtP1.Content = "210";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "35")
                    {

                        dat = dat.AddDays(300);
                        txtP1.Content = "300";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }

                    else if (cnj == "36")
                    {

                        dat = dat.AddDays(180);
                        txtP1.Content = "180";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "37")
                    {

                        dat = dat.AddDays(240);
                        txtP1.Content = "240";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }
                    else if (cnj == "38")
                    {

                        dat = dat.AddDays(5);
                        txtP1.Content = "5";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                    }

                    else if (cnj == "17")
                    {

                        dat = dat.AddDays(30);
                        DateTime fe2 = dat.AddDays(30);
                        txtP1.Content = "30";
                        txtP2.Content = "30";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                        lblF2.Content = fncFechaDeVencimiento(fe2);
                    }
                    else if (cnj == "19")
                    {

                        dat = dat.AddDays(22);
                        DateTime fe2 = dat.AddDays(22);
                        txtP1.Content = "22";
                        txtP2.Content = "22";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                        lblF2.Content = fncFechaDeVencimiento(fe2);
                    }
                    else if (cnj == "27")
                    {

                        dat = dat.AddDays(25);
                        DateTime fe2 = dat.AddDays(25);
                        txtP1.Content = "25";
                        txtP2.Content = "25";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                        lblF2.Content = fncFechaDeVencimiento(fe2);
                    }
                    else if (cnj == "18")
                    {
                        lblF2.Visibility = Visibility.Visible;

                        dat = dat.AddDays(30);
                        DateTime fe2 = dat.AddDays(25);
                        DateTime fe3 = fe2.AddDays(15);
                        txtP1.Content = "30";
                        txtP2.Content = "25";
                        txtP3.Content = "15";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                        lblF2.Content = fncFechaDeVencimiento(fe2);
                        lblF3.Content = fncFechaDeVencimiento(fe3);
                    }
                    else if (cnj == "28")
                    {
                        dat = dat.AddDays(30);
                        DateTime fe2 = dat.AddDays(25);
                        DateTime fe3 = fe2.AddDays(15);
                        txtP1.Content = "30";
                        txtP2.Content = "25";
                        txtP3.Content = "15";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                        lblF2.Content = fncFechaDeVencimiento(fe2);
                        lblF3.Content = fncFechaDeVencimiento(fe3);
                    }

                    else if (cnj == "30")
                    {

                        dat = dat.AddDays(30);
                        DateTime fe2 = dat.AddDays(25);
                        DateTime fe3 = fe2.AddDays(15);
                        txtP1.Content = "30";
                        txtP2.Content = "25";
                        txtP3.Content = "15";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                        lblF2.Content = fncFechaDeVencimiento(fe2);
                        lblF3.Content = fncFechaDeVencimiento(fe3);
                    }
                    else if (cnj == "43")
                    {

                        dat = dat.AddDays(30);
                        DateTime fe2 = dat.AddDays(25);
                        DateTime fe3 = fe2.AddDays(15);
                        txtP1.Content = "30";
                        txtP2.Content = "25";
                        txtP3.Content = "15";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                        lblF2.Content = fncFechaDeVencimiento(fe2);
                        lblF3.Content = fncFechaDeVencimiento(fe3);
                    }
                    else if (cnj == "11")
                    {

                        dat = dat.AddDays(20);
                        DateTime fe2 = dat.AddDays(20);
                        DateTime fe3 = fe2.AddDays(20);
                        DateTime fe4 = fe3.AddDays(20);
                        txtP1.Content = "20";
                        txtP2.Content = "20";
                        txtP3.Content = "20";
                        txtP4.Content = "20";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                        lblF2.Content = fncFechaDeVencimiento(fe2);
                        lblF3.Content = fncFechaDeVencimiento(fe3);
                        lblF4.Content = fncFechaDeVencimiento(fe4);
                    }
                    else if (cnj == "29")
                    {

                        dat = dat.AddDays(30);
                        DateTime fe2 = dat.AddDays(30);
                        DateTime fe3 = fe2.AddDays(30);
                        DateTime fe4 = fe3.AddDays(30);
                        txtP1.Content = "30";
                        txtP2.Content = "30";
                        txtP3.Content = "30";
                        txtP4.Content = "30";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                        lblF2.Content = fncFechaDeVencimiento(fe2);
                        lblF3.Content = fncFechaDeVencimiento(fe3);
                        lblF4.Content = fncFechaDeVencimiento(fe4);
                    }
                    else if (cnj == "40")
                    {

                        dat = dat.AddDays(30);
                        DateTime fe2 = dat.AddDays(30);
                        DateTime fe3 = fe2.AddDays(30);
                        DateTime fe4 = fe3.AddDays(30);
                        txtP1.Content = "30";
                        txtP2.Content = "30";
                        txtP3.Content = "30";
                        txtP4.Content = "30";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                        lblF2.Content = fncFechaDeVencimiento(fe2);
                        lblF3.Content = fncFechaDeVencimiento(fe3);
                        lblF4.Content = fncFechaDeVencimiento(fe4);
                    }
                    else if (cnj == "45")
                    {

                        dat = dat.AddDays(30);
                        DateTime fe2 = dat.AddDays(30);
                        DateTime fe3 = fe2.AddDays(30);
                        DateTime fe4 = fe3.AddDays(30);
                        txtP1.Content = "30";
                        txtP2.Content = "30";
                        txtP3.Content = "30";
                        txtP4.Content = "30";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                        lblF2.Content = fncFechaDeVencimiento(fe2);
                        lblF3.Content = fncFechaDeVencimiento(fe3);
                        lblF4.Content = fncFechaDeVencimiento(fe4);
                    }

                    else if (cnj == "41")
                    {

                        dat = dat.AddDays(30);
                        DateTime fe2 = dat.AddDays(30);
                        DateTime fe3 = fe2.AddDays(30);
                        DateTime fe4 = fe3.AddDays(30);
                        DateTime fe5 = fe4.AddDays(30);
                        txtP1.Content = "30";
                        txtP2.Content = "30";
                        txtP3.Content = "30";
                        txtP4.Content = "30";
                        txtP5.Content = "30";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                        lblF2.Content = fncFechaDeVencimiento(fe2);
                        lblF3.Content = fncFechaDeVencimiento(fe3);
                        lblF4.Content = fncFechaDeVencimiento(fe4);
                        lblF5.Content = fncFechaDeVencimiento(fe5);

                        //dat = dat.AddDays(30);
                        //DateTime fe2 = dat.AddDays(30);
                        //DateTime fe3 = fe2.AddDays(30);
                        //DateTime fe4 = fe3.AddDays(30);
                        //DateTime fe5 = fe4.AddDays(30);
                        //txtP1.Text = "30";
                        //txtP2.Text = "30";
                        //txtP3.Text = "30";
                        //txtP4.Text = "30";
                        //txtP5.Text = "30";
                        //lblF1.Content = dat.ToString("dd MMMM yyyy").ToUpper();
                        //lblF2.Content = fe2.ToString("dd MMMM yyyy").ToUpper();
                        //lblF3.Content = fe3.ToString("dd MMMM yyyy").ToUpper();
                        //lblF4.Content = fe4.ToString("dd MMMM yyyy").ToUpper();
                        //lblF5.Content = fe5.ToString("dd MMMM yyyy").ToUpper();





                    }
                    else if (cnj == "59")
                    {

                        dat = dat.AddDays(30);
                        DateTime fe2 = dat.AddDays(15);
                        DateTime fe3 = fe2.AddDays(15);
                        DateTime fe4 = fe3.AddDays(15);
                        DateTime fe5 = fe4.AddDays(15);
                        txtP1.Content = "30";
                        txtP2.Content = "15";
                        txtP3.Content = "15";
                        txtP4.Content = "15";
                        txtP5.Content = "15";
                        lblF1.Content = fncFechaDeVencimiento(dat);
                        lblF2.Content = fncFechaDeVencimiento(fe2);
                        lblF3.Content = fncFechaDeVencimiento(fe3);
                        lblF4.Content = fncFechaDeVencimiento(fe4);
                        lblF5.Content = fncFechaDeVencimiento(fe5);

                        //dat = dat.AddDays(30);
                        //DateTime fe2 = dat.AddDays(30);
                        //DateTime fe3 = fe2.AddDays(30);
                        //DateTime fe4 = fe3.AddDays(30);
                        //DateTime fe5 = fe4.AddDays(30);
                        //txtP1.Text = "30";
                        //txtP2.Text = "30";
                        //txtP3.Text = "30";
                        //txtP4.Text = "30";
                        //txtP5.Text = "30";
                        //lblF1.Content = dat.ToString("dd MMMM yyyy").ToUpper();
                        //lblF2.Content = fe2.ToString("dd MMMM yyyy").ToUpper();
                        //lblF3.Content = fe3.ToString("dd MMMM yyyy").ToUpper();
                        //lblF4.Content = fe4.ToString("dd MMMM yyyy").ToUpper();
                        //lblF5.Content = fe5.ToString("dd MMMM yyyy").ToUpper();





                    }


                }

                else
                {
                    lblCliente.Text = "";
                    lblDireccion.Text = "";
                    lblCiudad.Text = "";
                    lblColonia.Text = "";
                    lblFecha.Text = "";
                    lblcond.Text = "";
                    txtP1.Visibility = Visibility.Visible;
                    txtP2.Visibility = Visibility.Visible;
                    txtP3.Visibility = Visibility.Visible;
                    txtP4.Visibility = Visibility.Visible;
                    txtP5.Visibility = Visibility.Visible;
                    lblpa5.Visibility = Visibility.Visible;
                    labelD4.Visibility = Visibility.Visible;
                    labelD5.Visibility = Visibility.Visible;
                    btnImpr1.Visibility = Visibility.Hidden;
                    label12.Text = "Pagaré 3";
                    label10.Text = "Pagaré 2";
                    lblpa4.Text = "Pagaré 4";
                    lblpa5.Text = "Pagaré 5";
                    txtP1.Content = "";
                    txtP2.Content = "";
                    txtP3.Content = "";
                    txtP4.Content = "";
                    txtP5.Content = "";
                    label9.Text = "Días";
                    label11.Text = "Días";
                    label13.Text = "Días";
                    labelD4.Text = "Días";
                    labelD5.Text = "Días";
                    label20.Text = "";
                    lblPagare1.Text = "";
                    label21.Text = "";
                    label18.Text = "";
                    //lblMostrarLetras.Content = "";
                    lblF1.Content = "";
                    lblF2.Content = "";
                    lblF3.Content = "";
                    lblF4.Content = "";
                    lblF5.Content = "";
                    //lblx.Content = "";
                    lblPagare1.Text = "";
                    lblPagare2.Text = "";
                    lblPagare3.Text = "";
                    lblPagare4.Text = "";
                    lblPagare5.Text = "";




                    dr.Close();






                }
            }
            catch (Exception l)
            {
                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Revise su conexión a internet.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });

            }
        }

        private string fncFechaDeVencimiento(DateTime dteFechaDeVencimiento)
        {
            string dteFechaDeVencimientos = string.Empty; ;
            if (this.folioid.Text != "")
            {


                if (dteFechaDeVencimiento.DayOfWeek == DayOfWeek.Saturday)
                    dteFechaDeVencimiento = dteFechaDeVencimiento.AddDays(2);

                if (dteFechaDeVencimiento.DayOfWeek == DayOfWeek.Sunday)
                    dteFechaDeVencimiento = dteFechaDeVencimiento.AddDays(1);

                dteFechaDeVencimientos = dteFechaDeVencimiento.ToString("dd MMMM yyyy").ToUpper();
            }
            return dteFechaDeVencimientos;
        }

        static byte fncNumeroDePagares(byte bytCondicionPago)
        {
            switch (bytCondicionPago)
            {
                case 10:
                case 15:
                case 16:
                case 20:
                case 21:
                case 23:
                case 25:

                    {
                        return 0;
                        break;
                    }

                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 12:
                case 13:
                case 14:
                case 22:
                case 24:
                case 26:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:


                    {
                        return 1;
                        break;
                    }
                case 17:
                case 19:
                case 27:
                    {
                        return 2;
                        break;
                    }

                case 18:
                case 28:
                case 30:
                case 43:
                    {
                        return 3;
                        break;
                    }
                case 11:
                case 29:
                case 40:
                case 45:
                    {
                        return 4;
                        break;
                    }

                case 41:
                case 59:
                    {
                        return 5;
                        break;
                    }
                default:
                    return 6;
                    break;
            }
        }


        public static string GetSetting(string appName, string section, string key, string sDefault)
        {
            // Los datos de VB se guardan en:
            // HKEY_CURRENT_USER\Software\VB and VBA Program Settings
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"Software\VB and VBA Program Settings\" +
                                                              appName + "\\" + section);
            string s = sDefault;
            if (rk != null)
            {
                s = (string)rk.GetValue(key);
            }
            return s;
        }
    }
}

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

namespace GGGC.Admin.AZ.Pagares
{
    /// <summary>
    /// Interaction logic for VerPagare.xaml
    /// </summary>
    public partial class VerPagare : Window
    {
        public string UsarFolio;
        public string UsarCantidad;
        public string UsarFecha;
        public string UsarCantLetra;
        public string UsarNumPagare;
        public string UsarNumPagares;
        public string UsarNombre;
        public string UsarDireccion;
        public string UsarColonia;
        public string UsarCiudad;
        public string UsarFechaActual;
        public string UsarEstado;


        public VerPagare(string FolioFactura, string Cantidad, string Fecha, string CantLetra, string numPagare,string numPagares, string Nombre, string Direccion, string Colonia, string Ciudad, string estado)
        {
            InitializeComponent();
            UsarFolio = FolioFactura;
            UsarCantidad = Cantidad;
            UsarFecha = Fecha;
            UsarCantLetra = CantLetra;
            UsarNumPagare = numPagare;
            UsarNumPagares = numPagares;
            UsarNombre = Nombre;
            UsarDireccion = Direccion;
            UsarColonia = Colonia;
            UsarCiudad = Ciudad;
            UsarEstado = estado;
            //UsarFechaActual = FechaActual;

        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
            RptPagare report = new RptPagare(UsarFolio, UsarCantidad, UsarFecha, UsarCantLetra, UsarNumPagare,UsarNumPagares, UsarNombre, UsarDireccion, UsarColonia, UsarCiudad, UsarEstado);

            ReportViewer.Report = report;

            ReportViewer.RefreshReport();

            //-------Que muestra previsualizador de que se esta imprimiendo
            //System.Drawing.Printing.PrinterSettings printerSettings
            //         = new System.Drawing.Printing.PrinterSettings();


            //Telerik.Reporting.Processing.ReportProcessor reportProcessor
            //    = new Telerik.Reporting.Processing.ReportProcessor();

            //Telerik.Reporting.TypeReportSource typeReportSource =
            //    new Telerik.Reporting.TypeReportSource();

            //reportProcessor.PrintReport(report, printerSettings);
            // ------------codigo


            // Obtain the settings of the default printer
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





    }
}

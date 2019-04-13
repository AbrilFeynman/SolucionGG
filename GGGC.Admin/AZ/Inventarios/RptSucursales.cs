namespace GGGC.Admin.AZ.Inventarios
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Telerik.Reporting.Processing;

    /// <summary>
    /// Summary description for RptSucursales.
    /// </summary>
    public partial class RptSucursales : Telerik.Reporting.Report
    {
        private System.Data.DataTable tblTabla;


        string folio = "Rept";
        string fecha = "";
        public RptSucursales()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
                InitializeComponent();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al iniciar" + ex.Message);
            }
        }

        public RptSucursales(System.Data.DataTable tbl)
        {

            try


            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");

                InitializeComponent();
                this.tblTabla = tbl;


                string fecha = DateTime.Now.ToString();
                DateTime date = DateTime.Now;

                string dateday = date.ToString("dd-MM-yyyy HH mm ");
                objectDataSource1.DataSource = tbl;
                graph1.DataSource = objectDataSource1;


                //SaveReport(this.Report, @"C:\BIG\LRG\Excel\ORD_"+folio+ "_LRG920502BG7_"+ dateday + ".pdf");
                string camino = @"C:\Ektelesis.Net\CFDI\DATOS\PDF\" + dateday + ".pdf";

                SaveReport(this.Report, @"C:\Ektelesis.Net\CFDI\DATOS\PDF\" + dateday + ".pdf");

                MessageBox.Show("Reporte guardado en escritorio");
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }

        }












        private void SaveReport(Telerik.Reporting.Report report, string fileName)
        {
            ReportProcessor reportProcessor = new ReportProcessor();
            Telerik.Reporting.InstanceReportSource instanceReportSource = new Telerik.Reporting.InstanceReportSource();
            instanceReportSource.ReportDocument = report;
            RenderingResult result = reportProcessor.RenderReport("PDF", instanceReportSource, null);

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
            }
        }
    }
}
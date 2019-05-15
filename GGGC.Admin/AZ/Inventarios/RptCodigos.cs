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
    /// Summary description for RptCodigos.
    /// </summary>
    public partial class RptCodigos : Telerik.Reporting.Report
    {
        private System.Data.DataTable tblTabla;


        string folio = "Rept";
        string fecha = "";
        public RptCodigos()
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

        public RptCodigos(System.Data.DataTable tbl, string nombre)
        {

            try


            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");

                InitializeComponent();
                this.tblTabla = tbl;

                //Telerik.Reporting.BarSeries barSeries1 = new Telerik.Reporting.BarSeries();
                //Telerik.Reporting.LegendItem ad = new Telerik.Reporting.LegendItem();
                //ad.



                string fecha = DateTime.Now.ToString();
                DateTime date = DateTime.Now;

                //string titulo = "Del " + inicial + " Al " + final + "";
                // textBox4.Value = titulo;
                string dateday = date.ToString("dd-MM-yyyy HH mm ");
                objectDataSource1.DataSource = tbl;
                graph1.DataSource = objectDataSource1;

                textBox4.Value = nombre;


                //SaveReport(this.Report, @"C:\BIG\LRG\Excel\ORD_"+folio+ "_LRG920502BG7_"+ dateday + ".pdf");
                string camino = @"C:\Ektelesis.Net\CFDI\DATOS\PDF\" + dateday + ".pdf";

                SaveReport(this.Report, @"C:\Ektelesis.Net\CFDI\DATOS\PDF\" + dateday + ".pdf");
                string argument = @"/select, " + camino;
                System.Diagnostics.Process.Start("explorer.exe", argument);
                //  MessageBox.Show("Reporte guardado en escritorio");
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
namespace GGGC.Admin.AZ.Ordenes.Views
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Telerik.Reporting.Processing;

    /// <summary>
    /// Summary description for RptOrden.
    /// </summary>
    public partial class RptOrden : Telerik.Reporting.Report
    {
        private System.Data.DataTable tblTabla;
        private System.Data.DataTable tblTablaDetalle;

        string folio = "";
        string fecha = "";
        public RptOrden()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al iniciar" + ex.Message);
            }
        }

        public RptOrden(System.Data.DataTable tbl, System.Data.DataTable tblDetalle)
        {

            try


            {

                InitializeComponent();
                this.tblTabla = tbl;
                this.tblTablaDetalle = tblDetalle;
                generarnombre();
                generardetalle(tblDetalle);
                string fecha = DateTime.Now.ToString();
                DateTime date = DateTime.Now;
              
                string dateday = date.ToString("dd-MM-yyyy HH mm ");
                SaveReport(this.Report, @"C:\BIG\LRG\Excel\ORD_"+folio+ "_LRG920502BG7_"+ dateday + ".pdf");

                MessageBox.Show("Reporte guardado en escritorio");
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }

        }


        private void generardetalle(System.Data.DataTable tblDetalle)
        {

            try
            {
                objectDataSource1.DataSource = tblDetalle;
                this.table1.DataSource = objectDataSource1;




            }
            catch (Exception eq)
            {
                throw new Exception("Wrong" + eq.Message);
            }

        }

        private void generarnombre()

        {
            
            //Folio de la orden 
            folio = tblTabla.Rows[0][0].ToString();
            
            textBox1.Value = tblTabla.Rows[0][0].ToString();

            double cant = Convert.ToDouble(tblTabla.Rows[0][7]);
            //Subtotal
            textBox101.Value = "$" + cant.ToString("#,###.00", CultureInfo.InvariantCulture);

            double ivis = Convert.ToDouble(tblTabla.Rows[0][7]);
            double iva = ivis * 0.16;
            //IVA
            textBox68.Value = "$"+iva.ToString();

            double totall = Convert.ToDouble(tblTabla.Rows[0][8]);
            //TOTAL
            textBox69.Value = "$" +totall.ToString("#,###.00", CultureInfo.InvariantCulture);
            //Observaciones
            textBox79.Value = tblTabla.Rows[0][15].ToString();


            //Cliente
            textBox28.Value = tblTabla.Rows[0][12].ToString();
            //Telefono
            textBox30.Value = tblTabla.Rows[0][13].ToString();
            //RFC
            textBox32.Value = tblTabla.Rows[0][2].ToString();
            //Direccion
            textBox33.Value = tblTabla.Rows[0][14].ToString();


            //Fecha Recepcion
            textBox17.Value = tblTabla.Rows[0][4].ToString();
            //Fecha de entrega
            textBox18.Value = tblTabla.Rows[0][5].ToString();
            //Fecha vigencia 
            textBox5.Value = tblTabla.Rows[0][3].ToString();



            //Checkbox
            int bytValorEx = Convert.ToInt32(tblTabla.Rows[0][9]);
            int bytValorIn = Convert.ToInt32(tblTabla.Rows[0][10]);
            int bytValorAc = Convert.ToInt32(tblTabla.Rows[0][11]);
            pcrDespliegaExteriores(bytValorEx);
            pcrDespliegaInteriores(bytValorIn);
            pcrDespliegaAccesorios(bytValorAc);
        }



        private void pcrDespliegaExteriores(int bytValor)
            {
        
            if (bytValor != 0)
            {
                if ((bytValor & 1024) == 1024)
                    chkparabrisas.Value = true;
                if ((bytValor & 512) == 512)
                    chkcarroseria.Value = true;
                if ((bytValor & 256) == 256)
                    chktgasolina.Value = true;
                if ((bytValor & 128) == 128)
                    chkcompletas.Value = true;
                if ((bytValor & 64) == 64)
                    chkmoldeduras.Value = true;
                if ((bytValor & 32) == 32)
                    chktapas.Value = true;
                if ((bytValor & 16) == 16)
                    chkcristales.Value = true;
                if ((bytValor & 8) == 8)
                    chkespejo.Value = true;
                if ((bytValor & 4) == 4)
                    chkantena.Value = true;
                if ((bytValor & 2) == 2)
                    chkluces.Value = true;

            }



        }

        private void pcrDespliegaInteriores(int bytValor)
        {

            if (bytValor != 0)
            {
                if ((bytValor & 512) == 512)
                    chkvestiduras.Value = true;
                if ((bytValor & 256) == 256)
                    chktapetes.Value = true;
                if ((bytValor & 128) == 128)
                    chkmanijas.Value = true;
                if ((bytValor & 64) == 64)
                    chkcinturon.Value = true;
                if ((bytValor & 32) == 32)
                    chkespejoretro.Value = true;
                if ((bytValor & 16) == 16)
                    chkencendedor.Value = true;
                if ((bytValor & 8) == 8)
                    chkbocinas.Value = true;
                if ((bytValor & 4) == 4)
                    chkestereo.Value = true;
                if ((bytValor & 2) == 2)
                    chktablero.Value = true;

            }



        }



        private void pcrDespliegaAccesorios(int bytValor)
        {

            if (bytValor != 0)
            {
                if ((bytValor & 4096) == 4096)
                    chkrueda.Value = true;
                if ((bytValor & 2048) == 2048)
                    chkmaneralgato.Value = true;
                if ((bytValor & 1024) == 1024)
                    chkgato.Value = true;
                if ((bytValor & 512) == 512)
                    chktaponradiador.Value = true;
                if ((bytValor & 256) == 256)
                    chktaponaceite.Value = true;
                if ((bytValor & 128) == 128)
                    chkvarilla.Value = true;
                if ((bytValor & 64) == 64)
                    chkestuche.Value = true;
                if ((bytValor & 32) == 32)
                    chktriangulo.Value = true;
                if ((bytValor & 16) == 16)
                    chkllrefa.Value = true;
                if ((bytValor & 8) == 8)
                    chkfiltrodeaire.Value = true;
                if ((bytValor & 4) == 4)
                    chkbateri.Value = true;
                if ((bytValor & 2) == 2)
                    chkextinguidor.Value = true;

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
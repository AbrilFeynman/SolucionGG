namespace GGGC.Admin.AZ.Ordenes.Views
{
    using Microsoft.WindowsAzure.Storage;
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
    /// Summary description for Ordenprint.
    /// </summary>
    public partial class Ordenprint : Telerik.Reporting.Report
    {
        private System.Data.DataTable tblTabla;
        private System.Data.DataTable tblTablaDetalle;

        string folio = "";
        string fecha = "";
        public Ordenprint()
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

        public Ordenprint(System.Data.DataTable tbl, System.Data.DataTable tblDetalle)
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

             


            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }

        }
        private async void UploadBlob(string strPath, string fileName)
        {
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=gggcbig;AccountKey=CQBHPdnT3EFKPxLLKcm7sWSyx6/l90Xj1FJ9q8ia69pHLRRFnahEdfLXCOGDfc+7PzE+Ck/rniwJ+OHQh+i00Q==;EndpointSuffix=core.windows.net");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("erp");
            var blob = container.GetBlockBlobReference(fileName);

            using (FileStream fileStream = new FileStream(strPath, FileMode.Open))
            {
                await blob.UploadFromStreamAsync(fileStream);

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
            textBox68.Value = "$" + iva.ToString();

            double totall = Convert.ToDouble(tblTabla.Rows[0][8]);
            //TOTAL
            textBox69.Value = "$" + totall.ToString("#,###.00", CultureInfo.InvariantCulture);
            //Observaciones
            Observaciones.Value = tblTabla.Rows[0][20].ToString();


            //Cliente
            textBox7.Value = tblTabla.Rows[0][12].ToString();
            //Telefono
            textBox21.Value = tblTabla.Rows[0][13].ToString();
            //RFC
            textBox20.Value = tblTabla.Rows[0][2].ToString();
            //Direccion
            textBox19.Value = tblTabla.Rows[0][19].ToString();
            //marca
            textBox24.Value = tblTabla.Rows[0][14].ToString().ToUpper();
            //modelo
            textBox25.Value = tblTabla.Rows[0][15].ToString().ToUpper();
            //año
            textBox26.Value = tblTabla.Rows[0][16].ToString().ToUpper();
            //placas
            textBox27.Value = tblTabla.Rows[0][17].ToString().ToUpper();
            //km
            textBox28.Value = tblTabla.Rows[0][18].ToString().ToUpper();

            //Fecha Recepcion
            //   string dateday = date.ToString("dd-MM-yyyy HH mm ");
            DateTime fecha =Convert.ToDateTime( tblTabla.Rows[0][4]);
            string fechas = fecha.ToString("dd-MM-yyyy");
            textBox22.Value =fechas;
            
            DateTime Horar   =Convert.ToDateTime(tblTabla.Rows[0][4]);
            string hora = Horar.ToString("hh:mm tt");
            textBox2.Value = hora;
            //Fecha de entrega
            DateTime horae = Convert.ToDateTime( tblTabla.Rows[0][5]);
            string hre = horae.ToString("hh:mm tt");
            textBox23.Value = hre;
            //Fecha vigencia 
            // textBox5.Value = tblTabla.Rows[0][3].ToString();



            //Checkbox
            int bytValorEx = Convert.ToInt32(tblTabla.Rows[0][9]);
            int bytValorIn = Convert.ToInt32(tblTabla.Rows[0][10]);
            int bytValorAc = Convert.ToInt32(tblTabla.Rows[0][11]);
            int bytpendientes = Convert.ToInt32(tblTabla.Rows[0][22]);
            pcrDespliegaExteriores(bytValorEx);
            pcrDespliegaInteriores(bytValorIn);
            pcrDespliegaAccesorios(bytValorAc);
            pcrDespliegaPendientes(bytpendientes);
            gasolina();

        }


        private void gasolina()
        {
            double tanque = Convert.ToDouble(tblTabla.Rows[0][21]);
            if (tanque == 0)
            {

            }
            else if (tanque == 25)
            {

                textBox49.Style.BackgroundColor = Color.Gray;
                //textBox49.Style.LineColor = Color.White;
                textBox49.Style.Color = Color.White;
                // textBox49.Style.BorderColor.c = Color.Yellow;
            }
            else if (tanque == 50)
            {
                textBox50.Style.BackgroundColor = Color.Gray;
                textBox50.Style.Color = Color.White;
            }
            else if (tanque == 75)
            {
                textBox51.Style.BackgroundColor = Color.Gray;
                textBox51.Style.Color = Color.White;
            }
            else if (tanque == 100)
            {
                textBox52.Style.BackgroundColor = Color.Gray;
                textBox52.Style.Color = Color.White;

            }


        }


        private void pcrDespliegaExteriores(int bytValor)
        {


            if (bytValor != 0)
            {
                if ((bytValor & 4096) == 4096)
                    shape13.Visible = true;
                if ((bytValor & 2048) == 2048)
                    shape9.Visible = true;
                if ((bytValor & 1024) == 1024)
                    shape19.Visible = true;
                if ((bytValor & 512) == 512)
                    shape17.Visible = true;
                if ((bytValor & 256) == 256)
                    shape16.Visible = true;
                if ((bytValor & 128) == 128)
                    shape18.Visible = true;
                if ((bytValor & 64) == 64)
                    shape15.Visible = true;
                if ((bytValor & 32) == 32)
                    shape14.Visible= true;
                if ((bytValor & 16) == 16)
                    shape12.Visible = true;
                if ((bytValor & 8) == 8)
                    shape11.Visible = true;
                if ((bytValor & 4) == 4)
                    shape10.Visible = true;
                if ((bytValor & 2) == 2)
                    shape8.Visible = true;

            }



        }

        private void pcrDespliegaInteriores(int bytValor)
        {

            if (bytValor != 0)
            {
                if ((bytValor & 4096) == 4096)
                    shape23.Visible = true;
                if ((bytValor & 2048) == 2048)
                    shape25.Visible = true;
                if ((bytValor & 1024) == 1024)
                   shape30.Visible = true;
                if ((bytValor & 512) == 512)
                   shape20.Visible = true;
                if ((bytValor & 256) == 256)
                    shape21.Visible = true;
                if ((bytValor & 128) == 128)
                    shape22.Visible = true;
                if ((bytValor & 64) == 64)
                    shape24.Visible = true;
                if ((bytValor & 32) == 32)
                    shape26.Visible = true;
                if ((bytValor & 16) == 16)
                   shape27.Visible = true;
                if ((bytValor & 8) == 8)
                   shape28.Visible = true;
                if ((bytValor & 4) == 4)
                    shape29.Visible= true;
                if ((bytValor & 2) == 2)
                   shape31.Visible = true;

            }



        }

        private void pcrDespliegaAccesorios(int bytValor)
        {

            if (bytValor != 0)
            {
                if ((bytValor & 4096) == 4096)
                    shape39.Visible = true;
                if ((bytValor & 2048) == 2048)
                    shape43.Visible = true;
                if ((bytValor & 1024) == 1024)
                   shape35.Visible = true;
                if ((bytValor & 512) == 512)
                    shape41.Visible = true;
                if ((bytValor & 256) == 256)
                    shape37.Visible = true;
                if ((bytValor & 128) == 128)
                    shape32.Visible = true;
                if ((bytValor & 64) == 64)
                    shape34.Visible = true;
                if ((bytValor & 32) == 32)
                    shape38.Visible= true;
                if ((bytValor & 16) == 16)
                    shape42.Visible = true;
                if ((bytValor & 8) == 8)
                    shape36.Visible = true;
                if ((bytValor & 4) == 4)
                    shape40.Visible = true;
                if ((bytValor & 2) == 2)
                    shape33.Visible = true;

            }



        }



        private void pcrDespliegaPendientes(int bytValor)
        {

            if (bytValor != 0)
            {


                if ((bytValor & 128) == 128)
                   shape5.Visible = true;
                if ((bytValor & 64) == 64)
                    shape6.Visible = true;
                if ((bytValor & 32) == 32)
                    shape7.Visible = true;
                if ((bytValor & 16) == 16)
                    shape4.Visible = true;
                if ((bytValor & 8) == 8)
                    shape3.Visible= true;
                if ((bytValor & 4) == 4)
                    shape2.Visible = true;
                if ((bytValor & 2) == 2)
                    shape1.Visible = true;

            }



        }

        




    }
}
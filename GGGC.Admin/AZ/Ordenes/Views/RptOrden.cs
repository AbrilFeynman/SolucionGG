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
    using Microsoft.WindowsAzure.Storage;

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
                //SaveReport(this.Report, @"C:\BIG\LRG\Excel\ORD_"+folio+ "_LRG920502BG7_"+ dateday + ".pdf");
                string camino = @"C:\Ektelesis.Net\CFDI\DATOS\PDF\" + folio + ".pdf";
                SaveReport(this.Report, @"C:\Ektelesis.Net\CFDI\DATOS\PDF\" + folio + ".pdf");
                UploadBlob(camino, folio);
                // MessageBox.Show("Reporte guardado en escritorio");
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
            textBox22.Value = tblTabla.Rows[0][4].ToString();
            //Fecha de entrega
            textBox23.Value = tblTabla.Rows[0][5].ToString();
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
               
                textBox49.Style.BackgroundColor = Color.DarkBlue;
                //textBox49.Style.LineColor = Color.White;
                textBox49.Style.Color = Color.White;
               // textBox49.Style.BorderColor.c = Color.Yellow;
            }
            else if (tanque == 50)
            {
                textBox50.Style.BackgroundColor = Color.DarkBlue;
                textBox50.Style.Color = Color.White;
            }
            else if (tanque == 75)
            {
                textBox51.Style.BackgroundColor = Color.DarkBlue;
                textBox51.Style.Color = Color.White;
            }
            else if (tanque ==100)
            {
                textBox52.Style.BackgroundColor = Color.DarkBlue;
                textBox52.Style.Color = Color.White;

            }


        }


        private void pcrDespliegaExteriores(int bytValor)
            {
            

            if (bytValor != 0)
            {
                if ((bytValor & 4096) == 4096)
                    chkemblema.Value = true;
                if ((bytValor & 2048) == 2048)
                    chkcuartoluces.Value = true;
                if ((bytValor & 1024) == 1024)
                    checklimpiaparabrisas.Value = true;
                if ((bytValor & 512) == 512)
                    chkcarroseria.Value = true;
                if ((bytValor & 256) == 256)
                    chktapongasolina.Value = true;
                if ((bytValor & 128) == 128)
                    checkbocinaclaxon.Value = true;
                if ((bytValor & 64) == 64)
                   chkmoldaduras.Value = true;
                if ((bytValor & 32) == 32)
                    chktapas.Value = true;
                if ((bytValor & 16) == 16)
                    chkcristales.Value = true;
                if ((bytValor & 8) == 8)
                    chkespejolateral.Value = true;
                if ((bytValor & 4) == 4)
                    chkantena.Value = true;
                if ((bytValor & 2) == 2)
                    chkunidadluces.Value = true;

            }



        }

        private void pcrDespliegaInteriores(int bytValor)
        {
         
            if (bytValor != 0)
            {
                if ((bytValor & 4096) == 4096)
                    chkbotonesinteriores.Value = true;
                if ((bytValor & 2048) == 2048)
                    chkcenicero.Value = true;
                if ((bytValor & 1024) == 1024)
                    chkcalefaccion.Value = true;
                if ((bytValor & 512) == 512)
                    checkvestiduras.Value = true;
                if ((bytValor & 256) == 256)
                   checktapetes.Value = true;
                if ((bytValor & 128) == 128)
                    checkmanijasinterior.Value = true;
                if ((bytValor & 64) == 64)
                    chkcinturones.Value = true;
                if ((bytValor & 32) == 32)
                    chkespejoretrovisor.Value = true;
                if ((bytValor & 16) == 16)
                    chkencendedor.Value = true;
                if ((bytValor & 8) == 8)
                    chkbocinas.Value = true;
                if ((bytValor & 4) == 4)
                    chkradio.Value = true;
                if ((bytValor & 2) == 2)
                    chkinstrumentostablero.Value = true;

            }



        }

        private void pcrDespliegaAccesorios(int bytValor)
        {

            if (bytValor != 0)
            {
                if ((bytValor & 4096) == 4096)
                    checkmaneralgato.Value = true;
                if ((bytValor & 2048) == 2048)
                    checkllaverueda.Value = true;
                if ((bytValor & 1024) == 1024)
                    checkgato.Value = true;
                if ((bytValor & 512) == 512)
                    checktaponradiador.Value = true;
                if ((bytValor & 256) == 256)
                    checktaponaceite.Value = true;
                if ((bytValor & 128) == 128)
                    checkvarillaaceite.Value = true;
                if ((bytValor & 64) == 64)
                    checkestucheherramientas.Value = true;
                if ((bytValor & 32) == 32)
                    checktrianguloseg.Value = true;
                if ((bytValor & 16) == 16)
                    checkllantarefaccion.Value = true;
                if ((bytValor & 8) == 8)
                    checkfiltroaire.Value = true;
                if ((bytValor & 4) == 4)
                    checkbateriamca.Value = true;
                if ((bytValor & 2) == 2)
                    checkclaxon.Value = true;

            }



        }



        private void pcrDespliegaPendientes(int bytValor)
        {
          
            if (bytValor != 0)
            {
               
                   
                if ((bytValor & 128) == 128)
                    pcambioaceite.Value = true;
                if ((bytValor & 64) == 64)
                   pfrenos.Value = true;
                if ((bytValor & 32) == 32)
                    psuspen.Value = true;
                if ((bytValor & 16) == 16)
                    pamorti.Value = true;
                if ((bytValor & 8) == 8)
                    pbalanceo.Value = true;
                if ((bytValor & 4) == 4)
                    palineacion.Value = true;
                if ((bytValor & 2) == 2)
                    pllantas.Value = true;

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
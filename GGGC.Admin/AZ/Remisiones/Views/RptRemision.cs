namespace GGGC.Admin.AZ.Remisiones.Views
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using ThoughtWorks.QRCode.Codec;
    using System.IO;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Telerik.Reporting.Processing;
    using GGGC.Admin.AZ.Ordenes.Views;
    using Microsoft.WindowsAzure.Storage;
    using Telerik.Windows.Controls;
    using System.Windows;

    /// <summary>
    /// Summary description for RptRemision.
    /// </summary>
    public partial class RptRemision : Telerik.Reporting.Report
	{
        private string m_nombre;
        private string m_folio;
        private System.Data.DataTable tblTabla;
        private System.Data.DataTable tblTablaDetalle;

        private TimbreFiscalDigital timbre;

        public string Nombre
        {
            get
            {
                return m_nombre;
            }
        }

        public string Folio
        {
            get
            {
                return m_folio;
            }
        }

        public RptRemision()
		{
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Revise su conexón a internet.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }
        }


        public RptRemision(System.Data.DataTable tbl, System.Data.DataTable tblDetalle)
        {

            

                InitializeComponent();
                this.tblTabla = tbl;
                this.tblTablaDetalle = tblDetalle;
                generargral();
                generarHeader(tbl);
                generardetalle(tblDetalle);
                string fecha = DateTime.Now.ToString();
                generarCBB(tbl);
                string folio = m_nombre;
                string camino = @"C:\Ektelesis.Net\CFDI\DATOS\PDF\" + folio + ".pdf";
                SaveReport(this.Report, @"C:\Ektelesis.Net\CFDI\DATOS\PDF\" + folio + ".pdf");
                UploadBlob(camino,folio);
            //MessageBox.Show("Reporte guardado en escritorio");


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





        private void generarHeader(System.Data.DataTable tblHeader)
        {
            try
            {
                txtNumeroDeCliente.Value = tblHeader.Rows[0][1].ToString();
                txtReceptorNombre.Value = tblHeader.Rows[0][3].ToString();
                txtReceptorDireccion.Value = tblHeader.Rows[0][4].ToString();
                txtReceptorColonia.Value = tblHeader.Rows[0][5].ToString();
                txtReceptorCiudad.Value = tblHeader.Rows[0][7].ToString() + " "+ tblHeader.Rows[0][8].ToString()+" "+ tblHeader.Rows[0][6].ToString();
                txtReceptorRFC.Value = tblHeader.Rows[0][2].ToString();
                txtNota.Value = tblHeader.Rows[0][14].ToString();
             
                double cant = Convert.ToDouble(tblHeader.Rows[0][11]);
                decimal canti = Convert.ToDecimal(cant);
                txtSubtotal.Value = "$" + cant.ToString("#,###.00", CultureInfo.InvariantCulture);
                generarImportes(canti);
                // txtNumeroDeFolio.Value = tblHeader.Rows[0][1].ToString();



            }
            catch (Exception eq)
            {

                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Error al generar el archivo PDF.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }

        }


        private void generarCBB(System.Data.DataTable tblHeader)
        {
            try
            {

                //RFC Emisor + RFC Receptor + TotalNeto + UUID
                //?re=AVI090223896&rr=TYA8511158V9&tt=1047.48&id=9AE7E021-673D-4540-A35F-949A0EC73D30
                pictureBox1.Value = null;
                string datos = "?re={0}&rr={1}&tt={2}&id={3}";
                string rfc = tblHeader.Rows[0][2].ToString();
                string sTotal = Convert.ToString(tblHeader.Rows[0][11]);
                decimal factot = Convert.ToDecimal(tblHeader.Rows[0][11]);
                string entero = decimal.Truncate(factot).ToString().PadLeft(10, Convert.ToChar("0"));
                string decimales = sTotal.Split(Convert.ToChar("."))[1];
                string resultado = entero + "." + decimales;
                string UUID = "A9AC3A8D-7E57-4CA8-98A7-8093AB3B02D7";
                string[] pars = { rfc, rfc, resultado, UUID };
                datos = string.Format(datos, pars);

                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 4;
                qrCodeEncoder.QRCodeVersion = 8;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;

                pictureBox1.Value = qrCodeEncoder.Encode(datos);
                System.Threading.Thread.Sleep(800);
                
                
                string time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
                txtCadenaSAT.Value = "|| 1.0 | E5375B97 - 7E57 - 45A4 - A466 - AD4DDD0B10D5 | "+time+" | U7SzZTJcSA / BcTfcAQ1lLKQp5xDO7snrkMcwJL1wCjrPCZgxRF5VHjgdtQvnytqhGv2d7Sl3vq12IBuUr7rxKe9U + RQuz + hT0ulGHjh2wYzjfTkqVYN + xFNOOOHvxojdNxLbKgW / qBRnFJlwWNie + 9Onx7YAphLlAnU + 30Fz5MU =| 20001000000300022323 ||";
            }
            catch (Exception ex)
            {
                Log("Error. Origen:" + ex.Source + Environment.NewLine + "Pila: " + ex.StackTrace + Environment.NewLine + ex.Message);
                throw new Exception("Error al generar el CBB. Error: " + ex.Message);
            }
        }

        private void generarImportes(decimal subtotal)
        {
            try
            {

               


                string sTipoCambio = string.Empty;
                string moneda = "PESOS";
                string simbolo = "MXN";
                string cantidadLetra = string.Empty;

                decimal cantidad = decimal.Round(subtotal, 2);

                if (ComprobarCampo(subtotal))
                {
                    if (subtotal.ToString().ToUpper().Equals("USD"))
                    {
                        simbolo = "USD.";
                        moneda = "DOLARES";
                        sTipoCambio = " T/C: " + sTipoCambio;
                        cantidadLetra = Conversiones.NumeroALetras(cantidad.ToString(), moneda, simbolo) + sTipoCambio;
                    }
                }

                htmlMoneda.Value = "MONEDA: " + simbolo;

                if (cantidadLetra.Trim().Length == 0)
                {
                    cantidadLetra = "(" + Conversiones.NumeroALetras(cantidad.ToString(), moneda, simbolo) + ")";
                }

                this.htmlCantidadLetras.Value = string.Format(htmlCantidadLetras.Value.ToString(), cantidadLetra);
            }
            catch (Exception ex)
            {

                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Error al convertir moneda.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }
        }
        public static bool ComprobarCampo(object Campo)
        {
            bool bRes = true;
            if (Campo != null)
            {
                if (Convert.IsDBNull(Campo))
                {
                    bRes = false;
                }
                else
                {
                    if (Campo.ToString().Trim().Length == 0)
                    {
                        bRes = false;
                    }
                }
            }
            else
            {
                bRes = false;
            }
            return bRes;
        }


        private void generardetalle(System.Data.DataTable tblDetalle)
        {

            try
            {
                objectDataSource1.DataSource = tblDetalle;
                this.table1.DataSource = objectDataSource1;

                txtNumeroDeFolio.Value = tblDetalle.Rows[0][11].ToString();
                m_nombre = tblDetalle.Rows[0][11].ToString();
                DateTime deit = Convert.ToDateTime(tblDetalle.Rows[0][14]);
                txtFecha.Value = deit.ToString("dd/MMMM/yyyy").ToUpper();

            }
            catch (Exception eq)
            {

                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Error al generar detalle.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
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


        private void generargral()

        {
            string Domicilio = "TORONJA 116 COL. FRACC. LA GLORIETA" + Environment.NewLine +
                "DURANGO, DURANGO C.P. 34207" + Environment.NewLine;

            Domicilio += "TELS. (618) 129 78 25 /129 76 41" + Environment.NewLine + 
                "mayoreo@llantasyrinesdelguadiana.com";
            txtEmisorExpedido.Value = Domicilio;


            DateTime thisDay = DateTime.Today;
           // txtFecha.Value = thisDay.ToString("dd/MMMM/yyyy").ToUpper();

            txtEmisorRFC.Value = "R.F.C LRG-920502-BG7";
        }



        private void Log(string strCadena)
        {
            using (FileStream fs = new FileStream("C:\\Ektelesis.Net\\CFDI\\GGLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", FileMode.Append))
            {
                // Create a writer and specify the encoding.
                // The default (UTF-8) supports special Unicode characters,
                // but encodes all standard characters in the same way as
                // ASCII encoding.
                using (StreamWriter w = new StreamWriter(fs, System.Text.Encoding.ASCII))
                {
                    // Write a decimal, string, and char.
                    w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss") + Environment.NewLine + strCadena + Environment.NewLine + Environment.NewLine);
                }
            }

        }


    }


    public class Conversiones
    {
        public static string NumeroALetras(string num, string moneda, string simbolo)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;
            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }
            entero = Convert.ToInt64(Math.Truncate(nro));

            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                char sep = '0';
                dec = " " + moneda + " " + decimales.ToString().PadLeft(2, sep) + "/100 " + simbolo;
            }
            else
            {
                dec = " " + moneda + " 00/100 " + simbolo;
            }

            res = NumeroALetras(Convert.ToDouble(entero)) + dec;

            return res;
        }

        private static string NumeroALetras(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + NumeroALetras(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + NumeroALetras(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + NumeroALetras(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = NumeroALetras(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + NumeroALetras(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value % 1000);
            }
            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + NumeroALetras(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
            }
            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;
        }
    }

}
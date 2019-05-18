namespace GGGC.Admin.AZ.Cotizacion
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Telerik.Reporting.Processing;
    using Telerik.Windows.Controls;

    /// <summary>
    /// Summary description for RptBudget.
    /// </summary>
    public partial class RptBudget : Telerik.Reporting.Report
    {
        public RptBudget(System.Data.DataTable tbl, int sucursal)
        {
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
            InitializeComponent();
            objectDataSource1.DataSource = tbl;
            string folio = tbl.Rows[0][0].ToString();
            textBox5.Value = folio;
            textBox15.Value = tbl.Rows[0][3].ToString();
            DateTime fecha = new DateTime();
            fecha = Convert.ToDateTime(tbl.Rows[0][2]);
            txtfecha.Value =  fecha.Day.ToString() + " " + fecha.ToString("MMM").ToUpper() + " ," + fecha.Year.ToString();
            textBox32.Value = tbl.Rows[0][5].ToString();
            textBox13.Value = tbl.Rows[0][4].ToString();
            textBox3.Value = tbl.Rows[0][6].ToString();
            textBox14.Value = tbl.Rows[0][13].ToString();
            textBox20.Value = tbl.Rows[0][14].ToString();

            object sumObject;
            sumObject = tbl.Compute("Sum(Ptotal)", string.Empty);
            decimal suma = Convert.ToDecimal(sumObject);
            textBox28.Value = "$" + suma.ToString("#,###.00", CultureInfo.InvariantCulture);
            if (sucursal == 11 || sucursal == 12 || sucursal == 13 || sucursal == 15 || sucursal == 17 || sucursal == 18)
            {
                textBox30.Value = "27058 - TORREÓN, COAH.";
            }
            else
            {
                textBox30.Value = "34207 - DURANGO, DGO.";
            }
            decimal iva = suma * Convert.ToDecimal( 0.16);
            decimal total = suma + iva;
           textBox27.Value = "$" + iva.ToString("#,###.00", CultureInfo.InvariantCulture);
           textBox16.Value = "$" + total.ToString("#,###.00", CultureInfo.InvariantCulture);
            generarImportes(total);
            //textBox18.Value = tbl.Rows[0][13].ToString();
           // textBox19.Value = tbl.Rows[0][14].ToString();
            textBox12.Value = "$" + total.ToString("#,###.00", CultureInfo.InvariantCulture);
            textBox16.Value = "$" + total.ToString("#,###.00", CultureInfo.InvariantCulture);

            string camino = @"C:\Ektelesis.Net\CFDI\DATOS\PDF\COT_" + folio + "_LRG920502BG7.pdf";

            SaveReport(this.Report, camino);
      
        


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

               // htmlMoneda.Value = "MONEDA: " + simbolo;

                if (cantidadLetra.Trim().Length == 0)
                {
                    cantidadLetra = "(" + Conversiones.NumeroALetras(cantidad.ToString(), moneda, simbolo) + ")";
                }

              //  this.htmlCantidadLetras.Value = string.Format(htmlCantidadLetras.Value.ToString(), cantidadLetra);
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
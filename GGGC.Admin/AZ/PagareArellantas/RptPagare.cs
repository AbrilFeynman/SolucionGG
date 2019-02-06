namespace GGGC.Admin.AZ.PagareArellantas
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for RptPagare.
    /// </summary>
    public partial class RptPagare : Telerik.Reporting.Report
    {
        public RptPagare(string folio, string cantidad, string fecha, string cantLetra, string numpagare, string numerospagares, string nombre, string direccion, string colonia, string ciudad, string estado)
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");

            InitializeComponent();

            textBox3.Value = folio.ToUpper();
            textBox5.Value = cantidad;


            DateTime d = DateTime.Now;
            d = Convert.ToDateTime(fecha);


            string fechaDia = d.ToString("dd");
            textBox8.Value = fechaDia.ToString();


            string fechaMes = d.ToString("MMMM").ToUpper();
            textBox1.Value = fechaMes.ToString();

            string fechaA = d.ToString("yy");
            textBox2.Value = fechaA.ToString();

            textBox10.Value = "(" + " " + cantLetra + " " + ")";
            textBox14.Value = numpagare;
            textBox7.Value = numerospagares;
            textBox18.Value = nombre;
            textBox22.Value = direccion + " " + colonia;
            textBox26.Value = ciudad + "," + " " + estado + ".";

            DateTime j = DateTime.Now;


            string fechaAD2 = j.ToString("dd");
            textBox28.Value = fechaAD2;

            string fechaM2 = j.ToString("MMMM").ToUpper();
            textBox4.Value = fechaM2;

            string fechaActualA = j.ToString("yy");
            textBox6.Value = fechaActualA;



            //
            // TODO: Add any constructor code after InitializeComponent call
            


        }
    }
}
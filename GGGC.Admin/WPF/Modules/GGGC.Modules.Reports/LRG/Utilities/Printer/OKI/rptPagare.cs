namespace GGGC.Modules.Reports.LRG.Utilities.Printer.OKI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for rptPagare.
    /// </summary>
    public partial class rptPagare : Telerik.Reporting.Report
    {
        private System.Data.DataTable tblTabla;

        public rptPagare()
        {
            //
            // Required for telerik Reporting designer support
            //
            try
            {
                InitializeComponent();
                //aplicarFondo();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar reporte. Error: " + ex.Message);
            }

            
        }

        public rptPagare(System.Data.DataTable tbl)
        {
            try
            {
                InitializeComponent();
                //aplicarFondo();
                //this.factura = Factura;
                //this.timbre = Timbre;
                //this.cadenaOriginalSAT = cadenaOriginalSAT;
                //this.sucursal = Sucursal;
                this.tblTabla = tbl;
                //this.tblTablaDetalle = tblDetalle;
                //intTipoCFDI = tipoCFD;

                //this.sCuenta = "";
                //this.sMP = "";
                //this.sCuenta = strCuenta;
                //this.sMP = strMP;
                //tipoCFD = 2;
                //generarCBB();
                //generarEmisor();
                //generarReceptor();
                //generarConceptos();
                //generarImportes();
                //generarCadenaOriginalSAT();
                //generarDatosAdicionales();

                //if (tipoCFD == 1)
                //{
                //    //this.htmlSerieFolioInterno.Value = "FACTURA";
                //    this.txtTipoCFD.Value = "FACTURA";
                //}
                //else if (tipoCFD == 2)
                //{
                //    //this.htmlSerieFolioInterno.Value = "DEVOLUCIÓN";
                //    this.txtTipoCFD.Value = "FACTURA";
                //    //this.textBox8.Visible = false;
                //    //this.textBox6.Visible = false;

                //}
                //else if (tipoCFD == 3)
                //{
                //    //this.htmlSerieFolioInterno.Value = "DEVOLUCIÓN";
                //    this.txtTipoCFD.Value = "DEVOLUCIÓN";
                //}
                //else if (tipoCFD == 5)
                //{
                //    //this.htmlSerieFolioInterno.Value = "DEVOLUCIÓN";
                //    this.txtTipoCFD.Value = "DEVOLUCIÓN";
                //}
                //else if (tipoCFD == 11)
                //{
                //    //this.htmlSerieFolioInterno.Value = "RECIBO DE HONORARIOS";
                //    this.txtTipoCFD.Value = "NOTA DE CRÉDITO";
                //}

                //else if (tipoCFD == 10)
                //{
                //    //this.htmlSerieFolioInterno.Value = "RECIBO DE HONORARIOS";
                //    this.txtTipoCFD.Value = "NOTA DE CARGO";
                //}
                //else if (tipoCFD == 15)
                //{
                //    //this.htmlSerieFolioInterno.Value = "RECIBO DE HONORARIOS";
                //    this.txtTipoCFD.Value = "RECIBO DE INGRESO";
                //}
                //generarDatosComprobante();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar reporte. Error: " + ex.Message);
            }
        }
    }
}
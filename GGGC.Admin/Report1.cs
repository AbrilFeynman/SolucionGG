namespace GGGC.Admin
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class Report1 : Telerik.Reporting.Report
    {
        private System.Data.DataTable tblTabla;

        public Report1()
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

        public Report1(System.Data.DataTable tbl)
        {

            try


            {

                InitializeComponent();
                this.tblTabla = tbl;
                
                
                generardetalle(tblTabla);
                
               
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


    }
}
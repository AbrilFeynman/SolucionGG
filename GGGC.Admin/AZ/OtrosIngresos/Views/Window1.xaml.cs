using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GGGC.Admin.AZ.OtrosIngresos.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(string osos)
        {
            InitializeComponent();
            try { export15(osos); }
            catch (SqlException ex) { MessageBox.Show("Revise su conexión a internet"); }
        }
        private void export15(string folio)
        {
            try
            {
                DataTable tablaOrden = GetHeader(folio);
                DataTable tablaDeatil = GetDetail(folio);

                RptIngresos fac = new RptIngresos(tablaOrden, tablaDeatil);
                this.ReportViewer1.Report = fac;
                this.ReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Folio no encontrado");
            }

        }

        static DataTable GetHeader(string folio)
        {
            string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";


            SqlConnection sqlconn = new SqlConnection(conect);

            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR DE CONEXION" + ex.Message);
            }
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM IngresosHeader WHERE IngresosID = '" + folio + "' ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "IngresosHeader");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["IngresosHeader"];
            sqlconn.Close();

            return dtbl;
        }
        static DataTable GetDetail(string folio)
        {
            string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";


            SqlConnection sqlconn = new SqlConnection(conect);

            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR DE CONEXION" + ex.Message);
            }
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM IngresosDetail WHERE IngresosID = '" + folio + "' ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "IngresosDetail");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["IngresosDetail"];
            sqlconn.Close();

            return dtbl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



    }
}

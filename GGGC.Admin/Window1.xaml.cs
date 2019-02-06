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

namespace GGGC.Admin
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Generar_Click(object sender, RoutedEventArgs e)
        {
            DataTable tablaInv = GetInv();
          
            Report1 reporte = new Report1(tablaInv);
            this.ReportViewer1.Report = reporte;
            this.ReportViewer1.RefreshReport();

        }

        static DataTable GetInv()
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





            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM  Inventario ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "Inventario");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["Inventario"];
            sqlconn.Close();

            return dtbl;
        }



    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace GGGC.Admin.AZ.Inventarios
{
    /// <summary>
    /// Interaction logic for Inventarioview.xaml
    /// </summary>
    public partial class Inventarioview : UserControl
    {
        private int m_select = 0;
        private string m_consulta;
        public Inventarioview()
        {
            InitializeComponent();
        }

        private void Exportar_Click(object sender, RoutedEventArgs e)
        {
            m_select = radios();
            if (m_select > 0)
            {



                generarreporte();

            }
            else
            {
                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Selecciones un parametro para el reporte",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });

            }

        }

        private void generarreporte()
        {
            try
            {
             
                DataTable tablaDeatil = GetDetail();
                RptMarca fac = new RptMarca(tablaDeatil);

                //this.ReportViewer1.Report = fac;
                //this.ReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Folio no encontrado");
            }

        }

        static DataTable GetDetail()
        {
            string conect = "SERVER = 192.168.200.10; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007 ";

            SqlConnection sqlconn = new SqlConnection(conect);
            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR DE CONEXION" + ex.Message);
            }




            //SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM OrderHeader WHERE OrderID = " + folio + " ", sqlconn);
            //SqlDataAdapter adapter = new SqlDataAdapter("SELECT TOP 100 PERCENT SUM(Existencia) AS Cantidad, GrupoID AS Linea," +
            //    " Grupo FROM windowServiceExistencias WHERE(GrupoID IN(1, 2, 3, 4)) GROUP BY GrupoID," +
            //    " Grupo ORDER BY GrupoID", sqlconn);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM ExistenciasMarca", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "Vista");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["Vista"];
            sqlconn.Close();

            return dtbl;

        }



        int radios()
        {
            if (rdmarca.IsChecked == true)
            {
                m_consulta = "";
                
                return 1;
            }
            else if (rdlinea.IsChecked == true)
            {
                m_consulta = "";
               
                return 2;
            }
            else if (rdsucursales.IsChecked == true)
            {

                m_consulta = "";
                
                return 3;
            }
           

            return 0;
        }
    }
}

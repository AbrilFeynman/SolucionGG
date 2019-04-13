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
                switch (m_select)
                {
                    case 1:
                        GenerateMarca();
                        break;
                    case 2:
                        GenerateLinea();
                        break;
                    case 3:
                        GenerateSucursales();
                        break;
                        


                }



            }
            catch (Exception ex)
            {

                MessageBox.Show("Folio no encontrado");
            }

        }

        private void GenerateLinea()
        {
            DataTable tablaDeatil = GetDetail();
            RptMarca fac = new RptMarca(tablaDeatil);

        }

        private void GenerateMarca()
        {
            DataTable tablaMarca = GetMarca();
            RptTrimarca fac = new RptTrimarca(tablaMarca);

        }

        private void GenerateSucursales()
        {
            DataTable tablaSucursales = GetSucursales();
            RptSucursales fac = new RptSucursales(tablaSucursales);

        }

        static DataTable GetSucursales()
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
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Sucursales.Codigo_De_Sucursal AS Base, Existencias_Sucursales.Cantidad," +
                " Existencias_Sucursales.Codigo_De_Articulo AS Articulo FROM Existencias_Sucursales INNER JOIN  " +
                " Sucursales ON Existencias_Sucursales.Numero_Corto_De_Sucursal = Sucursales.Numero_Corto_De_Sucursal WHERE(Existencias_Sucursales.Codigo_De_Articulo = '63684')", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "Vista");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["Vista"];
            sqlconn.Close();

            return dtbl;

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


        static DataTable GetMarca()
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
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT windowServiceExistencias.Marca AS Marca, SUM(windowServiceExistencias.Existencia) AS Cantidad, Marcas.Nombre AS Descripcion" +
                " FROM  Marcas INNER JOIN  windowServiceExistencias ON Marcas.Codigo_De_Marca = windowServiceExistencias.Marca" +
                " WHERE(windowServiceExistencias.Marca IN('BFG', 'MICH', 'UNIR')) GROUP BY windowServiceExistencias.Marca, Marcas.Nombre", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "Vista1");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["Vista1"];
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

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
using System.Windows.Threading;
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
        private int increment;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public Inventarioview()
        {
            InitializeComponent();
        }

        private void Exportar_Click(object sender, RoutedEventArgs e)
        {
            m_select = radios();
            if (m_select > 0)
            {
                radbusy.BusyContent = "Generando reporte ";

                radbusy.IsBusy = true;
                increment = 0;
                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();


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

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            increment++;
            timerlabel.Content = increment.ToString();
            //Refresh(this.radbusy);
            if (increment == 3)
            {
                dispatcherTimer.Stop();
                radbusy.IsBusy = false;
            }
            // Updating the Label which displays the current second


            // Forcing the CommandManager to raise the RequerySuggested event
            // CommandManager.InvalidateRequerySuggested();
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
                    case 4:
                        if (m_select > 0 && dtinicial.SelectedDate != null && dtfinal.SelectedDate != null)
                        {
                            
                            int yInicial = Convert.ToInt16(Convert.ToDateTime(dtinicial.SelectedDate).Year.ToString());
                            int yFinal = Convert.ToInt16(Convert.ToDateTime(dtfinal.SelectedDate).Year.ToString());
                            if (yInicial >= 2017 && yFinal >= 2017)
                            {
                                if (dtinicial.SelectedDate <= dtfinal.SelectedDate)
                                {

                                    generarventas();

                                }
                                else
                                {
                                    RadWindow radWindow = new RadWindow();
                                    RadWindow.Alert(new DialogParameters()
                                    {
                                        Content = "La fecha inicial no debe de ser mayor que la fecha final.",
                                        Header = "BIG",

                                        DialogStartupLocation = WindowStartupLocation.CenterOwner
                                        // IconContent = "";
                                    });


                                }


                            }
                            else
                            {
                                RadWindow radWindow = new RadWindow();
                                RadWindow.Alert(new DialogParameters()
                                {
                                    Content = "El año del reporte deben de ser mayor o igual al año 2017",
                                    Header = "BIG",

                                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                                    // IconContent = "";
                                });
                            }



                        }
                        else
                        {
                            RadWindow radWindow = new RadWindow();
                            RadWindow.Alert(new DialogParameters()
                            {
                                Content = "Los parámetros son inválidos.",
                                Header = "BIG",

                                DialogStartupLocation = WindowStartupLocation.CenterOwner
                                // IconContent = "";
                            });

                        }
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

        private void generarventas()
        {
            string strFechaInicial = Convert.ToDateTime(dtinicial.SelectedDate).ToString("MM/dd/yyyy HH:mm:ss");
            string strFechaFinal = Convert.ToDateTime(dtfinal.SelectedDate).ToString("MM/dd/yyyy HH:mm:ss");
            string FechaInicial = Convert.ToDateTime(dtinicial.SelectedDate).ToString("MM/dd/yyyy");
            string FechaFinal = Convert.ToDateTime(dtfinal.SelectedDate).ToString("MM/dd/yyyy");
            DataTable tablaVentas = GetVentas(strFechaInicial, strFechaFinal);
            DataTable tablaMayoreo = GetVentasMayoreo(strFechaInicial, strFechaFinal);
            RptVentas fac = new RptVentas(tablaVentas,tablaMayoreo,FechaInicial,FechaFinal);

        }


        static DataTable GetVentas(string inicial, string final)
        {
            
           

            string conect = "SERVER = 192.168.200.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007 ";

            SqlConnection sqlconn = new SqlConnection(conect);
            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR DE CONEXION" + ex.Message);
            }




            SqlDataAdapter adapter = new SqlDataAdapter("SELECT    CASE WHEN Numero_Corto_De_Sucursal = 55 THEN 05 ELSE Numero_Corto_De_Sucursal END AS Numerosucursal, SUM(Total) AS Total,Codigo_De_Sucursal, " +
                "Sucursal as Nombre FROM dbo.fncReporteadorDeVentasConsolidado() fncReporteadorDeVentasConsolidado" +
                " WHERE(Fecha_Del_Documento BETWEEN CONVERT(DATETIME, '"+inicial+ "', 102)  AND" +
                " CONVERT(DATETIME, '"+final+ "', 102)) and (Numero_Corto_De_Sucursal not in(7,11,17)) GROUP BY Numero_Corto_De_Sucursal, Codigo_De_Sucursal, Sucursal order by Numerosucursal asc ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "Vista");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["Vista"];
            sqlconn.Close();

            return dtbl;

        }

        static DataTable GetVentasMayoreo(string inicial, string final)
        {



            string conect = "SERVER = 192.168.200.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007 ";

            SqlConnection sqlconn = new SqlConnection(conect);
            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR DE CONEXION" + ex.Message);
            }




            SqlDataAdapter adapter = new SqlDataAdapter("SELECT    CASE WHEN Numero_Corto_De_Sucursal = 55 THEN 05 ELSE Numero_Corto_De_Sucursal END AS Numerosucursal, SUM(Total) AS Total,Codigo_De_Sucursal, " +
                "Sucursal as Nombre FROM dbo.fncReporteadorDeVentasConsolidado() fncReporteadorDeVentasConsolidado" +
                " WHERE(Fecha_Del_Documento BETWEEN CONVERT(DATETIME, '" + inicial + "', 102)  AND" +
                " CONVERT(DATETIME, '" + final + "', 102)) and (Numero_Corto_De_Sucursal in(7,11,17)) GROUP BY Numero_Corto_De_Sucursal, Codigo_De_Sucursal, Sucursal order by Numerosucursal asc ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "Vista");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["Vista"];
            sqlconn.Close();

            return dtbl;

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
            else if (rdventas.IsChecked == true)
            {

                m_consulta = "";

                return 4;
            }


            return 0;
        }
    }
}

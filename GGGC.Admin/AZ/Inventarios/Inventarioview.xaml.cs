using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
                    case 5:
                        if (m_select > 0 && dtinicial.SelectedDate != null && dtfinal.SelectedDate != null)
                        {

                           

                            int yInicial = Convert.ToInt16(Convert.ToDateTime(dtinicial.SelectedDate).Year.ToString());
                            int yFinal = Convert.ToInt16(Convert.ToDateTime(dtfinal.SelectedDate).Year.ToString());
                            if (yInicial >= 2017 && yFinal >= 2017)
                            {
                                if (dtinicial.SelectedDate <= dtfinal.SelectedDate)
                                {

                                    generarventasant();

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
                    case 6:
                        if (m_select > 0 && messelec.SelectedDate != null && Ivaa.SelectedIndex > -1)
                        {


                            meta();



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



        private void meta()
        {

            int mess = Convert.ToDateTime(messelec.SelectedDate).Month;
            int year = Convert.ToDateTime(messelec.SelectedDate).Year;
            int sucursal = Convert.ToInt32(Ivaa.SelectionBoxItem);
            string fecha =Convert.ToDateTime( messelec.SelectedDate).ToString("MMMM-yyyy").ToUpper();

            DataTable crud = new DataTable();
            crud = GetDates(mess,year);
            DataTable detalle = new DataTable();
            detalle = GetDetalle(mess,year,sucursal);

            var lstLeftJoin =
                from fact in crud.AsEnumerable()
                join desc in detalle.AsEnumerable() on fact.Field<int>("Date") equals desc.Field<int>("Dia") into FactDesc
                from fd in FactDesc.DefaultIfEmpty()
                select new
                {
                    Dia = fact.Field<int>("Date"),
                    Total = (fd == null) ? 0 : fd.Field<decimal>("Total")
                }
                ;

            DataTable nueva = new DataTable();
            nueva.Columns.Add("Date", typeof(int));
            nueva.Columns.Add("Total", typeof(decimal));

            foreach (var item in lstLeftJoin)
            {
                
                nueva.Rows.Add(item.Dia, item.Total);

            }


            Rptmeta fac = new Rptmeta(nueva, fecha);





        }


        public DataTable GetDates(int mes, int anio)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(Int32));

            int year = anio;
            int month = mes;

            int daysInMonth = DateTime.DaysInMonth(year, month);
            for (int i = 0; i < daysInMonth; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Date"] = i + 1;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        static DataTable GetDetalle(int mes, int anio, int sucursal)
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




            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Codigo_De_Sucursal,SUM(Total)as Total, Fecha_Del_Documento as Fecha, Day(Fecha_Del_Documento) as Dia " +
                "FROM dbo.fncReporteadorDeVentas() fncReporteadorDeVentas WHERE month(Fecha_Del_Documento) = "+mes+" and year(Fecha_Del_Documento) = "+anio+" " +
                "and(Numero_Corto_De_Sucursal in ("+sucursal+")) group by Fecha_Del_Documento, Codigo_De_Sucursal ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "Vista");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["Vista"];
            sqlconn.Close();

            return dtbl;

        }




        private void generarventas()
        {
       

            string strFechaInicial = Convert.ToDateTime(dtinicial.SelectedDate).ToString("MM/dd/yyyy HH:mm:ss");
            string strFechaFinal = Convert.ToDateTime(dtfinal.SelectedDate).ToString("MM/dd/yyyy HH:mm:ss");
            string FechaInicial = Convert.ToDateTime(dtinicial.SelectedDate).ToString("MM/dd/yyyy");
            string FechaFinal = Convert.ToDateTime(dtfinal.SelectedDate).ToString("MM/dd/yyyy");
           
            DataTable tablaVentas = GetVentas(strFechaInicial, strFechaFinal);
            DataTable tablaMayoreo = GetVentasMayoreo(strFechaInicial, strFechaFinal);




           RptVentas fac = new RptVentas(tablaVentas, tablaMayoreo, FechaInicial, FechaFinal);

        }
        private void generarventasant()
        {
            //FECHAS CON UN AÑO MENOS
            DateTime unobef = Convert.ToDateTime(dtinicial.SelectedDate);
            unobef = unobef.AddYears(-1);
            string anioant = unobef.Year.ToString();
            string anio = unobef.Year.ToString();
            string anioactual = Convert.ToDateTime(dtinicial.SelectedDate).Year.ToString();
            DateTime dosbef = Convert.ToDateTime(dtfinal.SelectedDate);
            dosbef = dosbef.AddYears(-1);

            //FORMATOS DE LAS FECHAS PARA SQL Y PARAMETROS
            string strFechaInicial = Convert.ToDateTime(dtinicial.SelectedDate).ToString("MM/dd/yyyy HH:mm:ss");
            string strFechaFinal = Convert.ToDateTime(dtfinal.SelectedDate).ToString("MM/dd/yyyy HH:mm:ss");
            string FechaInicial = Convert.ToDateTime(dtinicial.SelectedDate).ToString("MM/dd/yyyy");
            string FechaFinal = Convert.ToDateTime(dtfinal.SelectedDate).ToString("MM/dd/yyyy");
            string strFechaInicialAnt = Convert.ToDateTime(unobef).ToString("MM/dd/yyyy HH:mm:ss");
            string strFechaFinalAnt = Convert.ToDateTime(dosbef).ToString("MM/dd/yyyy HH:mm:ss");

            //TABLAS AÑO ACTUAL AÑO ANTERIOR
            DataTable tablaVentas = GetVentasActual(strFechaInicial, strFechaFinal);
            DataTable tblventasanterior = GetVentasAnterior(strFechaInicialAnt, strFechaFinalAnt);

            DataTable tablaMayoreo = GetVentasMayoreoActual(strFechaInicial, strFechaFinal);
            DataTable tablaMayoreoanterior = GetVentasMayoreoAnt(strFechaInicialAnt, strFechaFinalAnt);

            //FUSIONAR LAS DOS TABLAS ANTERIOR Y ACTUAL
            // DataTable tablaMen = Gw
            var query = from table1 in tablaVentas.AsEnumerable()
                          join table2 in tblventasanterior.AsEnumerable() 
                          on table1.Field<int>("Numerosucursal") equals 
                          table2.Field<int>("Numerosucursal")
                          select new
                          {
                              Numerosucursal = table1.Field<int>("Numerosucursal"),
                              TotalActual = table1.Field<decimal>("TotalActual"),
                              Codigo_De_Sucursal = table1.Field<string>("Codigo_De_Sucursal"),
                              Nombre = table1.Field<string>("Nombre"),
                              TotalAnterior = table2.Field<decimal>("TotalAnterior"),
                          };
            DataTable nueva = new DataTable();
            nueva.Columns.Add("Numerosucursal", typeof(int));
            nueva.Columns.Add("TotalActual", typeof(decimal));
            nueva.Columns.Add("Codigo_De_Sucursal", typeof(string));
            nueva.Columns.Add("Nombre", typeof(string));
            nueva.Columns.Add("TotalAnterior", typeof(decimal));

            

            foreach (var item in query)
            {
                nueva.Rows.Add(item.Numerosucursal, item.TotalActual, item.Codigo_De_Sucursal,item.Nombre, item.TotalAnterior);

            }


            var query2 = from table1 in tablaMayoreo.AsEnumerable()
                         join table2 in tablaMayoreoanterior.AsEnumerable()
                         on table1.Field<int>("Numerosucursal") equals
                         table2.Field<int>("Numerosucursal")
                         select new
                         {
                             Numerosucursal = table1.Field<int>("Numerosucursal"),
                             TotalActual = table1.Field<decimal>("TotalActual"),
                             Codigo_De_Sucursal = table1.Field<string>("Codigo_De_Sucursal"),
                             Nombre = table1.Field<string>("Nombre"),
                             TotalAnterior = table2.Field<decimal>("TotalAnterior"),
                         };

            DataTable nueva2 = new DataTable();
            nueva2.Columns.Add("Numerosucursal", typeof(int));
            nueva2.Columns.Add("TotalActual", typeof(decimal));
            nueva2.Columns.Add("Codigo_De_Sucursal", typeof(string));
            nueva2.Columns.Add("Nombre", typeof(string));
            nueva2.Columns.Add("TotalAnterior", typeof(decimal));


            foreach (var item2 in query2)
            {
                nueva2.Rows.Add(item2.Numerosucursal, item2.TotalActual, item2.Codigo_De_Sucursal, item2.Nombre, item2.TotalAnterior);

            }
            // tablaVentas = query.CopyToDataTable<DataRow>();


            //tablaVentas.Merge(tblventasanterior);
            //tablaMayoreo.Merge(tablaMayoreoanterior);

            //REPORTE TELERIK
            RptComparison fac = new RptComparison(nueva,nueva2, FechaInicial, FechaFinal);

        }
        static DataTable GetVentasMayoreoActual(string inicial, string final)
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




            SqlDataAdapter adapter = new SqlDataAdapter("SELECT    CASE WHEN Numero_Corto_De_Sucursal = 55 THEN 05 ELSE Numero_Corto_De_Sucursal " +
                                                       "END AS Numerosucursal, " +
                                                       "SUM(Total) AS TotalActual, " +
                                                       "CASE WHEN Codigo_De_Sucursal = 'B1' THEN 'B01' " +
                                                       "WHEN Codigo_De_Sucursal = 'B2' THEN 'B02' " +
                                                       "WHEN Codigo_De_Sucursal = 'B3' THEN 'B03' " +
                                                       "WHEN Codigo_De_Sucursal = 'B4' THEN 'B04' " +

                                                       "WHEN Codigo_De_Sucursal = 'B8' THEN 'B08' " +
                                                       "WHEN Codigo_De_Sucursal = 'B9' THEN 'B09' ELSE Codigo_De_Sucursal " +
                                                       "END AS Codigo_De_Sucursal , Sucursal as Nombre " +
                                                       " FROM dbo.fncReporteadorDeVentas() fncReporteadorDeVentas" +
                                                        " WHERE(Fecha_Del_Documento BETWEEN CONVERT(DATETIME, '" + inicial + "', 102)  AND" +
                                                        " CONVERT(DATETIME, '" + final + "', 102)) and (Numero_Corto_De_Sucursal in(7,11,17)) GROUP BY Numero_Corto_De_Sucursal, Codigo_De_Sucursal, Sucursal order by Numerosucursal asc ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "Vista");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["Vista"];
            sqlconn.Close();

            return dtbl;

        }
        static DataTable GetVentasActual(string inicial, string final)
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




            SqlDataAdapter adapter = new SqlDataAdapter("SELECT    CASE WHEN Numero_Corto_De_Sucursal = 55 THEN 05 ELSE Numero_Corto_De_Sucursal " +
                                                        "END AS Numerosucursal, " +
                                                        "SUM(Total) AS TotalActual, " +
                                                        "CASE WHEN Codigo_De_Sucursal = 'B1' THEN 'B01' " +
                                                        "WHEN Codigo_De_Sucursal = 'B2' THEN 'B02' " +
                                                        "WHEN Codigo_De_Sucursal = 'B3' THEN 'B03' " +
                                                        "WHEN Codigo_De_Sucursal = 'B4' THEN 'B04' " +

                                                        "WHEN Codigo_De_Sucursal = 'B8' THEN 'B08' " +
                                                        "WHEN Codigo_De_Sucursal = 'B9' THEN 'B09' ELSE Codigo_De_Sucursal " +
                                                        "END AS Codigo_De_Sucursal , Sucursal as Nombre " +
                                                        " FROM dbo.fncReporteadorDeVentas() fncReporteadorDeVentas" +
                                                         " WHERE(Fecha_Del_Documento BETWEEN CONVERT(DATETIME, '" + inicial + "', 102)  AND" +
                                                         " CONVERT(DATETIME, '" + final + "', 102)) and (Numero_Corto_De_Sucursal not in(7,11,17)) GROUP BY Numero_Corto_De_Sucursal, Codigo_De_Sucursal, Sucursal order by Numerosucursal asc ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "Vista");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["Vista"];
            sqlconn.Close();

            return dtbl;

        }
        static DataTable GetVentasAnterior(string inicial, string final)
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




            SqlDataAdapter adapter = new SqlDataAdapter("SELECT    CASE WHEN Numero_Corto_De_Sucursal = 55 THEN 05 ELSE Numero_Corto_De_Sucursal " +
                                                        "END AS Numerosucursal, " +
                                                        "SUM(Total) AS TotalAnterior " +

                                                        " FROM dbo.fncReporteadorDeVentas() fncReporteadorDeVentas" +
                                                         " WHERE(Fecha_Del_Documento BETWEEN CONVERT(DATETIME, '" + inicial + "', 102)  AND" +
                                                         " CONVERT(DATETIME, '" + final + "', 102)) and (Numero_Corto_De_Sucursal not in(7,10,11,17)) GROUP BY Numero_Corto_De_Sucursal, Codigo_De_Sucursal, Sucursal order by Numerosucursal asc ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "Vista");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["Vista"];
            sqlconn.Close();

            return dtbl;

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




            SqlDataAdapter adapter = new SqlDataAdapter("SELECT    CASE WHEN Numero_Corto_De_Sucursal = 55 THEN 05 ELSE Numero_Corto_De_Sucursal "+
                                                        "END AS Numerosucursal, "+
                                                        "SUM(Total) AS Total, "+
                                                        "CASE WHEN Codigo_De_Sucursal = 'B1' THEN 'B01' "+
                                                        "WHEN Codigo_De_Sucursal = 'B2' THEN 'B02' "+
                                                        "WHEN Codigo_De_Sucursal = 'B3' THEN 'B03' "+
                                                        "WHEN Codigo_De_Sucursal = 'B4' THEN 'B04' "+
                                                      
                                                        "WHEN Codigo_De_Sucursal = 'B8' THEN 'B08' " +
                                                        "WHEN Codigo_De_Sucursal = 'B9' THEN 'B09' ELSE Codigo_De_Sucursal "+
                                                        "END AS Codigo_De_Sucursal , Sucursal as Nombre FROM dbo.fncReporteadorDeVentas() fncReporteadorDeVentas" +
                                                         " WHERE(Fecha_Del_Documento BETWEEN CONVERT(DATETIME, '" + inicial + "', 102)  AND" +
                                                         " CONVERT(DATETIME, '" + final + "', 102)) and (Numero_Corto_De_Sucursal not in(7,11,17)) GROUP BY Numero_Corto_De_Sucursal, Codigo_De_Sucursal, Sucursal order by Numerosucursal asc ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "Vista");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["Vista"];
            sqlconn.Close();

            return dtbl;

        }

        static DataTable GetVentasMayoreoAnt(string inicial, string final)
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






            SqlDataAdapter adapter = new SqlDataAdapter("SELECT    CASE WHEN Numero_Corto_De_Sucursal = 55 THEN 05 ELSE Numero_Corto_De_Sucursal " +
                                                        "END AS Numerosucursal, " +
                                                        "SUM(Total) AS TotalAnterior " +

                                                        " FROM dbo.fncReporteadorDeVentas() fncReporteadorDeVentas" +
                                                         " WHERE(Fecha_Del_Documento BETWEEN CONVERT(DATETIME, '" + inicial + "', 102)  AND" +
                                                         " CONVERT(DATETIME, '" + final + "', 102)) and (Numero_Corto_De_Sucursal  in(7,10,11,17)) GROUP BY Numero_Corto_De_Sucursal, Codigo_De_Sucursal, Sucursal order by Numerosucursal asc ", sqlconn);
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




            SqlDataAdapter adapter = new SqlDataAdapter("SELECT     Numero_Corto_De_Sucursal  AS Numerosucursal, SUM(Total) AS Total," +

                "CASE WHEN Codigo_De_Sucursal = 'B7' THEN 'B07' ELSE Codigo_De_Sucursal END AS Codigo_De_Sucursal, " +
                "Sucursal as Nombre FROM dbo.fncReporteadorDeVentas() fncReporteadorDeVentas" +
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




            //SqlDataAdapter adapter = SqlDataAdapter("SELECT * FROM OrderHeader WHERE OrderID = " + folio + " ", sqlconn);
            //SqlDataAdapter adapter = new SqlDataAdapter("SELECT TOP 100 PERCENT SUM(Existencia) AS Cantidad, GrupoID AS Linea," +
            //    " Grupo FROM windowServiceExistencias WHERE(GrupoID IN(1, 2, 3, 4)) GROUP BY GrupoID," +
            //    " Grupo ORDER BY GrupoID", sqlconn);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM ExistenciasMarca", sqlconn);
            // SqlDataAdapter adapter = new SqlDataAdapter("SELECT Existencia as Cantidad ,GrupoID as Linea, Grupo FROM windowServiceExistencias where (GrupoID IN(1, 2, 3, 4))", sqlconn);
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
            else if (rdventasanio.IsChecked == true)
            {

                m_consulta = "";

                return 5;
            }
            else if (rdventas_meta.IsChecked == true)
            {

                m_consulta = "";

                return 6;
            }



            return 0;
        }
    }
}

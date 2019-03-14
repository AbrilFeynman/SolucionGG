using System;
using Syncfusion.XlsIO;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using Telerik.Windows.Controls;
using System.Windows.Threading;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace GGGC.Admin.AZ.Plexi
{
    /// <summary>
    /// Interaction logic for Plexi.xaml
    /// </summary>
    public partial class Plexi : UserControl
    {
        private delegate void NoArgDelegate();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int increment;
        private string m_nombreexterno;
        private string m_nombreempresa;
        private int m_select;
        //private CashierStationMainWindowModel vm;
        private BackgroundWorker worker;
        public string Nombreexterno
        {
            get
            {
                return m_nombreexterno;
            }
        }
        public int Select
        {
            get
            {
                return m_select;
            }
        }
        public string Nombreempresa
        {
            get
            {
                return m_nombreempresa;
            }
        }

        DataTable dtConsulta = new DataTable();
        public Plexi()
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-ES");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            InitializeComponent();
            //vm = new CashierStationMainWindowModel();
            //this.DataContext = vm;

            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            EnableDisableBusyIndicatorAsync(true);

            System.Threading.Thread.Sleep(5000);

            EnableDisableBusyIndicatorAsync(false);
        }
        protected void EnableDisableBusyIndicatorAsync(bool enable)
        {
            //vm.IsBusy = enable;
        }
        public static void Refresh(DependencyObject obj)
        {

            obj.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle,

                (NoArgDelegate)delegate { });
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            increment++;
            timerlabel.Content = increment.ToString();
            //Refresh(this.radbusy);
            if (increment == 6)
            {
                dispatcherTimer.Stop();
                CashierBusyIndicator.IsBusy = false;
            }

        }

        private void Exportar_Click(object sender, RoutedEventArgs e)
        {
            //  worker.RunWorkerAsync();
            //Exportar.IsEnabled = false;
            m_select = radios();
            if (m_select > 0)
            {




                Exportar.IsEnabled = false;
                CashierBusyIndicator.IsBusy = true;
                        increment = 0;
                        dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                        dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                        dispatcherTimer.Start();



                        Refresh(CashierBusyIndicator);
                        //worker.RunWorkerAsync();
                        //Refresh(CashierBusyIndicator);
                        generarreporte(m_select);
                        dtConsulta = new System.Data.DataTable();
                Exportar.IsEnabled = true;
                //Exportar.IsEnabled = true;
                // buttons.IsEnabled = true;





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

        }


        private void generarreporte(int selected)
        {
           
            dtConsulta = getventas(selected);
            if (dtConsulta.Rows.Count > 0)
            {

                int sucursales = radios();
                if (sucursales == 1)
                {




                    DateTime date = DateTime.Now;
                    string datewithformat = date.ToString();
                    string dateday = date.ToString("dd-MMMM-yyyy").ToUpper();
                    string closure = date.ToString("dd MM yyyy HH mm").ToUpper();
                    using (ExcelEngine excelEngine = new ExcelEngine())
                    {
                        excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;
                        //IStyle headerStyle = wo.Styles.Add("HeaderStyle");
                        IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);
                        IWorksheet worksheet = workbook.Worksheets[0];
                        worksheet.EnableSheetCalculations();
                        
                        DataTable tabla = dtConsulta;
                        int osos = tabla.Rows.Count;
                        worksheet.Name = m_nombreexterno;
                        worksheet.ImportDataTable(tabla, true, 2, 1,true);
                        worksheet.AutoFilters.FilterRange = worksheet.Range["A2:F2"];

                        worksheet.Range["A1"].Text = "REPORTE - PRECIO LISTA Y EXISTENCIAS " + m_nombreexterno + "AL " + dateday;
                        // worksheet.Range["A1"].Text = "Llantas y Rines del Guadiana S.A. de C.V. - Existencias LRG Al "+dateday+"- B4 Francisco Villa";
                        worksheet.Rows[1].FreezePanes();
                        worksheet.Rows[2].FreezePanes();

                        #region
                        IStyle headerStyle = workbook.Styles.Add("HeaderStyle");
                        headerStyle.BeginUpdate();

                        //workbook.SetPaletteColor(8, System.Drawing.Color.FromArgb(46, 204, 113));

                        //headerStyle.Color = System.Drawing.Color.FromArgb(46, 204, 113);
                        headerStyle.Color = System.Drawing.Color.Gray;
                        headerStyle.Font.Bold = true;
                        headerStyle.Font.Color = ExcelKnownColors.White;
                        headerStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;

                        headerStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        headerStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;

                        headerStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                        headerStyle.EndUpdate();

                        worksheet.Rows[1].CellStyle = headerStyle;

                        IStyle pStyle = workbook.Styles.Add("pStyle");
                        pStyle.BeginUpdate();

                        //workbook.SetPaletteColor(9, System.Drawing.Color.FromArgb(89, 171, 227));

                        //pStyle.Color = colorss(m_select);

                        pStyle.Color = System.Drawing.Color.DodgerBlue;


                        pStyle.Font.Bold = true;

                        pStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;

                        pStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        pStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;

                        pStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                        pStyle.EndUpdate();
                        #endregion

                        #region //ancho y alineacion columnas
                        worksheet.Rows[0].CellStyle = pStyle;
                        worksheet.Columns[2].WrapText = true;
                        worksheet.SetColumnWidth(1, 10);
                        worksheet.SetColumnWidth(2, 10);
                        worksheet.SetColumnWidth(3, 10);
                        worksheet.SetColumnWidth(4, 40);
                        worksheet.SetColumnWidth(5, 15);//Estado
                        worksheet.SetColumnWidth(6, 15);

                        IStyle pStyles = workbook.Styles.Add("pStyles");
                        pStyles.BeginUpdate();
                        worksheet.Columns[3].HorizontalAlignment = ExcelHAlign.HAlignLeft;
                        worksheet.Columns[2].HorizontalAlignment = ExcelHAlign.HAlignLeft;

                        pStyles.EndUpdate();
                        #endregion
                        // Create Table with data in the given range
                        int soviet = osos;
                        int rojos = soviet + 3;
                        int rus = soviet + 4;
                        string rusia = rus.ToString();
                        string cossacks = rojos.ToString();
                        string gulag = "A2:H" + cossacks + "";
                        //IListObject table = worksheet.ListObjects.Create("Table1", worksheet[gulag]);
                        string registros = soviet.ToString();
                        //IRange range = worksheet.Range[gulag];
                        //table.ShowTotals = true;
                        //table.Columns[0].TotalsRowLabel = "Total";

                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        string chorchill = "E2,E" + cossacks + "";
                        string russevel = "E" + rusia + "";
                        string ftotal = "E" + rusia + "";
                        //string qty = "N" + rusia + "";
                        string fpl = "AA" + rusia + "";
                        string totalr = "A" + rusia + "";
                       // worksheet.Range[totalr].Text = registros + " Registros";
                       // worksheet.Range[totalr].CellStyle = pStyle;
                        //string nrusia = "=SUBTOTAL(9,E2:E" + cossacks + ")";
                        //worksheet.Range[russevel].Formula = nrusia;
                        //worksheet.Range[russevel].CellStyle = pStyle;
                        //worksheet.Range["AA2:" + fpl].NumberFormat = "#,##0.00";
                        worksheet.Range["E2:" + ftotal].NumberFormat = "#,##0.00";
                        //string sumqty = "=SUBTOTAL(9,N2:N" + cossacks + ")";
                        //worksheet.Range[qty].Formula = sumqty;
                        // worksheet.Range[qty].CellStyle = pStyle;
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        //table.Columns[5].TotalsCalculation = ExcelTotalsCalculation.Sum;
                        //hacer el subtotal pero conformula ** el otro marca error con total calculation 
                        //range.SubTotal(0, ConsolidationFunction.Sum, new int[] {1,rojos});
                        string namer = dateday;
                        string fileName = @"C:\BIG\LRG\Excel\REP. PRECIOS DE LISTA Y EXISTENCIAS  " + m_nombreexterno + " Al " + dateday + "( " + closure + " )" + ".xlsx";
                        // string fileName = "LRG-Existencias al " + namer + "B4 Francisco Villa.xlsx";
                        workbook.SaveAs(fileName);
                        //workbook.Close();
                        //excelEngine.Dispose();
                        string argument = @"/select, " + fileName;

                        System.Diagnostics.Process.Start("explorer.exe", argument);
                    }


                }
                else if (sucursales == 2) //base VPN
                {



                    DateTime date = DateTime.Now;
                    string datewithformat = date.ToString();
                    string dateday = date.ToString("dd-MMMM-yyyy").ToUpper();
                    string closure = date.ToString("dd MM yyyy HH mm").ToUpper();
                    using (ExcelEngine excelEngine = new ExcelEngine())
                    {
                        excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;
                        //IStyle headerStyle = wo.Styles.Add("HeaderStyle");
                        IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);
                        IWorksheet worksheet = workbook.Worksheets[0];
                        worksheet.EnableSheetCalculations();
                        
                        DataTable tabla = dtConsulta;
                        int osos = tabla.Rows.Count;
                        worksheet.Name = m_nombreexterno;
                        worksheet.ImportDataTable(tabla, true, 2, 1, true);
                   
                        worksheet.AutoFilters.FilterRange = worksheet.Range["A2:F2"];

                        worksheet.Range["A1"].Text = "REPORTE - PRECIO LISTA Y EXISTENCIAS " + m_nombreexterno + "AL " + dateday;
                        // worksheet.Range["A1"].Text = "Llantas y Rines del Guadiana S.A. de C.V. - Existencias LRG Al "+dateday+"- B4 Francisco Villa";
                        worksheet.Rows[1].FreezePanes();
                        worksheet.Rows[2].FreezePanes();

                        #region
                        IStyle headerStyle = workbook.Styles.Add("HeaderStyle");
                        headerStyle.BeginUpdate();

                        //workbook.SetPaletteColor(8, System.Drawing.Color.FromArgb(46, 204, 113));

                        //headerStyle.Color = System.Drawing.Color.FromArgb(46, 204, 113);
                        headerStyle.Color = System.Drawing.Color.Gray;
                        headerStyle.Font.Bold = true;
                        headerStyle.Font.Color = ExcelKnownColors.White;
                        headerStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;

                        headerStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        headerStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;

                        headerStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                        headerStyle.EndUpdate();

                        worksheet.Rows[1].CellStyle = headerStyle;

                        IStyle pStyle = workbook.Styles.Add("pStyle");
                        pStyle.BeginUpdate();

                        //workbook.SetPaletteColor(9, System.Drawing.Color.FromArgb(89, 171, 227));

                        //pStyle.Color = colorss(m_select);

                        pStyle.Color = System.Drawing.Color.DodgerBlue;


                        pStyle.Font.Bold = true;

                        pStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;

                        pStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        pStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;

                        pStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                        pStyle.EndUpdate();
                        #endregion

                        #region //ancho y alineacion columnas
                        worksheet.Rows[0].CellStyle = pStyle;
                        worksheet.Columns[2].WrapText = true;
                        worksheet.SetColumnWidth(1, 10);
                        worksheet.SetColumnWidth(2, 10);
                        worksheet.SetColumnWidth(3, 10);
                        worksheet.SetColumnWidth(4, 40);
                        worksheet.SetColumnWidth(5, 15);//Estado
                        worksheet.SetColumnWidth(6, 15);

                        IStyle pStyles = workbook.Styles.Add("pStyles");
                        pStyles.BeginUpdate();
                        worksheet.Columns[3].HorizontalAlignment = ExcelHAlign.HAlignLeft;
                        worksheet.Columns[2].HorizontalAlignment = ExcelHAlign.HAlignLeft;

                        pStyles.EndUpdate();
                        #endregion
                        // Create Table with data in the given range
                        int soviet = osos;
                        int rojos = soviet + 3;
                        int rus = soviet + 4;
                        string rusia = rus.ToString();
                        string cossacks = rojos.ToString();
                        string gulag = "A2:H" + cossacks + "";
                        //IListObject table = worksheet.ListObjects.Create("Table1", worksheet[gulag]);
                        string registros = soviet.ToString();
                        //IRange range = worksheet.Range[gulag];
                        //table.ShowTotals = true;
                        //table.Columns[0].TotalsRowLabel = "Total";

                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        string chorchill = "E2,E" + cossacks + "";
                        string russevel = "E" + rusia + "";
                        string ftotal = "E" + rusia + "";
                        //string qty = "N" + rusia + "";
                        string fpl = "AA" + rusia + "";
                        string totalr = "A" + rusia + "";
                        //worksheet.Range[totalr].Text = registros + " Registros";
                        //worksheet.Range[totalr].CellStyle = pStyle;
                        //string nrusia = "=SUBTOTAL(9,E2:E" + cossacks + ")";
                        //worksheet.Range[russevel].Formula = nrusia;
                        //worksheet.Range[russevel].CellStyle = pStyle;
                        //worksheet.Range["AA2:" + fpl].NumberFormat = "#,##0.00";
                        worksheet.Range["E2:" + ftotal].NumberFormat = "#,##0.00";
                        //string sumqty = "=SUBTOTAL(9,N2:N" + cossacks + ")";
                        //worksheet.Range[qty].Formula = sumqty;
                        // worksheet.Range[qty].CellStyle = pStyle;
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        //table.Columns[5].TotalsCalculation = ExcelTotalsCalculation.Sum;
                        //hacer el subtotal pero conformula ** el otro marca error con total calculation 
                        //range.SubTotal(0, ConsolidationFunction.Sum, new int[] {1,rojos});
                        string namer = dateday;
                        string fileName = @"C:\BIG\LRG\Excel\REP. PRECIOS DE LISTA Y EXISTENCIAS  " + m_nombreexterno + " Al " + dateday + "( " + closure + " )" + ".xlsx";
                        // string fileName = "LRG-Existencias al " + namer + "B4 Francisco Villa.xlsx";
                        workbook.SaveAs(fileName);
                        //workbook.Close();
                        //excelEngine.Dispose();
                        string argument = @"/select, " + fileName;

                        System.Diagnostics.Process.Start("explorer.exe", argument);
                    }




                }
                else if (sucursales == 3)
                {
                    RadWindow radWindow = new RadWindow();
                    RadWindow.Alert(new DialogParameters()
                    {
                        Content = "No hay formato para GT Radial.",
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
                    Content = "No hay registros para mostrar.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });

            }


        }

        int radios()
        {
            if (LRG.IsChecked == true)
            {
                m_nombreexterno = "MICH BFG UNI ";
                m_nombreempresa = "LLANTAS Y RINES DEL GUADIANA S.A. de C.V.";
                return 1;
            }
            else if (hank.IsChecked == true)
            {
                m_nombreexterno = "HANKOOK ";
                m_nombreempresa = "CENTRO LLANTERO TORNEL S.A. DE C.V.";
                return 2;
            }
            else if (gt.IsChecked == true)
            {

                m_nombreexterno = "GT RADIAL";
                m_nombreempresa = "ARELLANTAS S.A. DE C.V.";
                return 3;
            }
            

            return 0;
        }

        public System.Data.DataTable getventas(int selescted)
        {
            string conect = GetOrigen(selescted);
            System.Data.DataTable Tabla = new System.Data.DataTable();
            string strFuncion = Funcion(selescted);
            try
            {
                using (System.Data.SqlClient.SqlConnection Cnn = new System.Data.SqlClient.SqlConnection(conect))
                {

                    //Cnn.ConnectionTimeout = 500;
                    using (System.Data.SqlClient.SqlCommand Cmd = new System.Data.SqlClient.SqlCommand(strFuncion, Cnn))
                    {
                        try
                        {
                            //
                            Cmd.CommandType = CommandType.Text;
                            Cmd.CommandText = strFuncion;
                            Cmd.CommandTimeout = 0;
                            System.Data.SqlClient.SqlDataAdapter Datos = new System.Data.SqlClient.SqlDataAdapter(Cmd);

                            Cnn.Open();
                            Datos.Fill(Tabla);
                            return Tabla;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error de acceso a datos, Error: " + ex.ToString());
                        }
                        finally
                        {
                            if (Cnn.State == ConnectionState.Open)
                            {
                                Cnn.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception z)
            {
                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Error al acceso de datos .",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
                return Tabla;
            }
        }


        static string GetOrigen(int selected)

        {
            //int selectes = radios();
            string conexion;
            string server;
            string vpn;
            switch (selected)

            {
                case 1:
                    conexion = "SERVER = 192.168.200.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 2:
                    conexion = "SERVER = 192.168.200.2; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007;connection timeout=5000";
                    return conexion;
                    break;
                case 3:
                    conexion = "SERVER = 192.168.200.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 4:
                    vpn = "B21";
                    server = getvpn(vpn);
                    conexion = "SERVER = " + server + "; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007;connection timeout=5000";
                    return conexion;
                    break;
                case 5:
                    vpn = "B22";
                    server = getvpn(vpn);
                    conexion = "SERVER = " + server + "; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
            }
            return "";

        }

        static string getvpn(string b)
        {
            try
            {

                string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
                SqlConnection sqlcon = new SqlConnection(connectionStringer);
                sqlcon.Open();
                string consulta = "Select Address from CfnVentasGeneral where Base = '" + b + "' ";

                SqlDataAdapter sda = new SqlDataAdapter(consulta, sqlcon);

                DataSet dsPubs = new DataSet("Pubs");

                sda.Fill(dsPubs, "CfnVentasGeneral");

                DataTable dtbla = new DataTable();

                dtbla = dsPubs.Tables["CfnVentasGeneral"];

                string vpn = dtbla.Rows[0][0].ToString();

                sqlcon.Close();

                return vpn;
            }
            catch (Exception z)
            {

                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Error al validar ." + z.Message.ToString(),
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }

            return "";
        }


        public string Funcion(int c)
        {
           
            switch (c)
            {


                case 1:
                    return "SELECT  LEFT(lower(dbo.Articulos.Marca), 3) + dbo.Articulos.Codigo_De_Articulo AS Codigo, dbo.Articulos.Codigo_De_Articulo as Codigo_LRG, dbo.Articulos.Marca AS Marca, dbo.Articulos.Descripcion,  " +
        "ISNULL(dbo.vtaPrecios_Lista.PL, 0) AS PL, ISNULL(dbo.vtaScFormaExistrenciasPorSucursalArticulos.Existencia, 0) AS Existencia FROM dbo.vtaScFormaExistrenciasPorSucursalArticulos RIGHT OUTER JOIN  " +
        "dbo.Articulos ON dbo.vtaScFormaExistrenciasPorSucursalArticulos.Código = dbo.Articulos.Codigo_De_Articulo LEFT OUTER JOIN  " +
        "dbo.Lineas LEFT OUTER JOIN dbo.Tipo_De_Agrupacion_De_Lineas ON dbo.Lineas.Codigo_Tipo_De_Agrupacion_De_Lineas = dbo.Tipo_De_Agrupacion_De_Lineas.Codigo_Tipo_De_Agrupacion_De_Lineas ON  " +
        "dbo.Articulos.Codigo_De_Linea = dbo.Lineas.Codigo_De_Linea LEFT OUTER JOIN dbo.vtaPrecios_Lista ON dbo.Articulos.Codigo_De_Articulo = dbo.vtaPrecios_Lista.Codigo_De_Articulo  " +
        "WHERE(dbo.Articulos.Baja_Logica = 0) AND(dbo.Articulos.Marca IN('mich', 'bfg', 'unir')) AND Existencia >=0 and " +
       "(dbo.Tipo_De_Agrupacion_De_Lineas.Codigo_Tipo_De_Agrupacion_De_Lineas IN(1, 2, 3, 4))ORDER BY dbo.Articulos.Marca, dbo.Articulos.Codigo_De_Articulo  ";

                    break;
                case 2:
                    return "SELECT  'hnk' + dbo.Articulos.Codigo_De_Articulo AS Codigo, dbo.Articulos.Codigo_De_Articulo as Codigo_LRG, dbo.Articulos.Marca AS Marca, dbo.Articulos.Descripcion,  " +
        "ISNULL(dbo.vtaPrecios_Lista.PL, 0) AS PL, ISNULL(dbo.vtaScFormaExistrenciasPorSucursalArticulos.Existencia, 0) AS Existencia FROM dbo.vtaScFormaExistrenciasPorSucursalArticulos RIGHT OUTER JOIN  " +
        "dbo.Articulos ON dbo.vtaScFormaExistrenciasPorSucursalArticulos.Código = dbo.Articulos.Codigo_De_Articulo LEFT OUTER JOIN  " +
        "dbo.Lineas LEFT OUTER JOIN dbo.Tipo_De_Agrupacion_De_Lineas ON dbo.Lineas.Codigo_Tipo_De_Agrupacion_De_Lineas = dbo.Tipo_De_Agrupacion_De_Lineas.Codigo_Tipo_De_Agrupacion_De_Lineas ON  " +
        "dbo.Articulos.Codigo_De_Linea = dbo.Lineas.Codigo_De_Linea LEFT OUTER JOIN dbo.vtaPrecios_Lista ON dbo.Articulos.Codigo_De_Articulo = dbo.vtaPrecios_Lista.Codigo_De_Articulo  " +
        "WHERE(dbo.Articulos.Baja_Logica = 0) AND(dbo.Articulos.Marca IN('HANK')) AND Existencia >=0 and " +
       "(dbo.Tipo_De_Agrupacion_De_Lineas.Codigo_Tipo_De_Agrupacion_De_Lineas IN(1, 2, 3, 4))ORDER BY dbo.Articulos.Marca, dbo.Articulos.Codigo_De_Articulo  ";
                    break;
                case 3:

                  return "SELECT  LEFT(lower(dbo.Articulos.Marca), 3) + dbo.Articulos.Codigo_De_Articulo AS Codigo, dbo.Articulos.Codigo_De_Articulo as Codigo_LRG, dbo.Articulos.Marca AS Marca, dbo.Articulos.Descripcion,  " +
        "ISNULL(dbo.vtaPrecios_Lista.PL, 0) AS PL, ISNULL(dbo.vtaScFormaExistrenciasPorSucursalArticulos.Existencia, 0) AS Existencia FROM dbo.vtaScFormaExistrenciasPorSucursalArticulos RIGHT OUTER JOIN  " +
        "dbo.Articulos ON dbo.vtaScFormaExistrenciasPorSucursalArticulos.Código = dbo.Articulos.Codigo_De_Articulo LEFT OUTER JOIN  " +
        "dbo.Lineas LEFT OUTER JOIN dbo.Tipo_De_Agrupacion_De_Lineas ON dbo.Lineas.Codigo_Tipo_De_Agrupacion_De_Lineas = dbo.Tipo_De_Agrupacion_De_Lineas.Codigo_Tipo_De_Agrupacion_De_Lineas ON  " +
        "dbo.Articulos.Codigo_De_Linea = dbo.Lineas.Codigo_De_Linea LEFT OUTER JOIN dbo.vtaPrecios_Lista ON dbo.Articulos.Codigo_De_Articulo = dbo.vtaPrecios_Lista.Codigo_De_Articulo  " +
        "WHERE(dbo.Articulos.Baja_Logica = 0) AND(dbo.Articulos.Marca IN('mich', 'bfg', 'unir')) AND Existencia >=0 and " +
       "(dbo.Tipo_De_Agrupacion_De_Lineas.Codigo_Tipo_De_Agrupacion_De_Lineas IN(1, 2, 3, 4))ORDER BY dbo.Articulos.Marca, dbo.Articulos.Codigo_De_Articulo  ";
                    break;
                case 4:
                    return "SELECT  TOP 100 PERCENT   Folio_Del_Documento, convert(varchar, Fecha_Del_Documento, 103) as Fecha, Numero_De_Cliente, Nombre, Estado, Tipo_De_Documento, Forma_De_Pago,  " +
       " fncReporteadorDeVentas.Codigo_De_Articulo, Articulo, [Linea], fncReporteadorDeVentas.Marca, Codigo_De_Nivel_De_Precios, Nivel_De_Precio, Cantidad, Total, fncReporteadorDeVentas.Unidad,  " +
       "Codigo_De_Empleado,  " +
       "Empleado, Puesto, Codigo_De_Sucursal, Sucursal, dbo.Articulos.Rango_De_Velocidad, dbo.Tipo_De_Aplicacion.Descripcion AS Aplicacion, [Condición De Pago], [Tipo De Cliente], Month(Fecha_Del_Documento) As Mes, Year(Fecha_Del_Documento) as Anio " +
       "FROM         dbo.Tipo_De_Aplicacion LEFT OUTER JOIN " +
       "dbo.Articulos ON dbo.Tipo_De_Aplicacion.Codigo_Tipo_De_Aplicacion = dbo.Articulos.Codigo_Tipo_De_Aplicacion RIGHT  " +
       "OUTER JOIN " +
       "dbo.fncReporteadorDeVentas() fncReporteadorDeVentas ON dbo.Articulos.Codigo_De_Articulo = fncReporteadorDeVentas.Codigo_De_Articulo " +
       "WHERE (Codigo_De_Estatus_De_Documento = 1)   " +
       "ORDER BY Numero_Corto_De_Sucursal, Fecha_Del_Documento, Folio_Del_Documento ";
                    break;
                case 5:
                    return "SELECT  TOP 100 PERCENT   Folio_Del_Documento, convert(varchar, Fecha_Del_Documento, 103) as Fecha, Numero_De_Cliente, Nombre, Estado, Tipo_De_Documento, Forma_De_Pago,  " +
       " fncReporteadorDeVentas.Codigo_De_Articulo, Articulo, [Linea], fncReporteadorDeVentas.Marca, Codigo_De_Nivel_De_Precios, Nivel_De_Precio, Cantidad, Total, fncReporteadorDeVentas.Unidad,  " +
       "Codigo_De_Empleado,  " +
       "Empleado, Puesto, Codigo_De_Sucursal, Sucursal, dbo.Articulos.Rango_De_Velocidad, dbo.Tipo_De_Aplicacion.Descripcion AS Aplicacion, [Condición De Pago], [Tipo De Cliente], Month(Fecha_Del_Documento) As Mes, Year(Fecha_Del_Documento) as Anio " +
       "FROM         dbo.Tipo_De_Aplicacion LEFT OUTER JOIN " +
       "dbo.Articulos ON dbo.Tipo_De_Aplicacion.Codigo_Tipo_De_Aplicacion = dbo.Articulos.Codigo_Tipo_De_Aplicacion RIGHT  " +
       "OUTER JOIN " +
       "dbo.fncReporteadorDeVentas() fncReporteadorDeVentas ON dbo.Articulos.Codigo_De_Articulo = fncReporteadorDeVentas.Codigo_De_Articulo " +
       "WHERE  (Codigo_De_Estatus_De_Documento = 1)   " +
       "ORDER BY Numero_Corto_De_Sucursal, Fecha_Del_Documento, Folio_Del_Documento ";
                    break;


            }
            return "";
        
    }
}
}

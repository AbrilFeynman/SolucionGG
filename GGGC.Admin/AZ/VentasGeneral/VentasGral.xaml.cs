using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace GGGC.Admin.AZ.VentasGeneral
{
    /// <summary>
    /// Interaction logic for VentasGral.xaml
    /// </summary>
    public partial class VentasGral : UserControl
    {
        private delegate void NoArgDelegate();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int increment;
        private string m_nombreexterno;
        private string m_nombreempresa;
        private int m_select;
        private CashierStationMainWindowModel vm;
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
        public VentasGral()
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-ES");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            
            InitializeComponent();
            vm = new CashierStationMainWindowModel();
            this.DataContext = vm;

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
            vm.IsBusy = enable;
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
            if (m_select > 0 && dtinicial.SelectedDate != null && dtfinal.SelectedDate != null )
            {
                int yInicial = Convert.ToInt16(Convert.ToDateTime(dtinicial.SelectedDate).Year.ToString());
                int yFinal = Convert.ToInt16(Convert.ToDateTime(dtfinal.SelectedDate).Year.ToString());
                if (yInicial >= 2017 && yFinal >= 2017 )
                {
                    if (dtinicial.SelectedDate <= dtfinal.SelectedDate)
                    {
                       

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

                        //Exportar.IsEnabled = true;
                        // buttons.IsEnabled = true;
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
            
        }

        
        private void generarreporte(int selected)
        {
            
            dtConsulta = getventas(selected);
            if(dtConsulta.Rows.Count > 0) {

                int sucursales = radios();
                if (sucursales == 1 )
                {
                    string strFechaInicial = Convert.ToDateTime(dtinicial.SelectedDate).ToString("dd-MMM-yyyy").ToUpper();
                    string strFechaFinal = Convert.ToDateTime(dtfinal.SelectedDate).ToString("dd-MMM-yyyy").ToUpper();



                    DateTime date = DateTime.Now;
                    string datewithformat = date.ToString();
                    string dateday = date.ToString("dd MM yyyy");
                    string closure = date.ToString("dd MM yyyy HH mm");
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
                        worksheet.AutoFilters.FilterRange = worksheet.Range["A2:AG2"];

                        worksheet.Range["A1"].Text = m_nombreempresa+" - VENTAS GRAL " + m_nombreexterno + strFechaInicial + " Al " + strFechaFinal;
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
                        worksheet.SetColumnWidth(1, 10);
                        worksheet.SetColumnWidth(2, 10);
                        worksheet.SetColumnWidth(3, 10);
                        worksheet.SetColumnWidth(4, 10);
                        worksheet.SetColumnWidth(5, 12);//Estado
                        worksheet.SetColumnWidth(6, 7);
                        worksheet.SetColumnWidth(7, 10);
                        worksheet.SetColumnWidth(8, 10);
                        worksheet.SetColumnWidth(9, 45);
                        worksheet.SetColumnWidth(10, 10);//Linea
                        worksheet.SetColumnWidth(11, 12);
                        worksheet.SetColumnWidth(12, 7);
                        worksheet.SetColumnWidth(13, 10);
                        worksheet.SetColumnWidth(14, 5);
                        worksheet.SetColumnWidth(15, 15);
                        worksheet.SetColumnWidth(16, 5);
                        worksheet.SetColumnWidth(17, 4);
                        worksheet.SetColumnWidth(18, 4);
                        worksheet.SetColumnWidth(19, 4);
                        worksheet.SetColumnWidth(20, 4);
                        worksheet.SetColumnWidth(21, 40);
                        worksheet.SetColumnWidth(22, 20);
                        worksheet.SetColumnWidth(23, 5);
                        worksheet.SetColumnWidth(24, 20);
                        worksheet.SetColumnWidth(26, 12);

                        IStyle pStyles = workbook.Styles.Add("pStyles");
                        pStyles.BeginUpdate();
                        worksheet.Columns[3].HorizontalAlignment = ExcelHAlign.HAlignLeft;
                        worksheet.Columns[8].HorizontalAlignment = ExcelHAlign.HAlignLeft;

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
                        string chorchill = "O2,O" + cossacks + "";
                        string russevel = "O" + rusia + "";
                        string ftotal = "O" + rusia + "";
                        string qty = "N" + rusia + "";
                        string fpl = "AA" + rusia + "";
                        string totalr = "A" + rusia + "";
                        worksheet.Range[totalr].Text = registros + " Registros";
                        worksheet.Range[totalr].CellStyle = pStyle;
                        string nrusia = "=SUBTOTAL(9,O2:O" + cossacks + ")";
                        worksheet.Range[russevel].Formula = nrusia;
                        worksheet.Range[russevel].CellStyle = pStyle;
                        worksheet.Range["AA2:" + fpl].NumberFormat = "#,##0.00";
                        worksheet.Range["O2:" + ftotal].NumberFormat = "#,##0.00";
                        string sumqty = "=SUBTOTAL(9,N2:N" + cossacks + ")";
                        worksheet.Range[qty].Formula = sumqty;
                        worksheet.Range[qty].CellStyle = pStyle;
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        //table.Columns[5].TotalsCalculation = ExcelTotalsCalculation.Sum;
                        //hacer el subtotal pero conformula ** el otro marca error con total calculation 
                        //range.SubTotal(0, ConsolidationFunction.Sum, new int[] {1,rojos});
                        string namer = dateday;
                        string fileName = @"C:\BIG\LRG\Excel\REP. VENTAS GRAL " + m_nombreexterno + strFechaInicial + " Al " + strFechaFinal + "( " + closure + " )" + ".xlsx";
                        // string fileName = "LRG-Existencias al " + namer + "B4 Francisco Villa.xlsx";
                        workbook.SaveAs(fileName);
                        //workbook.Close();
                        //excelEngine.Dispose();
                        string argument = @"/select, " + fileName;

                        System.Diagnostics.Process.Start("explorer.exe", argument);
                    }


                }
                else //base VPN
                {
                    string strFechaInicial = Convert.ToDateTime(dtinicial.SelectedDate).ToString("dd-MMM-yyyy").ToUpper();
                    string strFechaFinal = Convert.ToDateTime(dtfinal.SelectedDate).ToString("dd-MMM-yyyy").ToUpper();



                    DateTime date = DateTime.Now;
                    string datewithformat = date.ToString();
                    string dateday = date.ToString("dd MM yyyy");
                    string closure = date.ToString("dd MM yyyy HH mm");
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
                        worksheet.AutoFilters.FilterRange = worksheet.Range["A2:AG2"];

                        worksheet.Range["A1"].Text = m_nombreempresa+" - VENTAS GRAL " + m_nombreexterno + strFechaInicial + " Al " + strFechaFinal;
                        // worksheet.Range["A1"].Text = "Llantas y Rines del Guadiana S.A. de C.V. - Existencias LRG Al "+dateday+"- B4 Francisco Villa";
                        worksheet.Rows[1].FreezePanes();
                        worksheet.Rows[2].FreezePanes();

                        #region
                        IStyle headerStyle = workbook.Styles.Add("HeaderStyle");
                        headerStyle.BeginUpdate();

                      

                       
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
                        pStyle.Font.Color = ExcelKnownColors.White;
                        //workbook.SetPaletteColor(9, System.Drawing.Color.FromArgb(89, 171, 227));

                        if (m_select == 2)
                        {
                            pStyle.Color = System.Drawing.Color.Orange;
                        }
                        else if (m_select == 3 || m_select == 4)
                        {
                            pStyle.Color = System.Drawing.Color.Red;
                        }
                        else
                        {
                            pStyle.Color = System.Drawing.Color.Gray;
                        }
                       

                        pStyle.Font.Bold = true;

                        pStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;

                        pStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        pStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;

                        pStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                        pStyle.EndUpdate();
                        #endregion

                        #region //ancho y alineacion columnas
                        worksheet.Rows[0].CellStyle = pStyle;
                        worksheet.SetColumnWidth(1, 10);
                        worksheet.SetColumnWidth(2, 10);
                        worksheet.SetColumnWidth(3, 10);
                        worksheet.SetColumnWidth(4, 10);
                        worksheet.SetColumnWidth(5, 12);//Estado
                        worksheet.SetColumnWidth(6, 7);
                        worksheet.SetColumnWidth(7, 10);
                        worksheet.SetColumnWidth(8, 10);
                        worksheet.SetColumnWidth(9, 45);
                        worksheet.SetColumnWidth(10, 10);//Linea
                        worksheet.SetColumnWidth(11, 12);
                        worksheet.SetColumnWidth(12, 7);
                        worksheet.SetColumnWidth(13, 10);
                        worksheet.SetColumnWidth(14, 5);
                        worksheet.SetColumnWidth(15, 15);
                        worksheet.SetColumnWidth(16, 5);
                        worksheet.SetColumnWidth(17, 4);
                        worksheet.SetColumnWidth(18, 40);
                        worksheet.SetColumnWidth(19, 20);
                        worksheet.SetColumnWidth(20, 4);
                        worksheet.SetColumnWidth(21, 17);
                        worksheet.SetColumnWidth(22, 7);
                        worksheet.SetColumnWidth(23, 15);
                        worksheet.SetColumnWidth(24, 15);
                        worksheet.SetColumnWidth(25, 15);
                        worksheet.SetColumnWidth(26, 7);
                        

                        IStyle pStyles = workbook.Styles.Add("pStyles");
                        pStyles.BeginUpdate();
                        worksheet.Columns[3].HorizontalAlignment = ExcelHAlign.HAlignLeft;
                        worksheet.Columns[8].HorizontalAlignment = ExcelHAlign.HAlignLeft;
                        worksheet.Columns[7].HorizontalAlignment = ExcelHAlign.HAlignLeft;
                        worksheet.Columns[21].HorizontalAlignment = ExcelHAlign.HAlignRight;

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
                        string chorchill = "O2,O" + cossacks + "";
                        string russevel = "O" + rusia + "";
                        string ftotal = "O" + rusia + "";
                        string ffecha = "B" + rusia + "";
                        string qty = "N" + rusia + "";
                       
                        string totalr = "A" + rusia + "";
                        worksheet.Range[totalr].Text = registros + " Registros";
                        worksheet.Range[totalr].CellStyle = pStyle;
                        string nrusia = "=SUBTOTAL(9,O2:O" + cossacks + ")";
                        worksheet.Range[russevel].Formula = nrusia;
                        worksheet.Range[russevel].CellStyle = pStyle;
                       
                        worksheet.Range["O2:" + ftotal].NumberFormat = "#,##0.00";
                        
                        string sumqty = "=SUBTOTAL(9,N2:N" + cossacks + ")";
                        worksheet.Range[qty].Formula = sumqty;
                        worksheet.Range[qty].CellStyle = pStyle;
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        //table.Columns[5].TotalsCalculation = ExcelTotalsCalculation.Sum;
                        //hacer el subtotal pero conformula ** el otro marca error con total calculation 
                        //range.SubTotal(0, ConsolidationFunction.Sum, new int[] {1,rojos});
                        string namer = dateday;
                        string fileName = @"C:\BIG\LRG\Excel\REP. VENTAS GRAL " + m_nombreexterno + strFechaInicial + " Al " + strFechaFinal + "( " + closure + " )" + ".xlsx";
                        // string fileName = "LRG-Existencias al " + namer + "B4 Francisco Villa.xlsx";
                        workbook.SaveAs(fileName);
                        //workbook.Close();
                        //excelEngine.Dispose();
                        string argument = @"/select, " + fileName;

                        System.Diagnostics.Process.Start("explorer.exe", argument);

                    }
                }

            }
            else {
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
                m_nombreexterno = "LRG ";
                m_nombreempresa = "LLANTAS Y RINES DEL GUADIANA S.A. de C.V.";
                return 1; }
            else if (CLT.IsChecked == true)
            {
                m_nombreexterno = "CLT ";
                m_nombreempresa = "CENTRO LLANTERO TORNEL S.A. DE C.V.";
                return 2; }
            else if (B20.IsChecked == true)
            {

                m_nombreexterno = "BASE 20 ";
                m_nombreempresa = "ARELLANTAS S.A. DE C.V.";
                return 3; }
            else if (B21.IsChecked == true)
            {

                m_nombreexterno = "BASE 21 ";
                m_nombreempresa = "ARELLANTAS S.A. DE C.V.";
                return 4; }
            else if (B22.IsChecked == true)
            {

                m_nombreexterno = "BASE 22 ";
                m_nombreempresa = "PROVEDOR DE LLANTAS Y SERVICIOS";
                return 5; }

            return 0;
        }

        public  System.Data.DataTable getventas(int selescted)
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
                    Content = "Error al acceso de datos ." ,
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
                return Tabla;
            }
        }


        static string  GetOrigen(int selected)

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
                    vpn = "B20";
                    server= getvpn(vpn);
                    conexion = "SERVER = "+server+"; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 4:
                    vpn = "B21";
                    server=getvpn(vpn);
                    conexion = "SERVER = " + server + "; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007;connection timeout=5000";
                    return conexion;
                    break;
                case 5:
                    vpn = "B22";
                    server=getvpn(vpn);
                    conexion = "SERVER = " + server + "; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
            }
            return "";

        }

        static string getvpn(string b)
        {
            try {

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
                    Content = "Error al validar ."+z.Message.ToString(),
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }

            return "";
        }


        public string Funcion(int c)
        {
            string strFechaInicial = Convert.ToDateTime(dtinicial.SelectedDate).ToString("MM/dd/yyyy HH:mm:ss");
            string strFechaFinal = Convert.ToDateTime(dtfinal.SelectedDate).ToString("MM/dd/yyyy HH:mm:ss");
            switch (c)
            {

           
                case 1:
                    return "SELECT  TOP 100 PERCENT   Folio_Del_Documento,convert(varchar, Fecha_Del_Documento, 103) as Fecha , Numero_De_Cliente, Nombre, Estado, Tipo_De_Documento, Forma_De_Pago, " +
        " fncReporteadorDeVentas.Codigo_De_Articulo, Articulo, [Linea], fncReporteadorDeVentas.Marca, Codigo_De_Nivel_De_Precios, Nivel_De_Precio, Cantidad, Total, fncReporteadorDeVentas.Unidad, fncReporteadorDeVentas.Ancho, fncReporteadorDeVentas.Serie, fncReporteadorDeVentas.Rin,  " +
        "Codigo_De_Empleado,  " +
        "Empleado, Puesto, Codigo_De_Sucursal, Sucursal, dbo.Articulos.Rango_De_Velocidad, convert(varchar, Fecha_De_Vencimiento, 103) AS Aplicacion, Precio_De_Lista, Precio_De_Venta, dbo.gg_fncPrecioDeListaActual(fncReporteadorDeVentas.Codigo_De_Articulo) AS PrecioDeListaActual, [Condición De Pago], Month(Fecha_Del_Documento) As Mes, Year(Fecha_Del_Documento) as Anio, dbo.EDI_fncTipoDeCliente(Numero_De_Cliente) AS NoEDI " +
        "FROM         dbo.Tipo_De_Aplicacion LEFT OUTER JOIN " +
        "dbo.Articulos ON dbo.Tipo_De_Aplicacion.Codigo_Tipo_De_Aplicacion = dbo.Articulos.Codigo_Tipo_De_Aplicacion RIGHT  " +
        "OUTER JOIN " +
        "dbo.fncReporteadorDeVentas() fncReporteadorDeVentas ON dbo.Articulos.Codigo_De_Articulo = fncReporteadorDeVentas.Codigo_De_Articulo " +
        "WHERE (Fecha_Del_Documento BETWEEN '" + strFechaInicial + "' AND '" + strFechaFinal + "' ) AND (Codigo_De_Estatus_De_Documento = 1)    " +
        " ORDER BY Numero_Corto_De_Sucursal, Fecha_Del_Documento ";
                    break;
                case 2:
                    return "SELECT  TOP 100 PERCENT   Folio_Del_Documento, convert(varchar, Fecha_Del_Documento, 103) as Fecha, Numero_De_Cliente, Nombre, Estado, Tipo_De_Documento, Forma_De_Pago,  " +
        " fncReporteadorDeVentas.Codigo_De_Articulo, Articulo, [Linea], fncReporteadorDeVentas.Marca, Codigo_De_Nivel_De_Precios, Nivel_De_Precio, Cantidad, Total, fncReporteadorDeVentas.Unidad,  " +
        "Codigo_De_Empleado,  " +
        "Empleado, Puesto, Codigo_De_Sucursal, Sucursal, dbo.Articulos.Rango_De_Velocidad, dbo.Tipo_De_Aplicacion.Descripcion AS Aplicacion, [Condición De Pago], [Tipo De Cliente], Month(Fecha_Del_Documento) As Mes, Year(Fecha_Del_Documento) as Anio " +
        "FROM         dbo.Tipo_De_Aplicacion LEFT OUTER JOIN " +
        "dbo.Articulos ON dbo.Tipo_De_Aplicacion.Codigo_Tipo_De_Aplicacion = dbo.Articulos.Codigo_Tipo_De_Aplicacion RIGHT  " +
        "OUTER JOIN " +
        "dbo.fncReporteadorDeVentas() fncReporteadorDeVentas ON dbo.Articulos.Codigo_De_Articulo = fncReporteadorDeVentas.Codigo_De_Articulo " +
        "WHERE (Fecha_Del_Documento BETWEEN '" + strFechaInicial + "' AND '" + strFechaFinal + "' ) AND (Codigo_De_Estatus_De_Documento = 1)   " +
        "ORDER BY Numero_Corto_De_Sucursal, Fecha_Del_Documento, Folio_Del_Documento ";
                    break;
                case 3:

                    return "SELECT  TOP 100 PERCENT   Folio_Del_Documento, convert(varchar, Fecha_Del_Documento, 103) as Fecha, Numero_De_Cliente, Nombre, Estado, Tipo_De_Documento, Forma_De_Pago,  " +
        " fncReporteadorDeVentas.Codigo_De_Articulo, Articulo, [Linea], fncReporteadorDeVentas.Marca, Codigo_De_Nivel_De_Precios, Nivel_De_Precio, Cantidad, Total, fncReporteadorDeVentas.Unidad,  " +
        "Codigo_De_Empleado,  " +
        "Empleado, Puesto, Codigo_De_Sucursal, Sucursal, dbo.Articulos.Rango_De_Velocidad, dbo.Tipo_De_Aplicacion.Descripcion AS Aplicacion, [Condición De Pago], [Tipo De Cliente], Month(Fecha_Del_Documento) As Mes, Year(Fecha_Del_Documento) as Anio " +
        "FROM         dbo.Tipo_De_Aplicacion LEFT OUTER JOIN " +
        "dbo.Articulos ON dbo.Tipo_De_Aplicacion.Codigo_Tipo_De_Aplicacion = dbo.Articulos.Codigo_Tipo_De_Aplicacion RIGHT  " +
        "OUTER JOIN " +
        "dbo.fncReporteadorDeVentas() fncReporteadorDeVentas ON dbo.Articulos.Codigo_De_Articulo = fncReporteadorDeVentas.Codigo_De_Articulo " +
        "WHERE (Fecha_Del_Documento BETWEEN '" + strFechaInicial + "' AND '" + strFechaFinal + "' ) AND (Codigo_De_Estatus_De_Documento = 1)   " +
        "ORDER BY Numero_Corto_De_Sucursal, Fecha_Del_Documento, Folio_Del_Documento ";
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
       "WHERE (Fecha_Del_Documento BETWEEN '" + strFechaInicial + "' AND '" + strFechaFinal + "' ) AND (Codigo_De_Estatus_De_Documento = 1)   " +
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
       "WHERE (Fecha_Del_Documento BETWEEN '" + strFechaInicial + "' AND '" + strFechaFinal + "' ) AND (Codigo_De_Estatus_De_Documento = 1)   " +
       "ORDER BY Numero_Corto_De_Sucursal, Fecha_Del_Documento, Folio_Del_Documento ";
                    break;


            }
            return "";
        }
    }
}

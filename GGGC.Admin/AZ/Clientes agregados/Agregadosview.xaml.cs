using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace GGGC.Admin.AZ.Clientes_agregados
{
    /// <summary>
    /// Interaction logic for Agregadosview.xaml
    /// </summary>
    public partial class Agregadosview : UserControl
    {

        private delegate void NoArgDelegate();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int increment;
        private string m_nombreexterno;
        private string m_nombreempresa;
        private int m_select;
       
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
        public Agregadosview()
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-ES");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            InitializeComponent();

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
            if (m_select > 0 && dtinicial.SelectedDate != null && dtfinal.SelectedDate != null)
            {
                int yInicial = Convert.ToInt16(Convert.ToDateTime(dtinicial.SelectedDate).Year.ToString());
                int yFinal = Convert.ToInt16(Convert.ToDateTime(dtfinal.SelectedDate).Year.ToString());
                if (yInicial >= 2017 && yFinal >= 2017)
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
            if (dtConsulta.Rows.Count > 0)
            {

                int tipo = radios();
                if (tipo == 1 )//credito
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
                        worksheet.ImportDataTable(tabla, true, 2, 1);
                        worksheet.AutoFilters.FilterRange = worksheet.Range["A2:AG2"];

                        worksheet.Range["A1"].Text = "Llantas y Rines del Guadiana S.A. de C.V.";
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

                        pStyle.Color = System.Drawing.Color.DeepSkyBlue;

                        pStyle.Font.Color = ExcelKnownColors.White;
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
                        worksheet.SetColumnWidth(3, 30);
                        worksheet.SetColumnWidth(4, 10);
                        worksheet.SetColumnWidth(5, 12);//Estado
                        
                        worksheet.SetColumnWidth(13, 15);



                        IStyle pStyles = workbook.Styles.Add("pStyles");
                        pStyles.BeginUpdate();
                        worksheet.Columns[1].HorizontalAlignment = ExcelHAlign.HAlignLeft;
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
                 
                        string chorchill = "O2,O" + cossacks + "";
                        string russevel = "O" + rusia + "";
                        string ftotal = "O" + rusia + "";
                        string qty = "N" + rusia + "";
                        string fpl = "AA" + rusia + "";
                        string totalr = "A" + rusia + "";
                        worksheet.Range[totalr].Text = registros + " Registros";
                        worksheet.Range[totalr].CellStyle = pStyle;
                       
                       
                       
                        worksheet.Range["AA2:" + fpl].NumberFormat = "#,##0.00";
                        worksheet.Range["O2:" + ftotal].NumberFormat = "#,##0.00";
                       
                        
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        //table.Columns[5].TotalsCalculation = ExcelTotalsCalculation.Sum;
                        //hacer el subtotal pero conformula ** el otro marca error con total calculation 
                        //range.SubTotal(0, ConsolidationFunction.Sum, new int[] {1,rojos});
                        string namer = dateday;
                        string fileName = @"C:\BIG\LRG\Excel\Rep. Clientes "+ m_nombreempresa + " Agregados Del - " + strFechaInicial + " Al " + strFechaFinal + "( " + closure + " )" + ".xlsx";
                        // string fileName = "LRG-Existencias al " + namer + "B4 Francisco Villa.xlsx";
                        workbook.SaveAs(fileName);
                        //workbook.Close();
                        //excelEngine.Dispose();
                        string argument = @"/select, " + fileName;

                        System.Diagnostics.Process.Start("explorer.exe", argument);
                    }


                }
                else //base Contado
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
                        worksheet.ImportDataTable(tabla, true, 2, 1);
                        worksheet.AutoFilters.FilterRange = worksheet.Range["A2:AG2"];

                        worksheet.Range["A1"].Text = "Llantas y Rines del Guadiana S.A. de C.V.";
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

                        pStyle.Color = System.Drawing.Color.DeepSkyBlue;

                        pStyle.Font.Color = ExcelKnownColors.White;
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
                        worksheet.SetColumnWidth(3, 30);
                        worksheet.SetColumnWidth(4, 10);
                        worksheet.SetColumnWidth(5, 14);//Estado

                        worksheet.SetColumnWidth(6, 14);
                        worksheet.SetColumnWidth(8, 27);
                        worksheet.SetColumnWidth(9, 27);
                        worksheet.SetColumnWidth(15, 12);



                        IStyle pStyles = workbook.Styles.Add("pStyles");
                        pStyles.BeginUpdate();
                        worksheet.Columns[1].HorizontalAlignment = ExcelHAlign.HAlignLeft;
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

                        string chorchill = "O2,O" + cossacks + "";
                        string russevel = "O" + rusia + "";
                        string ftotal = "O" + rusia + "";
                        string qty = "N" + rusia + "";
                        string fpl = "AA" + rusia + "";
                        string totalr = "A" + rusia + "";
                        worksheet.Range[totalr].Text = registros + " Registros";
                        worksheet.Range[totalr].CellStyle = pStyle;



                     


                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        //table.Columns[5].TotalsCalculation = ExcelTotalsCalculation.Sum;
                        //hacer el subtotal pero conformula ** el otro marca error con total calculation 
                        //range.SubTotal(0, ConsolidationFunction.Sum, new int[] {1,rojos});
                        string namer = dateday;
                        string fileName = @"C:\BIG\LRG\Excel\Rep. Clientes " + m_nombreempresa + " Agregados Del - " + strFechaInicial + " Al " + strFechaFinal + "( " + closure + " )" + ".xlsx";
                        // string fileName = "LRG-Existencias al " + namer + "B4 Francisco Villa.xlsx";
                        workbook.SaveAs(fileName);
                        //workbook.Close();
                        //excelEngine.Dispose();
                        string argument = @"/select, " + fileName;

                        System.Diagnostics.Process.Start("explorer.exe", argument);
                    }



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
            if (credito.IsChecked == true)
            {
                m_nombreexterno = "Clientes Agregados";
                m_nombreempresa = "De Crédito";
                return 1;
            }
            else if (contado.IsChecked == true)
            {
                m_nombreexterno = "Clientes Agregados";
                m_nombreempresa = "De Contado";
                return 2;
            }


            return 0;
        }

        public System.Data.DataTable getventas(int selescted)
        {
            System.Data.DataTable Tabla = new System.Data.DataTable();
            string conect = GetOrigen(selescted);
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
                            return Tabla;
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
            catch (Exception ex)
            {
                return Tabla;
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


        static string GetOrigen(int selected)

        {




            string conexion;
                    conexion = "SERVER = 192.168.200.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
       

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
            string strFechaInicial = Convert.ToDateTime(dtinicial.SelectedDate).ToString("MM/dd/yyyy HH:mm:ss");
            string strFechaFinal = Convert.ToDateTime(dtfinal.SelectedDate).ToString("MM/dd/yyyy HH:mm:ss");
            switch (c)
            {


                case 1:
                    return "SELECT   Numero_De_Cliente,RFC,Nombre,Direccion,Colonia,CP,Ciudad,Estado,Pais,Telefono_Personal,Telefono_Movil,Tipo_De_Cliente, convert(varchar, Fecha_De_Alta, 103) as Fecha_De_Alta " +
                 "FROM Clientes " +
                  "WHERE (Fecha_De_Alta BETWEEN '" + strFechaInicial + "' AND'" + strFechaFinal + "' ) " +
                  "ORDER BY Fecha_De_Alta DESC";
                    break;
                case 2:
                    return "SELECT  TOP 100 PERCENT Id_Radial, Tipo_De_Persona, Nombre, Apellido_Paterno, Apellido_Materno, Telefono_Personal, Correo_Electronico, Direccion, Colonia, CP, Ciudad," +
         "Estado, Pais, RFC, CONVERT(Varchar, Fecha_Y_Hora_De_Ultima_Actualizacion, 103) as Fechasubida " +
         "FROM Clientes_Frecuentes " +
         "WHERE (Fecha_Y_Hora_De_Ultima_Actualizacion  BETWEEN '" + strFechaInicial + "' AND'" + strFechaFinal + "')" +
         "ORDER BY Fecha_Y_Hora_De_Ultima_Actualizacion DESC";
                    break;
               


            }
            return "";
        }



    }
}

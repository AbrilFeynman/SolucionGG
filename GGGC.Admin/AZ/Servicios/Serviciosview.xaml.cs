using Syncfusion.XlsIO;
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

namespace GGGC.Admin.AZ.Servicios
{
    /// <summary>
    /// Interaction logic for Serviciosview.xaml
    /// </summary>
    public partial class Serviciosview : UserControl
    {

        DataTable dtblcatalogo = new DataTable();
        private delegate void NoArgDelegate();
        byte streditable = 0;
        int intcogid = 0;
        public Serviciosview()
        {
            InitializeComponent();
            cargardatos();
            llenarcombox();
            llenarConsulta();
            StyleManager.SetTheme(InvoiceRemision, new Windows8Theme());
        }
        private void llenarcombox()
        {


            marca.Items.Add("SERV");
            
           
            linea.Items.Add("Servicio Camión");
            linea.Items.Add("Servicio Auto y Camineta");
            linea.Items.Add("Servicio Taller Móvil");
            linea.Items.Add("Red Servicios Michelin");

            impuestos.Items.Add("IVA 0%");
            impuestos.Items.Add("IVA 11%");
            impuestos.Items.Add("IVA 16%");

            unidad.Items.Add("Servicio");

            status.Items.Add("Activo");
            status.Items.Add("Baja");



        }
        private void llenarConsulta()
        {
            try
            {

                string conect = "SERVER = gggctserver.database.windows.net; DATABASE =devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099";
                SqlConnection con = new SqlConnection(conect);

                string cmd = "SELECT[Codigo_De_Articulo] AS CODIGO,[Descripcion] AS DESCRIPCION,[Codigo_Editable] AS CODIGO_EDITABLE,[Linea] AS LINEA,[Marca] AS MARCA,[Impuesto] AS IMPUESTO,[Unidad] AS UNIDAD,[Baja_Logica] AS ESTATUS,[Comision_Mayor] AS COMISION_MAYOR_META,[Comision_Menor] AS COMISION_MENOR_META,convert(varchar, Fecha_Y_Hora_De_Ultima_Actualizacion, 103) as FECHA_ALTA FROM[dbo].[Servicios] order by  [Fecha_Y_Hora_De_Ultima_Actualizacion] desc ";
                con.Open();

                DataTable dtbl = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);

                DataSet dsPubs = new DataSet("Pubs");

                sda.Fill(dsPubs, "Servicios");

                dtbl = dsPubs.Tables["Servicios"];

                InvoiceRemision.ItemsSource = dsPubs.Tables["Servicios"].DefaultView;
                //dataGrid2.Columns[0].IsVisible = false;
                dtblcatalogo = dtbl;
                con.Close();
            }

            catch (SqlException ex)
            {

                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Revise su conexón a internet.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }

        }
        private void cargardatos()
        {
            try
            {
                string conect = "SERVER = gggctserver.database.windows.net; DATABASE =devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099";
                SqlConnection con = new SqlConnection(conect);

                string cmd = "SELECT TOP (1) [Prefijo],Numeroservicio  FROM [dbo].[DatosServicios] ";
                con.Open();


                SqlDataAdapter sda = new SqlDataAdapter(cmd, conect);

                DataSet dsPubs = new DataSet("Pubs");

                sda.Fill(dsPubs, "DatosServicios");

                DataTable dtbl = new DataTable();

                dtbl = dsPubs.Tables["DatosServicios"];

                folio.Content = dtbl.Rows[0][1].ToString() ;
                intcogid =Convert.ToInt32(dtbl.Rows[0][1].ToString());

                con.Close();
            }
            catch (SqlException ex)
            {




                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "No se pudo conectar a la base de  datos.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }

        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true)
            {
                txteditable.IsEnabled = true;
                txteditable.Text = "SERV ";
                streditable = 1;
            }
            else if (checkBox.IsChecked == false)
            {
                txteditable.IsEnabled = false;
                streditable = 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrWhiteSpace(codigo.Text) || string.IsNullOrWhiteSpace(descripcion.Text) || string.IsNullOrEmpty(linea.Text) || string.IsNullOrEmpty(marca.Text) || string.IsNullOrEmpty(impuestos.Text) || string.IsNullOrEmpty(unidad.Text) || string.IsNullOrEmpty(status.Text))
            {

                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Es necesario llenar todos los campos del formulario.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });

            }
            else
            {
                guardar();
            }

        }

        private void Exportar_Click_1(object sender, RoutedEventArgs e)
        {
            Refresh(CashierBusyIndicator);
            generar();


        }




        private async void generar()
        {
            //button.Content = "Running...";

            var result = await RunningProcessAsync();
            Exportar.Content = "Exportar";
            enableControls();

        }

        private void enableControls()
        {
            Exportar.IsEnabled = true;


        }
        public static void Refresh(DependencyObject obj)
        {

            obj.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle,

                (NoArgDelegate)delegate { });
        }
        private async Task<string> RunningProcessAsync()
        {

            Exportar.Content = "Exportando...";
            Refresh(Exportar);
            CashierBusyIndicator.IsBusy = true;
            Refresh(CashierBusyIndicator);

            await Task.Delay(2000);

            CashierBusyIndicator.IsBusy = false;
            Refresh(CashierBusyIndicator);

            generarreporte();
          
            return "Success";
        }
        private void generarreporte()
        {
            DataTable tabla = dtblcatalogo;

            //Refresh(CashierBusyIndicator);
            //generar();

            DateTime date = DateTime.Now;
            string datewithformat = date.ToString();
        string dateday = date.ToString("dd MMMM yyyy HH mm ").ToUpper();

            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;
                //IStyle headerStyle = wo.Styles.Add("HeaderStyle");
                IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);
    IWorksheet worksheet = workbook.Worksheets[0];
    worksheet.EnableSheetCalculations();
                
                int osos = tabla.Rows.Count;
    worksheet.Name = "Servicios";
                worksheet.ImportDataTable(tabla, true, 2, 1);
                worksheet.AutoFilters.FilterRange = worksheet.Range["A2:K2"];
                
                worksheet.Range["A1"].Text = "CATÁLOGO DE SERVICIOS AL " + dateday ;
                // worksheet.Range["A1"].Text = "Llantas y Rines del Guadiana S.A. de C.V. - Existencias LRG Al "+dateday+"- B4 Francisco Villa";
                worksheet.Rows[1].FreezePanes();
    worksheet.Rows[2].FreezePanes();

    IStyle headerStyle = workbook.Styles.Add("HeaderStyle");
    headerStyle.BeginUpdate();

                workbook.SetPaletteColor(8, System.Drawing.Color.FromArgb(46, 204, 113));

                headerStyle.Color = System.Drawing.Color.FromArgb(46, 204, 113);

                headerStyle.Font.Bold = true;

                headerStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;

                headerStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                headerStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;

                headerStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                headerStyle.EndUpdate();

                worksheet.Rows[1].CellStyle = headerStyle;

                IStyle pStyle = workbook.Styles.Add("pStyle");
    pStyle.BeginUpdate();

                workbook.SetPaletteColor(9, System.Drawing.Color.FromArgb(89, 171, 227));

                pStyle.Color = System.Drawing.Color.FromArgb(89, 171, 227);

                pStyle.Font.Bold = true;

                pStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;

                pStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                pStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;

                pStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                pStyle.EndUpdate();

                worksheet.Rows[0].CellStyle = pStyle;
                worksheet.SetColumnWidth(1, 10);
                worksheet.SetColumnWidth(2, 30);
                worksheet.SetColumnWidth(3, 15);
                worksheet.SetColumnWidth(4, 20);
                worksheet.SetColumnWidth(5, 7);
                worksheet.SetColumnWidth(6, 7);
                worksheet.SetColumnWidth(7, 10);
                worksheet.SetColumnWidth(8, 15);
                worksheet.SetColumnWidth(11, 15);

                IStyle pStyles = workbook.Styles.Add("pStyles");
    pStyles.BeginUpdate();
                worksheet.Columns[3].HorizontalAlignment = ExcelHAlign.HAlignLeft;

                pStyles.EndUpdate();

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
    string chorchill = "H2,H" + cossacks + "";
    string russevel = "H" + rusia + "";
    string totalr = "A" + rusia + "";
    worksheet.Range[totalr].Text = registros + " Registros";
                worksheet.Range[totalr].CellStyle = pStyle;
              
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                //table.Columns[5].TotalsCalculation = ExcelTotalsCalculation.Sum;
                //hacer el subtotal pero conformula ** el otro marca error con total calculation 
                //range.SubTotal(0, ConsolidationFunction.Sum, new int[] {1,rojos});
                string namer = dateday;
    string fileName = @"C:\BIG\LRG\Excel\CATÁLOGO DE SERVICIOS AL " + dateday + ".xlsx";
    // string fileName = "LRG-Existencias al " + namer + "B4 Francisco Villa.xlsx";
    workbook.SaveAs(fileName);

                string argument = @"/select, " + fileName;

    System.Diagnostics.Process.Start("explorer.exe", argument);

            }


        }
        private void guardar()
        {
            try
            {
                string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
                SqlConnection sqlcon = new SqlConnection(connectionStringer);
                sqlcon.Open();

                string cod = codigo.Text;
                string lin = linea.Text;
                string desc = descripcion.Text;
                string marc = marca.Text;
                int impuest = getimpuest();
                string uni = unidad.Text;
                int commay = Convert.ToInt32(Cmayor.Value);
                int commen = Convert.ToInt32(Cmenor.Value);
                int baja = Estado();


                string consulta = "INSERT INTO [dbo].[Servicios] ([Producto_Id],[Codigo_De_Articulo],[Descripcion],[Codigo_Editable],[Linea],[Marca],[Impuesto] ,[Unidad], [Baja_Logica],[Comision_Mayor],[Comision_Menor],[Fecha_Y_Hora_De_Ultima_Actualizacion]) " +
                    "VALUES(" + intcogid + ", '" + cod + "' ,'" + desc + "'," + streditable + ",'" + lin + "','" + marc + "'," + impuest + ",'" + uni + "'," + baja + "," + commay + "," + commen + ", getdate())";
                SqlCommand agregar = new SqlCommand(consulta, sqlcon);
                agregar.ExecuteNonQuery();


                sqlcon.Close();

                actfolio();

                cargardatos();
                clean();
                llenarConsulta();
                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Se guardo exitosamente.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
               // this.tabItem1.Refresh();

            }
            catch (SqlException ex)
            {
                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Error al insertar."+ex.Message.ToString(),
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }


        }

        private void clean()
        {
            codigo.Text = "";
            descripcion.Text = "";
            txteditable.Text = "";
            checkBox.IsChecked = false;
            linea.Text = "";
            marca.Text = "";
            impuestos.Text = "";
            unidad.Text = "";
            status.Text = "";
            Cmenor.Value = 1;
            Cmayor.Value = 1;


        }
        private void actfolio()
        {
            intcogid = intcogid + 1;
            try
            {
                string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
                SqlConnection sqlcon = new SqlConnection(connectionStringer);
                sqlcon.Open();
                string consulta = " UPDATE [dbo].[DatosServicios] SET [Prefijo] = 'LRG' ,[NumeroServicio] = " + intcogid + " WHERE Prefijo = 'LRG'";
                SqlCommand agregar = new SqlCommand(consulta, sqlcon);
                agregar.ExecuteNonQuery();
                sqlcon.Close();
            }
            catch (SqlException ex)
            {
                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Revise su conexón a internet.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }
        }
        int Estado()
        {
            if (status.SelectedIndex == 0)
            {
                return 0;

            }
            else if (status.SelectedIndex == 1)
            {
                return 1;
            }

            return 0;
        }
        int getimpuest()
        {
            if (impuestos.SelectedIndex == 0)
            { return 0; }
            else if (impuestos.SelectedIndex == 1)
            { return 11; }
            else if (impuestos.SelectedIndex == 2)
            {
                return 16;
            }
            return 0;
        }




    }
}

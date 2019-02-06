using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
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
using Microsoft.Win32;
using Syncfusion.XlsIO;
using Telerik.Windows.Controls;

namespace GGGC.Admin.AZ.Precios
{
    /// <summary>
    /// Interaction logic for PreciosView.xaml
    /// </summary>
    public partial class PreciosView : UserControl
    {
        public PreciosView()
        {


            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
            InitializeComponent();

           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog() == true)
            {
                StreamReader sr = File.OpenText(openFileDialog.FileName);
                

                folioid.Text = openFileDialog.FileName.ToString();
                
                exctodt(folioid.Text);
            }



               

        }
        DataTable customersTable = new DataTable();
        private void exctodt(string nombre)
        {
            ExcelEngine excelEngine = new ExcelEngine();
            excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;
            IApplication application = excelEngine.Excel;
            //string documento = @"C:\Users\Abril\Desktop\Precios ejemplo.xlsx";
            IWorkbook workbook = application.Workbooks.Open(nombre);
            IWorksheet sheet = workbook.Worksheets[0];
            try
            {
                if (sheet.Range["A1"].Text == "Codigo" && sheet.Range["B1"].Text == "PL")
                {

                    customersTable = sheet.ExportDataTable(sheet.UsedRange, ExcelExportDataTableOptions.ColumnNames | ExcelExportDataTableOptions.DetectColumnTypes);


                    //if (customersTable.Columns[0].)
                    //{

                    //}


                    customersTable.Columns.Add("01", Type.GetType("System.String"), "(PL * 0.95)");
                    customersTable.Columns.Add("02", Type.GetType("System.String"), "(PL * 0.85)");
                    customersTable.Columns.Add("03", Type.GetType("System.String"), "(PL * 0.95)");
                    customersTable.Columns.Add("04", Type.GetType("System.String"), "(PL * 0.85)");
                    customersTable.Columns.Add("05", Type.GetType("System.String"), "(PL * 0.95)");
                    customersTable.Columns.Add("06", Type.GetType("System.String"), "(PL * 0.85)");
                    customersTable.Columns.Add("07", Type.GetType("System.String"), "(PL * 0.95)");
                    customersTable.Columns.Add("08", Type.GetType("System.String"), "(PL * 0.85)");
                    customersTable.Columns.Add("09", Type.GetType("System.String"), "(PL * 0.95)");
                    customersTable.Columns.Add("10", Type.GetType("System.String"), "(PL * 0.85)");
                    customersTable.Columns.Add("11", Type.GetType("System.String"), "(PL * 0.95)");
                    customersTable.Columns.Add("12", Type.GetType("System.String"), "(PL * 0.85)");

                    dataGrid.ItemsSource = customersTable.DefaultView;
                    workbook.Close();

                }
                else
                {
                    MessageBox.Show("Nombre de columnas invalidas ");
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("El formato del documento es invalido ");
            }

        }

        private void guardar_Click(object sender, RoutedEventArgs e)
        {
            guardar.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(folioid.Text) || customersTable.Rows.Count == 0)
            {

                
                { System.Windows.MessageBox.Show("Debe de seleccionar un archivo ", "Error al guardar"); }


               

            }
            else
            {
                
                int oso = validarrepetidos();
                if (oso == 0)
                {
                    SaveTabla();
                }
                else
                {
                    MessageBox.Show("Existen códigos duplicados.");
                    guardar.IsEnabled = true;
                }
                

               
            }

        }


        private void SaveTabla()
        {


            try
            {
                string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
                SqlConnection sqlcon = new SqlConnection(connectionStringer);
                sqlcon.Open();

                foreach (DataRow Registro in customersTable.Rows)
                {
                    string  nombre = Registro[0].ToString();
                    var nivel = "PL";
                    var precio = Registro[01].ToString();
                    string consulta = "INSERT INTO [dbo].[Precios]([Codigo_De_Articulo],[Nivel_De_Precios],[Precio],[Moneda],[Fecha_Y_Hora_De_Ultima_Actualizacion])VALUES('" + nombre + "','" + nivel + "'," + precio + ",1,GETDATE())";
                    SqlCommand agregar = new SqlCommand(consulta, sqlcon);
                    agregar.ExecuteNonQuery();
                    //foreach (DataColumn dc in customersTable.Columns)
                    //{
                    //    string nivel = dc.ColumnName;
                    //    if (dc.ColumnName == "Codigo" || dc.ColumnName == "PrecioLista")
                    //    {

                    //    }
                    //    else
                    //    {
                    //        var precio = Registro[dc].ToString();
                    //        string consulta = "INSERT INTO [dbo].[Precios]([Codigo_De_Articulo],[Nivel_De_Precios],[Precio],[Moneda],[Fecha_Y_Hora_De_Ultima_Actualizacion])VALUES('" + nombre + "','" + nivel + "'," + precio + ",1,GETDATE())";
                    //        SqlCommand agregar = new SqlCommand(consulta, sqlcon);
                    //        agregar.ExecuteNonQuery();
                    //    }
                    //}






                }

                sqlcon.Close();
                guardar.IsEnabled = true;
                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Precios Actualizados Correctamente.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });

            }
            catch (SqlException ex)
            {

                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "No se pudo completar la actualización.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });
            }
        }
        int validarrepetidos()
        {
            var groups = customersTable.AsEnumerable()

                .GroupBy(r => new
                 {
                     product_id = r.Field<double>("Codigo"),
                     
                 });
            try
            {
                var tblDuplicates = groups
               .Where(grp => grp.Count() > 1)
               .SelectMany(grp => grp)
               .CopyToDataTable();

            }
            catch
            {
                return 0;
            }

            return 1;


        }
    }
}
//Expression Syntax
//When you create an expression, use the ColumnName property to refer to columns.For example, if the ColumnName for one column is "UnitPrice", and another "Quantity", the expression would be as follows:
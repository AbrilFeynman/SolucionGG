using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Documents.UI;


namespace GGGC.Admin.AZ.Operarios
{
    /// <summary>
    /// Interaction logic for OperariosView.xaml
    /// </summary>
    public partial class OperariosView : UserControl
    {
        private delegate void NoArgDelegate();
        public static byte intEMPRESAID = 0;
        public static byte intSUCURSALID = 0;
        System.Data.DataTable tblserv = new System.Data.DataTable();
        System.Data.DataTable tblserprec = new System.Data.DataTable();
        public const string appName = "GrupoGuadiana";
        public const string section = "Config";
        string noemployed;
        string nombreempl;
        public OperariosView()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
            // Sets the UI culture to French (France)
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-MX");
            intEMPRESAID = Convert.ToByte(GetSetting(appName, section, "EmpresaID", String.Empty));
            intSUCURSALID = Convert.ToByte(GetSetting(appName, section, "SucursalID", String.Empty));
            InitializeComponent();
            cargarcombo();

        }

        private void cargarcombo()
        {

           cbxemployed.ItemsSource = getoperarios().DefaultView;
           


        }

       public string Funcion(int oso)
        {
            string conexion;
            switch (oso)
            {
                case 1:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 1)";
                    return conexion;
                    break;

                case 2:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 2)";
                    return conexion;
                    break;
                case 3:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 3)";
                    return conexion;
                    break;
                case 4:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 4)";
                    return conexion;
                    break;
                case 5:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 55)";
                    return conexion;
                    break;

                case 6:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 6)";
                    return conexion;
                    break;
                case 7:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 7)";
                    return conexion;
                    break;
                case 8:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 8)";
                    return conexion;
                    break;
                case 9:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 9)";
                    return conexion;
                    break;
                case 10:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 10)";
                    return conexion;
                    break;
                case 11:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 11)";
                    return conexion;
                    break;
                case 12:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 12)";
                    return conexion;
                    break;
                case 13:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 13)";
                    return conexion;
                    break;
                case 14:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 14)";
                    return conexion;
                    break;
                case 15:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 15)";
                    return conexion;
                    break;
                case 16:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 16)";
                    return conexion;
                    break;
                case 17:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 17)";
                    return conexion;
                    break;
                case 18:
                    conexion = "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)AND (Numero_Corto_De_Sucursal = 18)";
                    return conexion;
                    break;



                default:
                    conexion = "SERVER = 192.168.14.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;

            }
           // return "SELECT     Codigo_De_Empleado, Nombre + ' ' + Apellido_Paterno + ' ' + Apellido_Materno AS Nombrecompleto, Codigo_De_Puesto AS Puesto FROM    Empleados WHERE     (Baja_Logica = 0)  AND (Codigo_De_Puesto = 223)";
        }
        public System.Data.DataTable getventas()
        {
            string conect = GetOrigen(intEMPRESAID);
            System.Data.DataTable Tabla = new System.Data.DataTable();
            string strFuncion = Funcion(intSUCURSALID);
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


        public System.Data.DataTable getoperarios()
        {
            string conect = GetOrigenope(intSUCURSALID);
            System.Data.DataTable Tabla = new System.Data.DataTable();
            string strFuncion = Funcion(intSUCURSALID);
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



        static string GetOrigen(int empresa)
        {
            string conexion;
            switch (empresa)
            {
                case 1:
                    conexion = "SERVER = 192.168.200.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;

                case 2:
                    conexion = "SERVER = 192.168.200.2; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
               
                default:
                    conexion = "SERVER = 192.168.14.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;

            }
        }

        static string GetOrigenope(int suc)
        {
            string conexion;
            switch (suc)
            {
                case 1:
                    conexion = "SERVER = 192.168.2.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;

                case 2:
                    conexion = "SERVER = 192.168.6.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 3:
                    conexion = "SERVER = 192.168.14.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 4:
                    conexion = "SERVER = 192.168.200.10; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 5:
                    conexion = "SERVER = 192.168.15.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;

                case 6:
                    conexion = "SERVER = 192.168.100.100; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 7:
                    conexion = "SERVER = 192.168.200.20; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 8:
                    conexion = "SERVER = 192.168.100.100; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 9:
                    conexion = "SERVER = 192.168.4.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 10:
                    conexion = "SERVER = 192.168.100.100; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 11:
                    conexion = "SERVER = 192.168.11.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 12:
                    conexion = "SERVER = 192.168.11.140; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 13:
                    conexion = "SERVER = 192.168.5.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 14:
                    conexion = "SERVER = 192.168.100.100; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 15:
                    conexion = "SERVER = 192.168.20.7; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 16:
                    conexion = "SERVER = 192.168.7.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 17:
                    conexion = "SERVER = 192.168.8.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;
                case 18:
                    conexion = "SERVER = 192.168.8.140; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;



                default:
                    conexion = "SERVER = 192.168.14.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
                    return conexion;
                    break;

            }
        }
        private async void generar()
        {
            //button.Content = "Running...";

            var result = await RunningProcessAsync();
            Exportar.Content = "Consultar";
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

            Exportar.Content = "Generando...";
            Refresh(Exportar);
            CashierBusyIndicator.IsBusy = true;
            Refresh(CashierBusyIndicator);

            await Task.Delay(2000);

            CashierBusyIndicator.IsBusy = false;
            Refresh(CashierBusyIndicator);

            generarconsulta();

            return "Success";
        }


        private void generarconsulta()
        {

            tblserprec = getprec();


            // var query = from row in tblserprec.AsEnumerable()

            //             group row by new { Codigo = row.Field<string>("Codigo_De_Articulo"), Descripcion = row.Field<string>("TipoServicio") } into z
            //             orderby z.Key.Descripcion
            //             select new
            //             {
            //                 Codigo = z.Key.Codigo,
            //                 Descripcion = z.Key.Descripcion,
            //                 Cantidad = z.Sum(s => s.Field<decimal>("Importe"))

            //             }
            //             ;


            //tblserprec = ConvertToDataTable(query);
            if (tblserprec.Rows.Count > 0)
            {
                tblserprec.Columns.Add("Com", Type.GetType("System.String"), "(1 * 0.16)");
                tblserprec.Columns.Add("Incentivo", Type.GetType("System.Double"), "(Cantidad * 0.16)");
                tblserprec.Columns.Add("ComM", Type.GetType("System.String"), "(1 * 0.22)");
                tblserprec.Columns.Add("IncentivoM", Type.GetType("System.Double"), "(Cantidad * 0.22)");

                var data = tblserprec.AsEnumerable().Select(row =>
                    new
                    {
                        Codigo_De_Empleado = row["Codigo_De_Empleado"].ToString(),
                        Factura = row["Factura"].ToString(),
                        NombreEmpleado = row["NombreEmpleado"].ToString(),
                        Codigo_De_Articulo = row["Codigo_De_Articulo"].ToString(),
                        TipoServicio = row["TipoServicio"].ToString(),
                        Cantidad = Convert.ToDecimal(row["Cantidad"].ToString()),
                        Importe = row["Importe"].ToString(),
                        Com = row["Com"].ToString(),
                        Incentivo = Convert.ToDecimal(row["Incentivo"].ToString()),
                        ComM = row["ComM"].ToString(),
                        IncentivoM = Convert.ToDecimal(row["IncentivoM"].ToString())
                    }
                    ).ToList();


                InvoiceRemision.ItemsSource = data;
                //InvoiceRemision.GroupHeaderTemplate//


               // InvoiceRemision.MasterTemplate.AutoExpandGroups = True;
               
                // Declare an object variable.
                object sumObject;
                sumObject = tblserprec.Compute("Sum(Cantidad)", string.Empty);
                //string osos = sumObject.ToString();
                //string ose = string.Format("{0:#,0.#}", float.Parse(osos));
               


                object sumObjecta;
                sumObjecta = tblserprec.Compute("Sum(Incentivo)", "[Com] IS NOT NULL");
                string ososa = sumObjecta.ToString();
                string osea = string.Format("{0:#,0.#}", float.Parse(ososa));
                

                object sumObjectx;
                sumObjectx = tblserprec.Compute("Sum(IncentivoM)", string.Empty);
                string ososx = sumObjectx.ToString();
                string osex = string.Format("{0:#,0.#}", float.Parse(ososx));
               


                //Style style = new Style(typeof(GroupHeaderRow));
                //style.Setters.Add(new Setter(GroupHeaderRow.ShowHeaderAggregatesProperty,"False"));
               
                //Resources.Add(typeof(GroupHeaderRow), style);


            }
            else
            {

                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "No hay filas que mostrar.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });

            }




        }

        private void Exportar_Click(object sender, RoutedEventArgs e)
        {
            if (dtfinal.SelectedDate  >=  dtinicial.SelectedDate  )
            {
                if (string.IsNullOrEmpty(cbxemployed.Text))
                {

                    RadWindow radWindow = new RadWindow();
                    RadWindow.Alert(new DialogParameters()
                    {
                        Content = "Debe de seleccionar a un empleado.",
                        Header = "BIG",

                        DialogStartupLocation = WindowStartupLocation.CenterOwner
                        // IconContent = "";
                    });

                }
                else
                {
                    //tabla de servicios y precios de la base
                    //tabla de servicios de la nube 

                    // tblserv =


                    Refresh(CashierBusyIndicator);
                    generar();

                   

                    //var query = from r in tblserprec.AsEnumerable()
                    //            group r by r.Field<string>("Codigo_De_Articulo") into groupedTable
                    //            select new
                    //            {

                    //                Servicio = groupedTable.Key,
                    //                Cantidad = groupedTable.Sum(s => s.Field<decimal>("Importe"))
                    //            };




                    //DataTable newDt = ConvertToDataTable(query); 

                }

            }
            else
            {
                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "La fecha inicial debe ser menor a la fecha final.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });

            }

        }


        public DataTable ConvertToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();


            // column names
            PropertyInfo[] oProps = null;


            if (varlist == null) return dtReturn;


            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = rec.GetType().GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;


                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }


                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }


                DataRow dr = dtReturn.NewRow();


                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }


                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }




        public System.Data.DataTable getprec()
        {
            DateTime strFechaInicial = Convert.ToDateTime(dtinicial.SelectedDate);
            DateTime strFechaFinal = Convert.ToDateTime(dtfinal.SelectedDate);
            string conect = GetOrigen(intEMPRESAID);
            System.Data.DataTable Tabla = new System.Data.DataTable();
            //string strFuncion = "Select  Codigo_De_Articulo AS codigo, Descripcion AS Descripcion, Importe_Total AS Total,Fecha_Y_Hora_De_Ultima_Actualizacion as Fecha " +
            //    "FROM  Facturas_Y_Devoluciones_Detalle INNER JOIN Facturas_Y_Devoluciones ON Facturas_Y_Devoluciones_Detalle.Numero_De_Documento = Facturas_Y_Devoluciones.Numero_De_Documento" +
            //    " WHERE(Codigo_De_Empleado = '"+noemployed+ "') AND  Facturas_Y_Devoluciones.Fecha_Y_Hora_De_Ultima_Actualizacion BETWEEN '" + strFechaInicial + "' AND '" + strFechaFinal + "'";

            string strFuncion = "SELECT * from dbo.fncoperarios(@FechaInicial,@FechaFinal,@noempleado);";
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
                            Cmd.CommandTimeout = 360;
                            Cmd.Parameters.Add("@FechaInicial", SqlDbType.SmallDateTime).Value = strFechaInicial;
                            Cmd.Parameters.Add("@FechaFinal", SqlDbType.SmallDateTime).Value = strFechaFinal;
                            Cmd.Parameters.Add("@noempleado", SqlDbType.SmallInt).Value = Convert.ToInt32(noemployed);
                            
                           
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

        public static string GetSetting(string appName, string section, string key, string sDefault)
        {
            // Los datos de VB se guardan en:
            // HKEY_CURRENT_USER\Software\VB and VBA Program Settings
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"Software\VB and VBA Program Settings\" +
                                                              appName + "\\" + section);
            string s = sDefault;
            if (rk != null)
            {
                s = (string)rk.GetValue(key);
            }
            return s;
        }

        private void cbxemployed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var objPrecio = cbxemployed.SelectedItem;
            if (objPrecio != null)
            { noemployed = ((System.Data.DataRowView)objPrecio).Row.ItemArray[0].ToString();
              nombreempl = ((System.Data.DataRowView)objPrecio).Row.ItemArray[1].ToString();
            }
            else { noemployed = "";
                nombreempl = "";
            }

        }


        private void GridViewGroupRow_Loaded(object sender, RoutedEventArgs args)
        {
            GridViewGroupRow groupRow = (GridViewGroupRow)args.OriginalSource;
            groupRow.IsExpanded = true;
        }



        private void button_Click(object sender, RoutedEventArgs e)
        {

            //GridViewSpreadExport spreadExporter = new GridViewSpreadExport(this.InvoiceRemision);
            //SpreadExportRenderer exportRenderer = new SpreadExportRenderer();
            //spreadExporter.RunExport("D:\\exportedFile.xlsx", exportRenderer);
            DateTime date = DateTime.Now;
            string closure = date.ToString("dd MM yyyy HH mm");
            string strFechaInicial = Convert.ToDateTime(dtinicial.SelectedDate).ToString("dd-MMM-yyyy").ToUpper();
            string strFechaFinal = Convert.ToDateTime(dtfinal.SelectedDate).ToString("dd-MMM-yyyy").ToUpper();

            string filename = "C:\\BIG\\LRG\\Excel\\LRG - REP. COMISION "+nombreempl+ " DEL " + strFechaInicial + " Al " + strFechaFinal+" "+"(" + closure + ").xlsx";
           
                using (var fileStream = File.Create(filename))
                {

                    InvoiceRemision.ExportToXlsx(fileStream,
                        new GridViewDocumentExportOptions()
                        {
                            ShowColumnFooters = true,
                            ShowColumnHeaders = true,

                            ShowGroupFooters = true,
                            ExportDefaultStyles = true
                        });
                }

            string argument = @"/select, " + filename;

            System.Diagnostics.Process.Start("explorer.exe", argument);
            //string extension = "xlsx";

            //SaveFileDialog dialog = new SaveFileDialog()
            //{
            //    DefaultExt = extension,
            //    Filter = String.Format("{1} files (.{0})|.{0}|All files (.)|.", extension, "Excel"),
            //    FilterIndex = 1
            //};

            //if (dialog.ShowDialog() == true)
            //{
            //    using (Stream stream = dialog.OpenFile())
            //    {
            //        InvoiceRemision.ExportToXlsx(stream,
            //            new GridViewDocumentExportOptions()
            //            {
            //                ShowColumnFooters = true,
            //                ShowColumnHeaders = true,

            //                ShowGroupFooters = true,
            //                ExportDefaultStyles = true
            //            });
            //    }
            //}




            //libreria de windows Forms 
            //GridViewSpreadExport spreadExporter = new GridViewSpreadExport(this.InvoiceRemision);
            //spreadExporter.ExportChildRowsGrouped = true;
            //SpreadExportRenderer exportRenderer = new SpreadExportRenderer();
            //spreadExporter.RunExport(@"..\..\exportedFile.xlsx", exportRenderer);



            //GridViewSpreadExport spreadExporter = new GridViewSpreadExport(this.InvoiceRemision);
            //SpreadExportRenderer exportRenderer = new SpreadExportRenderer();
            //spreadExporter.RunExport("D:\\exportedFile.xlsx", exportRenderer);
            //------------------GUARDA BASICO ------------------------------

        }

        private void Impri_Click(object sender, RoutedEventArgs e)
        {
            //if (InvoiceRemision != null)
            //{
            //    InvoiceRemision.Print(new PrintSettings());
            //}
            //this.radGridView1.Print();
            //this.radGridView1.Print(true);
        }




    }
}




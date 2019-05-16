using Microsoft.Win32;
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

namespace GGGC.Admin.AZ.Cotizacion
{
    /// <summary>
    /// Interaction logic for Cotizacionview.xaml
    /// </summary>
    public partial class Cotizacionview : UserControl
    {


        public static byte intEMPRESAID = 0;
        public static byte intSUCURSALID = 0;
        public const string appName = "GrupoGuadiana";
        public const string section = "Config";
        public Cotizacionview()
        {

            intEMPRESAID = Convert.ToByte(GetSetting(appName, section, "EmpresaID", String.Empty));
            intSUCURSALID = Convert.ToByte(GetSetting(appName, section, "SucursalID", String.Empty));
            InitializeComponent();
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

        static DataTable GetDetalle(string folio)
        {



           

            SqlConnection sqlconn = new SqlConnection(GetOrigen(intSUCURSALID));
            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR DE CONEXION" + ex.Message);
            }




            SqlDataAdapter adapter = new SqlDataAdapter("SELECT     Cotizaciones.Numero_De_Folio AS Folio, Sucursales.Descripcion as Sucursal, Cotizaciones.Fecha_De_Cotizacion as Fecha, Cotizaciones.De, Cotizaciones.Para, " +
                "Cotizaciones.Atencion, Cotizaciones.Saludo, Cotizaciones_Detalle.Codigo_De_Articulo as Codigo, Cotizaciones_Detalle.Descripcion AS Descripcion, Cotizaciones_Detalle.Unidad, Cotizaciones_Detalle.Cantidad, " +
                "Cotizaciones_Detalle.Precio_De_Venta as Pventa, Cotizaciones_Detalle.Importe_Total as Ptotal, Cotizaciones.Aclaraciones, Cotizaciones.Despedida FROM Cotizaciones INNER JOIN" +
                " Cotizaciones_Detalle ON Cotizaciones.Numero_De_Documento = Cotizaciones_Detalle.Numero_De_Documento INNER JOIN Sucursales ON Cotizaciones.Numero_Corto_De_Sucursal = Sucursales.Numero_Corto_De_Sucursal" +
                " WHERE(Cotizaciones.Numero_De_Folio = '"+folio+"') ORDER BY Cotizaciones.Fecha_Y_Hora_De_Ultima_Actualizacion DESC", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "Vista");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["Vista"];
            sqlconn.Close();

            return dtbl;

        }


        private void BtnImpr1_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {

                string folio = folioid.Text;


                DataTable Budget = new DataTable();
                Budget = GetDetalle(folio);
               


                if (Budget.Rows.Count > 0)

                {
                    //Guarda Localmente El PDF

                  //7  RptBudget rpt = new RptBudget(Budget);
                    //la manda a desplegar en una nueva ventana 
                    // string camino = @"C:\Ektelesis.Net\CFDI\DATOS\PDF\COT_" + folio + "_LRG920502BG7.pdf";
                    Window1 form = new Window1(Budget);
                    form.ShowDialog();


                }

                else
                {


                    RadWindow radWindow = new RadWindow();
                    RadWindow.Alert(new DialogParameters()
                    {
                        Content = "El folio no existe.",
                        Header = "BIG",

                        DialogStartupLocation = WindowStartupLocation.CenterOwner
                        // IconContent = "";
                    });

                   


                }
            }
            catch (Exception l)
            {
                RadWindow radWindow = new RadWindow();
                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Revise su conexión a internet.",
                    Header = "BIG",

                    DialogStartupLocation = WindowStartupLocation.CenterOwner
                    // IconContent = "";
                });

            }
        }



        static string GetOrigen(int empresa)
        {
            string conexion;
            switch (empresa)
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
                case 55:
                    conexion = "SERVER = 192.168.15.1; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
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
                    conexion = "SERVER = 192.168.100.103; DATABASE = Punto_De_Venta; USER ID = sa; PASSWORD = dgo2007";
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

    }
}

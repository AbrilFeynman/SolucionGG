using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GGGC.Admin.AZ.Ordenes.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(String osos)
        {
            InitializeComponent();
            try { export15(osos); }
            catch (SqlException ex) { MessageBox.Show("Revise su conexión a internet"); }
        }


        private void export15(string folio)
        {
            try
            {
                DataTable tablaOrden = GetHeader(folio);
                DataTable tablaDeatil = GetDetail(folio);
                RptOrden fac = new RptOrden(tablaOrden, tablaDeatil);
               
                this.ReportViewer1.Report = fac;
                this.ReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Folio no encontrado");
            }

        }

        static DataTable GetHeader(string folio)
        {
            string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";

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
            SqlDataAdapter adapter = new SqlDataAdapter("Select OrderHeader.OrderID, OrderHeader.CustomerID, OrderHeader.RFC, OrderHeader.OrderDate," +
                                                       " OrderHeader.ReceptionDate, OrderHeader.DueDate, OrderHeader.OrderQty, OrderHeader.Subtotal, OrderHeader.Total, " +
                                                       " OrderHeader.ExteriorValues, OrderHeader.InteriorValues, OrderHeader.AccesoriesValues, Clientes_Frecuentes.Nombre + ' ' + " +
                                                       " Clientes_Frecuentes.Apellido_Paterno + ' ' + Clientes_Frecuentes.Apellido_Materno as Nombre, " +
                                                       " Clientes_Frecuentes.Telefono_Personal as Telefono,  OrderHeader.Marca, OrderHeader.Modelo, OrderHeader.Year, OrderHeader.Placas,OrderHeader.Km, " +
                                                       " Clientes_Frecuentes.Direccion + ' ' + Clientes_Frecuentes.Colonia + ' ' + Clientes_Frecuentes.Ciudad as Direccion,  OrderHeader.Comments as Observaciones, OrderHeader.Gasolina,OrderHeader.PendientesValues From OrderHeader Inner join  Clientes_Frecuentes" +
                                                       "  ON OrderHeader.CustomerID = Clientes_Frecuentes.Id_Radial" +
                                                        " where OrderHeader.OrderID =  " + folio + "", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "OrderHeader");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["OrderHeader"];
            sqlconn.Close();

            return dtbl;
        }

        static DataTable GetDetail(string folio)
        {
            string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";


            SqlConnection sqlconn = new SqlConnection(conect);

            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR DE CONEXION" + ex.Message);
            }
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT OrderDetail.EntityKey " +
     ", OrderDetail.OrderID, OrderDetail.OrderDetailID, OrderDetail.ProductID, OrderDetail.CodeID, OrderDetail.ProductDescription, OrderDetail.Level, OrderDetail.UnitID," +
    " OrderDetail.Qty, OrderDetail.UnitPrice, OrderDetail.LineTotal, OrderDetail.ModifiedDate," +
     "OrderDetail.LastUpdate, OrderDetail.RowID, OrderHeader.Rampa From OrderHeader Inner join OrderDetail" +
     " ON OrderHeader.OrderID = OrderDetail.OrderID WHERE OrderDetail.OrderID = " + folio + " ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "OrderDetail");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["OrderDetail"];
            sqlconn.Close();

            return dtbl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}

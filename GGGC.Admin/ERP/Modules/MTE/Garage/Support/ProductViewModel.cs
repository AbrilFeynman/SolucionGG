using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GGGC.Modules.Data;
using System.Data;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class ProductViewModel
    {
        private ObservableCollection<Product> objects;

        public ObservableCollection<Product> Producto
        {
            get
            {
                if (objects == null)
                {
                    objects = new ObservableCollection<Product>();
                   // objects.Add(new Supplier(1, "Guillermo Uribe\nMarco Romero", "Base 4. 123/111", "1"));
                    //objects.Add(new Department("Patrimonio", "Fernando Lujan", "Base 4. 105","2"));
                    //objects.Add(new Department("Mantenimiento", "Adrián Favela", "Taller. 105", "3"));  
                    cargarProducts();
                }
                return objects;
            }
        }

        private void cargarProducts()
        {
            try
            {
                AccesoDatos sCen = new AccesoDatos(6);
                //System.Data.DataSet ds = new System.Data.DataSet();
                string sSQL = "SELECT ProductID, CodeID, Description FROM  Articulos ORDER BY CodeID  ";

                DataTable tbl = sCen.BaseDatos.Consulta(sSQL);
                int i = 0;
                foreach (var row in tbl.Rows)
                {
                    objects.Add(new Product(Convert.ToInt32(tbl.Rows[i]["ProductID"]), tbl.Rows[i]["CodeID"].ToString(), tbl.Rows[i]["Description"].ToString(), " "));
                    i++;
                }




                //foreach(var row in tbl.Rows)
                //{
                //    var obj = new Marca()
                //    {
                //        id_test = (int)tbl.Rows[0]["id_test"];
                //        name = (string)tbl.Rows[0]["name"];
                //    };
                //    //test.Add(obj);
                //  objects.Add(new Brand("1", "Alta"));
                //}

            }
            catch (Exception ex)
            {
                //  MessageBox.Show("Error en cargarDatos: " + ex.Message);
            }

        }

        //private void LoadData()
        //{
        //    //try
        //    //{
        //    //  MessageBox.Show("en lineas ");
        //    sCen = new AccesoDatos(6);
        //    System.Data.DataSet ds = new System.Data.DataSet();
        //    string sSQL = "SELECT SupplierID, RFC, TradeName FROM  mto_Suppliers WHERE Visibility = 1 ";
        //    //   MessageBox.Show("sql " + sSQL);
        //    //ds = sCen.BaseDatos.ConsultaDataset("spInventario");
        //    //DataTable tbl = sCen.BaseDatos.Consulta(sSQL);             
        //    //  string sSQL = "SELECT MarcaID, Marca FROM tblMarcas ORDER BY Marca ASC";
        //    //cboLine.ItemsSource = sCen.BaseDatos.Consulta(sSQL);
        //    cboSupplierName.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.Bas
        //    //   MessageBox.Show("consulta");
        //    cboSupplierName.SelectedValuePath = "SupplierID";
        //    cboSupplierName.DisplayMemberPath = "RFC";
        //    cboSupplierName.SelectedIndex = -1;

        //    //sSQL = "SELECT * FROM  Inventory_Brand WHERE BrandID IN(103,104,200) ORDER BY Brand ";
        //    //cboMarca.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.BaseDatos.Consulta(sSQL);
        //    //cboMarca.SelectedValuePath = "BrandID";
        //    //cboMarca.DisplayMemberPath = "Brand";
        //    //cboMarca.SelectedIndex = -1;
        //    //sSQL = "SELECT * FROM  Inventory_Unit ORDER BY UnitID ";
        //    //cboUnidad.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.BaseDatos.Consulta(sSQL);
        //    //cboUnidad.SelectedValuePath = "UnitID";
        //    //cboUnidad.DisplayMemberPath = "Unit";
        //    //cboUnidad.SelectedIndex = 0;
        //    //sSQL = "SELECT * FROM  Inventory_Tax ORDER BY TaxID ";
        //    //cboImpuesto.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.BaseDatos.Consulta(sSQL);
        //    //cboImpuesto.SelectedValuePath = "TaxID";
        //    //cboImpuesto.DisplayMemberPath = "Tax";
        //    //cboImpuesto.SelectedIndex = 1;
        //    //sSQL = "SELECT * FROM  Inventory_Status ORDER BY StatusID ";
        //    //cboEstatus.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.BaseDatos.Consulta(sSQL);
        //    //cboEstatus.SelectedValuePath = "StatusID";
        //    //cboEstatus.DisplayMemberPath = "Status";
        //    //cboEstatus.SelectedIndex = 0;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    // MessageBox.Show("Error en cargarDatos: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    //}
        //}

    }
}

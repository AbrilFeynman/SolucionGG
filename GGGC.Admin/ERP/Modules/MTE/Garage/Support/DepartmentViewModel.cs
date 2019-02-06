using GGGC.Modules.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
   public  class DepartmentViewModel
    {
        private ObservableCollection<Department> objects;
        public ObservableCollection<Department> Department
        {
            get
            {
                if (objects == null)
                {
                    objects = new ObservableCollection<Department>();
                    // objects.Add(new Department("Sistemas", "", "",""));
                    cargarElements();



                }

                return objects;
            }
        }

        private void cargarElements()
        {
            try
            {
                AccesoDatos sCen = new AccesoDatos(6);
                //System.Data.DataSet ds = new System.Data.DataSet();
                string sSQL = "SELECT GiroID, Giro FROM  lstGiros  Where Visibility = 1 ORDER BY Giro ASC ";

                DataTable tbl = sCen.BaseDatos.Consulta(sSQL);
                int i = 0;
                foreach (var row in tbl.Rows)
                {
                    objects.Add(new Department(tbl.Rows[i]["GiroID"].ToString(), tbl.Rows[i]["Giro"].ToString()));
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
                MessageBox.Show("Error en cargarDatos: " + ex.Message);
            }

        }

    }
}

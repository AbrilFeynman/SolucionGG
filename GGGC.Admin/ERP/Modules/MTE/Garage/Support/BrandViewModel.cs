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
  public class BrandViewModel
    {
      public ObservableCollection<Brand> objects;

      public ObservableCollection<Brand> Marca
        {
            get
            {
                if (objects == null)
                {
                    objects = new ObservableCollection<Brand>();
                    
                    cargarMarcas();

                    //objects.Add(new Brand("1", "Alta"));
                    //objects.Add(new Brand("2", "Media"));
                    //objects.Add(new Brand("3", "Baja"));
                    

                }

                return objects;
            }
        }


      private void cargarMarcas()
      {
          try
          {
              AccesoDatos sCen = new AccesoDatos(6);
              //System.Data.DataSet ds = new System.Data.DataSet();
              string sSQL = "SELECT * FROM  mto_Marcas  ";

              DataTable tbl = sCen.BaseDatos.Consulta(sSQL);
              int i = 0;
              foreach (var row in tbl.Rows)
              {
                  objects.Add(new Brand(tbl.Rows[i]["MarcaID"].ToString(), tbl.Rows[i]["Marca"].ToString()));
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






    }
}

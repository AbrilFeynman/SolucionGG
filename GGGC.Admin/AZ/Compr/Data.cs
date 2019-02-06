using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GGGC.Admin.AZ.Compr
{
   public class Data
    {
        private ObservableCollection<Articulo> articulos = new ObservableCollection<Articulo>();

        public void CreateArticulosData()
        {
            articulos.Add(new Articulo()
            {
                Codigo = "101025",
                Descripcion = " All Terrain 255 30",
                Nombrecompleto= "101025 All Terrain 255 30 "
            } );


            articulos.Add(new Articulo()
            {
                Codigo = "108885",
                Descripcion = " All Terrain 255 70",
                Nombrecompleto = "108885 All Terrain 255 70 "
            });

            articulos.Add(new Articulo()
            {
                Codigo = "101007",
                Descripcion = " MICH TRUCK 255 30",
                Nombrecompleto = "101007 All Terrain 255 30 "
            });
        }

        public ObservableCollection<Articulo> GetArticulos()
        {
            articulos.Clear();
            CreateArticulosData();
            return articulos;
        }
    }

}

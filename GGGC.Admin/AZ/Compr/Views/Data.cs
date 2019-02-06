using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace GGGC.Admin.AZ.Compr.Views
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
                Nombrecompleto = "101025AllTerrain25530 "
            });


            articulos.Add(new Articulo()
            {
                Codigo = "108885",
                Descripcion = " All Terrain 255 70",
                Nombrecompleto = "108885AllTerrain25570 "
            });

            articulos.Add(new Articulo()
            {
                Codigo = "101007",
                Descripcion = " MICH TRUCK 255 30",
                Nombrecompleto = "101007MICHTRUCK25530 "
            });

            articulos.Add(new Articulo()
            {
                Codigo = "1000",
                Descripcion = " HANKOOK 255 30",
                Nombrecompleto = "1000HANKOOK"
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

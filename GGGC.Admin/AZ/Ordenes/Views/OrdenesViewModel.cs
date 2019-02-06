using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GGGC.Admin.AZ.Ordenes.Views
{
   public class OrdenesViewModel
    {
        Data source = new Data();
        private ObservableCollection<Articulo> articulosList = new ObservableCollection<Articulo>();
        public ObservableCollection<Articulo> ArticulosList
        {
            get { return articulosList; }
        }

        public OrdenesViewModel()
        {
            foreach (var articulo in source.GetArticulos())
            {
                articulosList.Add(articulo);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.AZ.Compr
{
    public class ArticulosViewModel
    {
        Data source = new Data();
        private ObservableCollection<Articulo> articulosList = new ObservableCollection<Articulo>();
        public ObservableCollection<Articulo> ArticulosList
        {
            get { return articulosList; }
        }

        public ArticulosViewModel()
        {
            foreach (var articulo in source.GetArticulos())
            {
                articulosList.Add(articulo);
            }
        }
    }
}

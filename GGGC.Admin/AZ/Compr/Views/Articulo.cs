using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.AZ.Compr.Views
{
   public class Articulo
    {
        private string codigo = string.Empty;
        private string descripcion = string.Empty;
        private string nombrecompreto = string.Empty;

        public string Codigo
        {
            get { return codigo; }
            set
            {
                if (codigo != value)
                {
                    codigo = value;
                }
            }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                if (descripcion != value)
                {
                    descripcion = value;
                }
            }
        }

        public string Nombrecompleto
        {
            get { return nombrecompreto; }
            set
            {
                if (nombrecompreto != value)
                {
                    nombrecompreto = value;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class Vehiculo
    {

        public Vehiculo(string vehiculoID, string placas, string descripcion, string modelo)
        {
            this.VehiculoID = vehiculoID;
            this.Descripcion = descripcion;
            this.Modelo = modelo;
            this.Placas = placas;
        }

        public string Placas
        {
            get;
            set;
        }
        public string VehiculoID
        {
            get;
            set;
        }
        public string Descripcion
        {
            get;
            set;
        }
        public string Modelo
        {
            get;
            set;
        }

      
    }
}

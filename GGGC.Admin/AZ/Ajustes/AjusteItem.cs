using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.AZ.Ajustes
{
   public class AjusteItem
    {
        private string _Numero_De_Documento;
        private int _renglon;
        private string _codigo;
        private string _descripcion;
        private string _unidad; 
        private int _cantidad;

        public string Numero_De_Documento
        {
            get
            {
                return _Numero_De_Documento;
            }
            set
            {
                _Numero_De_Documento = value;
            }
        }

        public string Codigo
        {
            get
            {
                return _codigo;
            }
            set
            {
                _codigo = value;
            }
        }
        public string Descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                _descripcion = value;
            }
        }

        public string Unidad
        {
            get
            {
                return _unidad;
            }
            set
            {
                _unidad = value;
            }
        }
       
        public int Cantidad
        {
            get
            {
                return _cantidad;
            }
            set
            {

                _cantidad = value;

            }
        }
       
       
        public int Renglon
        {
            get
            {
                return _renglon;
            }
            set
            {
                _renglon = value;

            }
        }





    }
}

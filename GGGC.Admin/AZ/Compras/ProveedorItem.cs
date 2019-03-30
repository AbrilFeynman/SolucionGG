using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.AZ.Compras
{
   public class ProveedorItem : INotifyPropertyChanged
    {
        private int _Numero_Proveedor;
        private string _Nombre;
        private string _rfc;
   

        public event PropertyChangedEventHandler PropertyChanged;

        public int Numero_Proveedor
        {
            get
            {
                return _Numero_Proveedor;
            }
            set
            {
                _Numero_Proveedor = value;
            }
        }
        public string Nombre
        {
            get
            {
                return _Nombre;
            }
            set
            {
                _Nombre = value;
            }
        }

       
        public string RFC
        {
            get
            {
                return _rfc;
            }
            set
            {
                _rfc = value;
            }
        }

    }
}

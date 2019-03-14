using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.AZ.Ordenes.Views
{
   public class ClienteItem : INotifyPropertyChanged
    {
        private int _Numero_De_Cliente;
        private string _Nombre_De_Cliente;
        private string _rfc;
        private string _telefono;
        private string _direccion;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Numero_De_Cliente
        {
            get
            {
                return _Numero_De_Cliente;
            }
            set
            {
                _Numero_De_Cliente = value;
            }
        }
        public string Nombre_De_Cliente
        {
            get
            {
                return _Nombre_De_Cliente;
            }
            set
            {
                _Nombre_De_Cliente = value;
            }
        }

        public string Telefono
        {
            get
            {
                return _telefono;
            }
            set
            {
                _telefono = value;
            }
        }

        public string Direccion
        {
            get
            {
                return _direccion;
            }
            set
            {
                _direccion = value;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.AZ.Remisiones.Views
{
    public class ClienteItem : INotifyPropertyChanged
    {

        private int _Numero_De_Cliente;
        private string _rfc;
        private string _nombre;
        private string _direccion;
        private string _colonia;
        private string _cp;
        private string _ciudad;
        private string _estado;
        private string _pais;
        private string _correo;
      
        

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

        public string Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
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
        public string Colonia
        {
            get
            {
                return _colonia;
            }
            set
            {
                _colonia = value;
            }
        }

        public string Cp
        {
            get
            {
                return _cp;
            }
            set
            {
                _cp = value;
            }
        }


        public string Ciudad
        {
            get
            {
                return _ciudad;
            }
            set
            {
                _ciudad = value;
            }
        }

        public string Estado
        {
            get
            {
                return _estado;
            }
            set
            {
                _estado = value;
            }
        }

        public string Pais
        {
            get
            {
                return _pais;
            }
            set
            {
                _pais = value;
            }
        }

        public string Correo
        {
            get
            {
                return _correo;
            }
            set
            {
                _correo = value;
            }
        }


    }
}

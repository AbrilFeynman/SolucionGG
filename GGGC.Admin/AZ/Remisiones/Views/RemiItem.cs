using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.AZ.Remisiones.Views
{
   public class RemiItem : INotifyPropertyChanged
    {
        private string _Numero_De_Documento;
        private string _codigo;
        private string _descripcion;
        private string _marca;
        private double _preciolista;
        private string _medida;
        private double _cantidad;
        private double _rate;
        private double _iva;
        private int _renglon;
        private string _nivel;
        private double _total;
        public event PropertyChangedEventHandler PropertyChanged;


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
        public string Medida
        {
            get
            {
                return _medida;
            }
            set
            {
                _medida = value;
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

        public string Marca
        {
            get
            {
                return _marca;
            }
            set
            {
                _marca = value;
            }
        }
        public string Nivel
        {
            get
            {
                return _nivel;
            }
            set
            {
                _nivel = value;
            }
        }
        public double Cantidad
        {
            get
            {
                return _cantidad;
            }
            set
            {

                _cantidad = value;
                UpdateTotalAmount();
            }
        }
        public double Rate
        {
            get
            {
                return _rate;
            }
            set
            {
                _rate = value;
                UpdateTotalAmount();
            }
        }
        public double Iva
        {
            get
            {
                return _iva;
            }
            set
            {
                _iva = value;
                UpdateTotalAmount();
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
        public double Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
            }
        }


        public double Preciolista
        {
            get
            {
                return _preciolista;
            }
            set
            {
                _preciolista = value;
            }
        }

        void UpdateTotalAmount()
        {
            Total = (_cantidad * _rate);
        }




    }



}
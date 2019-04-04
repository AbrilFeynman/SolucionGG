using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GGGC.Admin.AZ.Compras
{
   public class CompraItem
    {
        private string _Numero_De_Documento;
        private string _codigo;
        private string _descripcion;
        private string _unidad;
        private double _preciouni;
        private double _cantidad;
        private double _precioextend;
        private double _precioextendsindesc;
        private double _preciocdesc;

        private int _renglon;
        private string _descuento;
    
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
        public string Descuento
        {
            get
            {
                return _descuento;
            }
            set
            {
                _descuento = value;
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
              
            }
        }
        public double Preciouni
        {
            get
            {
                return _preciouni;
            }
            set
            {
                _preciouni = value;
               
            }
        }
        public double Precioextend
        {
            get
            {
                return _precioextend;
            }
            set
            {
                _precioextend = value;
               
            }
        }
        public double Precioextendsindesc
        {
            get
            {
                return _precioextendsindesc;
            }
            set
            {
                _precioextendsindesc = value;

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
     


        public double Preciocdesc
        {
            get
            {
                return _preciocdesc;
            }
            set
            {
                _preciocdesc = value;
            }
        }

     

    }
}

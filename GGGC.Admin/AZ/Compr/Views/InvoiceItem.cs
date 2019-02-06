using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.AZ.Compr.Views
{
  public  class InvoiceItem : INotifyPropertyChanged
    {
        private string _codigo;
        private string _descripcion;
        private double _cantidad;
        private double _rate;
        private double _iva;
        private double _total;
        public event PropertyChangedEventHandler PropertyChanged;


        #region Properties
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
        #endregion

        void UpdateTotalAmount()
        {
            Total = (_cantidad * _rate + _iva);
        }

    }


}

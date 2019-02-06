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
        private string _rfc;
 
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

    }
}

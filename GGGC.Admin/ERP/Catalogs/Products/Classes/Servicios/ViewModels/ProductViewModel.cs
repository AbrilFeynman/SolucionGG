using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GGGC.Admin.ERP.Catalogs.Products.Classes.Servicios.Views;


namespace GGGC.Admin.ERP.Catalogs.Products.Classes.Servicios.ViewModels
{
    public class ProductViewModel: INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly ProductValidator _userValidator;
        //private string _zip;
        //private string _email;
        private string _descripcion;
         private string _linea;
        private int _lineId;

        public ProductViewModel()
        {
            _userValidator = new ProductValidator();
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set
            {
                _descripcion = value;
                OnPropertyChanged("Descripcion");
            }
        }

        public string Linea
        {
            get { return _linea; }
            set
            {
                _linea = value;
                OnPropertyChanged("Linea");
            }
        }


        public int LineID
        {
            get { return _lineId; }
            set
            {
                _lineId = value;
                OnPropertyChanged("LineID");
            }
        }
        //public string Email
        //{
        //    get { return _email; }
        //    set
        //    {
        //        _email = value;
        //        OnPropertyChanged("Email");
        //    }
        //}

        //public string Zip
        //{
        //    get { return _zip; }
        //    set
        //    {
        //        _zip = value;
        //        OnPropertyChanged("Zip");
        //    }
        //}

        public string this[string columnName]
        {
            get
            {
                var firstOrDefault = _userValidator.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return _userValidator != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }

        public string Error
        {
            get
            {
                if (_userValidator != null)
                {
                    var results = _userValidator.Validate(this);
                    if (results != null && results.Errors.Any())
                    {
                        var errors = string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
                        return errors;
                    }
                }
                return string.Empty;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

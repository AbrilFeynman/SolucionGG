using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core.Common.UI.Core;

namespace GGGC.Admin.ERP.Mobile.Views
{
    /// <summary>
    /// Lógica de interacción para MainView.xaml
    /// </summary>
    public partial class MainView : UserControlViewBase
    {
        public MainView()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        private string _textProperty;
        public string TextProperty
        {
            get { return _textProperty; }
            set
            {
                if (value.Length > 5)
                {
                    throw new Exception("Too many characters");
                }
                _textProperty = value;
            }
        }
    }
}

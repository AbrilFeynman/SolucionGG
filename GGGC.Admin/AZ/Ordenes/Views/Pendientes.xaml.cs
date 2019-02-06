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

namespace GGGC.Admin.AZ.Ordenes.Views
{
    /// <summary>
    /// Interaction logic for Pendientes.xaml
    /// </summary>
    public partial class Pendientes : UserControl
    {
        public Pendientes()
        {
            InitializeComponent();
        }

       public void guardar_Click(object sender, RoutedEventArgs e)
        {

            byte bytExteriore = fncObtenAccesorios();

        }

        public byte fncObtenAccesorios()
        {
            byte bytValor;
            bytValor = 0;
            if (chkgato.IsChecked == true)
                bytValor = Convert.ToByte(bytValor + 4096);
            if (chkmaneral.IsChecked == true)
                bytValor = Convert.ToByte(bytValor + 2048);
            if (chkllave.IsChecked == true)
                bytValor = Convert.ToByte(bytValor + 1024);
            if (chktaponaceite.IsChecked == true)
                bytValor = Convert.ToByte(bytValor + 512);
            if (chktaponradiador.IsChecked == true)
                bytValor = Convert.ToByte(bytValor + 256);
            if (chkvarilla.IsChecked == true)
                bytValor = Convert.ToByte(bytValor + 128);
            if (chkestuche.IsChecked == true)
                bytValor = Convert.ToByte(bytValor + 64);
            if (chktriangulo.IsChecked == true)
                bytValor = Convert.ToByte(bytValor + 32);
            if (chkllrefa.IsChecked == true)
                bytValor = Convert.ToByte(bytValor + 16);
            if (chkfiltroaire.IsChecked == true)
                bytValor = Convert.ToByte(bytValor + 8);
            if (chkllbateri.IsChecked == true)
                bytValor = Convert.ToByte(bytValor + 4);
            if (chkextinguidor.IsChecked == true)
                bytValor = Convert.ToByte(bytValor + 2);
            return bytValor;
        }

       


    }
}

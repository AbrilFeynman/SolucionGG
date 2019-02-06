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
    /// Interaction logic for Cliente.xaml
    /// </summary>
    public partial class Cliente : UserControl
    {
        OrdenItem m_invoiceItem;
        public string Marci;
        public Cliente()
        {
           m_invoiceItem = new OrdenItem();
           
            InitializeComponent();
            llenarcombos();
            Folioo.Focus();
        }

        private void llenarcombos()
        {
            Marca.Items.Add("Pagani ");
            Marca.Items.Add("Bugatti");
            Marca.Items.Add("Zenvo ");
            Marca.Items.Add("Lamborghini");

            Modelo.Items.Add("Huayra");
            Modelo.Items.Add("Chiron");
            Modelo.Items.Add("Veneno");
            Modelo.Items.Add("Lykan");

            Ano.Items.Add("2017");
            Ano.Items.Add("2018");
            Ano.Items.Add("2019");
            Ano.Items.Add("2020");

            Placas.Items.Add("xxx-xx");
            Placas.Items.Add("xxx-00");
            Placas.Items.Add("xxx-66");
            Placas.Items.Add("xxx-77");

            Kilometraje.Items.Add("100+");
            Kilometraje.Items.Add("1000+");
            Kilometraje.Items.Add("10000+");
            Kilometraje.Items.Add("100000+");

        }

        private void Marca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var objMarca = Marca.SelectedItem;
            if (objMarca != null)
            {
               Marci = Marca.SelectedItem.ToString();
            }
            else {Marca.Text = "Marca"; }
           
        }
    }
}

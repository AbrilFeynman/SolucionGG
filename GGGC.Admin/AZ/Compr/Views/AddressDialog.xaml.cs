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
using System.Windows.Shapes;

namespace GGGC.Admin.AZ.Compr.Views
{
    public partial class AddressDialog : Window
    {
        public event EventHandler CloseRequested;
        public event EventHandler UpdateRequested;
        BillingInformation info;

        public AddressDialog()
           : this(new BillingInformation())
        {
        }
        public AddressDialog(BillingInformation billInfo)
        {
            InitializeComponent();
            info = billInfo;
            this.DataContext = info;

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (CloseRequested != null)
                CloseRequested(this, EventArgs.Empty);
        }

        private void updtButton_Click(object sender, RoutedEventArgs e)
        {
            BillingInfoEventArgs args = new BillingInfoEventArgs();
            args.BillingInformation = info;
            if (UpdateRequested != null)
                UpdateRequested(this, args);
        }

        private void invoiceNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value = 0;
            if (int.TryParse(invoiceNo.Text, out value))
            {
                invoiceNo.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 64, 81));
            }
            else
            {

                invoiceNo.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 37, 37));
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            
        }
    }

    public class BillingInfoEventArgs : EventArgs
    {
        BillingInformation m_billInfo;
        public BillingInformation BillingInformation
        {
            get { return m_billInfo; }
            set { m_billInfo = value; }
        }
    }
}

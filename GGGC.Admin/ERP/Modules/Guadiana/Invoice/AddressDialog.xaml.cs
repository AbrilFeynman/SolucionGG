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
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GGGC.Admin.ERP.Modules.Guadiana.Invoice
{
    /// <summary>
    /// Interaction logic for AddressDialog.xaml
    /// </summary>
    public partial class Address : Window
    {
        public event EventHandler CloseRequested;
        public event EventHandler UpdateRequested;
        BillingInformation info;
        /// <summary>
        /// 
        /// </summary>
        public Address()
            : this(new BillingInformation())
        {
        }

          public Address(BillingInformation billInfo)
        {
            InitializeComponent();
            //var bounds = Window.Current.Bounds;
            //this.Height = bounds.Height;
            info = billInfo;
            this.DataContext = info;
        }
        ///// <summary>
        ///// Invoked when this page is about to be displayed in a Frame.
        ///// </summary>
        ///// <param name="e">Event data that describes how this page was reached.  The Parameter
        ///// property is typically used to configure the page.</param>
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (CloseRequested != null)
                CloseRequested(this, EventArgs.Empty);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    /// <summary>
    /// 
    /// </summary>
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

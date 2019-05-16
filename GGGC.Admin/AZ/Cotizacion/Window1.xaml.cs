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
using Telerik.Windows.Documents.Fixed;



namespace GGGC.Admin.AZ.Cotizacion
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(System.Data.DataTable tbl)
        {
            InitializeComponent();
            RptBudget rpt = new RptBudget(tbl);
            this.ReportViewer1.Report = rpt;
            this.ReportViewer1.RefreshReport();
        }
       



    }
}

﻿using System;
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

namespace GGGC.Admin.ERP.Designs
{
    /// <summary>
    /// Interaction logic for TicketsAdmin.xaml
    /// </summary>
    public partial class TicketsAdmin : UserControl
    {
        public TicketsAdmin()
        {
            InitializeComponent();
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((ContentControl)this.Parent).Content = null;

        }
    }
}

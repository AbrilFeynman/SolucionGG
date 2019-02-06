﻿using Core.Common.UI.Core;
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
using Core.Common.Core;
using GGGC.Admin.ERP.Modules.B20.SO.ViewModels;


namespace GGGC.Admin.ERP.Modules.B20.SO.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControlViewBase
    {
        public MainView()
        {
            InitializeComponent();
            main.DataContext = ObjectBase.Container.GetExportedValue<MainViewModel>();
        }
    }
}

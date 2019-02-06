using System.Windows;
using GGGC.Admin.Sign.ViewModels;

namespace GGGC.Admin.Sign.Views
{
	public partial class MainLogin
	{
		#region Fields

		public LoginViewModel ViewModel;

		#endregion

		#region Constructor

        public MainLogin()
		{
			InitializeComponent();

			//this.ViewModel = new LoginViewModel();
			//this.DataContext = this.ViewModel;
		}

		#endregion

		#region Event handler

		private void btnLock_Click(object sender, RoutedEventArgs e)
		{
			this.SmartLoginOverlayControl.Lock();
		}

		#endregion
	}
}

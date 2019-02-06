using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using GGGC.Admin.Sign.Models;
using SoftArcs.WPFSmartLibrary.MVVMCommands;
using SoftArcs.WPFSmartLibrary.MVVMCore;
using SoftArcs.WPFSmartLibrary.SmartUserControls;
using Telerik.Windows.Controls;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using ViewModelBase = SoftArcs.WPFSmartLibrary.MVVMCore.ViewModelBase;
using GGGC.Modules.Data;

namespace GGGC.Admin.Sign.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		#region Fields

		List<User> userList;
		private readonly string userImagesPath = @"\Images";
		//private RadOutlookBar ro = null;

		private bool ch = false;

		#endregion // Fields

		#region Constructors

		public LoginViewModel( ref bool CHANGUE)
		{
            //ref RadOutlookBar rob,
            //ref RadTreeView tv

            userImagesPath = GetCurrentDirectory() + @"\Resources\Images\App";

		  //  MessageBox.Show(GetCurrentDirectory().ToString());

			//MessageBox.Show(userImagesPath.ToString());
			//ro = rob;
			ch = CHANGUE;
			if (ViewModelHelper.IsInDesignModeStatic == false)
			{
				this.initializeAllCommands();

				//+ This is only neccessary if you want to display the appropriate image while typing the user name.
				//+ If you want a higher security level you wouldn't do this here !
				//! Remember : ONLY for demonstration purposes I have used a local Collection
				this.getAllUser();
			}
		}

		public static string GetCurrentDirectory()
		{
			string path = null;

			path = AppDomain.CurrentDomain.BaseDirectory;
			if (path.IndexOf(@"\bin") > 0)
			{
				path = path.Substring(0, path.LastIndexOf(@"\bin"));
			}

			return path;
		}

		#endregion // Constructors

		#region Public Properties

		public string UserName
		{
			get { return GetValue( () => UserName ); }
			set
			{
				SetValue( () => UserName, value );

				this.UserImageSource = this.getUserImagePath();
				//this.UserID = 
			  //  this.UserID = this.g
			}
		}

		public string Password
		{
			get{ return GetValue( () => Password ); }
			set { SetValue( () => Password, value ); }
		}

		public string Name
		{
			get { return GetValue(() => Name); }
			set { SetValue(() => Name, value); }
		}

		public int UserID
		{
			get { return GetValue(() => UserID); }
			set { SetValue(() => UserID, value); }
		}

		public string StoreID
		{
			get { return GetValue(() => StoreID); }
			set { SetValue(() => StoreID, value); }
		}

		public string PerfilID
		{
			get { return GetValue(() => PerfilID); }
			set { SetValue(() => PerfilID, value); }
		}

		public string UserImageSource
		{
			get { return GetValue( () => UserImageSource ); }
			set { SetValue( () => UserImageSource, value ); }
		}

		#endregion // Public Properties

		#region Submit Command Handler

		public ICommand SubmitCommand { get; private set; }

		private void ExecuteSubmit(object commandParameter)
		{
			var accessControlSystem = commandParameter as SmartLoginOverlay;

			if (accessControlSystem != null)
			{
				if (this.validateUser( this.UserName, this.Password.ToUpper() ) == true)
				{
					accessControlSystem.Unlock();
					GlobalModule.blnEntrar = true;

					this.UserID  = GlobalModule.ActualUserID ;
				   // accessControlSystem.Visibility=Visibility.Hidden;
		   
				   // accessControlSystem.
					//accessControlSystem.
					//ro.Width = 240;
                    

					//Shell
				   // ro.Visibility = System.Windows.Visibility.Visible;

					ch = true;
					//CHANGUE
					//Shell.
				   // this.
				}
				else
				{
					accessControlSystem.ShowWrongCredentialsMessage();
				}
			}
		}

		private bool CanExecuteSubmit(object commandParameter)
		{
			return !string.IsNullOrEmpty( this.Password );
		}

		#endregion // Submit Command Handler

		#region Private Methods

		private void initializeAllCommands()
		{
			this.SubmitCommand = new ActionCommand( this.ExecuteSubmit, this.CanExecuteSubmit );
		}

		private void getAllUser()
		{
			//+ Here you would implement code, which will get all user from a database,
			//+ a webservice or from somewhere else (if you want to display the right image)
			//! Remember : ONLY for demonstration purposes I have used a local Collection
			//this.userList = new List<User>()
			//                     {
			//                        new User() { UserName="hol", Password="hol",
			//                                         ImageSourcePath = Path.Combine( userImagesPath, "DemoUser2.png") },
			//                        new User() { UserName="bluehairbeauty", Password="blue1",
			//                                         ImageSourcePath = Path.Combine( userImagesPath, "DemoUser1.png") },

			//                       new User() { UserName="GUA", Password="GUA", ImageSourcePath = Path.Combine( userImagesPath, "DemoUser2.png") },
			//                        new User() { UserName="RGV", Password="RGV", ImageSourcePath = Path.Combine( userImagesPath, "DemoUser2.png") },
			//                          new User() { UserName="MFR", Password="MFR388", ImageSourcePath = Path.Combine( userImagesPath, "DemoUser2.png") },
			//                            new User() { UserName="AAG", Password="AAG", ImageSourcePath = Path.Combine( userImagesPath, "DemoUser2.png") },
			//                             new User() { UserName="YMP", Password="YMP", ImageSourcePath = Path.Combine( userImagesPath, "DemoUser2.png") },
			//                               new User() { UserName="LFLP", Password="LFLP", ImageSourcePath = Path.Combine( userImagesPath, "DemoUser2.png") },
			//                                 new User() { UserName="ROSY", Password="ROSY", ImageSourcePath = Path.Combine( userImagesPath, "DemoUser2.png") },
			//                     };


			GETuSUARIOS();
		}


        private void GETuSUARIOS()
		{
			try
			{
				AccesoDatos sCen = new AccesoDatos(6);
				string sSQL = "SELECT UserName, Password, UserID, SucursalID, Nick, NombreCompleto  FROM tblTesting";
			  //  ds = new Dataset();
				DataTable tbl = sCen.BaseDatos.Consulta(sSQL);

						 var categoryList = new List<User>(tbl.Rows.Count);
			foreach (DataRow row in tbl.Rows)
			{
			   // var values = row.ItemArray;
				var category = new User()
					{
						UserName = row.Field<string>("UserName").ToUpper(),
						Password = row.Field<string>("Password").ToUpper(),
						UserID = row.Field<int>("UserID"),
						StoreID = row.Field<byte>("SucursalID").ToString(),
						PerfilID = row.Field<string>("Nick"),
						Name = row.Field<string>("NombreCompleto"),
						ImageSourcePath = Path.Combine(userImagesPath, "DemoUser2.png")

						//DisplayOrder = values[2],
						//IsActive = (double)values[3] == 1 ? true : false
					};
				categoryList.Add(category);
			}


            


            //898131180

				this.userList = categoryList;

				//var NameList = (from r in tbl.AsEnumerable()
				//            select new 
				//            {
				//                 UserName = r.Field<string>("UserName"),
				//               Password = r.Field<string>("Password")
							   
							   
				//            });

				//this.userList = NameList.ToList();


				  //this.userList = new List<User>()
				  //    {
				  //        new User() { UserName="hol", Password="hol"
				  //    }
					   
					  //(from r in tbl.AsEnumerable()
					   //         select new
					   //         {
					   //             UserName = r.Field<string>("UserName"),
					   //             Password = r.Field<string>("Password"),
								 
					   //         });

			   // rgv.ItemsSource = sCen.BaseDatos.Consulta(sSQL);

				//rgv.ToExcelML(rgv.ItemsSource, true);
				//rgv.Columns[0].IsVisible = false;
				//[Codigo_Tipo_De_Agrupacion_De_Lineas] AS Codigo,
				//radGridView1.DataSource = sCen.BaseDatos.Consulta(sSQL);
				//radGridView1.BestFitColumns();
				//this.radGridView1.EnableAlternatingRowColor = true;
				//this.radGridView1.EnableGrouping = true;
				//this.radGridView1.EnableFiltering = true;
				//this.radGridView1.AutoGenerateColumns = true;
				//this.radGridView1.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;

				//this.rgv.Columns["Fecha"].ShowFieldFilters = false;
				//this.rgv.Columns["Fecha"].IsFilterable = false;
				//this.rgv.Columns["Estatus"].IsFilterable = false;

				//  this.lblRows.Text = rgv.Items.Count.ToString() + " Registros---";


			}
			catch (Exception ex)
			{
				// MessageBox.Show("Error en cargarDatos: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private bool validateUser(string username, string password)
		{
			//+ Here you would implement code, which will get the validation for the given credentials
			//+ from a database, a webservice or from somewhere else
			//! Remember : ONLY for demonstration purposes I have used a local Collection
			User validatedUser = this.userList.FirstOrDefault( user => user.UserName.Equals( username ) &&
																				user.Password.Equals( password ) );
			if (validatedUser!=null)
			{
				GlobalModule.ActualUserID = validatedUser.UserID;
				GlobalModule.ActualUserPassword = validatedUser.Password;
				GlobalModule.ActualStoreID = validatedUser.StoreID;
				GlobalModule.ActualPerfilID = validatedUser.PerfilID;
				GlobalModule.ActualName = validatedUser.Name;
				GlobalModule.ActualUserName = validatedUser.UserName;
				//GlobalModule.ActualUserName = validatedUser.UserName;
				//GlobalModule.ActualUserName = validatedUser.UserName;

                if (validatedUser.UserName == "AFAVELA")
                    GlobalModule.ActualPerfilID = "1";




			}
				
			
			return validatedUser != null;
		}

		private string getUserImagePath()
		{
			User currentUser = this.userList.FirstOrDefault( user => user.UserName.Equals( this.UserName ) );

			if (currentUser != null)
			{
				return currentUser.ImageSourcePath;
			}

			return String.Empty;
		}

		#endregion
	}
}

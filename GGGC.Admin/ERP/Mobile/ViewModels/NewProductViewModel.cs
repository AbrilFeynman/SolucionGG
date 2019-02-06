using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GGGC.Admin.Support.Commands;
using System.ComponentModel;
using GGGC.Admin.ERP.Mobile.Model;
using System.Windows;

namespace NewProducts.ViewModels
{
	 class NewProductViewModel:ViewModelBase ,IDataErrorInfo
	 {
		  #region private members

		  private readonly Product currentProduct;
		  private Dictionary<string, bool> validProperties;
		  private bool allPropertiesValid = false;

		  private DelegateCommand exitCommand;
		  private DelegateCommand saveCommand;

		  #endregion

		  #region constructors

		  public NewProductViewModel(Product newProduct)
		  {
				this.currentProduct = newProduct;
				this.validProperties = new Dictionary<string, bool>();
				this.validProperties.Add("ProductName", false);
				this.validProperties.Add("Height", false);
				this.validProperties.Add("Width", false);
		  }

		  #endregion

		  #region state properties

		  public string ProductName
		  {
				get { return currentProduct.ProductName; }
				set
				{
					 if (currentProduct.ProductName != value)
					 {
						  currentProduct.ProductName = value;
						  base.OnPropertyChanged("ProductName");
					 }
				}
		  }

		  public int Width
		  {
				get { return currentProduct.Width; }
				set
				{
					 if (this.currentProduct.Width != value)
					 {
						  this.currentProduct.Width = value;
						  base.OnPropertyChanged("Width");
						  base.OnPropertyChanged("Height");
					 }
				}
		  }

		  public int Height
		  {
				get { return currentProduct.Height; }
				set
				{
					 if (this.currentProduct.Height != value)
					 {
						  this.currentProduct.Height = value;
						  base.OnPropertyChanged("Height");
						  base.OnPropertyChanged("Width");
					 }
				}
		  }

		  public bool AllPropertiesValid
		  {
				get { return allPropertiesValid; }
				set
				{
					 if (allPropertiesValid != value)
					 {
						  allPropertiesValid = value;
						  base.OnPropertyChanged("AllPropertiesValid");
					 }
				}
		  }

		  #endregion

		  #region IDataErrorInfo members

		  public string Error
		  {
				get { return (currentProduct as IDataErrorInfo).Error; }
		  }

		  public string this[string propertyName]
		  {
				get 
				{
					 string error = (currentProduct as IDataErrorInfo)[propertyName];
					 validProperties[propertyName] = String.IsNullOrEmpty(error) ? true : false;
					 ValidateProperties();
					 CommandManager.InvalidateRequerySuggested();
					 return error;
				}
		  }

		  #endregion

		  #region commands

		  public ICommand ExitCommand
		  {
				get
				{
					 if (exitCommand == null)
					 {
						  exitCommand = new DelegateCommand(Exit);
					 }
					 return exitCommand;
				}
		  }

		  public ICommand SaveCommand
		  {
				get
				{
					 if (saveCommand == null)
					 {
						  saveCommand = new DelegateCommand(Save);
					 }
					 return saveCommand;
				}
		  }

		  #endregion

		  #region private helpers

		  private void ValidateProperties()
		  {
				foreach(bool isValid in validProperties.Values)
				{
					 if (!isValid)
					 {
						  this.AllPropertiesValid = false;
						  return;
					 }
				}
				this.AllPropertiesValid = true;
		  }

		  private void Exit()
		  {
				Application.Current.Shutdown();
		  }

		  private void Save()
		  {
				currentProduct.Save();
		  }
		  
		  #endregion
	 }
}

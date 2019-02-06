using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using GGGC.Admin.Support.Commands;
using System.Windows;


namespace GGGC.Admin.ERP.Modules.Sales.Orders.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class QueryViewModel : ViewModelBase, IDataErrorInfo
    {
        //private readonly Product currentProduct;
        //private Dictionary<string, bool> validProperties;
        //private bool allPropertiesValid = false;

        private DelegateCommand exitCommand;
        private DelegateCommand saveCommand;

        //private MobileServiceCollection<TodoItem, TodoItem> items;
        //private IMobileServiceTable<TodoItem> todoTable = App.MobileService.GetTable<TodoItem>();
   // GGGC.Admin.ERP.Mobile.Rines.Model.Product _newProduct;

        [ImportingConstructor]
        public QueryViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;
            // Create the ViewModel and expose it using the View's DataContext
            //Views.NewProductView newProductView = new Views.NewProductView();
            //NewProducts.Models.Product newProduct = new Models.Product();
            //newProductView.DataContext = new ViewModels.NewProductViewModel(newProduct);
            //newProductView.Show();
            //_newProduct = new GGGC.Admin.ERP.Mobile.Rines.Model.Product();
            ////Product newProduct;
            //this.currentProduct = Cars;
            //this.validProperties = new Dictionary<string, bool>();
            //this.validProperties.Add("ProductName", false);
            //this.validProperties.Add("ProductCode", false);



            //this.validProperties.Add("Height", false);
            //this.validProperties.Add("Width", false);

        }

        IServiceFactory _ServiceFactory;

        public override string ViewTitle
        {
            get { return "Consultar"; }
        }

   //    IServiceFactory _ServiceFactory;                  

        //public  string ViewTitle
        //{
        //    get { return "Mi titulo"; }
        //}

       protected void OnViewLoaded()
        {
            // can check properties for null here if not want to re-get every time view shows
            try
            {
                //this.currentProduct = Cars;
                //this.validProperties = new Dictionary<string, bool>();
                //this.validProperties.Add("ProductName", false);
                //this.validProperties.Add("Height", false);
                //this.validProperties.Add("Width", false);
             //   Product newProduct;
           // this.currentProduct = Cars;
            //this.validProperties = new Dictionary<string, bool>();
            //this.validProperties.Add("ProductName", false);
            //this.validProperties.Add("Height", false);
            //this.validProperties.Add("Width", false);
                //WithClient<IProductService>(_ServiceFactory.CreateClient<IProductService>(), catalogClient =>
                //{
                //    Products = catalogClient.GetAllProducts();
                //});
                //WithClient<IInventoryService>(_ServiceFactory.CreateClient<IInventoryService>(), catalogClient =>
                //{
                //    Cars = catalogClient.GetAllCars();
                //});

                //OKKKKK
                //WithClient<ICatalogService>(_ServiceFactory.CreateClient<ICatalogService>(), catalogClient =>
                //{
                //    Cars = catalogClient.GetAllLines();
                //});

                //WithClient<ICatalogService>(_ServiceFactory.CreateClient<ICatalogService>(), catalogClient =>
                //{
                //    Cars = catalogClient.GetPrice();
                //});
            }

            catch (Exception e)
            {
                string s = e.Message.ToString();
                // throw;
            }
            //WithClient<IPatrimonyService>(_ServiceFactory.CreateClient<IPatrimonyService>(), catalogClient =>
            //{
            //    Lines = catalogClient.GetAllTickets();
            //});
        }
      
       // CustomerRentalData[] _CurrentlyRented;

      // public GGGC.Admin.ERP.Mobile.Rines.Model.Product Cars
      // {
      //     get { return _newProduct; }
      //     set
      //     {
      //         if (_newProduct != value)
      //         {
      //             _newProduct = value;
      //             OnPropertyChanged(() => Cars, false);
      //         }
      //     }
      // }
      //  #region private members

      //#endregion
      
      //  #region state properties

      //  public string ProductName
      //  {
      //      get { return currentProduct.ProductName; }
      //      set
      //      {
      //          if (currentProduct.ProductName != value)
      //          {
      //              currentProduct.ProductName = value;
      //              base.OnPropertyChanged("ProductName");
      //          }
      //      }
      //  }

      //  public string ProductCode
      //  {
      //      get { return currentProduct.ProductCode; }
      //      set
      //      {
      //          if (currentProduct.ProductCode != value)
      //          {
      //              currentProduct.ProductCode = value;
      //              base.OnPropertyChanged("ProductCode");
      //          }
      //      }
      //  }

      //  //public int Width
      //  //{
      //  //    get { return currentProduct.Width; }
      //  //    set
      //  //    {
      //  //        if (this.currentProduct.Width != value)
      //  //        {
      //  //            this.currentProduct.Width = value;
      //  //            base.OnPropertyChanged("Width");
      //  //            base.OnPropertyChanged("Height");
      //  //        }
      //  //    }
      //  //}

      //  //public int Height
      //  //{
      //  //    get { return currentProduct.Height; }
      //  //    set
      //  //    {
      //  //        if (this.currentProduct.Height != value)
      //  //        {
      //  //            this.currentProduct.Height = value;
      //  //            base.OnPropertyChanged("Height");
      //  //            base.OnPropertyChanged("Width");
      //  //        }
      //  //    }
      //  //}

      //  public bool AllPropertiesValid
      //  {
      //      get { return allPropertiesValid; }
      //      set
      //      {
      //          if (allPropertiesValid != value)
      //          {
      //              allPropertiesValid = value;
      //              base.OnPropertyChanged("AllPropertiesValid");
      //          }
      //      }
      //  }

      //  #endregion

      //  #region IDataErrorInfo members

      //  public string Error
      //  {
      //      get { return (currentProduct as IDataErrorInfo).Error; }
      //  }

      //  public string this[string propertyName]
      //  {
      //      get
      //      {
      //          string error = (currentProduct as IDataErrorInfo)[propertyName];
      //          validProperties[propertyName] = String.IsNullOrEmpty(error) ? true : false;
      //          ValidateProperties();
      //          CommandManager.InvalidateRequerySuggested();
      //          return error;
      //      }
      //  }

      //  #endregion

      //  #region commands

      //  public ICommand ExitCommand
      //  {
      //      get
      //      {
      //          if (exitCommand == null)
      //          {
      //              exitCommand = new DelegateCommand(Exit);
      //          }
      //          return exitCommand;
      //      }
      //  }

      //  public ICommand SaveCommand
      //  {
      //      get
      //      {
      //          if (saveCommand == null)
      //          {
      //              saveCommand = new DelegateCommand(Save);
      //          }
      //          return saveCommand;
      //      }
      //  }

      //  #endregion

      //  #region private helpers

      //  private void ValidateProperties()
      //  {
      //      foreach (bool isValid in validProperties.Values)
      //      {
      //          if (!isValid)
      //          {
      //              this.AllPropertiesValid = false;
      //              return;
      //          }
      //      }
      //      this.AllPropertiesValid = true;
      //  }

      //  private void Exit()
      //  {
      //    //  Application.Current.Shutdown();
      //      //Panel panel = default(Panel);
      //      //panel = (Panel)this.Parent;
      //      //panel.Children.RemoveAt(0);
      //      this.IsDirty = true;
      //  }

      //  private void Save()
      //  {
      //      currentProduct.Save(currentProduct);
      //  }

      //  #endregion
    }
}

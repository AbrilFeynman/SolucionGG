using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using GGGC.Admin.ERP.Modules.Inventory.Adjustments.Model;
using GGGC.Admin.Support.Commands;

namespace GGGC.Admin.ERP.Modules.Inventory.Adjustments.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NewViewModel : ViewModelBase, IDataErrorInfo
    {
            private readonly Adjustment currentEntity;
            Adjustment _newEntity;
            private Dictionary<string, bool> validProperties;
            private bool allPropertiesValid = false;

            private Support.Commands.DelegateCommand exitCommand;
            private DelegateCommand saveCommand;

            [ImportingConstructor]
            public NewViewModel(IServiceFactory serviceFactory)
            {
                //Adjustment newEntity


                // Constructor IServiceFactory serviceFactory
                _ServiceFactory = serviceFactory;

                // Create the ViewModel and expose it using the View's DataContext
                //Views.NewProductView newProductView = new Views.NewProductView();
                //NewProducts.Models.Product newProduct = new Models.Product();
                //newProductView.DataContext = new ViewModels.NewProductViewModel(newProduct);
                //newProductView.Show();

                //_newProduct = new GGGC.Admin.ERP.Mobile.Rines.Model.Product();

                ////Product newProduct;


                //_newProduct = new GGGC.Admin.ERP.Mobile.Rines.Model.Product();
                ////Product newProduct;
                //this.currentProduct = Cars;

                 _newEntity = new Adjustment();

                 this.currentEntity = ENTITY;
                //this.validProperties = new Dictionary<string, bool>();
                //this.validProperties.Add("ProductName", false);
                //this.validProperties.Add("ProductCode", false);
                //this.validProperties.Add("Height", false);
                //this.validProperties.Add("Width", false);

                this.validProperties = new Dictionary<string, bool>();
                this.validProperties.Add("Fecha", false);
               this.validProperties.Add("Folio", false);
                
                // this.validProperties.Add("ProductCode", false);

            }

            public override string ViewTitle
            {
                get { return "Nuevo AjustE"; }
            }

            public Adjustment ENTITY
            {
                get { return _newEntity; }
                set
                {
                    if (_newEntity != value)
                    {
                        _newEntity = value;
                        OnPropertyChanged(() => ENTITY, false);
                    }
                }
            }

            private IServiceFactory _ServiceFactory;


           

            #region state properties

            public string ProductName
            {
                get { return currentEntity.ProductName; }
                set
                {
                    if (currentEntity.ProductName != value)
                    {
                        currentEntity.ProductName = value;
                        base.OnPropertyChanged("ProductName");
                    }
                }
            }

            public string Folio
            {
                get { return currentEntity.Folio; }
                set
                {
                    if (currentEntity.Folio != value)
                    {
                        currentEntity.Folio = value;
                        base.OnPropertyChanged("Folio");
                    }
                }
            }


            public DateTime Fecha
            {
                get { return currentEntity.Fecha; }
                set
                {
                    if (currentEntity.Fecha != value)
                    {
                        currentEntity.Fecha = value;
                        base.OnPropertyChanged("Fecha");
                    }
                }
            }

            //public int Width
            //{
            //    get { return currentProduct.Width; }
            //    set
            //    {
            //        if (this.currentProduct.Width != value)
            //        {
            //            this.currentProduct.Width = value;
            //            base.OnPropertyChanged("Width");
            //            base.OnPropertyChanged("Height");
            //        }
            //    }
            //}

            //public int Height
            //{
            //    get { return currentProduct.Height; }
            //    set
            //    {
            //        if (this.currentProduct.Height != value)
            //        {
            //            this.currentProduct.Height = value;
            //            base.OnPropertyChanged("Height");
            //            base.OnPropertyChanged("Width");
            //        }
            //    }
            //}

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
                get { return (currentEntity as IDataErrorInfo).Error; }
            }

            public string this[string propertyName]
            {
                get
                {
                    string error = (currentEntity as IDataErrorInfo)[propertyName];
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
                foreach (bool isValid in validProperties.Values)
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
                //  Application.Current.Shutdown();
                //Panel panel = default(Panel);
                //panel = (Panel)this.Parent;
                //panel.Children.RemoveAt(0);
                this.IsDirty = true;
            }

            private void Save()
            {
                currentEntity.Save(currentEntity);
            }

            #endregion


            //public override string ViewTitle
            //{
            //    get { return "Nuevo Rin"; }
            //}
        
    }
}
